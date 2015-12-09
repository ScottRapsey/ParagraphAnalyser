using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParagraphAnalyser.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Results(string paragraph, string charsForSentences, string charsForWords, bool ignoreCase)
        {
            var charsWeCareAboutForSentences = SplitStringToChars(charsForSentences);
            var charsWeCareAboutForWords = SplitStringToChars(charsForWords);

            var result = ParagraphAnalyser.Core.Analyser.AnalyseParagraph(paragraph, charsWeCareAboutForSentences, charsWeCareAboutForWords, ignoreCase);

            return View(result);
        }
        private IEnumerable<char> SplitStringToChars(string value)
        {
            var splitString = value.Split(new[] { ',' }, options: StringSplitOptions.RemoveEmptyEntries);
            return splitString.Select(i => i.First()).Distinct();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}