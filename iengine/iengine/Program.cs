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
            string method = "";
            if (args.Length != 0)
            {
                if (args.Length == 3)
                {
                    Items = file.ReadFile(args[2]);
                    method = file.ReadFile(args[1]).ToString().ToUpper();
                    
                }
                else
                {
                    Console.WriteLine("Invalid number of argumentst. Argument number was: " + args.Length);
                    return;
                }
            }
            else
            {
                Items = file.ReadFile("test1");
                method = "BC";
                //method = "FC";
                //method = "TT";
            }
            functions = new Process(method, Items);
            functions.TruthTableCheck(Items);

            //Write answer output to console based on chosen method
            Console.WriteLine(functions.RunMethod());

        }
    }
}
