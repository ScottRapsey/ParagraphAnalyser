using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParagraphAnalyser.Core
{
    public static class GroupedFirstCharModelExtensions
    {
        public static IEnumerable<FirstCharModel> Matching(this GroupedFirstCharModel item)
        {
            if (item == null) return new List<FirstCharModel>();

            return item.AllFirstCharItems.Where(i => i.Matched);
        }
        public static int SumOfMatching(this GroupedFirstCharModel item)
        {
            if (item == null) return 0;

            return item.Matching().Sum(i => i.Items.Count());
        }

        public static IEnumerable<FirstCharModel> NotMatching(this GroupedFirstCharModel item)
        {
            if (item == null) return new List<FirstCharModel>();

            return item.AllFirstCharItems.Where(i => i.Matched == false);
        }
        public static int SumOfNotMatching(this GroupedFirstCharModel item)
        {
            if (item == null) return 0;

            return item.NotMatching().Sum(i => i.Items.Count());
        }

        public static IEnumerable<FirstCharModel> NotFound(this GroupedFirstCharModel item)
        {
            if (item == null) return new List<FirstCharModel>();

            return item.AllFirstCharItems.Where(i => i.Items.Count() == 0);
        }
        public static int SumOfNotFound(this GroupedFirstCharModel item)
        {
            if (item == null) return 0;

            return item.NotFound().Sum(i => i.Items.Count());
        }

        public static IEnumerable<FirstCharModel> Found(this GroupedFirstCharModel item)
        {
            if (item == null) return new List<FirstCharModel>();

            return item.AllFirstCharItems.Where(i => i.Items.Any());
        }
        public static int SumOfFound(this GroupedFirstCharModel item)
        {
            if (item == null) return 0;

            return item.Found().Sum(i => i.Items.Count());
        }

        public static IEnumerable<FirstCharModel> OrderedMatchingAndNotFound(this GroupedFirstCharModel item)
        {
            if (item == null) return new List<FirstCharModel>();

            var result = new List<FirstCharModel>();
            foreach (var chr in item.CharsWeCareAbout)
            {
                result.Add(item.AllFirstCharItems.First(i => i.FirstChar == chr));
            }
            return result;
        }

    }

    public class AnalyseResultModel
    {
        public GroupedFirstCharModel SentenceGroupedData { get; set; }
        public GroupedFirstCharModel WordGroupedData { get; set; }
    }
    public class GroupedFirstCharModel
    {
        public List<char> CharsWeCareAbout { get; set; } = new List<char>();
        public List<FirstCharModel> AllFirstCharItems { get; set; } = new List<FirstCharModel>();
    }
    public class FirstCharModel
    {
        public bool Matched { get; set; }
        public char FirstChar { get; set; }
        public List<string> Items { get; set; } = new List<string>();
    }

}
