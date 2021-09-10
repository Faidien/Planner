using System;
using System.IO;
using System.Text;
using Title = MyLibrary.Class1;


namespace ConsoleApp1
{
    struct Planner
    {
        private static string GetPath()
        {
            return Directory.GetCurrentDirectory() + @"\data.txt";
        }
        public static void Init()
        {
            Random r = new Random();
            string db = GetPath();

            Console.Write("Ваш дневник приветсвует Вас!\nСейчас проверю наличие базы данных записей");
            Title.Turn(r.Next(5, 15));
            Console.WriteLine();

            if (File.Exists(db))
            {
                Console.Write("Файл найден, выполняю загрузку...");
                Title.Turn(r.Next(8, 16));
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

        public static void CreateRec(int mode)
        {
            switch (mode)
            {
                case 1:
                    // Human input mode
                    Console.Write("Введите текст записи: ");
                    string text = Console.ReadLine();
                    Record rc = new Record(text, 1);
                    rc.Print();
                    WriteToBase(rc);
                    Console.WriteLine("Human input mode");
                    Console.ReadLine();
                    break;
                case 2:
                    // PC gen mode
                    Console.WriteLine("PC gen mode");
                    Console.ReadLine();
                    break;
                default:
                    break;
            }
        }

        private static void WriteToBase(Record rc)
        {
            string path = GetPath();
            using (StreamWriter sw = new StreamWriter(File.Open(path, FileMode.Append, FileAccess.Write), Encoding.Unicode))
            {
                sw.WriteLine(rc.ToSave());
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
    }
}
