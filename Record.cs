using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    struct Record
    {
        public uint ID {  get; set; }
        public string Title { get; set; }
        public string Text {  get; set; }
        public DateTime DataCreate {  get; set; }
        public int Importance { get; set; }


        //public Record(string text, int importance)
        //{
        //    this.ID = 1;
        //    this.Text = text;
        //    this.Title = text.Substring(0, text.IndexOf(" ")) + "...";
        //    this.DataCreate = DateTime.Now;
        //    this.Importance = importance;
        //}
        public void Print()
        {
            Console.WriteLine("Была создана запись: ");
            Console.WriteLine($" 1. Номер записи: {ID}.\n 2. Заголовок записи: {Title}.\n 3. Текст записи: {Text}.\n" +
                $" 4. Дата создания записи: {DataCreate.ToShortDateString()}.\n" +
                $" 5. Важность записи: {Importance} ");
        }

        public string ToSave()
        {
            string s = $"{ID}, {Title}, {Text},{Importance},{DataCreate.ToShortDateString()}";
            return s;
        }
    }
}
