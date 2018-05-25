﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace iengine.Tests
{
    [TestClass]
    public class BackChainingTests
    {
        [TestMethod]
        public void TestOutputForGivenExample1()
        {
            ReadTextFile file = new ReadTextFile();
            List<Item> Items = file.ReadFile("Test/GivenExample1.txt");
            Process function = new Process("BC", Items, file.Rules);

            Assert.AreEqual("YES: p2, p3, p1, d", function.RunMethod());
        }
        [TestMethod]
        public void TestOutputForGivenExample2()
        {
            ReadTextFile file = new ReadTextFile();
            List<Item> Items = file.ReadFile("Test/GivenExample2.txt");
            Process function = new Process("BC", Items, file.Rules);

            Assert.AreEqual("NO", function.RunMethod());
        }
        [TestMethod]
        public void TestOutputMultiAndStatement()
        {
            ReadTextFile file = new ReadTextFile();
            List<Item> Items = file.ReadFile("Test/TTtest1.txt");
            Process function = new Process("BC", Items, file.Rules);

            Assert.AreEqual("YES: p2, p3, b, a, p1, d", function.RunMethod());
        }
        
        [TestMethod]
        public void TestOutputNonExsistantQueryInRules()
        {
            ReadTextFile file = new ReadTextFile();
            List<Item> Items = file.ReadFile("Test/TTtest2.txt");
            Process function = new Process("BC", Items, file.Rules);

            Assert.AreEqual("NO", function.RunMethod());
        }
        [TestMethod]
        public void TestOutputValidRulesInvalidQuery()
        {
            ReadTextFile file = new ReadTextFile();
            List<Item> Items = file.ReadFile("Test/TTtest3.txt");
            Process function = new Process("BC", Items, file.Rules);

            Assert.AreEqual("NO", function.RunMethod());
        }
    }
}
