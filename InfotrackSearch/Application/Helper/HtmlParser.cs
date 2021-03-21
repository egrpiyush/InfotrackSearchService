using Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.Helper
{
    public static class HtmlParser
    {
        public static void ParseHtml(string html, string urlOfInterest, ref int keyCount, Dictionary<int, SearchResultViewModel> searchDictionary)
        {
            var matches = Regex.Matches(html, @"(<li class=#ads-fr# data-bg=#1#>(.+?)<\/li>|<!--m-->(.+?)<!--n-->)");
            var pattern = urlOfInterest;
            foreach (var match in matches)
            {
                searchDictionary.Add(keyCount, new SearchResultViewModel
                {
                    IsInfotrack = Regex.IsMatch(match.ToString(), pattern)
                });
                keyCount++;
            }
        }
    }
}
