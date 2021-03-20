using Application.Queries.GetSearchResult;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interface
{
    public interface IGoogleSearch
    {
        SearchResultViewModel Search(string searchQuery);
    }
}
