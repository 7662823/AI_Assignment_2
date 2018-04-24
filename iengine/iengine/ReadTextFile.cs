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
                            string[] item;
                            string identifier = string.Join("|", identifiers.ToArray());
                            //splits the string with the corresponding identifiers and removes any spaces from the split string
                            item = Regex.Split(r.Replace(" ", string.Empty), @"(" + identifier +@")");
                            foreach(string i in item)
                            {
                                //if item is a identifier
                                if (identifiers.Any(i.Contains)){

                                }
                                else
                                {
                                    bool newItem = true;
                                    foreach(Item j in result)
                                    {
                                        if (j.name == i)
                                        {
                                            newItem = false;
                                        }
                                    }
                                    if(newItem == true)
                                    {
                                        result.Add(new Item());
                                        result.Last().name = i;
                                    }
                                }
                            }
                        }
                    }
                    else if (ask == true)
                    {

                    }
                  
                }
            }
            return result;
        }
    }
}
