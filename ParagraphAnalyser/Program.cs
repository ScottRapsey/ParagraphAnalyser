using CommandLine;
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


    class Program
    {
        static void Main(string[] args)
        {
            //no point reinventing the wheel by writing our own commandLine parser
            var result = CommandLine.Parser.Default.ParseArguments<Options>(args);
            var exitCode = result
                 .MapResult(
                     options =>
                     {
                         AnalyseAndOutput(options.Paragraph, options.WordCharacters.ToArray(), options.SentenceCharacters.ToArray());
                         return 0;
                     },
                     parseErrors =>
                     {
                         //log the errors away somewhere
                         System.Diagnostics.Debug.WriteLine(parseErrors);
                         return 1;
                     });

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();

            //return exitCode;

            //var text = "This is a paragraph. A sentence? another? Another!  2 spaces. now another sentence. wow! This was a paragraph";
            //var groupedSentences = ParagraphAnalyser.GetSentencesGroupedBySeperators(text, ignoreCase: false);
            //var groupedWords = ParagraphAnalyser.GetWordsGroupedBySeperators(text);

            //var charsWeCareAboutForWords = new[] { 'r', 'e', 'g', 'i', 's', 't' };
            //var wordOutput = OutputGenerator.GetOutputStringForWords(groupedWords, charsWeCareAboutForWords, ignoreCase: true, includeOther: true);
            //Console.WriteLine(wordOutput);

            //var charsWeCareAboutForSentences = new[] { 'n', 'o', 'w' };
            //var paragraphOutput = OutputGenerator.GetOutputStringForSentences(groupedSentences, charsWeCareAboutForSentences, ignoreCase: true, includeOther: true);
            //Console.WriteLine(paragraphOutput);

            //Console.WriteLine("Press any key to exit");
            //Console.ReadLine();
        }

        private static void AnalyseAndOutput(string paragraph, char[] charsWeCareAboutForWords, char[] charsWeCareAboutForSentences)
        {
            var groupedSentences = ParagraphAnalyser.GetSentencesGroupedBySeperators(paragraph, ignoreCase: false);
            var groupedWords = ParagraphAnalyser.GetWordsGroupedBySeperators(paragraph);


            var wordOutput = OutputGenerator.GetOutputStringForWords(groupedWords, charsWeCareAboutForWords, ignoreCase: true, includeOther: true);
            Console.WriteLine(wordOutput);

            var paragraphOutput = OutputGenerator.GetOutputStringForSentences(groupedSentences, charsWeCareAboutForSentences, ignoreCase: true, includeOther: true);
            Console.WriteLine(paragraphOutput);
        }

        class Options
        {
            [Option('p', "paragraph", Required = true,
              HelpText = "Paragraph to analyse")]
            public string Paragraph { get; set; }

            [Option('w', "wordcharacters", Required = true, Separator = ',',
              HelpText = "Characters we care about for words")]
            public IEnumerable<char> WordCharacters { get; set; }

            [Option('s', "sentencecharacters", Required = true, Separator = ',',
              HelpText = "Characters we care about for sentences")]
            public IEnumerable<char> SentenceCharacters { get; set; }
        }
    }
}
