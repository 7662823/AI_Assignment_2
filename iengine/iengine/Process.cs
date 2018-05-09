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
        private List<List<string>> rules;

        public Item FindQuery()
        {
            return items.FirstOrDefault(ItemToCheck => ItemToCheck.query);
        }

        public Process(string method, List<Item> _items, List<List<string>> Rules)
        {
            items = _items;
            currentMethod = method;
            rules = Rules;
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
            bool reachedEnd = false;
            List<Item> SearchPath = new List<Item>();
            List<Item> TruePath = new List<Item>();

            //adds target/query to path.
            foreach (Item i in items)
            {
                if (i.query == true)
                {
                    SearchPath.Add(i);
                }
            }

            while(isTrue == false && reachedEnd == false)//fix
            {

                //first check unchecked items in path
                foreach( Item i in SearchPath.ToList())
                {
                    if(i.Checked == true)
                    {
                        //do nothing?
                    }
                    else
                    {
                        foreach(Relation r in i.relations)
                        {
                            foreach(string s in r.name)
                            {
                                var match = items.FirstOrDefault(stringToCheck => stringToCheck.Contains(s));

                                
                                if (r.clause == "!=>") // if the relation is "being implied by" which is all we care about
                                {
                                    if (!SearchPath.Contains(match)) //if path does not contain match
                                    {
                                        SearchPath.Add(match); //add match
                                    }
                                    if (match.relations.Exists(x => x.clause == "-" && x.name.Contains(i.name))) //if the thing implying currently searched item is of type "-" and relates back to i
                                    {
                                        if (match.valid == true)
                                        {
                                            SearchPath.Last().Checked = true;
                                        }
                                    }
                                    /*else if(match.valid == true)
                                    {
                                        SearchPath.Last().Checked = true;
                                        if (!SearchPath.Exists(x => x.Checked == false))
                                        {
                                            isTrue = true;
                                        }
                                    }*/
                                }
                              }
                            if(isTrue)
                            { break; }
                        }

                        i.Checked = true;
                        
                    }
                    if (isTrue)
                    { break; }
                }
               
               if(!SearchPath.Exists(x => x.Checked == false)) //end of loop, check that there are still unchecked items in path.
                {
                    reachedEnd = true;
                }
                bool currentItemTrue = true ;
                if (reachedEnd == true)
                {
                    isTrue = true;
                    foreach (Item i in SearchPath)
                    {
                    
                        if(i.valid == true || i.relations.Exists(x => x.clause == "!=>"))
                        {
                            currentItemTrue = true;
                        }
                        else
                        {
                            currentItemTrue = false;
                        }
                        if(!currentItemTrue)
                        {
                            isTrue = false;
                        }
                    }
                    
                }
               //check to make sure not all options in the searchpath are true, otherwise isTrue = true
            }
                       
            if(isTrue == true)
            {
                foreach(Item i in SearchPath)
                {
                    i.Checked = false; //unchecks all in search path so check can be reused
                }

                answer = "YES";
                Item currentItem = new Item();
                Item nextItem = new Item();
                TruePath.Add(SearchPath.Last(x => x.valid));//adds inital valid item/true item (with the way the code is set there should only start out being one
                currentItem = TruePath[0];

                while (currentItem.query == false)
                {
                    foreach(Relation r in currentItem.relations)
                    {
                        foreach(string name in r.name)
                        {
                            var match = items.FirstOrDefault(stringToCheck => stringToCheck.Contains(name));
                            if (SearchPath.Contains(match))
                            {
                                if (r.clause == "=>")
                                {
                                    //if the item implies something and it is valid then make the item it is implying valid and add the item to the list

                                    match.valid = true;
                                    if (SearchPath.Contains(match) && !TruePath.Contains(match))
                                    {
                                        TruePath.Add(match);
                                    }
                                }
                                else if (r.clause == "-") //it implies truth with another 
                                {
                                    List<Item> others = new List<Item>();
                                    //find other item in searchpath that contains clause "-" and r.name = match.name
                                    //check other to see if true
                                    //if true add that other to truthpath, then add match

                                    foreach (Relation matchR in match.relations)
                                    {
                                        foreach (string matchRName in matchR.name)
                                        {
                                            if (matchR.clause == "!=>" && matchRName != currentItem.name)
                                            {
                                                others.Add(SearchPath.FirstOrDefault(stringToCheck => stringToCheck.Contains(matchRName)));
                                            }
                                        }
                                    }
                                    bool matchisTrue = true;
                                    bool thisMatchIsTrue = false;
                                    foreach (Item other in others)
                                    {
                                        if (other.valid == true)
                                        {
                                            if (!TruePath.Contains(other))
                                            {
                                                TruePath.Add(other);
                                            }
                                            thisMatchIsTrue = true;

                                        }
                                        else
                                        {
                                            thisMatchIsTrue = false;
                                        }
                                        if (!thisMatchIsTrue)
                                        {
                                            matchisTrue = false;
                                        }
                                    }
                                    if (matchisTrue)
                                    {
                                        if (!TruePath.Contains(match))
                                        {
                                            match.valid = true;
                                            TruePath.Add(match);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    currentItem.Checked = true;
                    currentItem = TruePath.Last();
                    if(currentItem.Checked == true)
                    {
                        foreach (Item i in TruePath)
                        {
                            if (i.Checked != true)
                            {
                                currentItem = i;
                                break;
                            }
                        }
                    }            
                }
            }
            else
            {
                answer = "NO";
                return answer;
            }
            result = answer + ": ";

            foreach(Item i in TruePath)
            {
                if (i != TruePath.Last<Item>())
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
            //generate a truth table the size of number of items
            List<List<bool>> itemTable = GenerateTruthTable(items.Count);
            List<List<bool>> Ruletable = new List<List<bool>>();
            string result = "";
            int validCount = 0;
            bool itemValid = true;
            bool queryReault = true;
            foreach (List<bool> conditions in itemTable)
            {
                foreach (List<string> r in rules)
                {
                    string target = r.Last();
                    List<String> itemNames = new List<string>();
                    foreach (string s in r)
                    {
                        if (s != "&" && s != "=>")
                            itemNames.Add(s);
                        //else if (s == "=>")
                        //    break;
                    }
                    foreach(string s in itemNames)
                    {
                        if (s == target)
                        {
                            if (!conditions[items.IndexOf(items.FirstOrDefault(stringToCheck => stringToCheck.Contains(target)))])
                            {
                                itemValid = false;
                                break;
                            }
                        }
                        else
                        {
                            if (!conditions[items.IndexOf(items.FirstOrDefault(stringToCheck => stringToCheck.Contains(s)))])
                            {
                                break;
                            }
                        }
                    }

                    //if (r.Count == 1)
                    //{
                    //    //for C, then C must be true, otherwise break the function and mark the item as valid being false.
                    //    if (!conditions[items.IndexOf(items.FirstOrDefault(stringToCheck => stringToCheck.Contains(r[0])))])
                    //    {
                    //            itemValid = false;
                    //            break;
                    //    }
                    //}
                    //else if (r.Count == 3)
                    //{
                    //    //for A=>C, if B = true, then C must be true, otherwise break the function and mark the item as valid being false.
                    //    if (conditions[items.IndexOf(items.FirstOrDefault(stringToCheck => stringToCheck.Contains(r[0])))])
                    //    {
                    //        if (!conditions[items.IndexOf(items.FirstOrDefault(stringToCheck => stringToCheck.Contains(r[2])))])
                    //        {
                    //            itemValid = false;
                    //            break;
                    //        }
                    //    }
                    //}
                    //else if (r.Count == 5)
                    //{
                    //    //for A&B=>C, if A & B = true, then C must be true, otherwise break the function and mark the item as valid being false.
                    //    if (conditions[items.IndexOf(items.FirstOrDefault(stringToCheck => stringToCheck.Contains(r[0])))] &&
                    //        conditions[items.IndexOf(items.FirstOrDefault(stringToCheck => stringToCheck.Contains(r[2])))])
                    //    {
                    //        if (!conditions[items.IndexOf(items.FirstOrDefault(stringToCheck => stringToCheck.Contains(r[4])))])
                    //        {
                    //            itemValid = false;
                    //            break;
                    //        }
                    //    }
                    //}
                }
                if (itemValid)
                {
                    validCount++;
                    if (!conditions[items.IndexOf(items.FirstOrDefault(q => q.query))])
                    {
                        queryReault = false;
                        break;
                    }
                }
                else
                    itemValid = true;
            }
            //if query result is false/no, there is no need to produce the numeber of valid nodes.
            if (validCount == 0)
                result = "NO";
            else
            {
                if (queryReault)
                    result = "YES: " + validCount.ToString();
                else
                    result = "NO ";

            }
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
                                }
                            }
                            else if (r.clause == "-")
                            {
                            //if the item is related to another item, add the other item to the list to be checked later
                                    agenda.Add(items.FirstOrDefault(stringToCheck => stringToCheck.Contains(s)));
                            }
                            else if (r.clause == "!=>")
                            {
                            //if the item is implied by another item where all instances are true, make the current item true
                            if (currentItem.Checked == false)
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
                            }

                        }
                    }
                //adds the current item name to the output display
                if (currentItem.Checked != true)
                {
                    currentItem.Checked = true;
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
