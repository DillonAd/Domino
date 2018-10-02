using domino.Logging;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace domino
{
    [Command(Name = "start", Description = "Starts the file watcher.")]
    [HelpOption]
    public class Domino_Start
    {
        [Argument(0, Name = "script", ShowInHelpText = true, Description = "Script to execute on file change.")]
        [Required]
        public virtual string ScriptName { get; }
        
        public IHostBuilder ConfigureHostBuilder()
        {
            var hostBuilder = new HostBuilder()
                .UseConsoleLifetime()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<ILogger, Logger>()
                        .AddSingleton(options => new CommanderOptions(ScriptName))
                        .AddSingleton<IIgnorePatternCollection, IgnorePatternCollection>()
                        .AddSingleton<ICommander, Commander>()
                        .AddHostedService<WatcherHost>();
                });

            return hostBuilder;
        }

        private async Task OnExecuteAsync() =>
            await ConfigureHostBuilder().Build().RunAsync();
    }
}
