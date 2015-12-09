using ParagraphAnalyser.Core;
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
            GroupedFirstCharModel groupedItems,
            bool includeOther)
        {
            var result = string.Format("Sentences{0}Letter{1}Quantity{0}", Environment.NewLine, "\t\t\t");
            result = result + GetOutputString(groupedItems, includeOther);
            return result;
        }
        internal static string GetOutputStringForWords(
            GroupedFirstCharModel groupedItems,
            bool includeOther)
        {
            var result = string.Format("Words{0}Letter{1}Quantity{0}", Environment.NewLine, "\t\t\t");
            result = result + GetOutputString(groupedItems, includeOther);
            return result;
        }
        private static string GetOutputString(
            GroupedFirstCharModel groupedItems,
            bool includeOther)
        {
            var result = string.Empty;

            foreach (var matchingItem in groupedItems.OrderedMatchingAndNotFound())
            {
                result = string.Format("{0} {1}{2}{3}{4}", result, matchingItem.FirstChar, "\t\t\t", matchingItem.Items.Count(), Environment.NewLine);
            }

            if (includeOther)
            {
                //The spec is unclear if Other should be
                //Count of unique characters that words start with that are not in the set we care about
                //or
                //The count of words starting with a character other than the ones we care about

                //so lets do both

                //Count of unique characters that words start with that are not in the set we care about
                result = result + "Other Unique Chars\t\t" + groupedItems.NotMatching().Count() + Environment.NewLine;
                //The count of words starting with a character other than the ones we care about
                result = result + "Sum of Other Unique\t" + groupedItems.SumOfNotMatching() + Environment.NewLine;
            }

            //The spec is unclear if the Total should be
            //Total words detected that start with a character we care about
            //or
            //Total unique characters detected that start words
            //or
            //Total words detected 

            result = result + "Total That Match\t" + groupedItems.SumOfMatching() + Environment.NewLine;
            result = result + "Total Unique Chars\t" + groupedItems.Found().Count() + Environment.NewLine;
            result = result + "Total All Words\t" + groupedItems.SumOfFound() + Environment.NewLine;

            return result;
        }
    }

}
