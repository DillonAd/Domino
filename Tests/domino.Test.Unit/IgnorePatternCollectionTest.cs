using domino.Logging;
using Moq;
using Xunit;

namespace domino.Test
{
    public class IgnorePatternCollectionTest
    {
        [Theory]
        [InlineData("*.log", "testFile.log")]
        [InlineData("*og", "testFile.log")]
        [InlineData("test*.log", "testFile.log")]
        [InlineData("*File.log", "testFile.log")]
        [InlineData("te*le.log", "testFile.log")]
        [InlineData("testFile.l?g", "testFile.log")]
        [InlineData("*_Layout.cshtml", "Views/Shared/_Layout.cshtml")]
        [InlineData("*_Layout.cshtml", @"Views\Shared\_Layout.cshtml")]
        [InlineData("*Layout.cshtml", "Views/Shared/_Layout.cshtml")]
        [InlineData("*Layout.cshtml", @"Views\Shared\_Layout.cshtml")]
        [InlineData("*Views/Shared/_Layout.cshtml", "Views/Shared/_Layout.cshtml")]
        [Trait("Category", "unit")]
        public void ShouldIgnore(string pattern, string fileName)
        {
            // Assemble
            var ignoreFileMock = new Mock<IIgnoreFile>();
            ignoreFileMock.Setup(i => i.Contents)
                          .Returns(new [] { pattern });

            var ignorePatterns = new IgnorePatternCollection(ignoreFileMock.Object);

            // Act
            var result = ignorePatterns.ShouldIgnore(fileName);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("*.log", "testFile.lo")]
        [InlineData("*.log", "testFile.og")]
        [InlineData("test.*log", "testFile.log")]
        [InlineData("log*", "testFile.log")]
        [InlineData("*file.log", "testFile.log")]
        [InlineData("testfile.log", "testFile.log")]
        [InlineData("testFile.l?g", "testFile.lot")]
        [Trait("Category", "unit")]
        public void ShouldNotIgnore(string pattern, string fileName)
        {
            // Assemble
            var ignoreFileMock = new Mock<IIgnoreFile>();
            ignoreFileMock.Setup(i => i.Contents)
                          .Returns(new [] { pattern });

            var ignorePatterns = new IgnorePatternCollection(ignoreFileMock.Object);

            // Act
            var result = ignorePatterns.ShouldIgnore(fileName);

            // Assert
            Assert.False(result);
        }
    }
}