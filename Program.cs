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
            Record r = new Record();
            
            Title.Print("Ежедневник");
            Console.Title = "Ежедневник 1.0";
            Planner.Init();
            while (!isExit)
            {
                var actConsoleColor = Console.ForegroundColor;
                Console.Clear();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("*ГЛАВНОЕ МЕНЮ*");
                Console.ForegroundColor = actConsoleColor;
                Console.WriteLine();
                Console.WriteLine("Сделайте выбор:\n  1. Создание записи \n  2. Действия с записями" +
                    "\n  3. Настройки\n  4. Для выхода нажмите любую другую клавишу...");
                userAnswer = Console.ReadLine();
                switch (userAnswer)
                {
                    case "1":
                        CreateRecPage.Show();
                        break;
                    case "2":
                        ActionRecPage.Show();
                        break;
                    case "3":
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
                " что человек однажды начнет мыслить, как компьютер.", "Дневники и техника дойдут до такого совершенства, что человек " +
                "сможет обойтись без себя."};

            Random r = new Random();
            Console.WriteLine(byeSentence[r.Next(byeSentence.Length)]);

            Environment.Exit(0);
        }
    }
}
