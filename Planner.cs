using System;
using System.IO;
using System.Text;
using Title = MyLibrary.Class1;


namespace ConsoleApp1
{
    struct Planner
    {
        Record[] records;
        public void Init()
        {
            Random r = new Random();
            string db = Directory.GetCurrentDirectory() + @"\data.csv";

            Console.Write("Ваш дневник приветсвует Вас!\nСейчас проверю наличие базы данных записей");
            Title.Turn(r.Next(5,15));
            Console.WriteLine();

            if (File.Exists(db))
            {
                Console.Write("Файл найден, выполняю загрузку...");
                Title.Turn(r.Next(10, 20));
                OpenBase(db);
                Console.WriteLine();
            }

            else
            {
                Console.Write("Файл не найден, выполнить создание дневника?(1 - да, остальные клавиши - нет): ");
                string answer = Console.ReadLine();
                if (answer == "1")
                    CreateBase(db);
                else
                {
                    Program.Exit();
                }
            }
        }

        private static void CreateBase(string path)
        {
            using (FileStream fs = File.Create(path))
            {
            }
        }

        private static void OpenBase(string path)
        {
            using (FileStream fs = File.OpenRead(path))
            {
            }
        }

        //public Planner()
        //{

        //}
    }
}
