using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParagraphAnalyser.Core;


namespace ParagraphAnalyser.Core.Test
{

    [TestClass]
    public class AnalyserGetGroupedFirstCharModelIgnoreCase
    {
        private bool IgnoreCase = true;

        [TestMethod]
        public void SingleCharacterWeCareAboutWithNoMatch()
        {
            var charWeCareAbout = 'X';
            var charsWeCareAbout = new[] { charWeCareAbout };
            var groups = new List<IGrouping<char, string>>();
            groups.Add(new TestGrouping(items: new[] { "Albuquerque" }, getKey: () => 'A'));
            var result = Analyser.GetGroupedFirstCharModel(groups, charsWeCareAbout, this.IgnoreCase);

            Assert.IsNotNull(result, "result should not be null");
            Assert.IsNotNull(result.AllFirstCharItems, "AllFirstCharItems should not be null");
            Assert.AreEqual(2, result.AllFirstCharItems.Count(), "AllFirstCharItems Count"); //one for x and one for A
            Assert.AreEqual(1, result.Found().Count(), "Found Count"); //Should have found 1 word
            Assert.AreEqual(1, result.NotFound().Count(), "NotFound Count"); //x wasn't found
            Assert.AreEqual(1, result.Matching().Count(), "Matching Count"); //Should have a match for X, it should have zero items tho
            Assert.AreEqual(1, result.NotMatching().Count(), "NotMatching Count"); //A wasn't matching

            Assert.AreEqual(1, result.Found().First().Items.Count(), "First Found Item Count");
            Assert.AreEqual(0, result.NotFound().First().Items.Count(), "First NotFound Item Count");
            Assert.AreEqual(0, result.Matching().First().Items.Count(), "First Matching Item Count");
            Assert.AreEqual(1, result.NotMatching().First().Items.Count(), "First NotMatching Item Count");

            Assert.AreEqual('A', result.Found().First().FirstChar, "First Found First Char");
            Assert.AreEqual(charWeCareAbout, result.NotFound().First().FirstChar, "First NotFound First Char");
            Assert.AreEqual(charWeCareAbout, result.Matching().First().FirstChar, "First Matching First Char");
            Assert.AreEqual('A', result.NotMatching().First().FirstChar, "First NotMatching First Char");
        }

        [TestMethod]
        public void SingleCharacterWeCareAboutWithMatch()
        {
            var charWeCareAbout = 'A';
            var charsWeCareAbout = new[] { charWeCareAbout };
            var groups = new List<IGrouping<char, string>>();
            groups.Add(new TestGrouping(items: new[] { "Albuquerque" }, getKey: () => 'A'));
            var result = Analyser.GetGroupedFirstCharModel(groups, charsWeCareAbout, this.IgnoreCase);

            Assert.IsNotNull(result, "result should not be null");
            Assert.IsNotNull(result.AllFirstCharItems, "AllFirstCharItems should not be null");
            Assert.AreEqual(1, result.AllFirstCharItems.Count(), "AllFirstCharItems Count");
            Assert.AreEqual(1, result.Found().Count(), "Found Count");
            Assert.AreEqual(0, result.NotFound().Count(), "NotFound Count");
            Assert.AreEqual(1, result.Matching().Count(), "Matching Count");
            Assert.AreEqual(0, result.NotMatching().Count(), "NotMatching Count");

            Assert.AreEqual(1, result.Found().First().Items.Count(), "First Found Item Count");
            //Assert.AreEqual(0, result.NotFound().First().Items.Count(), "First NotFound Item Count");
            Assert.AreEqual(1, result.Matching().First().Items.Count(), "First Matching Item Count");
            //Assert.AreEqual(1, result.NotMatching().First().Items.Count(), "First NotMatching Item Count");

            Assert.AreEqual('A', result.Found().First().FirstChar, "First Found First Char");
            //Assert.AreEqual(charWeCareAbout, result.NotFound().First().FirstChar, "First NotFound First Char");
            Assert.AreEqual(charWeCareAbout, result.Matching().First().FirstChar, "First Matching First Char");
            //Assert.AreEqual('A', result.NotMatching().First().FirstChar, "First NotMatching First Char");
        }

