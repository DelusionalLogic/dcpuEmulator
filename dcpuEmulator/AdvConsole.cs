using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dcpuEmulator
{
    class AdvConsole
    {

        private static int fromTop = 0;

        private static int _logLevel = 20;
        public static int logLevel
        {
            get { return _logLevel; }
            set { _logLevel = value; }
        }

        private static void clearLine()
        {
            Console.CursorLeft = 0;
            for (int i = 0; i < Console.BufferWidth - 1; i++)
                Console.Write(" ");
            Console.CursorLeft = 0;
        }

        /// <summary>
        /// Write a line to console
        /// </summary>
        /// <param name="message">String to be written</param>
        /// <param name="foreground">Foreground color</param>
        /// <param name="background">Background color</param>
        public static void Write(string message, ConsoleColor foreground = ConsoleColor.Gray, ConsoleColor background = ConsoleColor.Black)
        {
            Console.SetCursorPosition(0, fromTop);
            clearLine();
            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
            Console.WriteLine(message);
            fromTop = Console.CursorTop;
            drawInput("Running");
        }

        /// <summary>
        /// Redraw the input area
        /// </summary>
        /// <param name="message">Message for input</param>
        /// <param name="foreground">Foreground color</param>
        /// <param name="background">Background color</param>
        private static void drawInput(string message, ConsoleColor foreground = ConsoleColor.Gray, ConsoleColor background = ConsoleColor.Black)
        {
            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
            Console.CursorLeft = 0;
            Console.CursorTop = Console.WindowTop + Console.WindowHeight - 1;
            clearLine();
            Console.Write("{0}>", message);
        }

        /// <summary>
        /// Logs a message
        /// </summary>
        /// <param name="message">The message to log</param>
        public static void Log(object message)
        {
            if (_logLevel >= 10)
                Write("[Log] " + message.ToString(), ConsoleColor.Yellow);
        }

        /// <summary>
        /// Logs a debug message
        /// </summary>
        /// <param name="message">The message to log</param>
        public static void Debug(object message)
        {
            if (_logLevel >= 20)
                Write("[Debug] " + message.ToString(), ConsoleColor.Gray);
        }

        /// <summary>
        /// Logs a result message
        /// </summary>
        /// <param name="message">The message to log</param>
        public static void Result(object message)
        {
            if (_logLevel >= 0)
                Write("[Result] " + message.ToString(), ConsoleColor.Green);
        }

        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="message">The message to log</param>
        public static void Error(object message)
        {
            if (_logLevel >= 1)
                Write("[Error] " + message.ToString(), ConsoleColor.Red);
        }

        /// <summary>
        /// Insert new line
        /// </summary>
        public static void NewLine()
        {
            Write("");
        }

        /// <summary>
        /// Read user input
        /// </summary>
        /// <returns>User input</returns>
        public static string ReadLine()
        {
            return ReadLine("Ready");
        }


        /// <summary>
        /// Clear the console
        /// </summary>
        public static void Clear()
        {
            Console.Clear();
            fromTop = 0;
        }

        /// <summary>
        /// Read user input with messsage
        /// </summary>
        /// <param name="message">Message for user</param>
        /// <param name="foreground">Foreground color</param>
        /// <param name="background">Background color</param>
        /// <returns>User input</returns>
        public static string ReadLine(string message, ConsoleColor foreground = ConsoleColor.Gray, ConsoleColor background = ConsoleColor.Black)
        {
            int top = Console.WindowTop;
            drawInput(message, foreground, background);
            string line = Console.ReadLine();
            Console.SetWindowPosition(0, top);
            return line;
        }

        /// <summary>
        /// Draw a menu
        /// </summary>
        /// <param name="items">Array of items for the menu</param>
        /// <returns>The selected item index</returns>
        public static int openMenu(object[] items)
        {
            bool error = false;
            while (true)
            {
                Clear();
                for (int i = 0; i < items.Length; i++)
                {
                    Write(string.Format("{0}. {1}", i + 1, items[i]));
                }

                int choice;

                if (int.TryParse(ReadLine(error ? "Input Error" : "Please select", error ? ConsoleColor.Red : ConsoleColor.Gray), out choice))
                {
                    if (choice >= 1 && choice <= items.Length)
                    {
                        Clear();
                        return choice - 1;
                    }
                }
                error = true;
            }
        }
    }
}
