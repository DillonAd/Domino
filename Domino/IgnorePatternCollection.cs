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

        public IgnorePatternCollection(IIgnoreFile ignoreFile, ILogger logger)
        {
            _logger = logger;
            _ignorePatterns = ignoreFile.Contents
                                        .Select(ip => ip.Replace(".", "[.]")
                                                        .Replace("*", ".*")
                                                        .Replace("?", "."));
        }

        public bool ShouldIgnore(string fileName) =>
            _ignorePatterns.Any(ip => new Regex(ip).IsMatch(fileName));
    }
}
