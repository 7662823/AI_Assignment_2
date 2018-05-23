using Microsoft.VisualStudio.TestTools.UnitTesting;
using iengine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iengine.Tests
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
            Relation aRelation = new Relation();
            aRelation.name.Add("p2");
            aRelation.clause = "=>";
            a.relations.Add(aRelation);

            Item b = new Item();
            b.name = "b";
            b.valid = true;
            b.query = true;
            Relation bRelation = new Relation();
            bRelation.name.Add("c");
            bRelation.clause = "-";
            b.relations.Add(bRelation);

            Item p2 = new Item();
            p2.name = "p2";
            p2.valid = true;
            p2.query = false;
            Relation p2Relation1 = new Relation();
            p2Relation1.name.Add("a");
            p2Relation1.clause = "!=>";
            p2.relations.Add(p2Relation1);

            Relation p2Relation2 = new Relation();
            p2Relation2.name.Add("c");
            p2Relation2.clause = "-";
            p2.relations.Add(p2Relation2);

            Item c = new Item();
            c.name = "c";
            c.valid = false;
            c.query = false;
            Relation cRelation = new Relation();
            cRelation.name.Add("p2");
            cRelation.name.Add("b");
            cRelation.clause = "!=>";
            c.relations.Add(cRelation);

            _expected.Add(a);
            _expected.Add(b);
            _expected.Add(p2);
            _expected.Add(c);
        }

        [TestMethod]
        public void Test_ReadTextFile_Names()
        {
            ReadTextFile file = new ReadTextFile();
            List<Item> Items = file.ReadFile("Test/test1.txt");

            for (int i = 0; i < Items.Count; i++)
                Assert.AreEqual(_expected[i].name, Items[i].name);
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
