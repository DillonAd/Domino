using McMaster.Extensions.CommandLineUtils;

namespace domino
{
    [Command(Name = "domino", Description = "Domino is a file watcher that runs a custom script when changes occur.")]
    [VersionOption("GetVersion")]
    [HelpOption]
    [Subcommand("start", typeof(Domino_Start))]
    [Subcommand("ignore", typeof(Domino_Ignore))]
    public class Domino
    {
        public static string GetVersion() =>
             typeof(Domino).Assembly.GetName().Version.ToString();

        public static void Main(string[] args) =>
            CommandLineApplication.ExecuteAsync<Domino>(args);

        private int OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();
            return 1;
        }   
    }
}
