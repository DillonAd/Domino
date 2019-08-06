using domino.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace domino
{
    public class IgnoreFile : IIgnoreFile
    {
        public const string IgnoreFileName = ".dominoignore";

        private readonly ILogger _logger;

        public IEnumerable<string> Contents { get; }

        public IgnoreFile(ILogger logger)
        {
            _logger = logger;
            string filePath = FindFile(Directory.GetCurrentDirectory());
            Contents = File.ReadAllLines(filePath);
        }

        private string FindFile(string directory)
        {
            try
            {
                var filePath = Path.Combine(directory, IgnoreFileName);

                if (File.Exists(filePath))
                {
                    return filePath;
                }
                else
                {
                    if (Directory.GetDirectoryRoot(directory) == directory)
                    {
                        return string.Empty;
                    }
                    else 
                    {
                        return FindFile(Directory.GetParent(directory).FullName);
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.Debug(ex);
                throw;
            }
        }
    }
}