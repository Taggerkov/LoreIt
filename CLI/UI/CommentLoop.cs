using System.Text;
using RepoContracts;
using ServerEntities;

namespace CLI.UI;

/// Provides an interface for handling channel-related commands within the command-line interface.
public static class CommentLoop {
    /// Represents the command string that triggers the comment operation.
    private const string HelpStr = "!help",
        ShowAllStr = "!showall",
        ViewStr = "!view",
        NewStr = "!new",
        DeleteStr = "!delete",
        ReturnStr = "!return";

    /// Handles commands related to loop operations.
    /// <param name="commentRepo">The repository used for managing comments.</param>
    /// <param name="postRepo">The repository used for managing posts.</param>
    /// <param name="userRepo">The repository used for managing users.</param>
    /// <return>A task that represents the asynchronous operation, containing a boolean value indicating if the application should return to the main loop.</return>
    public static Task<bool> Show(ICommentRepo commentRepo, IPostRepo postRepo, IUserRepo userRepo) {
        ArgumentNullException.ThrowIfNull(commentRepo, nameof(commentRepo));
        ArgumentNullException.ThrowIfNull(postRepo, nameof(postRepo));
        ArgumentNullException.ThrowIfNull(userRepo, nameof(userRepo));
        Console.Clear();
        Console.WriteLine($"LoreIt - Comment ({DateTime.Now})\n");
        while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)) {
            Console.WriteLine("\nPlease enter your command: ");
            var userInput = Console.ReadLine() ?? string.Empty;
            try {
                var splitInput = userInput.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine();
                var result = splitInput[0] switch {
                    HelpStr => ShowHelp(),
                    ShowAllStr => ShowAll(commentRepo),
                    ViewStr => ViewAsync(commentRepo, postRepo, userRepo, splitInput[1]),
                    NewStr => NewAsync(commentRepo, userRepo, splitInput[1]),
                    DeleteStr => DeleteAsync(commentRepo, splitInput[1]),
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
            .AppendLine($"{ShowAllStr} - Views all registered comments.")
            .AppendLine($"{ViewStr} commentId - Displays detailed information about a comment given it's unique identifier.")
            .AppendLine($"{NewStr} postId - Creates a new comment.")
            .AppendLine($"{DeleteStr} commentId - Deletes a comment given it's unique identifier from the service.")
            .AppendLine($"{ReturnStr} - Return to the main application.");
        Console.WriteLine(help.ToString());
        return Task.FromResult(false);
    }

    /// Displays all registered comments.
    /// <param name="commentRepo">The repository instance handling comment data interactions.</param>
    /// <return>A task representing the asynchronous operation, containing a boolean value indicating if the application should return to the main loop.</return>
    private static Task<bool> ShowAll(ICommentRepo commentRepo) {
        var comments = commentRepo.GetAllComments();
        Console.WriteLine("Available users:");
        if (comments.Count() > 1) {
            Console.WriteLine("No available users found...");
            return Task.FromResult(false);
        }
        foreach (var c in comments) Console.WriteLine($"Comment - {c.Id}");
        return Task.FromResult(false);
    }

    /// Retrieves and displays details of a specified comment.
    /// <param name="commentRepo">The repository instance handling comment data interactions.</param>
    /// <param name="postRepo">The repository instance handling post data interactions.</param>
    /// <param name="userRepo">The repository instance handling user data interactions.</param>
    /// <param name="commentId">The identifier of the comment to be viewed.</param>
    /// <return>A task that represents the asynchronous operation, containing a boolean value indicating if the application should return to the main loop.</return>
    private static async Task<bool> ViewAsync(ICommentRepo commentRepo, IPostRepo postRepo, IUserRepo userRepo, string commentId) {
        try {
            var comment = await commentRepo.GetAsync(int.Parse(commentId));
            var user = await userRepo.GetByIdAsync(comment.CommenterId);
            var post = await postRepo.GetAsync(comment.PostId);
            var msg = new StringBuilder();
            msg.AppendLine($"Comment - {comment.Id} // Comment Details:").AppendLine($"Id: {comment.Id}").AppendLine($"From Post: {post.Title}")
                .AppendLine($"Commenter: {user.Username}").AppendLine($"Content: {comment.Content}").AppendLine($"Publish Date: {comment.PublishDate}")
                .AppendLine($"Last Modification {comment.LastModifiedDate}");
            Console.WriteLine(msg.ToString());
        }
        catch (Exception e) {
            Console.WriteLine($"Error viewing comment {commentId} : {e.Message}");
            throw;
        }
        return false;
    }

    /// Creates a new comment asynchronously.
    /// <param name="commentRepo">The repository instance handling comment data interactions.</param>
    /// <param name="userRepo">The repository instance handling user data interactions.</param>
    /// <param name="postId">The ID of the post.</param>
    /// <return>A task that represents the asynchronous operation, containing a boolean value indicating whether the channel creation was successful.</return>
    private static async Task<bool> NewAsync(ICommentRepo commentRepo, IUserRepo userRepo, string postId) {
        if (CheckLogin()) {
            Console.WriteLine("You are not logged in!");
            return false;
        }
        Console.WriteLine("\nWrite your Comment:");
        try {
            var user = await userRepo.GetByUsernameAsync(SignInLoop.LoginUsername!);
            await commentRepo.AddAsync(new Comment(user.Id, int.Parse(postId), Console.ReadLine() ?? string.Empty));
        }
        catch (InvalidOperationException e) {
            Console.WriteLine(e.Message);
            throw;
        }
        return true;
    }
    
    /// Deletes a comment asynchronously by ID.
    /// <param name="commentRepo">The repository interface for accessing and managing comments.</param>
    /// <param name="commentId">The ID of the comment to be deleted.</param>
    /// <return>A task that represents the asynchronous operation, containing a boolean value indicating whether the post was successfully deleted or not.</return>
    private static async Task<bool> DeleteAsync(ICommentRepo commentRepo, string commentId) {
        if (CheckLogin()) {
            Console.WriteLine("You are not logged in!");
            return false;
        }
        try {
            await commentRepo.DeleteAsync(int.Parse(commentId));
        }
        catch (Exception) {
            Console.WriteLine("Error managing argument.");
            throw;
        }
        return true;
    }
    
    /// Checks if the user is logged in.
    /// <return>A boolean value indicating whether the user is logged in or not.</return>
    private static bool CheckLogin() {
        return SignInLoop.LoginUsername is not null;
    }
}