        [TestMethod]
        public void SingleCharacterWeCareAboutWithCaseSensitiveMatch()
        {
            var charWeCareAbout = 'a';
            var charsWeCareAbout = new[] { charWeCareAbout };
            var groups = new List<IGrouping<char, string>>();
            groups.Add(new TestGrouping(items: new[] { "Albuquerque" }, getKey: () => 'A'));
            var result = Analyser.GetGroupedFirstCharModel(groups, charsWeCareAbout, this.IgnoreCase);

            Assert.IsNotNull(result, "result should not be null");
            Assert.IsNotNull(result.AllFirstCharItems, "AllFirstCharItems should not be null");
            Assert.AreEqual(1, result.AllFirstCharItems.Count(), "AllFirstCharItems Count"); //one for a and one for A
            Assert.AreEqual(1, result.Found().Count(), "Found Count"); //Should have found 1 word
            Assert.AreEqual(0, result.NotFound().Count(), "NotFound Count");
            Assert.AreEqual(1, result.Matching().Count(), "Matching Count"); //Should have a match for a
            Assert.AreEqual(0, result.NotMatching().Count(), "NotMatching Count");

            Assert.AreEqual(1, result.Found().First().Items.Count(), "First Found Item Count");
            //Assert.AreEqual(0, result.NotFound().First().Items.Count(), "First NotFound Item Count");
            Assert.AreEqual(1, result.Matching().First().Items.Count(), "First Matching Item Count");
            //Assert.AreEqual(1, result.NotMatching().First().Items.Count(), "First NotMatching Item Count");

            Assert.AreEqual('a', result.Found().First().FirstChar, "First Found First Char");
            //Assert.AreEqual(charWeCareAbout, result.NotFound().First().FirstChar, "First NotFound First Char");
            Assert.AreEqual(charWeCareAbout, result.Matching().First().FirstChar, "First Matching First Char");
            //Assert.AreEqual('A', result.NotMatching().First().FirstChar, "First NotMatching First Char");
        }

        [TestMethod]
        public void MultiCharactersWeCareAboutWithSomeMatches()
        {
            var charsWeCareAbout = new[] { 'r', 'e', 'g' };
            var groups = new List<IGrouping<char, string>>();
            groups.AddRange(new[] {
                new TestGrouping(items: new[] { "Albuquerque", "Albury", }, getKey: () => 'A'),
                new TestGrouping(items: new[] { "Register" }, getKey: () => 'R'),
                new TestGrouping(items: new[] { "Now", }, getKey: () => 'N'),
                new TestGrouping(items: new[] { "Engelberg" }, getKey: () => 'E')
                });
            var result = Analyser.GetGroupedFirstCharModel(groups, charsWeCareAbout, this.IgnoreCase);

            Assert.IsNotNull(result, "result should not be null");
            Assert.IsNotNull(result.AllFirstCharItems, "AllFirstCharItems should not be null");
            Assert.AreEqual(5, result.AllFirstCharItems.Count(), "AllFirstCharItems Count");
            Assert.AreEqual(4, result.Found().Count(), "Found Count");
            Assert.AreEqual(1, result.NotFound().Count(), "NotFound Count");
            Assert.AreEqual(3, result.Matching().Count(), "Matching Count");
            Assert.AreEqual(2, result.NotMatching().Count(), "NotMatching Count");

            Assert.AreEqual(1, result.Found().First().Items.Count(), "First Found Item Count");
            Assert.AreEqual(0, result.NotFound().First().Items.Count(), "First NotFound Item Count");
            Assert.AreEqual(1, result.Matching().First().Items.Count(), "First Matching Item Count");
            Assert.AreEqual(2, result.NotMatching().First().Items.Count(), "First NotMatching Item Count");

            Assert.AreEqual('r', result.Found().First().FirstChar, "First Found First Char");
            Assert.AreEqual('g', result.NotFound().First().FirstChar, "First NotFound First Char");
            Assert.AreEqual('r', result.Matching().First().FirstChar, "First Matching First Char");
            Assert.AreEqual('A', result.NotMatching().First().FirstChar, "First NotMatching First Char");
        }
    }

    [TestClass]
    public class AnalyserGetGroupedFirstCharModelDontIgnoreCase
    {
        private bool IgnoreCase = false;

