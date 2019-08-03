using System;

namespace domino.Logging
{
    public interface ILogger
    {
        void Output(string message);
        
        void Debug(string message);
        void Error(string message);
        void Info(string message);
        void Warn(string message);

        void Debug(Exception ex);
        void Error(Exception ex);
        void Info(Exception ex);
        void Warn(Exception ex);
    }
}
