using domino.Logging;
using Moq;
using System.IO;
using Xunit;

namespace domino.Test.Unit
{
    public class WatcherHostTest
    {
        [Fact]
        [Trait("Category", "unit")]
        public void WatcherHost_ExecutedAction()
        {
            // Assemble
            var commanderMock = new Mock<ICommander>();
            var ignorePatternsMock = new Mock<IIgnorePatternCollection>();
            ignorePatternsMock.Setup(ip => ip.ShouldIgnore(It.IsAny<string>()))
                              .Returns(false);

            var loggerMock = new Mock<ILogger>();
            var watcherMock = new Mock<IWatcher>();

            var host = new WatcherHost(
                commanderMock.Object,
                ignorePatternsMock.Object,
                loggerMock.Object,
                watcherMock.Object);

            const string fileName = "testFile.src";
            var eventArgs = new FileSystemEventArgs(WatcherChangeTypes.Changed, string.Empty, fileName);

            // Act
            watcherMock.Raise(w => w.FileChanged += null, eventArgs);

            // Assert
            commanderMock.Verify(c => c.Execute(fileName), Times.Once);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void WatcherHost_ActionNotExecuted()
        {
            // Assemble
            var commanderMock = new Mock<ICommander>();
            var ignorePatternsMock = new Mock<IIgnorePatternCollection>();
            ignorePatternsMock.Setup(ip => ip.ShouldIgnore(It.IsAny<string>()))
                              .Returns(true);
                              
            var loggerMock = new Mock<ILogger>();
            var watcherMock = new Mock<IWatcher>();

            var host = new WatcherHost(
                commanderMock.Object,
                ignorePatternsMock.Object,
                loggerMock.Object,
                watcherMock.Object);

            const string fileName = "testFile.src";
            var eventArgs = new FileSystemEventArgs(WatcherChangeTypes.Changed, string.Empty, fileName);

            // Act
            watcherMock.Raise(w => w.FileChanged += null, eventArgs);

            // Assert
            commanderMock.Verify(c => c.Execute(fileName), Times.Never);
        }
    }
}