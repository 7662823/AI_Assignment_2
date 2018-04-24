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
            file.ReadFile("test1");
        }
    }
}
