using System.Text;
using RepoContracts;
using ServerEntities;

namespace CLI.UI;

/// Provides an interface for handling channel-related commands within the command-line interface.
public static class ChannelLoop {
    /// Represents the command string that triggers the channel operation.
    private const string HelpStr = "!help",
        ShowAllStr = "!showall",
        ViewStr = "!view",
        NewStr = "!new",
        DeleteStr = "!delete",
        ReturnStr = "!return";

    /// Handles commands related to user operations.
    /// <param name="channelRepo">The repository instance handling channel data interactions.</param>
    /// <param name="postRepo">The repository instance handling post data interactions.</param>
    /// <param name="userRepo">The repository instance handling user data interactions.</param>
    /// <return>A task that represents the asynchronous operation, containing a boolean value indicating if the application should return to the main loop.</return>
    public static Task<bool> Show(IChannelRepo channelRepo, IPostRepo postRepo, IUserRepo userRepo) {
        ArgumentNullException.ThrowIfNull(channelRepo, nameof(channelRepo));
        ArgumentNullException.ThrowIfNull(postRepo, nameof(postRepo));
        ArgumentNullException.ThrowIfNull(userRepo, nameof(userRepo));
        Console.Clear();
        Console.WriteLine($"LoreIt - Channel ({DateTime.Now})\n");
        while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)) {
            Console.WriteLine("Please enter your command: ");
            var userInput = Console.ReadLine() ?? string.Empty;
            var splitInput = userInput.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine();
            var result = splitInput[0] switch {
                HelpStr => ShowHelp(),
                ShowAllStr => ShowAll(channelRepo),
                ViewStr => ViewAsync(channelRepo, userRepo, splitInput[1]),
                NewStr => NewAsync(channelRepo, userRepo, splitInput[1]),
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
            .AppendLine($"{ShowAllStr} - Views all registered channels.")
            .AppendLine($"{ViewStr} channelId - Displays detailed information about a channel given it's unique identifier.")
            .AppendLine($"{NewStr} channelTitle - Creates a new channel.")
            .AppendLine($"{DeleteStr} - Deletes a channel given it's unique identifier from the service.")
            .AppendLine($"{ReturnStr} - Return to the main application.");
        Console.WriteLine(help.ToString());
        return Task.FromResult(false);
    }

    /// Displays all registered channels.
    /// <param name="channelRepo">The repository instance handling channel data interactions.</param>
    /// <return>A task representing the asynchronous operation, containing a boolean value indicating if the application should return to the main loop.</return>
    private static Task<bool> ShowAll(IChannelRepo channelRepo) {
        var channels = channelRepo.GetAllChannels();
        Console.WriteLine("Available channels:");
        if (channels.Count() > 1) {
            Console.WriteLine("No available channels found...");
            return Task.FromResult(false);
        }
        foreach (var c in channels) Console.WriteLine($"{c.Title} - {c.Id}");
        return Task.FromResult(false);
    }

    /// Retrieves and displays details of a specified channel.
    /// <param name="channelRepo">The repository instance handling channel data interactions.</param>
    /// <param name="userRepo">The repository instance handling user data interactions.</param>
    /// <param name="channelId">The identifier of the channel to be viewed.</param>
    /// <return>A task that represents the asynchronous operation, containing a boolean value indicating if the application should return to the main loop.</return>
    private static async Task<bool> ViewAsync(IChannelRepo channelRepo, IUserRepo userRepo, string channelId) {
        try {
            var channel = await channelRepo.GetAsync(int.Parse(channelId));
            var user = await userRepo.GetByIdAsync(channel.OwnerId);
            var msg = new StringBuilder();
            msg.AppendLine($"{channel.Title} - {channel.Id} // Channel Details:").AppendLine($"Id: {channel.Id}").AppendLine($"Title: {channel.Title}")
                .AppendLine($"Owner: {user.Username}").AppendLine($"Description: {channel.Description}").AppendLine($"Rules: {channel.Rules}")
                .AppendLine($"Publish Date: {channel.PublishDate}").AppendLine($"Last Modification {channel.LastModifiedDate}");
            Console.WriteLine(msg.ToString());
        }
        catch (Exception e) {
            Console.WriteLine($"Error viewing channel {channelId} : {e.Message}");
            throw;
        }
        return false;
    }

    /// Creates a new channel asynchronously.
    /// <param name="channelRepo">The repository instance handling channel data interactions.</param>
    /// <param name="userRepo">The repository instance handling user data interactions.</param>
    /// <param name="title">The title of the new channel.</param>
    /// <return>A task that represents the asynchronous operation, containing a boolean value indicating whether the channel creation was successful.</return>
    private static async Task<bool> NewAsync(IChannelRepo channelRepo, IUserRepo userRepo, string title) {
        if (SignInLoop.LoginUsername is null) {
            Console.WriteLine("You are not logged in!");
            return false;
        }
        var user = await userRepo.GetByUsernameAsync(SignInLoop.LoginUsername);
        await channelRepo.AddAsync(new Channel(user.Id, title));
        return true;
    }
}