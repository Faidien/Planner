﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class CreateRecPage
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
                Console.WriteLine("*СОЗДАНИЕ ЗАПИСЕЙ*");
                Console.ForegroundColor = actConsoleColor;
                Console.WriteLine();
                Console.WriteLine($"Сделайте выбор:\n  1. Ввод записи \n  2. Сгенерировать запись" +
                    "\n  3. Для выхода в главное меню нажмите любую другую клавишу...");
                userAnswer = Console.ReadLine();
                switch (userAnswer)
                {
                    case "1":
                        Planner.CreateRec(1);
                        break;
                    case "2":
                        Planner.CreateRec(2);
                        break;
                    default:
                        isExit = true;
                        break;
                }
            }
        }
    }
}
