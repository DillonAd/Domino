using domino.Logging;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace domino
{
    public sealed class WatcherHost : IHostedService, IDisposable
    {
        private readonly FileSystemWatcher _watcher;
        private readonly ICommander _commander;
        private readonly ILogger _logger;
        private readonly IIgnorePatternCollection _ignorePatternCollection;

        private DateTime _lastFileChanged { get; set; }

        public WatcherHost(ICommander commander, ILogger logger, IIgnorePatternCollection ignorePatternCollection)
        {
            _commander = commander;
            _logger = logger;
            _ignorePatternCollection = ignorePatternCollection;

            _watcher = new FileSystemWatcher(Directory.GetCurrentDirectory());
            _watcher.IncludeSubdirectories = true;

            _watcher.NotifyFilter = NotifyFilters.LastWrite;
            _watcher.Changed += FileChanged;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _watcher.EnableRaisingEvents = true;

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _watcher.EnableRaisingEvents = false;

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _commander.Dispose();
        }

        private void FileChanged(object sender, FileSystemEventArgs e)
        {
            if (!_ignorePatternCollection.ShouldIgnore(e.Name))
            {
                if (DateTime.Now.Subtract(_lastFileChanged).TotalMilliseconds < 1000)
                {
                    return;
                }

                _logger.Info($"{e.Name} changed...");
                _commander.Execute(e.Name);

                _lastFileChanged = DateTime.Now;
            }
        }
    }
}
