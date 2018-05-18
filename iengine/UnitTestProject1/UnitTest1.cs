using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using iengine;

namespace UnitTestProject1
{
    [TestClass]
    public class ReadTextFileTest
    {
        private List<Item> _expected;
        
        [TestInitialize]
        public void Initialise()
        {
             _expected = new List<Item>();
            Item a = new Item();
            a.name = "a";
            a.valid = true;
            a.query = false;
   
            Item b = new Item();
            b.name = "b";
            b.valid = true;
            b.query = true;

            Item p2 = new Item();
            p2.name = "p2";
            p2.valid = true;
            p2.query = false;

            _expected.Add(a);
            _expected.Add(b);
            _expected.Add(p2);
        }

        [TestMethod]
        public void Test_ReadTextFile_Names()
        {
            ReadTextFile file = new ReadTextFile();
            List<Item> Items = file.ReadFile("Test/test1.txt");

            for(int i = 0; i < Items.Count; i++)
                Assert.AreEqual(_expected[i].name,Items[i].name );
        }

        [TestMethod]
        public void Test_ReadTextFile_Valid()
        {
            ReadTextFile file = new ReadTextFile();
            List<Item> Items = file.ReadFile("Test/test1.txt");

            for (int i = 0; i < Items.Count; i++)
                Assert.AreEqual(_expected[i].valid, Items[i].valid);
        }

        [TestMethod]
        public void Test_ReadTextFile_Query()
        {
            ReadTextFile file = new ReadTextFile();
            List<Item> Items = file.ReadFile("Test/test1.txt");

            for (int i = 0; i < Items.Count; i++)
                Assert.AreEqual(_expected[i].query, Items[i].query);
        }

        [TestMethod]
        public void Test_ReadTextFile_Relation()
        {
            ReadTextFile file = new ReadTextFile();
            List<Item> Items = file.ReadFile("Test/test1.txt");

            for (int i = 0; i < Items.Count; i++)
            {
                for (int j = 0; j < Items[i].relations.Count; j++)
                {
                    for (int k = 0; k < Items[i].relations[j].name.Count; k++)
                        Assert.AreEqual(_expected[i].relations[j].name[k], Items[i].relations[j].name[k]);

                    Assert.AreEqual(_expected[i].relations[j].clause, Items[i].relations[j].clause);
                }
            }
            
        }
    }
}
