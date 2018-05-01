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
            List<Item> Items;
            Process functions;
            string method;
            if (args.Length != 0)
            {
                Items = file.ReadFile(args[1]);
                method = file.ReadFile(args[0]).ToString().ToUpper();
            }
            else
            {
                Items = file.ReadFile("test1");
            }

            
            functions.TruthTableCheck(Items);
        }
    }
}
