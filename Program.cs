using System;
using System.IO;
using ConsoleHelp = MyLibrary.Class1;

namespace ConsoleApp1
{
    class Program
    {
        //Флаг темной темы
        static bool isBlackThemeOn = true;

        //Флаг отображения последних записей в гл меню
        static bool isShowRec = true;

        //Флаг выхода из цикла.
        static bool isExit = false;

        //Ответ юзера на запрос о вводе
        static string userAnswer = "";

        //Путь к файлу - файл будет создан\считан в папке с исходной программой
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
                Console.WriteLine("Сделайте выбор:\n  1. Создание записи\n  2. Выборка записей" +
                    "\n  3. Действия с записями" +
                    "\n  4. Настройки\n  5. Для выхода нажмите любую другую клавишу...");

                ShowTodayLastRec(rp);
                userAnswer = Console.ReadLine();
                switch (userAnswer)
                {
                    case "1":
                        CreatePage(rp);// "Показывает" страницу с созданием записей
                        isExit = false;
                        rp.SaveAllRec();
                        break;
                    case "2":
                        ChoosePage(rp); // "Показывает" страницу с опциями выборки записей
                        isExit = false;
                        rp.SaveAllRec();
                        break;
                    case "3":
                        ActionPage(rp); // ""Показывает" страницу с действиями над записью
                        isExit = false;
                        break;
                    case "4":
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
        /// Вывод записей в консоль
        /// </summary>
        /// <param name="rp"></param>
        private static void ShowTodayLastRec(Repository rp)
        {
            if (isShowRec)
            {
                Console.WriteLine("\n\n============================================");
                Console.WriteLine("Последние записи на сегодня:");
                DateTime n = new DateTime();
                int countRec = rp.records.Length - 1;
                n = DateTime.Now;
                for (int i = 0; i < 3; i++)
                {
                    if (rp.records[countRec].ID != 0)
                    {
                        if (rp.records[countRec].DataCreate.Day == n.Day)
                        {
                            Console.WriteLine("*************************************");
                            rp.records[countRec--].Print(1);
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        countRec--;
                        i--;
                    }

                    }
                }

                Console.WriteLine("============================================");

            }
        }

        /// <summary>
        /// Страница с настройками
        /// </summary>
        public static void SettingsPage()
        {
            while (!isExit)
            {
                ConsoleHelp.PrintSubtitle("НАСТРОЙКИ");
                Console.WriteLine($"Сделайте выбор:\n  1. Темная тема({GetThemeStatus()}) \n " +
                    $" 2. Показывать записи на сегодня({GetShowTodayRecStatus()})" +
                    "\n  3. Для выхода в главное меню нажмите любую другую клавишу...");
                userAnswer = Console.ReadLine();
                switch (userAnswer)
                {
                    case "1":
                        ChaneConsoleTheme();
                        break;
                    case "2":
                        ChangeShowRec();
                        break;
                    default:
                        isExit = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Изменение состояния флага Показ записей
        /// </summary>
        private static void ChangeShowRec()
        {
            isShowRec = !isShowRec;
        }

        /// <summary>
        /// Отображение статуса показа записи в настройках. По умолчанию выключено
        /// </summary>
        /// <returns></returns>
        private static object GetShowTodayRecStatus()
        {
            return isShowRec ? "включено" : "выключено";
        }

        /// <summary>
        /// Включает выключает темную тему
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
        /// Отображение статуса темной темы. По умолчанию включено
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
                Console.WriteLine($"Сделайте выбор:\n  1. Редактирование записи \n  2. Удаление записи" +
                    "\n  3. Для выхода в главное меню нажмите любую другую клавишу...");
                userAnswer = Console.ReadLine();
                switch (userAnswer)
                {
                    case "1":
                        rp.EditRec();
                        break;
                    case "2":
                        rp.DeleteRec();
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

        /// <summary>
        /// Страница выборки записей
        /// </summary>
        /// <param name="rp"></param>
        public static void ChoosePage(Repository rp)
        {
            while (!isExit)
            {
                ConsoleHelp.PrintSubtitle("ВЫБОР ЗАПИСИ(-ЕЙ)");
                Console.WriteLine($"Сделайте выбор:\n  1. Выбор записи по дате(краткий формат) \n  " +
                    $"2. Выбор записи по номеру(краткий формат)" +
                    "\n  3. Выбор записи по дате(полный вывод)\n  4. Выбор записи по номеру(полный вывод)" +
                    "\n  5. Для выхода в главное меню нажмите любую другую клавишу...");
                userAnswer = Console.ReadLine();
                switch (userAnswer)
                {
                    case "1":
                        rp.ChooseRecs(1, "short");// по дате, короткий формат
                        break;
                    case "2":
                        rp.ChooseRecs(2, "short");// по ид, короткий формат
                        break;
                    case "3":
                        rp.ChooseRecs(1, "full");// по дате, полный формат
                        break;
                    case "4":
                        rp.ChooseRecs(2, "full");// по ид, полный формат
                        break;
                    default:
                        isExit = true;
                        break;
                }
            }
        }
    }
}
