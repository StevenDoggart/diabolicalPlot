using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections;


namespace DiabolicalPlot.Business.Test
{
    [TestClass]
    public class IdentifierParserTest
    {
        [TestMethod]
        public void Parse_null_returns_empty_list()
        {
            var parser = new IdentifierParser();
            var result = parser.Parse(null);
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any());

        }


        [TestMethod]
        public void Parse_empty_string_returns_empty_list()
        {
            var parser = new IdentifierParser();
            var result = parser.Parse("");
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any());
        }


        [TestMethod]
        public void Parse_word()
        {
            var parser = new IdentifierParser();
            var result = parser.Parse("word").ToArray();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(IdentifierPartTypeEnum.Word, result[0].Type);
            Assert.AreEqual("word", result[0].Value);
        }


        [TestMethod]
        public void Parse_oneTwoTHREEFour5Six7eight_9_ten()
        {
            var parser = new IdentifierParser();
            var result = parser.Parse("oneTwoTHREEFour5Six7eight_9__ten").ToArray();
            Assert.IsNotNull(result);
            Assert.AreEqual(12, result.Count());

            Assert.AreEqual(IdentifierPartTypeEnum.Word, result[0].Type);
            Assert.AreEqual("one", result[0].Value);

            Assert.AreEqual(IdentifierPartTypeEnum.Word, result[1].Type);
            Assert.AreEqual("Two", result[1].Value);

            Assert.AreEqual(IdentifierPartTypeEnum.Word, result[2].Type);
            Assert.AreEqual("THREE", result[2].Value);

            Assert.AreEqual(IdentifierPartTypeEnum.Word, result[3].Type);
            Assert.AreEqual("Four", result[3].Value);

            Assert.AreEqual(IdentifierPartTypeEnum.Word, result[4].Type);
            Assert.AreEqual("5", result[4].Value);

            Assert.AreEqual(IdentifierPartTypeEnum.Word, result[5].Type);
            Assert.AreEqual("Six", result[5].Value);

            Assert.AreEqual(IdentifierPartTypeEnum.Word, result[6].Type);
            Assert.AreEqual("7", result[6].Value);

            Assert.AreEqual(IdentifierPartTypeEnum.Word, result[7].Type);
            Assert.AreEqual("eight", result[7].Value);

            Assert.AreEqual(IdentifierPartTypeEnum.Other, result[8].Type);
            Assert.AreEqual("_", result[8].Value);

            Assert.AreEqual(IdentifierPartTypeEnum.Word, result[9].Type);
            Assert.AreEqual("9", result[9].Value);

            Assert.AreEqual(IdentifierPartTypeEnum.Other, result[10].Type);
            Assert.AreEqual("__", result[10].Value);

            Assert.AreEqual(IdentifierPartTypeEnum.Word, result[11].Type);
            Assert.AreEqual("ten", result[11].Value);
        }
    }
}
