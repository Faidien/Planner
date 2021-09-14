using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    struct Record
    {
        /// <summary>
        /// ИД записи - порядковые номера от 1. При удалении не сдвигаются номера.
        /// </summary>
        public uint ID { get; set; }

        /// <summary>
        /// Заглавие записи - первое слово записи для "короткого" вывода в консоль
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Текст записи, полностью, без сокращений.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Дата и время создания записи
        /// </summary>
        public DateTime DataCreate { get; set; }

        /// <summary>
        /// Приоритетность записи. Возможно в дневник будут записываться дела, и нужно будет их различать по важности
        /// </summary>
        public int Importance { get; set; }

        /// <summary>
        /// Конструктор для введения пользователем инфы
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="importance"></param>
        public Record(uint id, string text, int importance) : this(id, "", text, importance, DateTime.Now)
        {
            this.Title = (text.IndexOf(" ") == -1) ? text : text.Substring(0, text.IndexOf(" ")) + "...";
        }

        /// <summary>
        /// Стандарт конструктор
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <param name="importance"></param>
        /// <param name="dateCreate"></param>
        public Record(uint id, string title, string text, int importance, DateTime dateCreate)
        {
            this.ID = id;
            this.Title = title;
            this.Text = text;
            this.Importance = importance;
            this.DataCreate = dateCreate;
        }

        /// <summary>
        /// Вывод в консоль всей инфы
        /// </summary>
        public void Print()
        {
            Console.WriteLine("*************************************");
            Console.WriteLine($" 1. Номер записи: {ID}.\n 2. Заголовок записи: {Title}.\n 3. Текст записи: {Text}.\n" +
                $" 4. Дата создания записи: {DataCreate}.\n" +
                $" 5. Важность записи: {Importance}");
            Console.WriteLine();
        }

        /// <summary>
        /// Вывод в консоль инфы(для гл меню)
        /// </summary>
        /// <param name="mode"></param>
        public void Print(int mode)
        {
            Console.WriteLine("*************************************");
            Console.WriteLine($"  1. Номер записи: {ID}.\n  2. Заголовок записи: {Title}.\n" +
                $"  3. Дата и время создания записи: {DataCreate}.\n" +
                $"  5. Важность записи: {Importance}");
            Console.WriteLine();
        }

        /// <summary>
        /// СОхранение записей в определенном формате.
        /// </summary>
        /// <returns></returns>
        public string Save()
        {
            string s = $"{ID}~{Title}~{Text}~{Importance}~{DataCreate}";
            return s;
        }
    }
}
