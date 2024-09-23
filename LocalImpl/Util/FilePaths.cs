namespace LocalImpl.Util;

/// <summary>
/// Provides file paths for different types of data storage in the local file system.
/// </summary>
internal static class FilePaths {
    /// <summary>
    /// Defines the base directory path where various data files such as channels, comments, posts,
    /// and users are stored in the local file system.
    /// </summary>
    private const string FolderPath = @"files\";

    /// <summary>
    /// Represents the file path for storing Channel data in the local file system. The path is
    /// constructed by appending the filename 'channels.lit' to the base folder path defined in
    /// the containing class.
    /// </summary>
    internal const string ChannelsPath = $"{FolderPath}channels.lit";

    /// <summary>
    /// Represents the file path for storing Comment data in the local file system. The path is
    /// constructed by appending the filename 'comments.lit' to the base folder path defined in
    /// the containing class.
    /// </summary>
    internal const string CommentsPath = $"{FolderPath}comments.lit";

    /// <summary>
    /// Represents the file path for storing Post data in the local file system. The path is
    /// constructed by appending the filename 'posts.lit' to the base folder path defined in
    /// the containing class.
    /// </summary>
    internal const string PostsPath = $"{FolderPath}posts.lit";

    /// <summary>
    /// Represents the file path for storing User data in the local file system. The path is
    /// constructed by appending the filename 'users.lit' to the base folder path defined in
    /// the containing class.
    /// </summary>
    internal const string UsersPath = $"{FolderPath}users.lit";
}