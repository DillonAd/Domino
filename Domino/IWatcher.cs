using System;
using System.IO;

namespace domino
{
    public interface IWatcher : IDisposable
    {
        event FileSystemEventHandler FileChanged;

        void Start();
        void Stop();
    }
}