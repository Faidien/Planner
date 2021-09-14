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
        /// <summary>
        /// Получить макс ИД записи
        /// </summary>
        /// <param name="arr"></param>
        public void GetID(Record[] arr)
        {
            uint maxID = 0;
            foreach (Record item in arr)
            {
                maxID = item.ID > maxID ? item.ID : maxID;
            }
            id = maxID + 1;
        }

        /// <summary>
        /// Создание БД для записей в случае если файла нет
        /// </summary>
        /// <param name="path"></param>
        public void CreateBase(string path)
        {

            using (FileStream fs = File.Create(path))
            {
            }
        }

        /// <summary>
        /// Добавление записи к массиву
        /// </summary>
        /// <param name="rec"></param>
        public void Add(Record rec)
        {
            this.Resize(index >= this.records.Length);
            this.records[index] = rec;
            this.index++;
        }

        /// <summary>
        /// Изменение размера массива, если след запись не лезет в текущую размерность
        /// </summary>
        /// <param name="Flag"></param>
        private void Resize(bool Flag)
        {
            if (Flag)
            {
                Array.Resize(ref this.records, this.records.Length * 2);
            }
        }

        /// <summary>
        /// Открыть БД, прочитать все строки, добавитьь в репозиторий.
        /// </summary>
        /// <param name="path"></param>
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
                        args = sr.ReadLine().Split('~');
                        if (args != null)
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

        /// <summary>
        /// Инициализация репозитория, проверка существования файла, управление методами создать и открыть БД
        /// </summary>
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
                Console.Write("База данных успешно загружена. \nНажмите Enter для входа в программу.");
                Console.ReadLine();
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

        /// <summary>
        /// Создание записи, загрузка записи в массив
        /// </summary>
        /// <param name="mode"></param>
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
                    Add(new Record(id++, text, CheckPriority(priority)));
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

        /// <summary>
        /// Проверка на валидность введного приоритета. Если не валиден - то по умолчанию приоритет будет 1
        /// </summary>
        /// <param name="priority"></param>
        /// <returns></returns>
        private int CheckPriority(int priority)
        {
            if (priority < 0 && priority > 8)
            {
                return 1;
            }
            else
            {
                return priority;
            }
        }

        /// <summary>
        /// Стандартный конструктор, инициализация переменных и репозитория
        /// </summary>
        /// <param name="path"></param>
        public Repository(string path)
        {
            this.id = 0;
            this.path = path;
            this.index = 0;
            this.records = new Record[2];
            Init();
        }

        /// <summary>
        /// Запись всех записей на диск, в конце работы программы
        /// </summary>
        public void SaveAllRec()
        {
            using (StreamWriter sw = new StreamWriter(File.Open(path, FileMode.Create, FileAccess.Write), Encoding.Unicode))
            {
                foreach (var item in records)
                {
                    if (item.ID != 0)
                    {
                        sw.WriteLine(item.Save());
                    }
                }
            }
        }

        /// <summary>
        /// Удаление записи по ИД записи
        /// </summary>
        public void DeleteRec()
        {
            Console.Write("Введите номер записи для удаления: ");
            uint.TryParse(Console.ReadLine(), out uint recForDel);
            for (int i = 0; i < records.Length; i++)
            {
                if (records[i].ID == recForDel)
                {
                    records[i] = new Record();
                }
            }
        }

        /// <summary>
        /// Редактирование записи по ИД записи
        /// </summary>
        public void EditRec()
        {
            Console.Write("Введите номер записи для редактирования: ");
            uint.TryParse(Console.ReadLine(), out uint recForEdit);
            for (int i = 0; i < records.Length; i++)
            {
                if (records[i].ID == recForEdit)
                {
                    string newText = "";
                    Console.WriteLine($"Старый текст записи: {records[i].Text}");
                    Console.WriteLine($"Старый приоритет записи: {records[i].Importance}");
                    Console.Write("Введите новый текст для записи:");
                    newText = Console.ReadLine();
                    Console.Write("Введите новый приоритет для записи:");
                    int.TryParse(Console.ReadLine(), out int newPriority);
                    records[i].Text = newText;
                    records[i].Importance = CheckPriority(newPriority);
                    records[i].Title = (records[i].Text.IndexOf(" ") == -1) ?
                        records[i].Text : records[i].Text.Substring(0, records[i].Text.IndexOf(" ")) + "...";
                }
            }
        }
    }
}
