using System;

namespace domino.Logging
{
    public class Logger : ILogger
    {
        public void Output(string message) =>
            Console.WriteLine(message);

        public void Debug(string message) =>
            WriteLine(Format("Debug", message));

        public void Debug(Exception ex) =>
            Debug(GetException(ex));

        public void Error(string message) =>
            WriteLine(Format("Error", message));

        public void Error(Exception ex) =>
            Error(GetException(ex));

        public void Info(string message) => 
            WriteLine(Format("Info", message));

        public void Info(Exception ex) =>
            Info(GetException(ex));

        public void Warn(string message) => 
            WriteLine(Format("Warn", message));

        public void Warn(Exception ex) =>
            Warn(GetException(ex));

        private string Format(string level, string message) =>
            string.IsNullOrWhiteSpace(message) ?
                string.Empty :
                $"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff")} [{level}] {message}";

        private void WriteLine(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                Console.WriteLine(message);
            }
        }

        private string GetException(Exception ex) =>
            ex == null ? string.Empty : $"{ex.Message}\n{ex.StackTrace}\n\n";
    }
}
