using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iengine
{
    class Process
    {
        private string currentMethod;
        private List<Item> items;

        public Process(string method, List<Item> _items)
        {
            items = _items;
            currentMethod = method;
        }

        public string RunMethod()
        {
            string output;
            switch (currentMethod)
            {
                case "BC":
                    output = BackwardChaining();
                    break;
                //case "FC":
                //   output = ForwardChaining();
                //    break;
                case "TT":
                   output = TruthTable();
                    break;
                default:
                    output = "Can't Identify search method (argument 2)";
                    break;
            }
            return output;
        }

        public string BackwardChaining()
        {
            string result = "";
            string answer;
            bool isTrue = false;
            List<Item> Path = new List<Item>();
           
            




            if(isTrue == true)
            {
                answer = "YES";
            }
            else
            {
                answer = "NO";
            }
            result = answer + ": ";

            foreach(Item i in Path)
            {
                if (i != Path.Last<Item>())
                {
                    result += i.name + ", ";
                }
                else
                {
                    result += i.name;
                }
            }
            return result;
        }

        public string TruthTable()
        {
            string result = "";
            return result;
        } 



        private List<List<bool>> GenerateTruthTable(int tableSize)
        {
            List<bool> boolTable = new List<bool>();
            List<List<bool>> truthTable = new List<List<bool>>();
            truthTable.Add(boolTable);

            for (int i = 0; i <tableSize; i++)
            {
                boolTable.Add(false);
            }
            for (int i = 0; !truthTable[i].All(c => c == true); i++)
            {
                //boolTable[boolTable.Capacity - 1] = !boolTable.Last();
                boolTable = new List<bool>(boolTable);
                for (int j = boolTable.Count - 1; j >= 0; j--)
                {
                    if (boolTable[j] == true)
                    {
                        boolTable[j] = false;
                    }
                    else
                    {
                        boolTable[j] = true;
                        break;
                    }
                }
                truthTable.Add(boolTable);
            }
            return truthTable;
        }


        public void TruthTableCheck(List<Item> rules)
        {
            //generate a truth table the size of number of items
            List<List<bool>> table = GenerateTruthTable(rules.Count);


        }

        public string FowardChaining(List<Item> items)
        {
            string result = "";


            foreach (Item i in items)
            {
                if(i.valid == true )
                {
                    foreach(Relation r in i.relations)
                    {
                        foreach (string s in r.name) {
                            if (r.clause == "=>")
                            {
                                var match = items.FirstOrDefault(stringToCheck => stringToCheck.Contains(s));
                                match.valid = true;
                            }
                            else if (r.clause == "-"){

                            }
                            else if (r.clause == "!=>")
                            {

                            }
                            
                        }
                    }


                }
            }
            return result;
        }
    }
}
