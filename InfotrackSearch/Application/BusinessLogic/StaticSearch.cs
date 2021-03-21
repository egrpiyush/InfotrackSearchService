using Application.Helper;
using Application.Interface;
using Application.ViewModel;
using System.Collections.Generic;
using System.Linq;

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
                HtmlParser.ParseHtml(result.Replace('"', '#'), urlOfInterest, ref keyCount, searchDictionary);
            }

            var filteredSearchResult = searchDictionary.Where(p => p.Value.IsInfotrack);
            var retVal = new List<int>();
            foreach (var searchResult in filteredSearchResult)
            {
                retVal.Add(searchResult.Key);
            }
            return retVal;
        }
    }
}