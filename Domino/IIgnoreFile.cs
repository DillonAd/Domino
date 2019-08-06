using System.Collections.Generic;

namespace domino
{
    public interface IIgnoreFile
    {
        IEnumerable<string> Contents { get; }
    }
}