        [TestMethod]
        public void SingleCharacterWeCareAboutWithNoMatch()
        {
            var charWeCareAbout = 'X';
            var charsWeCareAbout = new[] { charWeCareAbout };
            var groups = new List<IGrouping<char, string>>();
            groups.Add(new TestGrouping(items: new[] { "Albuquerque" }, getKey: () => 'A'));
            var result = Analyser.GetGroupedFirstCharModel(groups, charsWeCareAbout, this.IgnoreCase);

            Assert.IsNotNull(result, "result should not be null");
            Assert.IsNotNull(result.AllFirstCharItems, "AllFirstCharItems should not be null");
            Assert.AreEqual(2, result.AllFirstCharItems.Count(), "AllFirstCharItems Count"); //one for x and one for A
            Assert.AreEqual(1, result.Found().Count(), "Found Count"); //Should have found 1 word
            Assert.AreEqual(1, result.NotFound().Count(), "NotFound Count"); //x wasn't found
            Assert.AreEqual(1, result.Matching().Count(), "Matching Count"); //Should have a match for X, it should have zero items tho
            Assert.AreEqual(1, result.NotMatching().Count(), "NotMatching Count"); //A wasn't matching

            Assert.AreEqual(1, result.Found().First().Items.Count(), "First Found Item Count");
            Assert.AreEqual(0, result.NotFound().First().Items.Count(), "First NotFound Item Count");
            Assert.AreEqual(0, result.Matching().First().Items.Count(), "First Matching Item Count");
            Assert.AreEqual(1, result.NotMatching().First().Items.Count(), "First NotMatching Item Count");

            Assert.AreEqual('A', result.Found().First().FirstChar, "First Found First Char");
            Assert.AreEqual(charWeCareAbout, result.NotFound().First().FirstChar, "First NotFound First Char");
            Assert.AreEqual(charWeCareAbout, result.Matching().First().FirstChar, "First Matching First Char");
            Assert.AreEqual('A', result.NotMatching().First().FirstChar, "First NotMatching First Char");
        }

        [TestMethod]
        public void SingleCharacterWeCareAboutWithMatch()
        {
            var charWeCareAbout = 'A';
            var charsWeCareAbout = new[] { charWeCareAbout };
            var groups = new List<IGrouping<char, string>>();
            groups.Add(new TestGrouping(items: new[] { "Albuquerque" }, getKey: () => 'A'));
            var result = Analyser.GetGroupedFirstCharModel(groups, charsWeCareAbout, this.IgnoreCase);

            Assert.IsNotNull(result, "result should not be null");
            Assert.IsNotNull(result.AllFirstCharItems, "AllFirstCharItems should not be null");
            Assert.AreEqual(1, result.AllFirstCharItems.Count(), "AllFirstCharItems Count");
            Assert.AreEqual(1, result.Found().Count(), "Found Count");
            Assert.AreEqual(0, result.NotFound().Count(), "NotFound Count");
            Assert.AreEqual(1, result.Matching().Count(), "Matching Count");
            Assert.AreEqual(0, result.NotMatching().Count(), "NotMatching Count");

            Assert.AreEqual(1, result.Found().First().Items.Count(), "First Found Item Count");
            //Assert.AreEqual(0, result.NotFound().First().Items.Count(), "First NotFound Item Count");
            Assert.AreEqual(1, result.Matching().First().Items.Count(), "First Matching Item Count");
            //Assert.AreEqual(1, result.NotMatching().First().Items.Count(), "First NotMatching Item Count");

            Assert.AreEqual('A', result.Found().First().FirstChar, "First Found First Char");
            //Assert.AreEqual(charWeCareAbout, result.NotFound().First().FirstChar, "First NotFound First Char");
            Assert.AreEqual(charWeCareAbout, result.Matching().First().FirstChar, "First Matching First Char");
            //Assert.AreEqual('A', result.NotMatching().First().FirstChar, "First NotMatching First Char");
        }

