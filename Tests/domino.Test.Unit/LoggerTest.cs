using System;
using domino.Logging;
using Xunit;

namespace domino.Test.Unit
{
    public class LoggerTest
    {
        [Fact]
        public void DebugNullMessageDoesntThrow()
        {
            var sut = new Logger();
            string message = null;
            sut.Debug(message);
        }

        [Fact]
        public void DebugNullExceptionDoesntThrow()
        {
            var sut = new Logger();
            Exception exception = null;
            sut.Debug(exception);
        }
    }
}
