using Application.Interface;
using Application.Queries.GetStaticSearchResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.BusinessLogic
{
    public class StaticSearch : IStaticSearch
    {
        private readonly IStaticSearchService _service;
        private Dictionary<int, SearchResultViewModel> searchDictionary;
        public StaticSearch(IStaticSearchService service)
        {
            _service = service;
            searchDictionary = new Dictionary<int, SearchResultViewModel>();
        }

        public List<int> Search(string searchQuery, string urlOfInterest)
        {
            var keyCount = 1;
            for (int i = 1; i <= 10; i++)
            {
                var pageNumber = (i < 10) ? "0" + i : i.ToString();
                var result = _service.Search(string.Format(@"https://infotrack-tests.infotrack.com.au/Google/Page{0}.html", pageNumber));
                if (string.IsNullOrWhiteSpace(result))
                    break;
                ParseHtml(result.Replace('"', '#'), searchQuery, urlOfInterest, ref keyCount);
            }

            var filteredSearchResult = searchDictionary.Where(p => p.Value.IsInfotrack);
            var retVal = new List<int>();
            foreach (var searchResult in filteredSearchResult)
            {
                retVal.Add(searchResult.Key);
            }
            return retVal;
        }

        private void ParseHtml(string html, string searchQuery, string urlOfInterest, ref int keyCount)
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