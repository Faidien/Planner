using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Repository
    {
        public Record[] records;
        public Repository(params Record[] args)
        {
            records = args;
        }
    }
}
