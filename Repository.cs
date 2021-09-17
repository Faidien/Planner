using System;
using System.IO;
using System.Text;
using ConsoleHelp = MyLibrary.Class1;

namespace ConsoleApp1
{
    class Repository
    {
        /// <summary>
        /// Репозиторий записей
        /// </summary>
        public Record[] records;

        /// <summary>
        /// Индекс записи в массиве, для определения размерности
        /// </summary>
        int index;

        /// <summary>
        /// Путь к файлу
        /// </summary>
        private string path;

        /// <summary>
        /// Порядковый номер записи, должен браться из файла, если таковой есть.
        /// Если файла нет - запись будет первой. 
        /// </summary>
        public int id;

        int mode;

        /// <summary>
        /// Флаг найдена ли запись или нет
        /// </summary>
        public bool isFind;

        /// <summary>
        /// Получить макс ИД записи
        /// </summary>
        /// <param name="arr"></param>
        public void GetID(Record[] arr)
        {
            int maxID = 0;
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

            using FileStream fs = File.Create(path);
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
                using (StreamReader sr = new(path))
                {
                    while (!sr.EndOfStream)
                    {
                        args = sr.ReadLine().Split('~');
                        if (args != null)
                            Add(new Record(Convert.ToInt32(args[0]), args[1], args[2], Convert.ToInt32(args[3]), Convert.ToDateTime(args[4])));
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

            Random r = new();
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
        /// Создание записи -1, генерация записи - 2, загрузка записи в массив
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
                    Footer("создана!", true);
                    break;
                case 2:
                    // PC gen mode
                    Random r = new();
                    string[] sentense = { "Достал.", "Работа - это такое место, где с утра хочется есть, " +
                            "после обеда - спать, и все время такое чувство, что пора домой.","Скажи мне, кто твой враг," +
                            "и я скажу тебе, где достать патроны дешевле", "Мне нравится работать программистом по своему" +
                            "графику. Захотел - пришел на работу к 7 часам утра. Захотел - ушел в 12 ночи." +
                            "А захотел - вообще домой не пошел! ", "Не волнуйтесь, если что-то не работает. " +
                            "Если бы всё работало, вас бы уволили.", "В теории, теория и практика неразделимы." +
                            "На практике это не так.", "Иногда лучше остаться спать дома в понедельник, " +
                            "чем провести всю неделю в отладке написанного в понедельник кода.","Многие из вас " +
                            "знакомы с достоинствами программиста. Их всего три, и разумеется это: лень, " +
                            "нетерпеливость и гордыня.","Если вы дадите человеку программу, то займете его на один день. " +
                            "Если вы научите человека программировать, то займете его на всю жизнь.",
                            "Отладка кода — это как охота. Охота на баги.","Работает? Не трогай.",
                            "Насколько проще было бы писать программы, если бы не заказчики.",
                            "Молодые специалисты не умеют работать, а опытные специалисты умеют не работать.",
                            "Чтобы написать чистый код, мы сначала пишем грязный код, а затем рефакторим его.",
                            "Тестирование не позволяет обнаружить такие ошибки, как создание не того приложения.",
                            "Сначала учите науку программирования и всю теорию. " +
                            "Далее выработайте свой программистский стиль. Затем забудьте всё и просто программируйте.",
                            "Люди думают, что безопасность — это существительное, что-то, что можно купить." +
                            "На самом же деле безопасность — это абстрактное понятие, как счастье.",
                            "Чтобы понять алгоритм, нужно его увидеть.","Программы становятся медленнее быстрее, " +
                            "чем «железо» становится быстрее.","Магия перестаёт существовать после того, " +
                            "как вы понимаете, как она работает.","640 Кб должно хватить для любых задач.",
                            "Основная проблема программистов состоит в том, что их ошибки невозможно предугадать."};
                    string textC = sentense[r.Next(sentense.Length)];
                    Add(new Record(id++, textC, CheckPriority(8)));
                    Console.WriteLine($"Текст сгенерированной записи: {textC}");
                    Footer("сгенерирована!", true);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Сортировка данных - главный метод выбора сортировки
        /// </summary>
        internal void SortRec()
        {
            Console.WriteLine();
            Console.WriteLine("Введите параметр сортировки:\n 1. По номеру записи" +
                "\n 2. По заголовку записи\n 3. По тексту записи\n 4. По приоритетности записи" +
                "\n 5. По дате и времени записи");
            do
            {
                int.TryParse(Console.ReadLine(), out mode);

            } while (mode < 1 || mode > 5);

            QuickSort(records, mode);

        }
        //метод для обмена элементов массива
        static void Swap<T>(ref T x, ref T y)
        {
            var t = x;
            x = y;
            y = t;
        }

        //метод возвращающий индекс опорного элемента
        static int Partition(Record[] array, int minIndex, int maxIndex, int mode)
        {
            bool result;
            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
            {
                if (array[i].ID == 0)
                {
                    continue;
                }
                else
                {
                    switch (mode)
                    {
                        case 1:
                            result = array[i].ID < array[maxIndex].ID;
                            break;
                        case 2:
                            result = array[i].Title.CompareTo(array[maxIndex].Title) == -1;
                            break;
                        case 3:
                            result = array[i].Text.CompareTo(array[maxIndex].Text) == -1;
                            break;
                        case 4:
                            result = array[i].Importance < array[maxIndex].Importance;
                            break;
                        case 5:
                            result = array[i].DataCreate < array[maxIndex].DataCreate;
                            break;
                        default:
                            result = false;
                            break;
                    }
                    if (result)
                    {
                        pivot++;
                        Swap(ref array[pivot], ref array[i]);
                    }
                    //break;
                }

            }

            pivot++;
            Swap(ref array[pivot], ref array[maxIndex]);
            return pivot;
        }

        //быстрая сортировка
        static Record[] QuickSort(Record[] array, int minIndex, int maxIndex, int mode)
        {
            if (minIndex >= maxIndex)
            {
                return array;
            }
            var pivotIndex = Partition(array, minIndex, maxIndex, mode);
            QuickSort(array, minIndex, pivotIndex - 1, mode);
            QuickSort(array, pivotIndex + 1, maxIndex, mode);

            return array;
        }

        static Record[] QuickSort(Record[] array, int mode)
        {
            return QuickSort(array, 0, array.Length - 1, mode);
        }


        /// <summary>
        /// Выборка записей. Есть два режима: 1 - по дате, 2 - по ид, 
        /// и два формата вывода:  full - полный, short = короткий
        /// </summary>
        internal void ChooseRecs(int mode, string format)
        {
            DateTime startDate, endDate;
            switch (mode)
            {
                case 1:
                    Console.Write("Введите начальную дату: ");
                    DateTime.TryParse(Console.ReadLine(), out startDate);
                    Console.Write("Введите конечную дату: ");
                    DateTime.TryParse(Console.ReadLine(), out endDate);

                    Console.WriteLine("\n\n============================================");
                    Console.WriteLine("Записи за выбранные даты:");
                    foreach (var item in records)
                    {
                        if (item.ID != 0)
                        {
                            if (endDate == default || startDate == default)
                            {
                                if (format == "short")
                                    item.Print(1);
                                else
                                    item.Print();
                                isFind = true;
                            }
                            else if (item.DataCreate <= endDate && item.DataCreate >= startDate)
                            {
                                if (format == "short")
                                    item.Print(1);
                                else
                                    item.Print();
                                isFind = true;
                            }
                            else
                            {
                                isFind = false;
                            }
                        }
                    }
                    Console.WriteLine("\n\n============================================");
                    Footer("выбрана(-ы)!", isFind);
                    break;

                case 2:
                    Console.WriteLine("Вывод записей по номеру.");
                    Console.Write("Введите номер записи: ");
                    int.TryParse(Console.ReadLine(), out int ID);

                    Console.WriteLine("\n\n============================================");
                    Console.WriteLine($"Запись по номеру {ID}:");
                    foreach (var item in records)
                    {
                        if (item.ID != 0 && item.ID == ID)
                        {
                            if (format == "short")
                                item.Print(1);
                            else
                                item.Print();
                            isFind = true;
                            break;
                        }
                        else
                        {
                            isFind = false;
                        }
                    }

                    Console.WriteLine("\n\n============================================");
                    Footer("выбрана(-ы)!", isFind);
                    break;
                case 3:
                    Console.WriteLine("Вывод всех записей");


                    Console.WriteLine("\n\n============================================");
                    Console.WriteLine($"Записи:");
                    foreach (var item in records)
                    {
                        if (item.ID != 0)
                        {
                            if (format == "short")
                                item.Print(1);
                            else
                                item.Print();
                        }
                    }

                    Console.WriteLine("\n\n============================================");
                    Footer("выбрана(-ы)!", true);
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Проверка на валидность введеного приоритета. Если не валиден - то по умолчанию приоритет будет 1
        /// </summary>
        /// <param name="priority"></param>
        /// <returns></returns>
        private int CheckPriority(int priority)
        {
            if (priority < 0 || priority > 8)
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
            int.TryParse(Console.ReadLine(), out int recForDel);
            for (int i = 0; i < records.Length; i++)
            {
                if (records[i].ID != 0)
                {
                    if (records[i].ID == recForDel)
                    {
                        records[i] = new Record();
                        isFind = true;
                        break;
                    }
                    else
                    {
                        isFind = false;
                    }
                }

            }
            Footer("удалена!", isFind);
        }

        /// <summary>
        /// Редактирование записи по ИД записи
        /// </summary>
        public void EditRec()
        {
            Console.Write("Введите номер записи для редактирования: ");
            int.TryParse(Console.ReadLine(), out int recForEdit);
            for (int i = 0; i < records.Length; i++)
            {
                if (records[i].ID != 0)
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
                        isFind = true;
                        break;
                    }
                    else
                    {
                        isFind = false;
                    }
                }

            }
            Footer("отредактирована!", isFind);

        }

        /// <summary>
        /// Вывод в конце операций
        /// </summary>
        /// <param name="text"></param>
        private void Footer(string text, bool isFind)
        {
            if (isFind)
            {
                Console.WriteLine($"\nЗапись {text}.\n" +
                $"Для продолжения нажмите Enter");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Запись не найдена!\n" +
                $"Для продолжения нажмите Enter");
                Console.ReadLine();
            }

        }
    }
}