        [TestMethod]
        public void SingleCharacterWeCareAboutWithCaseSensitiveMatch()
        {
            var charWeCareAbout = 'a';
            var charsWeCareAbout = new[] { charWeCareAbout };
            var groups = new List<IGrouping<char, string>>();
            groups.Add(new TestGrouping(items: new[] { "Albuquerque" }, getKey: () => 'A'));
            var result = Analyser.GetGroupedFirstCharModel(groups, charsWeCareAbout, this.IgnoreCase);

            Assert.IsNotNull(result, "result should not be null");
            Assert.IsNotNull(result.AllFirstCharItems, "AllFirstCharItems should not be null");
            Assert.AreEqual(2, result.AllFirstCharItems.Count(), "AllFirstCharItems Count"); //one for a and one for A
            Assert.AreEqual(1, result.Found().Count(), "Found Count"); //Should have found 1 word
            Assert.AreEqual(1, result.NotFound().Count(), "NotFound Count");
            Assert.AreEqual(1, result.Matching().Count(), "Matching Count"); //Should have a match for a
            Assert.AreEqual(1, result.NotMatching().Count(), "NotMatching Count");

            Assert.AreEqual(1, result.Found().First().Items.Count(), "First Found Item Count");
            //Assert.AreEqual(0, result.NotFound().First().Items.Count(), "First NotFound Item Count");
            Assert.AreEqual(0, result.Matching().First().Items.Count(), "First Matching Item Count");
            //Assert.AreEqual(1, result.NotMatching().First().Items.Count(), "First NotMatching Item Count");

            Assert.AreEqual('A', result.Found().First().FirstChar, "First Found First Char");
            //Assert.AreEqual(charWeCareAbout, result.NotFound().First().FirstChar, "First NotFound First Char");
            Assert.AreEqual(charWeCareAbout, result.Matching().First().FirstChar, "First Matching First Char");
            //Assert.AreEqual('A', result.NotMatching().First().FirstChar, "First NotMatching First Char");
        }

        [TestMethod]
        public void MultiCharactersWeCareAboutWithSomeMatches()
        {
            var charsWeCareAbout = new[] { 'R', 'e', 'g' };
            var groups = new List<IGrouping<char, string>>();
            groups.AddRange(new[] {
                new TestGrouping(items: new[] { "Albuquerque", "Albury", }, getKey: () => 'A'),
                new TestGrouping(items: new[] { "Register" }, getKey: () => 'R'),
                new TestGrouping(items: new[] { "Now", }, getKey: () => 'N'),
                new TestGrouping(items: new[] { "Engelberg" }, getKey: () => 'E')
                });
            var result = Analyser.GetGroupedFirstCharModel(groups, charsWeCareAbout, this.IgnoreCase);

            Assert.IsNotNull(result, "result should not be null");
            Assert.IsNotNull(result.AllFirstCharItems, "AllFirstCharItems should not be null");
            Assert.AreEqual(6, result.AllFirstCharItems.Count(), "AllFirstCharItems Count");
            Assert.AreEqual(4, result.Found().Count(), "Found Count");
            Assert.AreEqual(2, result.NotFound().Count(), "NotFound Count");
            Assert.AreEqual(3, result.Matching().Count(), "Matching Count");
            Assert.AreEqual(3, result.NotMatching().Count(), "NotMatching Count");

            Assert.AreEqual(1, result.Found().First().Items.Count(), "First Found Item Count");
            Assert.AreEqual(0, result.NotFound().First().Items.Count(), "First NotFound Item Count");
            Assert.AreEqual(1, result.Matching().First().Items.Count(), "First Matching Item Count");
            Assert.AreEqual(2, result.NotMatching().First().Items.Count(), "First NotMatching Item Count");

            Assert.AreEqual('R', result.Found().First().FirstChar, "First Found First Char");
            Assert.AreEqual('e', result.NotFound().First().FirstChar, "First NotFound First Char");
            Assert.AreEqual('R', result.Matching().First().FirstChar, "First Matching First Char");
            Assert.AreEqual('A', result.NotMatching().First().FirstChar, "First NotMatching First Char");
        }
    }

    public class TestGrouping : List<string>, IGrouping<char, string>
    {
        private Func<char> GetKeyFunc;
        public TestGrouping(IEnumerable<string> items, Func<char> getKey) : base(items)
        {
            this.GetKeyFunc = getKey;
        }

        public char Key
        {
            get
            {
                return this.GetKeyFunc.Invoke();
            }
        }
    }
}
