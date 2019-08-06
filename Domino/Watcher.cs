using System.IO;

namespace domino
{
    public class Watcher : IWatcher
    {
        public event FileSystemEventHandler FileChanged;

        private readonly FileSystemWatcher _watcher;

        public Watcher()
        {
            _watcher = new FileSystemWatcher(Directory.GetCurrentDirectory());
            _watcher.IncludeSubdirectories = true;

            _watcher.NotifyFilter = NotifyFilters.LastWrite;
            _watcher.Changed += FileChanged;
        }

        public void Start()
        {
            _watcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            _watcher.EnableRaisingEvents = false;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                FileChanged = null;
                _watcher.Dispose();
            }
        }
    }
}