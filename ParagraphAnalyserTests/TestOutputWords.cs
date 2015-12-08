using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParagraphAnalyser.Core;

namespace ParagraphAnalyserTests
{
    [TestClass]
    public class TestOutputWords
    {
        [TestMethod]
        public void SingleCharacterWithMatch()
        {
            var groupedWords = Analyser.GetWordsGroupedBySeperators("A");
            Assert.AreEqual(1, groupedWords.Count());
            Assert.AreEqual('A', groupedWords.First().Key);
            Assert.AreEqual(1, groupedWords.First().Count());

            //doing expected result like this isn't pretty and I don't like it
            //but when you're dealing with string output there aren't a lot of better options
            //tests like this are extremely brittle and pretty much serve the opposite purpose than what they are supposed to do
            //rather than providing security around making changes, they create friction to altering anything about the output in the future
            //in  a more realistic scenario we'd be putting the output together either using razor, or we'd be putting converting the raw data to json and then building the output in client browser
            //but this is supposed to be a 2 hour project not a 20 hour project
            //honestly, I believe there's a special place in hell for people that write tests like this, but I felt like I had to include something
            var expectedResult = "Words\r\nLetter\t\t\tQuantity\r\n A\t\t\t1\r\nTotal That Match\t1\r\nTotal Unique Chars\t1\r\nTotal Words\t\t1\r\n";
            var charsWeCareAbout = new[] { 'A' };
            var result = ParagraphAnalyser.OutputGenerator.GetOutputStringForWords(groupedWords, charsWeCareAbout, ignoreCase:true, includeOther: false);
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void SingleCharacterWithMatchIncludeOther()
        {
            var groupedWords = Analyser.GetWordsGroupedBySeperators("A");
            Assert.AreEqual(1, groupedWords.Count());
            Assert.AreEqual('A', groupedWords.First().Key);
            Assert.AreEqual(1, groupedWords.First().Count());

            var expectedResult = "Words\r\nLetter\t\t\tQuantity\r\n A\t\t\t1\r\nOther Unique\t\t0\r\nSum of Other Unique\t0\r\nTotal That Match\t1\r\nTotal Unique Chars\t1\r\nTotal Words\t\t1\r\n";
            var charsWeCareAbout = new[] { 'A' };
            var result = ParagraphAnalyser.OutputGenerator.GetOutputStringForWords(groupedWords, charsWeCareAbout, ignoreCase: true, includeOther: true);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void SingleCharacterWithNoMatch()
        {
            var groupedWords = Analyser.GetWordsGroupedBySeperators("A");
            Assert.AreEqual(1, groupedWords.Count());
            Assert.AreEqual('A', groupedWords.First().Key);
            Assert.AreEqual(1, groupedWords.First().Count());

            var expectedResult = "Words\r\nLetter\t\t\tQuantity\r\n x\t\t\t0\r\nTotal That Match\t0\r\nTotal Unique Chars\t1\r\nTotal Words\t\t1\r\n";
            var charsWeCareAbout = new[] { 'x' };
            var result = ParagraphAnalyser.OutputGenerator.GetOutputStringForWords(groupedWords, charsWeCareAbout, ignoreCase: true, includeOther: false);
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void SingleCharacterWithNoMatchIncludeOther()
        {
            var groupedWords = Analyser.GetWordsGroupedBySeperators("A");
            Assert.AreEqual(1, groupedWords.Count());
            Assert.AreEqual('A', groupedWords.First().Key);
            Assert.AreEqual(1, groupedWords.First().Count());

            var expectedResult = "Words\r\nLetter\t\t\tQuantity\r\n x\t\t\t0\r\nOther Unique\t\t1\r\nSum of Other Unique\t1\r\nTotal That Match\t0\r\nTotal Unique Chars\t1\r\nTotal Words\t\t1\r\n";
            var charsWeCareAbout = new[] { 'x' };
            var result = ParagraphAnalyser.OutputGenerator.GetOutputStringForWords(groupedWords, charsWeCareAbout, ignoreCase: true, includeOther: true);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestFromSpecIncludeOther()
        {
            var text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque rhoncus magna eu ullamcorper consectetur. Nulla facilisi. Sed lobortis facilisis felis, ac tincidunt turpis porttitor eget. Suspendisse laoreet finibus turpis ut molestie. In eget lacus sit amet metus efficitur fermentum sit amet ut risus. Donec eget laoreet purus, finibus ornare felis. Maecenas dictum mauris magna, sit amet euismod nisl dignissim quis. Duis ante nunc, laoreet nec posuere vel, mollis sit amet massa. Donec elit massa, gravida at diam id, tristique blandit libero. Curabitur mattis sapien turpis, non bibendum eros lobortis eu. Praesent sed turpis urna.";
            var groupedWords = Analyser.GetWordsGroupedBySeperators(text);
            Assert.AreEqual(19, groupedWords.Count());
            Assert.AreEqual('L', groupedWords.First().Key);
            Assert.AreEqual(8, groupedWords.First().Count());

            var expectedResult = "Words\r\nLetter\t\t\tQuantity\r\n r\t\t\t2\r\n e\t\t\t10\r\n g\t\t\t1\r\n i\t\t\t3\r\n s\t\t\t9\r\n t\t\t\t6\r\nOther Unique\t\t13\r\nSum of Other Unique\t63\r\nTotal That Match\t31\r\nTotal Unique Chars\t19\r\nTotal Words\t\t94\r\n";
            var charsWeCareAbout = new[] { 'r', 'e', 'g', 'i', 's', 't' };
            var result = ParagraphAnalyser.OutputGenerator.GetOutputStringForWords(groupedWords, charsWeCareAbout, ignoreCase: true, includeOther: true);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestFromSpecDontIncludeOther()
        {
            var text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque rhoncus magna eu ullamcorper consectetur. Nulla facilisi. Sed lobortis facilisis felis, ac tincidunt turpis porttitor eget. Suspendisse laoreet finibus turpis ut molestie. In eget lacus sit amet metus efficitur fermentum sit amet ut risus. Donec eget laoreet purus, finibus ornare felis. Maecenas dictum mauris magna, sit amet euismod nisl dignissim quis. Duis ante nunc, laoreet nec posuere vel, mollis sit amet massa. Donec elit massa, gravida at diam id, tristique blandit libero. Curabitur mattis sapien turpis, non bibendum eros lobortis eu. Praesent sed turpis urna.";
            var groupedWords = Analyser.GetWordsGroupedBySeperators(text);
            Assert.AreEqual(19, groupedWords.Count());
            Assert.AreEqual('L', groupedWords.First().Key);
            Assert.AreEqual(8, groupedWords.First().Count());

            var expectedResult = "Words\r\nLetter\t\t\tQuantity\r\n r\t\t\t2\r\n e\t\t\t10\r\n g\t\t\t1\r\n i\t\t\t3\r\n s\t\t\t9\r\n t\t\t\t6\r\nTotal That Match\t31\r\nTotal Unique Chars\t19\r\nTotal Words\t\t94\r\n";
            var charsWeCareAbout = new[] { 'r', 'e', 'g', 'i', 's', 't' };
            var result = ParagraphAnalyser.OutputGenerator.GetOutputStringForWords(groupedWords, charsWeCareAbout, ignoreCase: true, includeOther: false);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
