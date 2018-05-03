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

        public Item FindQuery()
        {
            return items.FirstOrDefault(ItemToCheck => ItemToCheck.query);
        }

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
                case "FC":
                    output = ForwardChaining();
                    break;
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

            Path.Add(FindQuery());
                       



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

        public string ForwardChaining()
        {
            string result = "";
            List<Item> agenda = new List<Item>();

            bool itemfound = false;
            //adds the initial valid items to the search list first
            foreach (Item i in items)
            {
                if (i.valid)
                    agenda.Add(i);
            }

            while(agenda.Count != 0)
            {
                Item currentItem = new Item();
                bool novaliditem = true;
                //checks the initially valid items before checking the nonvalid items in the list
                for  (int i = agenda.Count - 1; i >=0; i--)
                {
                    if (agenda[i].valid == true)
                    {
                        currentItem = agenda[i];
                        agenda.Remove(agenda[i]);
                        novaliditem = false;
                        break;
                    }
                }
                if (novaliditem)
                {
                    currentItem = agenda.Last();
                    agenda.Remove(agenda.Last());
                }
                    //checks each relation in the current item selected
                    foreach (Relation r in currentItem.relations)
                    {
                        foreach (string s in r.name)
                        {
                            if (r.clause == "=>")
                            {
                            //if the item implies something and it is valid then make the item it is implying valid and add the item to the list
                                if (currentItem.valid == true) { 
                                var match = items.FirstOrDefault(stringToCheck => stringToCheck.Contains(s));
                                match.valid = true;
                                agenda.Add(match);
                                if (currentItem.Checked != true)
                                {
                                    currentItem.Checked = true;
                                    result += currentItem.name + ", ";
                                }
                                }
                            }
                            else if (r.clause == "-")
                            {
                            //if the item is related to another item, add the other item to the list to be checked later
                                    var match = items.FirstOrDefault(stringToCheck => stringToCheck.Contains(s));
                                    agenda.Add(match);
                                    if (currentItem.Checked != true)
                                    {
                                        result += currentItem.name + ", ";
                                        currentItem.Checked = true;
                                    }
                            }
                            else if (r.clause == "!=>")
                            {
                                //if the item is implied by another item where all instances are true, make the current item true
                                if (currentItem.valid == false)
                                {
                                    var match = items.FirstOrDefault(stringToCheck => stringToCheck.Contains(s));
                                    if (match.valid == false)
                                    {
                                        currentItem.valid = false;
                                        break;
                                    }
                                    else
                                        currentItem.valid = true;
                                }
                                
                                
                                if (currentItem.Checked != true)
                                {
                                    result += currentItem.name + ", ";
                                    currentItem.Checked = true;
                                }
                            
                            }

                        }
                    }
                
                if (currentItem.relations.Count == 0)
                {
                    result += currentItem.name + ", ";
                }
                if (currentItem.query == true)
                {
                    itemfound = true;
                    if(currentItem.Checked)
                        result = result.Remove(result.Count() - 2);
                    else
                        result += currentItem.name;
                    if (currentItem.valid == true)
                        result = "YES: " + result;
                    else
                        result = "NO: " + result;

                    break;
                }


            }
            if (!itemfound)
                result = "NO: " + result.Remove(result.Count() - 2);

            return result;
        }
    }
}
