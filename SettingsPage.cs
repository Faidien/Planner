using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    static class SettingsPage
    {
        private static bool isBlackThemeOn = true;
        public static void Show()
        {
            bool isExit = false;
            string userAnswer = "";

            while (!isExit)
            {
                var actConsoleColor = Console.ForegroundColor;
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("*НАСТРОЙКИ*");
                Console.ForegroundColor = actConsoleColor;
                Console.WriteLine();
                Console.WriteLine($"Сделайте выбор:\n  1. Темная тема({ThemeStatus()}) \n  2. Показывать записи на сегодня(выключено)" +
                    "\n  3. Для выхода в главное меню нажмите любую другую клавишу...");
                userAnswer = Console.ReadLine();
                switch (userAnswer)
                {
                    case "1":
                        ChaneConsoleTheme();
                        break;
                    case "2":
                        Console.WriteLine("Выбор 2 варианта!");
                        break;
                    default:
                        isExit = true;
                        break;
                }
            }
        }

        private static void ChaneConsoleTheme()
        {
            var isBlackTheme = Console.BackgroundColor == ConsoleColor.Black;
            if (isBlackTheme)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.SetBufferSize(Console.BufferWidth + 1, Console.BufferHeight);
                isBlackThemeOn = false;


            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetBufferSize(Console.BufferWidth - 1, Console.BufferHeight);
                isBlackThemeOn = true;

            }


        }
        private static string ThemeStatus()
        {
            return isBlackThemeOn ? "включено" : "выключено";
        }
    }
}
