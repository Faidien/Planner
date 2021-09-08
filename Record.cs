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
        public string Description {  get; set; }
        public DateTime Created {  get; set; }
        public int Importance { get; set; }
        public string Type {  get; set; }
    }
}
