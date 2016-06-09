using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logging
{
    public class Logger
    {
        public Logger()
        {
        }

        public void Info(string message, 
            [CallerMemberName] string memberName = "", 
            [CallerFilePath] string sourceFilePath = "", 
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            this.Log(message, LogMessageType.Info, memberName, sourceFilePath, sourceLineNumber);
        }

        public void Error(string message,
            Exception exception,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            this.Log(message, LogMessageType.Error, memberName, sourceFilePath, sourceLineNumber, exception);
        }

        public void Warning(string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            this.Log(message, LogMessageType.Warning, memberName, sourceFilePath, sourceLineNumber);
        }

        public void Message(string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            this.Log(message, LogMessageType.Message, memberName, sourceFilePath, sourceLineNumber);
        }

        private void Log(string message, 
            LogMessageType logMessageType,
            [CallerMemberName] string memberName = "", 
            [CallerFilePath] string sourceFilePath = "", 
            [CallerLineNumber] int sourceLineNumber = 0,
            Exception exception = null)
        {
        }
    }

    public enum LogMessageType
    {
        Info,
        Error,
        Warning,
        Message
    }
}
