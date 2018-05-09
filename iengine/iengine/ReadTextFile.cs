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
      public List<Item> Items;

        public List<List<string>> Rules;

        public ReadTextFile()
        {
            Items = new List<Item>();
            Rules = new List<List<string>>();
        }

        public List<Item> addRule(string[] rule, string clause, string target)
        {
            //check if the list of items (the rules) comtains the item already
            var matchingItem = Items.FirstOrDefault(stringToCheck => stringToCheck.Contains(target));
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
                Items.Add(new Item());
                Items.Last().name = target;
                foreach (string s in rule)
                {
                    newRelation.name.Add(s);
                }
             

                newRelation.clause = clause;
                Items.Last().relations.Add(newRelation);
            }
                return Items;
        }


        public List<Item> ReadFile(string fileName)
        {
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
                            else
                            {
                                Rules.Add(new List<string>(SplitRule));
                            }

                            // if the rule only contains one item, itll mean that item is just saying it is true, so sets item to true.
                            if(SplitRule.Length == 1)
                            {
                                var match = Items.FirstOrDefault(stringToCheck => stringToCheck.Contains(SplitRule[0]));
                                if (match != null)
                                {
                                    match.valid = true;
                                }
                                else
                                {
                                    Items.Add(new Item());
                                    Items.Last().name = SplitRule[0];
                                    Items.Last().valid = true;
                                }
                            }
                            else if(SplitRule.Length > 1)//any other size than 1:
                            {
                                
                                List<string> rule = new List<string>();
                                string clause = "=>";
                                int clausePosition;
                                string target;
                                int numberofANDS = SplitRule.Count(x => x == "&");
                                
                                
                                clausePosition = Array.IndexOf(SplitRule, clause);
                                target = SplitRule[clausePosition + 1];
                                
                                for(int i = 0; i < clausePosition; i++)
                                {
                                    if (SplitRule[i] != "&")
                                    {
                                        rule.Add(SplitRule[i]);
                                    }
                                }

                                if(rule.Count == 1)
                                {
                                    //create the relation in the items being "rule, clause, target" eg, "A => B" -Sean made changes. here mine counts A as the rule, => as the clause and B as the target
                                    addRule(new string[] { target }, clause, rule[0]);
                                    // '!' indicates it is the reverse order
                                    addRule(new string[] { rule[0] }, "!" + clause, target);
                                }
                                else
                                {
                                    foreach( string ruleChar in rule)
                                    {
                                       addRule(new string[] { target }, "-", ruleChar);
                                    }
                                    addRule(rule.ToArray(), "!" + clause, target);
                                }


                                
                            }
                        }
                    }
                    else if (ask == true)
                    {
                        var item = Items.FirstOrDefault(ItemToCheck => ItemToCheck.Contains(s));
                        if(item != null)
                        {
                            item.query = true;
                        }
                    }

                }
            }
            return Items;
        }
    }
}
