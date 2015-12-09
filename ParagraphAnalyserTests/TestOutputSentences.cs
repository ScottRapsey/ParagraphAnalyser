using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParagraphAnalyser.Core;

namespace ParagraphAnalyserTests
{
    [TestClass]
    public class TestOutputSentences
    {
        [TestMethod]
        public void TwoWordsWithMatch()
        {
            var charsWeCareAbout = new[] { 'A' };
            var groupedSentences = Analyser.GetSentencesGroupedByFirstChars("Aaaa. Abbb.", charsWeCareAbout, ignoreCase: true);
            Assert.AreEqual(1, groupedSentences.AllFirstCharItems.Count());
            Assert.AreEqual('A', groupedSentences.AllFirstCharItems.First().FirstChar);
            Assert.AreEqual(2, groupedSentences.AllFirstCharItems.First().Items.Count());

            //doing expected result like this isn't pretty and I don't like it
            //but when you're dealing with string output there aren't a lot of better options
            //tests like this are extremely brittle and pretty much serve the opposite purpose than what they are supposed to do
            //rather than providing security around making changes, they create friction to altering anything about the output in the future
            //in  a more realistic scenario we'd be putting the output together either using razor, or we'd be putting converting the raw data to json and then building the output in client browser
            //but this is supposed to be a 2 hour project not a 20 hour project
            //honestly, I believe there's a special place in hell for people that write tests like this, but I felt like I had to include something
            var expectedResult = "Sentences\r\nLetter\t\t\tQuantity\r\n A\t\t\t2\r\nTotal That Match\t2\r\nTotal Unique Chars\t1\r\nTotal All Words\t2\r\n";
            var result = ParagraphAnalyser.OutputGenerator.GetOutputStringForSentences(groupedSentences, includeOther: false);
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void TwoWordsWithMatchIncludeOther()
        {
            var charsWeCareAbout = new[] { 'A' };
            var groupedSentences = Analyser.GetSentencesGroupedByFirstChars("Aa. Ab.", charsWeCareAbout, ignoreCase: true);
            Assert.AreEqual(1, groupedSentences.Found().Count());
            Assert.AreEqual('A', groupedSentences.Found().First().FirstChar);
            Assert.AreEqual(2, groupedSentences.Found().First().Items.Count());

            var expectedResult = "Sentences\r\nLetter\t\t\tQuantity\r\n A\t\t\t2\r\nOther Unique Chars\t\t0\r\nSum of Other Unique\t0\r\nTotal That Match\t2\r\nTotal Unique Chars\t1\r\nTotal All Words\t2\r\n";
            var result = ParagraphAnalyser.OutputGenerator.GetOutputStringForSentences(groupedSentences, includeOther: true);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TwoWordsWithNoMatch()
        {
            var charsWeCareAbout = new[] { 'x' };
            var groupedSentences = Analyser.GetSentencesGroupedByFirstChars("Aaa. abbb.", charsWeCareAbout, ignoreCase: true);
            Assert.AreEqual(1, groupedSentences.Found().Count());
            Assert.AreEqual('A', groupedSentences.Found().First().FirstChar);
            Assert.AreEqual(2, groupedSentences.Found().First().Items.Count());

            var expectedResult = "Sentences\r\nLetter\t\t\tQuantity\r\n x\t\t\t0\r\nTotal That Match\t0\r\nTotal Unique Chars\t1\r\nTotal All Words\t2\r\n";
            var result = ParagraphAnalyser.OutputGenerator.GetOutputStringForSentences(groupedSentences, includeOther: false);
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void TwoWordsWithNoMatchIncludeOther()
        {
            var charsWeCareAbout = new[] { 'x' };
            var groupedSentences = Analyser.GetSentencesGroupedByFirstChars("Aaaa. Aaaaa.", charsWeCareAbout, ignoreCase: true);
            Assert.AreEqual(1, groupedSentences.Found().Count());
            Assert.AreEqual('A', groupedSentences.Found().First().FirstChar);
            Assert.AreEqual(2, groupedSentences.Found().First().Items.Count());

            var expectedResult = "Sentences\r\nLetter\t\t\tQuantity\r\n x\t\t\t0\r\nOther Unique Chars\t\t1\r\nSum of Other Unique\t2\r\nTotal That Match\t0\r\nTotal Unique Chars\t1\r\nTotal All Words\t2\r\n";
            var result = ParagraphAnalyser.OutputGenerator.GetOutputStringForSentences(groupedSentences, includeOther: true);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestFromSpecIncludeOther()
        {
            var charsWeCareAbout = new[] { 'r', 'e', 'g', 'i', 's', 't' };
            var text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque rhoncus magna eu ullamcorper consectetur. Nulla facilisi. Sed lobortis facilisis felis, ac tincidunt turpis porttitor eget. Suspendisse laoreet finibus turpis ut molestie. In eget lacus sit amet metus efficitur fermentum sit amet ut risus. Donec eget laoreet purus, finibus ornare felis. Maecenas dictum mauris magna, sit amet euismod nisl dignissim quis. Duis ante nunc, laoreet nec posuere vel, mollis sit amet massa. Donec elit massa, gravida at diam id, tristique blandit libero. Curabitur mattis sapien turpis, non bibendum eros lobortis eu. Praesent sed turpis urna.";
            var groupedSentences = Analyser.GetSentencesGroupedByFirstChars(text, charsWeCareAbout, ignoreCase: false);
            Assert.AreEqual(8, groupedSentences.Found().Count());
            Assert.AreEqual('L', groupedSentences.Found().First().FirstChar);
            Assert.AreEqual(1, groupedSentences.Found().First().Items.Count());

            var expectedResult = "Sentences\r\nLetter\t\t\tQuantity\r\n r\t\t\t0\r\n e\t\t\t0\r\n g\t\t\t0\r\n i\t\t\t0\r\n s\t\t\t0\r\n t\t\t\t0\r\nOther Unique Chars\t\t8\r\nSum of Other Unique\t12\r\nTotal That Match\t0\r\nTotal Unique Chars\t8\r\nTotal All Words\t12\r\n";
            var result = ParagraphAnalyser.OutputGenerator.GetOutputStringForSentences(groupedSentences, includeOther: true);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestFromSpecDontIncludeOther()
        {
            var charsWeCareAbout = new[] { 'r', 'e', 'g', 'i', 's', 't' };
            var text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque rhoncus magna eu ullamcorper consectetur. Nulla facilisi. Sed lobortis facilisis felis, ac tincidunt turpis porttitor eget. Suspendisse laoreet finibus turpis ut molestie. In eget lacus sit amet metus efficitur fermentum sit amet ut risus. Donec eget laoreet purus, finibus ornare felis. Maecenas dictum mauris magna, sit amet euismod nisl dignissim quis. Duis ante nunc, laoreet nec posuere vel, mollis sit amet massa. Donec elit massa, gravida at diam id, tristique blandit libero. Curabitur mattis sapien turpis, non bibendum eros lobortis eu. Praesent sed turpis urna.";
            var groupedSentences = Analyser.GetSentencesGroupedByFirstChars(text, charsWeCareAbout, ignoreCase: false);
            Assert.AreEqual(8, groupedSentences.Found().Count());
            Assert.AreEqual('L', groupedSentences.Found().First().FirstChar);
            Assert.AreEqual(1, groupedSentences.Found().First().Items.Count());

            var expectedResult = "Sentences\r\nLetter\t\t\tQuantity\r\n r\t\t\t0\r\n e\t\t\t0\r\n g\t\t\t0\r\n i\t\t\t0\r\n s\t\t\t0\r\n t\t\t\t0\r\nTotal That Match\t0\r\nTotal Unique Chars\t8\r\nTotal All Words\t12\r\n";
            var result = ParagraphAnalyser.OutputGenerator.GetOutputStringForSentences(groupedSentences, includeOther: false);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
