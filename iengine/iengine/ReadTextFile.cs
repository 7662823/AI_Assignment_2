using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace iengine
{
    class ReadTextFile
    {
      public List<Item> Rules;

        public ReadTextFile()
        {
            Rules = new List<Item>();
        }

        public List<Item> addRule(string[] rule, string clause, string target)
        {
            //check if the list of items (the rules) comtains the item already
            var matchingItem = Rules.FirstOrDefault(stringToCheck => stringToCheck.Contains(target));
            Relation newRelation = new Relation();

            //if the target item has been found in the current rule list, then add another relation rule to it
            //otherwise create a new item in the rule list
            if (matchingItem != null)
            {
                //add the rule relation to the name array for however many relations there are
                foreach (string s in rule)
                {
                    newRelation.name.Add(s);
                }
        
                newRelation.clause = clause;
                matchingItem.relations.Add(newRelation);
            }
            else
            {
                Rules.Add(new Item());
                Rules.Last().name = target;
                foreach (string s in rule)
                {
                    newRelation.name.Add(s);
                }
             

                newRelation.clause = clause;
                Rules.Last().relations.Add(newRelation);
            }
                return Rules;
        }
        public List<Item> ReadFile(string fileName)
        {
            //List<Item> result = new List<Item>();
            string[] lines = System.IO.File.ReadAllLines(Directory.GetCurrentDirectory() + "/" + fileName + ".txt");
            bool tell = false;
            bool ask = false;
            string[] rules;
            string[] identifiers = { "=>", "&"};
            foreach(string s in lines)
            {
                if (s == "TELL")
                {
                    tell = true;
                    ask = false;

                }
                else if (s == "ASK")
                {
                    ask = true;
                    tell = false;

                }
                else
                {
                    if (tell == true)
                    {
                        rules = s.Split(';');
                        foreach (string r in rules)
                        {
                            string[] SplitRule;
                            string identifier = string.Join("|", identifiers.ToArray());
                            //splits the string with the corresponding identifiers and removes any spaces from the split string
                            SplitRule = Regex.Split(r.Replace(" ", string.Empty), @"(" + identifier +@")");
                            if(SplitRule[0] == "")
                            {
                                //string[] a;
                                SplitRule = new string[0];
                            }
                            switch (SplitRule.Length)
                            {
                                //assign item to be true
                                case 1:

                                    var match = Rules.FirstOrDefault(stringToCheck => stringToCheck.Contains(SplitRule[0]));
                                    if (match != null)
                                    {
                                        match.valid = true;
                                    }
                                    else {
                                        Rules.Add(new Item());
                                        Rules.Last().name = SplitRule[0];
                                        Rules.Last().valid = true;
                                    }
                                    break;
                                case 3:
                                    //create the relation in the items being "SplitRule[0] SplitRule[1] SplitRule[2]" eg, "A => B"
                                    addRule(new string[] { SplitRule[2] }, SplitRule[1], SplitRule[0]);
                                    // '!' indicates it is the reverse order
                                    addRule(new string[] { SplitRule[0] }, "!"+SplitRule[1], SplitRule[2]);

                                    break;
                                case 5:

                                    //
                                    addRule(new string[] { SplitRule[4] }, SplitRule[3], SplitRule[0]);
                                    addRule(new string[] { SplitRule[4] }, SplitRule[3], SplitRule[2]);
                                    addRule(new string[] { SplitRule[0], SplitRule[2] }, "!"+SplitRule[3], SplitRule[4]);

                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (ask == true)
                    {
                        var item = Rules.FirstOrDefault(ItemToCheck => ItemToCheck.Contains(s));
                        if(item != null)
                        {
                            item.ASK = true;
                        }
                    }

                }
            }
            //Rules = result;
            return Rules;
        }
    }
}
