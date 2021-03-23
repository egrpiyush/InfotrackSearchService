using Application.Queries.GetGoogleSearchResult;
using Application.Queries.GetStaticSearchResult;
using Application.ViewModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.WebAPIs.Controllers
{
    [ApiController]
    public class SearchController : ApiControllerBase
    {
        private readonly ILogger<SearchController> _logger;

        public SearchController(ILogger<SearchController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<SearchResponse> GetStaticSearchResult(string searchFor, string searchInUrl, string urlOfInterest)
        {
            var response = await Mediator.Send(new GetStaticSearchResultQuery
            {
                SearchFor = searchFor,
                SearchInUrl = searchInUrl,
                UrlOfInterest = urlOfInterest
            });
            return response;
        }

        [HttpGet]
        public async Task<SearchResponse> GetGoogleSearchResult([FromBody] GetGoogleSearchResultQuery query)
        {
            var response = await Mediator.Send(query);
            return response;
        }
    }
}
