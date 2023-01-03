using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
    public class Logger
    {
        public void LogMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[MESSAGE] ({DateTime.Now.ToLongTimeString().ToString()}) {message.ToString()}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void LogInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"[INFO] ({DateTime.Now.ToLongTimeString().ToString()}) {message.ToString()}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void LogWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"[WARNING] ({DateTime.Now.ToLongTimeString().ToString()}) {message.ToString()}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] ({DateTime.Now.ToLongTimeString().ToString()}) {message.ToString()}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void LogDebug(string message)
        {
#if DEBUG
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"[DEBUG] ({DateTime.Now.ToLongTimeString().ToString()}) {message.ToString()}");
            Console.ForegroundColor = ConsoleColor.White;
#endif
        }
    }
}