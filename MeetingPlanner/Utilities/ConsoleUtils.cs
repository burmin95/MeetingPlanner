using MeetingPlanner.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeetingPlanner.Utilities
{
    public static class ConsoleUtils
    {
        public static void WriteError(string text) => WriteWithColor(text, ConsoleColor.Red);
        public static void WriteWarning(string text) => WriteWithColor(text, ConsoleColor.Yellow);
        public static void WriteSuccess(string text) => WriteWithColor(text, ConsoleColor.Green);
        public static void WriteInvite(string text) => WriteWithColor(text, ConsoleColor.Blue);

        private static void WriteWithColor(string text, ConsoleColor color)
        {
            try
            {
                Console.ForegroundColor = color;
                Console.WriteLine(text);
            }
            finally
            {
                Console.ResetColor();
            }
        }

        public static T ItemSelector<T>(List<T> items, string errorText)
        {
            while (true)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(i + 1);
                    Console.ResetColor();
                    Console.WriteLine($" - {items[i].ToString()}");
                }
                var itemNumber = ConsoleReaders.ReadInt();
                if ((itemNumber < 1) || (itemNumber > items.Count))
                {
                    ConsoleUtils.WriteError(errorText);
                    continue;
                }

                return items[itemNumber - 1];
            }
        }
    }
}