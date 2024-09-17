using System.Text;
using CLI.UI;
using MemoryRepo;

var init = new StringBuilder();
init.AppendLine("Welcome to the LoreIt!").AppendLine("Using v0.2 // Logic: Local - Storage: In Memory")
    .AppendLine($"Today is: {DateTime.Now.ToShortDateString()}").AppendLine("\nGit: https://github.com/Taggerkov/LoreIt")
    .AppendLine("Use '!help' command to get a list of available commands.");
Console.WriteLine(init.ToString());
try {
    var cliApp = new CliApp(new UserImpl(), new ChannelImpl(), new PostImpl(), new CommentImpl());
    await cliApp.Run();
}
catch (Exception) {
    Console.Write("An error occured while initializing the command line interface!\nClosing the application ...");
    Thread.Sleep(2000);
}