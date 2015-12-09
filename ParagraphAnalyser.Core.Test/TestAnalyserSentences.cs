﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParagraphAnalyser.Core;

namespace ParagraphAnalyserTests
{
    [TestClass]
    public class TestAnalyserSentencesGroupBy
    {
        [TestMethod]
        public void SingleCharacter()
        {
            var result = Analyser.GetSentencesGroupedByFirstChars("A");
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual('A', result.First().Key);
            Assert.AreEqual(1, result.First().Count());
        }
        [TestMethod]
        public void TwoSameCharacterCaseSensitive()
        {
            var result = Analyser.GetSentencesGroupedByFirstChars("A. A.", ignoreCase: true);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual('A', result.First().Key);
            Assert.AreEqual(2, result.First().Count());
        }
        [TestMethod]
        public void TwoSameCharacterDifferentCaseCaseSensitive()
        {
            var result = Analyser.GetSentencesGroupedByFirstChars("A. a.", ignoreCase: false);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual('A', result.First().Key);
            Assert.AreEqual(1, result.First().Count());
            Assert.AreEqual('a', result.ToArray()[1].Key);
            Assert.AreEqual(1, result.ToArray()[1].Count());
        }
        [TestMethod]
        public void TwoSameCharacterDifferentCaseCaseInsensitive()
        {
            var result = Analyser.GetSentencesGroupedByFirstChars("A. a.", ignoreCase: true);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual('A', result.First().Key); //could legitimately return 'a'
            Assert.AreEqual(2, result.First().Count());
        }

        [TestMethod]
        public void TwoSameWordsCaseSensitive()
        {
            var result = Analyser.GetSentencesGroupedByFirstChars("Albuquerque. Albuquerque.", ignoreCase: true);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual('A', result.First().Key);
            Assert.AreEqual(2, result.First().Count());
        }
        [TestMethod]
        public void TwoSameWordsDifferentCaseCaseSensitive()
        {
            var result = Analyser.GetSentencesGroupedByFirstChars("Albuquerque. albuquerque.", ignoreCase: false);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual('A', result.First().Key);
            Assert.AreEqual(1, result.First().Count());
            Assert.AreEqual('a', result.ToArray()[1].Key);
            Assert.AreEqual(1, result.ToArray()[1].Count());
        }
        [TestMethod]
        public void TwoSameWordsDifferentCaseCaseInsensitive()
        {
            var result = Analyser.GetSentencesGroupedByFirstChars("Albuquerque. albuquerque.", ignoreCase: true);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual('A', result.First().Key); //could legitimately return 'a'
            Assert.AreEqual(2, result.First().Count());
        }

        [TestMethod]
        public void LongParagraphTest()
        {
            var text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque rhoncus magna eu ullamcorper consectetur. Nulla facilisi. Sed lobortis facilisis felis, ac tincidunt turpis porttitor eget. Suspendisse laoreet finibus turpis ut molestie. In eget lacus sit amet metus efficitur fermentum sit amet ut risus. Donec eget laoreet purus, finibus ornare felis. Maecenas dictum mauris magna, sit amet euismod nisl dignissim quis. Duis ante nunc, laoreet nec posuere vel, mollis sit amet massa. Donec elit massa, gravida at diam id, tristique blandit libero. Curabitur mattis sapien turpis, non bibendum eros lobortis eu. Praesent sed turpis urna.";
            var result = Analyser.GetSentencesGroupedByFirstChars(text, ignoreCase: false);
            Assert.AreEqual(8, result.Count());
            Assert.AreEqual('L', result.First().Key);
            Assert.AreEqual(1, result.First().Count());
        }
    }

}
