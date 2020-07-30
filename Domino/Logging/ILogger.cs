using System;

namespace domino.Logging
{
    public interface ILogger
    {
        void Output(string message);
        
        void Debug(string message);
        void Debug(Exception ex);
        void Error(Exception ex);
        void Error(string message);
        void Info(Exception ex);
        void Info(string message);
        void Warn(string message);
        void Warn(Exception ex);
    }
}
