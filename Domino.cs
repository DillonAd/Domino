using McMaster.Extensions.CommandLineUtils;

namespace domino
{
    [Command(Name = "domino", Description = "My global command line tool.")]
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
    }
}
