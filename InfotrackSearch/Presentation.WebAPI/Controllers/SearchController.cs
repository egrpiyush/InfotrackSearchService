using Application.Queries.GetStaticSearchResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ApiControllerBase
    {
        private readonly ILogger<SearchController> _logger;

        public SearchController(ILogger<SearchController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<object> Get()
        {
            var response = await Mediator.Send(new GetStaticSearchResultQuery { SearchTerm = "Online title search" });

            if (response == null)
            {
                return (object)NoContent();
            }

            return response;

            return (object)NoContent();
        }
    }
}
