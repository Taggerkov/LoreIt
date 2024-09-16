using System.Text;
using RepoContracts;

namespace CLI.UI;

public class CliApp(IUserRepo userRepo, IChannelRepo channelRepo, IPostRepo postRepo, ICommentRepo commentRepo) {
    /// <summary>Represents the repository for managing user-related operations.</summary>
    private readonly IUserRepo _userRepo = CheckNull(userRepo);

    /// <summary>Represents the repository for managing channel-related operations.</summary>
    private readonly IChannelRepo _channelRepo = CheckNull(channelRepo);

    /// <summary>Represents the repository for managing post-related operations.</summary>
    private readonly IPostRepo _postRepo = CheckNull(postRepo);

    /// <summary>Represents the repository for managing comment-related operations.</summary>
    private readonly ICommentRepo _commentRepo = CheckNull(commentRepo);

    /// <summary>Represents the command string that triggers the main operation.</summary>
    private const string HelpStr = "!help",
        SignInStr = "!signin",
        UserStr = "!user",
        ChannelStr = "!channel",
        ShutDownStr = "!shutdown";

    /// Ensures the provided object is not null.
    /// <param name="obj">The object to check for null.</param>
    /// <return>The original object if it is not null.</return>
    /// <exception cref="ArgumentNullException">Thrown when the object is null.</exception>
    private static T CheckNull<T>(T obj) {
        return obj ?? throw new ArgumentNullException(nameof(obj));
    }

    public Task Run() {
        while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)) {
            Console.WriteLine("Please enter your command: ");
            var userInput = Console.ReadLine() ?? string.Empty;
            var splitInput = userInput.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine();
            var result = splitInput[0] switch {
                HelpStr => ShowHelp(),
                SignInStr => SignInLoop.Show(userRepo),
                UserStr => UserLoop.Show(userRepo),
                ChannelStr => ChannelLoop.Show(channelRepo, postRepo, userRepo),
                ShutDownStr => Task.FromResult(true),
                _ => null
            };
            if (result is null) Console.WriteLine("Command not recognized.");
            else if (result.GetAwaiter().GetResult()) break;
        }
        
        return Task.FromCanceled(new CancellationToken());
    }

    /// Displays a list of available commands and their descriptions.
    /// <return>A task that represents the asynchronous operation, containing a boolean value indicating if the application should shut down.</return>
    private static Task<bool> ShowHelp() {
        var help = new StringBuilder();
        help.AppendLine("List of available commands:").AppendLine($"{HelpStr} - Shows all available commands.")
            .AppendLine($"{SignInStr} - Redirects to the SignIn application.")
            .AppendLine($"{UserStr} - Redirect to the User application.")
            .AppendLine($"{ShutDownStr} - Shuts down the application.");
        Console.WriteLine(help.ToString());
        return Task.FromResult(false);
    }
}