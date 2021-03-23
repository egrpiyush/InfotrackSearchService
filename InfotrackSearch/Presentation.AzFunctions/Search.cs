using Application.Queries.GetStaticSearchResult;
using AzureFunctionsV2.HttpExtensions.Annotations;
using AzureFunctionsV2.HttpExtensions.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.AzFunctions
{
    public class Search
    {
        private readonly IMediator _mediator;

        public Search(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName(nameof(GetStaticSearchResult))]
        public async Task<IActionResult> GetStaticSearchResult(
            [HttpTrigger(AuthorizationLevel.Function, "get")]
            HttpRequest req,
            [HttpQuery] HttpParam<string> searchFor,
            [HttpQuery] HttpParam<string> searchInUrl,
            [HttpQuery] HttpParam<string> urlOfInterest,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetStaticSearchResultQuery
            {
                SearchFor = searchFor,
                SearchInUrl = searchInUrl,
                UrlOfInterest = urlOfInterest
            });
            return new OkObjectResult(response);
        }
    }
}
