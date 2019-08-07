using domino.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace domino
{
    public class IgnorePatternCollection : IIgnorePatternCollection
    {
        private readonly IEnumerable<string> _ignorePatterns;

        public IgnorePatternCollection(IIgnoreFile ignoreFile)
        {
            _ignorePatterns = ignoreFile.Contents
                                        .Select(ip => "^" + 
                                                      Regex.Escape(ip)
                                                           .Replace(@"\*", ".*")
                                                           .Replace(@"\?", ".") + 
                                                      "$");
        }

        public bool ShouldIgnore(string fileName) =>
            _ignorePatterns.Any(ip => new Regex(ip).IsMatch(fileName));
    }
}
