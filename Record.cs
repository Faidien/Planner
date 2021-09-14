using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    struct Record
    {
        public uint ID { get; private set; }
        public string Title { get;  set; }
        public string Text { get; set; }
        public DateTime DataCreate { get; private set; }
        public int Importance { get; set; } // Приоритетность


        public Record(uint id, string text, int importance) : this(id, "", text, importance, DateTime.Now)
        {
            this.Title = (text.IndexOf(" ") == -1) ? text : text.Substring(0, text.IndexOf(" ")) + "...";
        }

        public Record(uint id, string title, string text, int importance, DateTime dateCreate)
        {
            this.ID = id;
            this.Title = title;
            this.Text = text;
            this.Importance = importance;
            this.DataCreate = dateCreate;
        }

        public void Print()
        {
            Console.WriteLine("Была создана запись: ");
            Console.WriteLine($" 1. Номер записи: {ID}.\n 2. Заголовок записи: {Title}.\n 3. Текст записи: {Text}.\n" +
                $" 4. Дата создания записи: {DataCreate.ToShortDateString()}.\n" +
                $" 5. Важность записи: {Importance} ");
        }

        public string Save()
        {
            string s = $"{ID},{Title},{Text},{Importance},{DataCreate.ToShortDateString()}";
            return s;
        }

        //public void Delete(Record rec, uint DeleteID)
        //{
        //    if (rec.ID == DeleteID)
        //    {
        //        rec = new Record();
        //    }
        //}
    }
}
