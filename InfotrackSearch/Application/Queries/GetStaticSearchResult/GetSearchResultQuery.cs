using Application.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.GetStaticSearchResult
{
    public class GetStaticSearchResultQuery : IRequest<SearchResponse>
    {
        public string SearchTerm { get; set; }
        public string SearchUrl { get; set; }

        public class Handler : IRequestHandler<GetStaticSearchResultQuery, SearchResponse>
        {
            private readonly IStaticSearch _staticSearch;
            public Handler(IStaticSearch staticSearch)
            {
                _staticSearch = staticSearch;
            }
            public async Task<SearchResponse> Handle(GetStaticSearchResultQuery request, CancellationToken cancellationToken)
            {
                var response = new SearchResponse();
                var result = _staticSearch.Search(request.SearchTerm);
                response.FoundAt = string.Join(",", result);
                response.IsSignificant = result.Max() <= 50 ? true : false;
                return response;
            }
        }
    }
}
