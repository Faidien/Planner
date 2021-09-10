using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class ActionRecPage
    {
        public static void Show()
        {
            bool isExit = false;
            string userAnswer = "";

            while (!isExit)
            {
                var actConsoleColor = Console.ForegroundColor;
                Console.Clear();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("*ОПЕРАЦИИ С ЗАПИСЯМИ*");
                Console.ForegroundColor = actConsoleColor;
                Console.WriteLine();
                Console.WriteLine($"Сделайте выбор:\n  1. Редактирование записи \n  2. Удаление записи\n  3. Выборка записей" +
                    "\n  4. Для выхода в главное меню нажмите любую другую клавишу...");
                userAnswer = Console.ReadLine();
                switch (userAnswer)
                {
                    case "1":
                        Console.WriteLine("Выбор 1 варианта!");
                        break;
                    case "2":
                        Console.WriteLine("Выбор 2 варианта!");
                        break;
                    case "3":
                        Console.WriteLine("Выбор 3 варианта!");
                        break;
                    default:
                        isExit = true;
                        break;
                }
            }
        }
    }
}
