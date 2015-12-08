using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParagraphAnalyser
{
    //    4.	Write code to analyse a paragraph of written text.The function should count the number of words and paragraphs that start with certain characters.You can assume the following.
    //      a.All sentences end with one of the following three characters: 
    //          i.  .
    //          ii. ?
    //          iii.    !
    //      b.All words are separated by a single space character
    //
    //    Example output:
    //
    //  Words
    //  Letter  Quantity
    //  r
    //  e
    //  g
    //  i
    //  s
    //  t
    //  Total
    //  Sentences
    //  Letter	Quantity
    //  n	
    //  o	
    //  w	
    //  Other (i.e. not n, o, or w)	
    //  Total	

    internal static class OutputGenerator
    {
        internal static string GetOutputStringForSentences(
            IEnumerable<IGrouping<char, string>> groupedSentences,
            IEnumerable<char> charsWeCareAbout,
            bool ignoreCase,
            bool includeOther)
        {
            var result = string.Format("Sentences{0}Letter{1}Quantity{0}", Environment.NewLine, "\t\t\t");
            result = result + GetOutputString(groupedSentences, charsWeCareAbout, ignoreCase, includeOther);
            return result;
        }
        internal static string GetOutputStringForWords(
            IEnumerable<IGrouping<char, string>> groupedWords,
            IEnumerable<char> charsWeCareAbout,
            bool ignoreCase,
            bool includeOther)
        {
            var result = string.Format("Words{0}Letter{1}Quantity{0}", Environment.NewLine, "\t\t\t");
            result = result + GetOutputString(groupedWords, charsWeCareAbout, ignoreCase, includeOther);
            return result;
        }
        private static string GetOutputString(
            IEnumerable<IGrouping<char, string>> groupedWords,
            IEnumerable<char> charsWeCareAbout,
            bool ignoreCase,
            bool includeOther)
        {
            var result = string.Empty;
            var matchCounter = 0;
            //if this method gets called a lot, move this to a private module level field
            IEqualityComparer<char> charComparer = null;
            if (ignoreCase)
                charComparer = new CharComparerCurrentCultureIgnoreCase();
            else
                charComparer = new CharComparerCurrentCulture();

            foreach (var charWeCareAbout in charsWeCareAbout)
            {
                var group = groupedWords.FirstOrDefault(i => charComparer.Equals(i.Key, charWeCareAbout));
                var count = 0;
                if (group != null)
                {
                    count = group.Count();
                    matchCounter += count;
                }
                result = string.Format("{0} {1}{2}{3}{4}", result, charWeCareAbout, "\t\t\t", count, Environment.NewLine);
            }

            if (includeOther)
            {
                //The spec is unclear if Other should be
                //Count of unique characters that words start with that are not in the set we care about
                //or
                //The count of words starting with a character other than the ones we care about
                var groups = groupedWords.Where(i => !charsWeCareAbout.Contains(i.Key));
                result = result + "Other Unique\t\t" + groups.Count() + Environment.NewLine;
                var sumOfRemaining = groups.Sum(i => i.Count());
                result = result + "Sum of Other Unique\t" + sumOfRemaining + Environment.NewLine;
            }

            //The spec is unclear if the Total should be
            //Total words detected that start with a character we care about
            //or
            //Total unique characters detected that start words
            //or
            //Total words detected 

            result = result + "Total That Match\t" + matchCounter + Environment.NewLine;
            result = result + "Total Unique Chars\t" + groupedWords.Count() + Environment.NewLine;
            result = result + "Total Words\t\t" + groupedWords.Sum(i => i.Count()) + Environment.NewLine;

            return result;
        }
    }

}
