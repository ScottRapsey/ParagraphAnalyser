using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParagraphAnalyser.Core
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

    public static class Analyser
    {
        //our word seperators are provided in the spec, so no need to allow a user to enter them
        private static readonly char[] wordSeperators = new[] { ' ' };
        //our sentence seperators are provided in the spec, so no need to allow a user to enter them
        private static readonly char[] sentenceSeperators = new[] { '.', '?', '!' };

        public static IEnumerable<IGrouping<char, string>> GetSentencesGroupedBySeperators(
            string paragraph,
            bool ignoreCase = true) //no mention in the spec of whether this should be case sensitive or not, so make it an option
        {
            return GetItemsGroupedBySeperators(paragraph, sentenceSeperators, ignoreCase);
        }
        public static IEnumerable<IGrouping<char, string>> GetWordsGroupedBySeperators(
            string paragraph,
            bool ignoreCase = true) //no mention in the spec of whether this should be case sensitive or not
        {
            return GetItemsGroupedBySeperators(paragraph, wordSeperators, ignoreCase);
        }
        private static IEnumerable<IGrouping<char, string>> GetItemsGroupedBySeperators(
            string paragraph,
            char[] seperators,
            bool ignoreCase)
        {
            //when processing sentences, this will return the last sentence wether it ends in a "." a "?" or a "!", 
            //it doesn't matter what it ends with it will be included.
            //the sentence ending part of the spec was an assurance rather than a requirement, so I think this is fair

            //split the paragraph on space
            var eachItem = paragraph.Split(seperators, options: StringSplitOptions.RemoveEmptyEntries).AsEnumerable();
            //clean up a bit, make sure we don't have any leading or trailing spaces
            eachItem = eachItem.Select(i => i.Trim());

            //if this method gets called a lot, move both charComparers to a private module level fields and access the correct one as required rather than instantiting a new one each time
            IEqualityComparer<char> charComparer = null;
            if (ignoreCase)
                charComparer = new CharComparerCurrentCultureIgnoreCase();
            else
                charComparer = new CharComparerCurrentCulture();

            //groupby the first character and return the result
            return eachItem.GroupBy(i => i.First(), comparer: charComparer);
        }
    }
}
