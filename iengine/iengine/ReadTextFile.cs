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
        public List<Item> ReadFile(string fileName)
        {
            List<Item> result = new List<Item>();
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
                                    var match = result.FirstOrDefault(stringToCheck => stringToCheck.Contains(SplitRule[0]));
                                    if (match != null)
                                    {
                                        match.valid = true;
                                    }
                                    else {
                                        result.Add(new Item());
                                        result.Last().name = SplitRule[0];
                                        result.Last().valid = true;
                                    }
                                    break;
                                case 3:
                                    var match2 = result.FirstOrDefault(stringToCheck => stringToCheck.Contains(SplitRule[0]));
                                    var match3 = result.FirstOrDefault(stringToCheck => stringToCheck.Contains(SplitRule[2]));

                                    if (match2 != null)
                                    {
                                        Relation newRelation = new Relation();
                                        newRelation.name.Add(SplitRule[2]);
                                        newRelation.clause = "=>";
                                        match2.relations.Add(newRelation);
                                    }
                                    else
                                    {
                                        result.Add(new Item());
                                        result.Last().name = SplitRule[0];
                                        Relation newRelation = new Relation();
                                        newRelation.name.Add(SplitRule[2]);
                                        newRelation.clause = "=>";
                                        result.Last().relations.Add(newRelation);
                                    }

                                    if (match3 != null)
                                    {
                                        Relation newRelation = new Relation();
                                        newRelation.name.Add(SplitRule[0]);
                                        newRelation.clause = "<=";
                                        match3.relations.Add(newRelation);
                                    }
                                    else
                                    {
                                        result.Add(new Item());
                                        result.Last().name = SplitRule[2];
                                        Relation newRelation = new Relation();
                                        newRelation.name.Add(SplitRule[0]);
                                        newRelation.clause = "<=";
                                        result.Last().relations.Add(newRelation);
                                    }
                                    break;
                                case 5:
                                    var match4 = result.FirstOrDefault(stringToCheck => stringToCheck.Contains(SplitRule[0]));
                                    var match5 = result.FirstOrDefault(stringToCheck => stringToCheck.Contains(SplitRule[2]));
                                    var match6 = result.FirstOrDefault(stringToCheck => stringToCheck.Contains(SplitRule[4]));

                                    if (match4 != null)
                                    {
                                        Relation newRelation = new Relation();
                                       // newRelation.name.Add(SplitRule[2]);
                                        newRelation.name.Add(SplitRule[4]);

                                        newRelation.clause = "=>";
                                        match4.relations.Add(newRelation);
                                    }
                                    else
                                    {
                                        result.Add(new Item());
                                        result.Last().name = SplitRule[0];
                                        Relation newRelation = new Relation();
                                        newRelation.name.Add(SplitRule[4]);
                                        newRelation.clause = "=>";
                                        result.Last().relations.Add(newRelation);
                                    }

                                    if (match5 != null)
                                    {
                                        Relation newRelation = new Relation();
                                        newRelation.name.Add(SplitRule[4]);
                                        newRelation.clause = "=>";
                                        match5.relations.Add(newRelation);
                                    }
                                    else
                                    {
                                        result.Add(new Item());
                                        result.Last().name = SplitRule[2];
                                        Relation newRelation = new Relation();
                                        newRelation.name.Add( SplitRule[4]);
                                        newRelation.clause = "=>";
                                        result.Last().relations.Add(newRelation);
                                    }
                                    if (match6 != null)
                                    {
                                        Relation newRelation = new Relation();
                                        newRelation.name.Add( SplitRule[0]);
                                        newRelation.name.Add(SplitRule[2]);

                                        newRelation.clause = "<=";
                                        match6.relations.Add(newRelation);
                                    }
                                    else
                                    {
                                        result.Add(new Item());
                                        result.Last().name = SplitRule[4];
                                        Relation newRelation = new Relation();
                                        newRelation.name.Add(SplitRule[0]);
                                        newRelation.name.Add(SplitRule[2]);

                                        newRelation.clause = "<=";
                                        result.Last().relations.Add(newRelation);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (ask == true)
                    {
                        var item = result.FirstOrDefault(ItemToCheck => ItemToCheck.Contains(s));
                        if(item != null)
                        {
                            item.ASK = true;
                        }
                    }

                }
            }
            Rules = result;
            return result;
        }
    }
}
