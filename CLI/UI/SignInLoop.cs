using System.Text;
using RepoContracts;
using ServerEntities;

namespace CLI.UI;

/// The SignInLoop class provides functionalities for user sign-in operations within a command-line interface (CLI) environment.
/// It handles commands such as login, signup, logout, delete user, and more.
public static class SignInLoop {

    /// <summary>Stores the username of the currently logged-in user.</summary>
    internal static string? LoginUsername { get; set; } = null;

    /// <summary>Represents the command string that triggers the signin operation.</summary>
    private const string HelpStr = "!help",
        LoginStr = "!login",
        SignupStr = "!signup",
        LogoutStr = "!logout",
        ReturnStr = "!return";

    /// Processes various commands related to user authentication and management.
    /// <param name="userRepo">The repository interface for managing user data.</param>
    /// <return>A task that represents the asynchronous operation, containing a boolean value indicating if the application should shut down.</return>
    public static Task<bool> Show(IUserRepo userRepo) {
        ArgumentNullException.ThrowIfNull(userRepo, nameof(userRepo));
        Console.Clear();
        Console.WriteLine($"LoreIt - Login ({DateTime.Now})\n");
        while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)) {
            Console.WriteLine("Please enter your command: ");
            var userInput = Console.ReadLine() ?? string.Empty;
            var splitInput = userInput.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine();
            var result = splitInput[0] switch {
                HelpStr => ShowHelp(),
                LoginStr => LoginAsync(userRepo, splitInput[1], splitInput[2]),
                SignupStr => SignUpAsync(userRepo, splitInput[1], splitInput[2]),
                LogoutStr => LogOut(),
                ReturnStr => Task.FromResult(true),
                _ => null
            };
            if (result is null) Console.WriteLine("Command not recognized.");
            else if (result.GetAwaiter().GetResult()) break;
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
            .AppendLine($"{LoginStr} yourUsername yourPassword - Logs in the user to the service.")
            .AppendLine($"{SignupStr} yourUsername yourPassword - Signs up a user to the service.")
            .AppendLine($"{LogoutStr} - Logs out the user from the service.")
            .AppendLine($"{ReturnStr} - Return to the main application.");
        Console.WriteLine(help.ToString());
        return Task.FromResult(false);
    }

    /// Logs in an existing user asynchronously.
    /// <param name="userRepo">The repository for user data operations.</param>
    /// <param name="username">The username of the existing user.</param>
    /// <param name="password">The password of the existing user.</param>
    /// <return>A task that represents the asynchronous operation, containing a boolean value indicating if the application should return to the mainLoop</return>
    private static async Task<bool> LoginAsync(IUserRepo userRepo, string username, string password) {
        var user = await userRepo.GetByUsernameAsync(username);
        if (!user.CheckPassword(password)) return false;
        Console.WriteLine($"You have logged in as {user.Username}.");
        Thread.Sleep(1000);
        LoginUsername = username;
        return true;
    }

    /// Registers a new user with the given username and password.
    /// <param name="userRepo">The repository interface for user data operations.</param>
    /// <param name="username">The username for the new user.</param>
    /// <param name="password">The password for the new user.</param>
    /// <return>A task that represents the asynchronous operation, containing a boolean value indicating if the application should return to the mainLoop</return>
    private static async Task<bool> SignUpAsync(IUserRepo userRepo, string username, string password) {
        var allUsers = userRepo.GetAllUsers();
        foreach (var u in allUsers)
            if (u.Username == username) {
                Console.WriteLine($"User {username} already exists.");
                return false;
            }
        var user = await User.CreateAsync(username, password);
        user = await userRepo.AddAsync(user);
        Console.WriteLine($"User {user.Username} has been signed up.");
        return false;
    }

    /// Logs out the currently logged-in user from the service.
    /// <return>A task that represents the asynchronous operation, containing a boolean value indicating if the application should return to the mainLoop</return>
    private static Task<bool> LogOut() {
        if (LoginUsername is not null) {
            Console.WriteLine("You are already logged out!");
            return Task.FromResult(false);
        }
        LoginUsername = null;
        Console.WriteLine("Logged out the service.");
        return Task.FromResult(true);
    }
}