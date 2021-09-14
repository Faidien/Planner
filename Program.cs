using System;
using System.IO;
using ConsoleHelp = MyLibrary.Class1;

namespace ConsoleApp1
{
    class Program
    {
        static bool isBlackThemeOn = true;
        static bool isExit = false;
        static string userAnswer = "";
        static string path = Directory.GetCurrentDirectory() + @"\data.txt";

        /// <summary>
        /// Страница главная всей программы
        /// </summary>
        static void Main()
        {
            Console.Title = "Ежедневник 1.5";
            Repository rp = new Repository(path);
            while (!isExit)
            {
                ConsoleHelp.PrintSubtitle("ГЛАВНОЕ МЕНЮ");
                Console.WriteLine("Сделайте выбор:\n  1. Создание записи \n  2. Действия с записями" +
                    "\n  3. Настройки\n  4. Для выхода нажмите любую другую клавишу...");
                userAnswer = Console.ReadLine();
                switch (userAnswer)
                {
                    case "1":
                        CreatePage(rp);// "Показывает" страницу с созданием записей
                        isExit = false;
                        break;
                    case "2":
                        ActionPage(rp); // "Показывает" страницу с действиями над записью
                        isExit = false;
                        break;
                    case "3":
                        SettingsPage(); // "Показывает" страницу настроек
                        isExit = false;
                        break;
                    default:
                        isExit = true;
                        break;
                }
            }
            Exit(rp);
        }

        /// <summary>
        /// Страница с настройками
        /// </summary>
        public static void SettingsPage()
        {
            while (!isExit)
            {
                ConsoleHelp.PrintSubtitle("НАСТРОЙКИ");
                Console.WriteLine($"Сделайте выбор:\n  1. Темная тема({GetThemeStatus()}) \n  2. Показывать записи на сегодня(выключено)" +
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

        /// <summary>
        /// Смена темы консоли
        /// </summary>
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

        /// <summary>
        /// Получение состояния флага "темная тема" - вкл или выкл
        /// </summary>
        /// <returns></returns>
        private static string GetThemeStatus()
        {
            return isBlackThemeOn ? "включено" : "выключено";
        }

        /// <summary>
        /// Страница с созданием записи
        /// </summary>
        /// <param name="p"></param>
        public static void CreatePage(Repository rp)
        {
            while (!isExit)
            {
                ConsoleHelp.PrintSubtitle("СОЗДАНИЕ ЗАПИСИ");
                Console.WriteLine($"Сделайте выбор:\n  1. Ввод записи \n  2. Сгенерировать запись" +
                    "\n  3. Для выхода в главное меню нажмите любую другую клавишу...");
                userAnswer = Console.ReadLine();
                switch (userAnswer)
                {
                    case "1":
                        rp.CreateRec(1);
                        break;
                    case "2":
                        rp.CreateRec(2);
                        break;
                    default:
                        isExit = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Страница с редактированием, удалением, выборкой записей
        /// </summary>
        /// <param name="p"></param>
        public static void ActionPage(Repository rp)
        {
            while (!isExit)
            {
                ConsoleHelp.PrintSubtitle("ДЕЙСТВИЯ С ЗАПИСЯМИ");
                Console.WriteLine($"Сделайте выбор:\n  1. Редактирование записи \n  2. Удаление записи\n  3. Выборка записей" +
                    "\n  4. Для выхода в главное меню нажмите любую другую клавишу...");
                userAnswer = Console.ReadLine();
                switch (userAnswer)
                {
                    case "1":
                        rp.EditRec();
                        break;
                    case "2":
                        rp.DeleteRec();
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

        /// <summary>
        /// Выход из программы с текстом на прощание
        /// </summary>
        public static void Exit(Repository rp = null)
        {
            if (rp != null)
            {
                rp.SaveAllRec();
            }


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
