using domino.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace domino
{
    public class IgnorePatternCollection : IIgnorePatternCollection
    {
        private const string IgnoreFileName = ".dominoignore";

        private readonly ILogger _logger;
        private readonly IEnumerable<string> _ignorePatterns;

        public IgnorePatternCollection(ILogger logger)
        {
            _logger = logger;
            _ignorePatterns = GetIgnoredPatterns(Directory.GetCurrentDirectory());
        }

        public bool ShouldIgnore(string fileName) =>
            _ignorePatterns.Any(ip => 
                new Regex(ip.Replace(".", "[.]")
                            .Replace("*", ".*")
                            .Replace("?", "."))
                .IsMatch(fileName));

        private IEnumerable<string> GetIgnoredPatterns(string directory)
        {
            try
            {
                var filePath = Path.Combine(directory, IgnoreFileName);

                if (File.Exists(filePath))
                {
                    return File.ReadAllLines(filePath);
                }
                else
                {
                    if (Directory.GetDirectoryRoot(directory) == directory)
                    {
                        return new List<string>();
                    }

                    return GetIgnoredPatterns(Directory.GetParent(directory).FullName);
                }
            }
            catch(Exception ex)
            {
                _logger.Debug(ex);
                return new List<string>();
            }
        }
    }
}
