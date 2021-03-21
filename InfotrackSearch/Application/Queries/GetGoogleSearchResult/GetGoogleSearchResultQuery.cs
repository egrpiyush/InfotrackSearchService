using Application.Interface;
using Application.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.GetGoogleSearchResult
{
    public class GetGoogleSearchResultQuery : IRequest<SearchResponse>
    {
        public string SearchFor { get; set; }
        public string SearchInUrl { get; set; }
        public string UrlOfInterest { get; set; }

        public class Handler : IRequestHandler<GetGoogleSearchResultQuery, SearchResponse>
        {
            private readonly IGoogleSearch _search;
            public Handler(IGoogleSearch search)
            {
                _search = search;
            }

            public async Task<SearchResponse> Handle(GetGoogleSearchResultQuery request, CancellationToken cancellationToken)
            {
                var response = new SearchResponse();
                var result = _search.Search(request.SearchFor, request.UrlOfInterest);
                if (result == null || !result.Any())
                    return response;
                response.FoundAt = string.Join(",", result);
                response.IsSignificant = result.Max() <= 50;
                return response;
            }
        }
    }
}
