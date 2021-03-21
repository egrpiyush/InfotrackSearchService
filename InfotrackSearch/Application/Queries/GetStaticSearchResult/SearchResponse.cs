using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.GetStaticSearchResult
{
    public class SearchResponse
    {
        public string FoundAt { get; set; }

        public bool IsSignificant { get; set; }
    }
}
