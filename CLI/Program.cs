using System.Text;
using CLI.UI;
using LocalImpl;

var init = new StringBuilder();
init.AppendLine("Welcome to the LoreIt!").AppendLine("Using v0.3 // Logic: Local - Storage: Local Files")
    .AppendLine($"Today is: {DateTime.Now.ToShortDateString()}").AppendLine("\nGit: https://github.com/Taggerkov/LoreIt")
    .AppendLine("Use '!help' command to get a list of available commands.");
Console.WriteLine(init.ToString());
try {
    var cliApp = new CliApp(UserLocal.Get(), ChannelLocal.Get(), PostLocal.Get(), CommentLocal.Get());
    await cliApp.Run();
}
catch (Exception) {
    Console.Write("An error occured while initializing the command line interface!\nClosing the application ...");
    Thread.Sleep(2000);
}