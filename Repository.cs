using System;
using System.IO;
using System.Text;
using ConsoleHelp = MyLibrary.Class1;

namespace ConsoleApp1
{
    class Repository
    {
        public Record[] records;
        int index;
        private string path;
        public uint id;// Порядковый номер записи. По идее должен инрементироваться. Последний номер должен браться из файла, если таковой есть
                       // Если файла нет - запись будет первой. 
        public void GetID(Record[] arr)
        {
            uint maxID = 0;
            foreach (Record item in arr)
            {
                maxID = item.ID > maxID ? item.ID : maxID;
            }
            id = maxID + 1;
        }

        private void WriteToBase(Record rc)
        {
            using (StreamWriter sw = new StreamWriter(File.Open(path, FileMode.Append, FileAccess.Write), Encoding.Unicode))
            {
                sw.WriteLine(rc.ToSave());
            }
        }

        public void CreateBase(string path)
        {

            using (FileStream fs = File.Create(path))
            {
            }
        }
        public void Add(Record rec)
        {
            this.Resize(index >= this.records.Length);
            this.records[index] = rec;
            this.index++;
        }
        private void Resize(bool Flag)
        {
            if (Flag)
            {
                Array.Resize(ref this.records, this.records.Length * 2);
            }
        }

        public void OpenBase(string path)
        {
            string[] args;
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------");
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {

                    while (!sr.EndOfStream)
                    {
                        args = sr.ReadLine().Split(',');
                        Add(new Record(Convert.ToUInt32(args[0]), args[1], args[2], Convert.ToInt32(args[3]), Convert.ToDateTime(args[4])));
                    }
                }
            }

            catch
            {
                Console.WriteLine("Файл не может быть прочитан!");
                Console.Read();
                Program.Exit();
            }
            GetID(records);
        }

        public void Init()
        {

            Random r = new Random();
            Console.Write("Ваш дневник приветсвует Вас!\nСейчас проверю наличие базы данных записей");
            ConsoleHelp.Turn(r.Next(5, 15));
            Console.WriteLine();

            if (File.Exists(path))
            {
                Console.Write("Файл найден, выполняю загрузку...");
                ConsoleHelp.Turn(r.Next(8, 16));
                OpenBase(path);
                Console.WriteLine();
            }

            else
            {
                Console.Write("Файл не найден, выполнить создание дневника?(1 - да, остальные клавиши - нет): ");
                string answer = Console.ReadLine();
                if (answer == "1")
                {
                    CreateBase(path);
                }
                else
                {
                    Program.Exit();
                }
            }
        }

        public void CreateRec(int mode)
        {
            switch (mode)
            {
                case 1:
                    // Human input mode
                    Console.Write("Введите текст записи: ");
                    string text = Console.ReadLine();
                    Console.Write("Введите приоритет записи: ");
                    int.TryParse(Console.ReadLine(), out int priority);
                    Add(new Record(id++, text, priority));
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
        public Repository(string path)
        {
            this.id = 0;
            this.path = path;
            this.index = 0;
            this.records = new Record[2];
            Init();
        }

        public void Save()
        {
            using (StreamWriter sw = new StreamWriter(File.Open(path, FileMode.Create, FileAccess.Write), Encoding.Unicode))
            {
                foreach (var item in records)
                {
                    if (item.ID != 0)
                    {
                        sw.WriteLine(item.ToSave());
                    }
                }
            }
        }
    }
}
