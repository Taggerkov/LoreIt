using System.Text;
using RepoContracts;
using ServerEntities;

namespace CLI.UI;

/// Provides an interface for handling post-related commands within the command-line interface.
public static class PostLoop {
    /// Represents the command string that triggers the post operation.
    private const string HelpStr = "!help",
        ShowAllStr = "!showall",
        ViewStr = "!view",
        NewStr = "!new",
        DeleteStr = "!delete",
        ReturnStr = "!return";

    /// Handles commands related to post operations.
    /// <param name="postRepo">The repository interface for accessing and managing posts.</param>
    /// <param name="userRepo">The repository interface for accessing and managing users.</param>
    /// <return>A task that represents the asynchronous operation, containing a boolean value indicating if the application should return to MainLoop or not</return>
    public static Task<bool> Show(IPostRepo postRepo, IUserRepo userRepo) {
        ArgumentNullException.ThrowIfNull(postRepo, nameof(postRepo));
        ArgumentNullException.ThrowIfNull(userRepo, nameof(userRepo));
        Console.Clear();
        Console.WriteLine($"LoreIt - Post ({DateTime.Now})\n");
        while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)) {
            Console.WriteLine("Please enter your command: ");
            var userInput = Console.ReadLine() ?? string.Empty;
            var splitInput = userInput.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine();
            var result = splitInput[0] switch {
                HelpStr => ShowHelp(),
                ShowAllStr => ShowAll(postRepo),
                ViewStr => ViewAsync(postRepo, userRepo, splitInput[1]),
                NewStr => NewAsync(postRepo, userRepo, splitInput[1]),
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
            .AppendLine($"{ShowAllStr} - Views all registered posts.")
            .AppendLine($"{ViewStr} postId - Displays detailed information about a post given it's unique identifier.")
            .AppendLine($"{NewStr} postTitle - Creates a new post.")
            .AppendLine($"{DeleteStr} - Deletes a post given it's unique identifier from the service.")
            .AppendLine($"{ReturnStr} - Return to the main application.");
        Console.WriteLine(help.ToString());
        return Task.FromResult(false);
    }

    /// Displays all registered posts.
    /// <param name="postRepo">The repository instance handling post data interactions.</param>
    /// <return>A task representing the asynchronous operation, containing a boolean value indicating if the application should return to the main loop.</return>
    private static Task<bool> ShowAll(IPostRepo postRepo) {
        var posts = postRepo.GetAllPosts();
        Console.WriteLine("Available posts:");
        if (posts.Count() > 1) {
            Console.WriteLine("No available posts found...");
            return Task.FromResult(false);
        }
        foreach (var p in posts) Console.WriteLine($"{p.Title} - {p.Id}");
        return Task.FromResult(false);
    }

    /// Retrieves and displays details of a specified post.
    /// <param name="postRepo">The repository instance handling post data interactions.</param>
    /// <param name="userRepo">The repository instance handling user data interactions.</param>
    /// <param name="postId">The identifier of the post to be viewed.</param>
    /// <return>A task that represents the asynchronous operation, containing a boolean value indicating if the application should return to the main loop.</return>
    private static async Task<bool> ViewAsync(IPostRepo postRepo, IUserRepo userRepo, string postId) {
        try {
            var post = await postRepo.GetAsync(int.Parse(postId));
            var user = await userRepo.GetByIdAsync(post.AuthorId);
            var msg = new StringBuilder();
            msg.AppendLine($"{post.Title} - {post.Id} // Post Details:").AppendLine($"Id: {post.Id}").AppendLine($"Title: {post.Title}")
                .AppendLine($"Owner: {user.Username}").AppendLine($"Content: {post.Content}").AppendLine($"Tags: {post.Tags}")
                .AppendLine($"Publish Date: {post.PublishDate}").AppendLine($"Last Modification {post.LastModifiedDate}");
            Console.WriteLine(msg.ToString());
        }
        catch (Exception e) {
            Console.WriteLine($"Error viewing post {postId} : {e.Message}");
            throw;
        }
        return false;
    }

    /// Creates a new post asynchronously.
    /// <param name="postRepo">The repository instance handling post data interactions.</param>
    /// <param name="userRepo">The repository instance handling user data interactions.</param>
    /// <param name="title">The title of the new post.</param>
    /// <return>A task that represents the asynchronous operation, containing a boolean value indicating whether the channel creation was successful.</return>
    private static async Task<bool> NewAsync(IPostRepo postRepo, IUserRepo userRepo, string title) {
        if (SignInLoop.LoginUsername is null) {
            Console.WriteLine("You are not logged in!");
            return false;
        }
        var user = await userRepo.GetByUsernameAsync(SignInLoop.LoginUsername);
        await postRepo.AddAsync(new Post(user.Id, title));
        return true;
    }
}