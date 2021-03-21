﻿using Application.Queries.GetStaticSearchResult;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interface
{
    public interface IBingSearch
    {
        SearchResultViewModel Search(string searchQuery);
    }
}
