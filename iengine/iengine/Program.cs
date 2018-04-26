using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iengine
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadTextFile file = new ReadTextFile();
            List<Item> Rules = file.ReadFile("test1");
            Process functions = new Process();
            functions.TruthTableCheck(Rules);
        }
    }
}
