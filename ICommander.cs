using System;

namespace domino
{
    public interface ICommander : IDisposable
    {
        void Execute(string fileName);
    }
}
