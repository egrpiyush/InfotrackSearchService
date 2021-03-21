using Application.Helper;
using Application.Interface;
using Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.BusinessLogic
{
    public class GoogleSearch : IGoogleSearch
    {
        private readonly IGoogleSearchService _service;
        private Dictionary<int, SearchResultViewModel> searchDictionary;
        public GoogleSearch(IGoogleSearchService service)
        {
            _service = service;
            searchDictionary = new Dictionary<int, SearchResultViewModel>();
        }

        public List<int> Search(string searchFor, string urlOfInterest)
        {
            var keyCount = 1;
            for (int i = 1; i <= 10; i++)
            {
                var pageNumber = (i < 10) ? "0" + i : i.ToString();
                var result = _service.Search(searchFor, i);
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
