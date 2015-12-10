using ParagraphAnalyser.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ParagraphAnalyser.Web.WebForms
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                var charsWeCareAboutForSentences = SplitStringToChars(this.charsForSentences.Text);
                var charsWeCareAboutForWords = SplitStringToChars(this.charsForWords.Text);

                Result = ParagraphAnalyser.Core.Analyser.AnalyseParagraph(this.paragraph.Text, charsWeCareAboutForSentences, charsWeCareAboutForWords, ignoreCase: this.ignoreCase.Checked);

            }
        }

        public AnalyseResultModel Result;

        private IEnumerable<char> SplitStringToChars(string value)
        {
            //it turns out a string is an enumerable of chars, so...
            return value.Replace(",", "").Trim().Distinct();
        }
    }
}