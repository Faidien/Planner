using System;
using Title = MyLibrary.Class1;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            bool isExit = false;

            string userAnswer = "";
            // Я таки смог прикрутить библиотеку, чтобы печатать тему задания более менее "красиво": * * * * * *  Заголовок * * * * * * 
            Title.Print("Ежедневник");
            Console.Title = "Ежедневник 1.0";
            Planner p = new();
            p.Init();
            while (!isExit)
            {
                var actConsoleColor = Console.ForegroundColor;
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("*ГЛАВНОЕ МЕНЮ*");
                Console.ForegroundColor = actConsoleColor;
                Console.WriteLine();
                Console.WriteLine("Сделайте выбор:\n  1. Создание записи \n  2. Редактирование записи\n  3. Удаление записи" +
                    "\n  4. Настройки\n  5. Для выхода нажмите любую другую клавишу...");
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
                    case "4":
                        SettingsPage.Show();
                        break;
                    default:
                        isExit = true;
                        break;
                }
            }
            Exit();
        }

        public static void Exit()
        {
            Console.WriteLine("__________________________________________________________________________________________________________");
            string[] byeSentence = { "До свидания!", "Удачного дня!", "Не забывай про свои дела, что записал!", "Хорошего дня на работе!",
                "Любовь приходит и уходит...\nА кушать хочется всегда.\nИз этого всего выходит,\nЧто в жизни главное - ЕДА!", "Люди не " +
                "хранят дневники для себя. \nОни хранят их для других как секрет, о котором не хочется рассказывать, но при этом хочется, " +
                "чтобы о нем знали все. \nЕдинственный надежный сейф - твоя собственная память, в которую никто не может залезть без твоего " +
                "ведома.", "Опасность не в том, что компьютер однажды начнет мыслить, как человек, а в том," +
                " что человек однажды начнет мыслить, как компьютер.", "Дневники и техника дойдет до такого совершенства, что человек " +
                "сможет обойтись без себя."};

            Random r = new Random();
            Console.WriteLine(byeSentence[r.Next(byeSentence.Length)]);

            Environment.Exit(0);
        }
    }
}
