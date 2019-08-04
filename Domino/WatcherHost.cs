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
        private readonly ICommander _commander;
        private readonly IIgnorePatternCollection _ignorePatternCollection;
        private readonly ILogger _logger;
        private readonly IWatcher _watcher;

        private DateTime _lastFileChanged { get; set; }

        public WatcherHost(ICommander commander, IIgnorePatternCollection ignorePatternCollection, ILogger logger, IWatcher watcher)
        {
            _commander = commander;
            _ignorePatternCollection = ignorePatternCollection;
            _logger = logger;
            _watcher = watcher;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _watcher.Start();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _watcher.Stop();

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _watcher.Dispose();
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