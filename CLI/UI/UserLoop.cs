using System.Text;
using RepoContracts;

namespace CLI.UI;

/// Provides an interface for handling user-related commands within the command-line interface.
public static class UserLoop {
    /// Represents the command string that triggers the user operation.
    private const string HelpStr = "!help",
        ShowAllStr = "!showall",
        ViewStr = "!view",
        DeleteStr = "!delete",
        ReturnStr = "!return";

    /// Handles commands related to user operations.
    /// <param name="userRepo">The repository instance handling user data interactions.</param>
    /// <return>A task that represents the asynchronous operation, containing a boolean value indicating if the application should return to the main loop.</return>
    public static Task<bool> Show(IUserRepo userRepo) {
        ArgumentNullException.ThrowIfNull(userRepo, nameof(userRepo));
        Console.Clear();
        Console.WriteLine($"LoreIt - User ({DateTime.Now})\n");
        while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)) {
            Console.WriteLine("\nPlease enter your command: ");
            var userInput = Console.ReadLine() ?? string.Empty;
            try {
                var splitInput = userInput.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine();
                var result = splitInput[0] switch {
                    HelpStr => ShowHelp(),
                    ShowAllStr => ShowAll(userRepo),
                    ViewStr => View(userRepo, splitInput[1]),
                    DeleteStr => Delete(userRepo),
                    ReturnStr => Task.FromResult(true),
                    _ => null
                };
                if (result is null) Console.WriteLine("Command not recognized.");
                else if (result.GetAwaiter().GetResult()) break;
            }
            catch (Exception) {
                Console.WriteLine("Could not understand command or arguments.");
                throw;
            }
        }
        Console.Clear();
        Console.WriteLine($"LoreIt - Main ({DateTime.Now})\n");
        return Task.FromResult(false);
    }

    /// Displays a list of available commands and their descriptions.
    /// <return>A task that represents the asynchronous operation, containing a boolean value indicating if the application should return to the mainLoop</return>
    private static Task<bool> ShowHelp() {
        var help = new StringBuilder();
        help.AppendLine("List of available commands:").AppendLine($"{HelpStr} - Shows all available commands.")
            .AppendLine($"{ShowAllStr} - Views all registered users.")
            .AppendLine($"{ViewStr} userId - Displays detailed information about a user given their unique identifier.")
            .AppendLine($"{DeleteStr} - Deletes the currently logged-in user from the service.")
            .AppendLine($"{ReturnStr} - Return to the main application.");
        Console.WriteLine(help.ToString());
        return Task.FromResult(false);
    }

    /// Displays all registered users.
    /// <param name="userRepo">The user repository to retrieve all users from.</param>
    /// <return>A task that represents the asynchronous operation, containing a boolean value indicating if the operation was successful</return>
    private static Task<bool> ShowAll(IUserRepo userRepo) {
        var allUsers = userRepo.GetAllUsers();
        Console.WriteLine("Available users:");
        if (allUsers.Count() > 1) {
            Console.WriteLine("No available users found...");
            return Task.FromResult(false);
        }
        foreach (var u in allUsers) Console.WriteLine($"{u.Username} - {u.Id}");
        return Task.FromResult(false);
    }

    /// Displays detailed information about a user given their unique identifier.
    /// <param name="userRepo">The repository instance containing user data.</param>
    /// <param name="userId">The unique identifier of the user to be viewed.</param>
    /// <return>A task that represents the asynchronous operation, containing a boolean value indicating if the view operation was successful.</return>
    private static async Task<bool> View(IUserRepo userRepo, string userId) {
        try {
            var user = await userRepo.GetByIdAsync(int.Parse(userId));
            var msg = new StringBuilder();
            msg.AppendLine($"{user.Username} - {user.Id} // User Details:").AppendLine($"Id: {user.Id}").AppendLine($"Username: {user.Username}")
                .AppendLine($"Email: {user.Email}").AppendLine($"Is Admin: {user.IsAdmin}").AppendLine($"Publish Date: {user.PublishDate}")
                .AppendLine($"Last Modification {user.LastModifiedDate}");
            Console.WriteLine(msg.ToString());
        }
        catch (Exception e) {
            Console.WriteLine($"Error viewing user {userId} : {e.Message}");
            throw;
        }
        return false;
    }

    /// Deletes the currently logged-in user from the service.
    /// <param name="userRepo">The user repository to delete the user from.</param>
    /// <return>A task that represents the asynchronous operation, containing a boolean value indicating if the application should return to the mainLoop</return>
    private static async Task<bool> Delete(IUserRepo userRepo) {
        if (SignInLoop.LoginUsername is null) {
            Console.WriteLine("You are not logged in!");
            return false;
        }
        try {
            var user = await userRepo.GetByUsernameAsync(SignInLoop.LoginUsername);
            await userRepo.DeleteAsync(user.Id);
        }
        catch (InvalidOperationException e) {
            Console.WriteLine(e.Message);
            throw;
        }
        SignInLoop.LoginUsername = null;
        Console.WriteLine("Your account has been deleted!");
        return true;
    }
}