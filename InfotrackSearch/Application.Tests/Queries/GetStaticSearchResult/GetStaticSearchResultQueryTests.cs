using Application.BusinessLogic;
using Application.Queries.GetStaticSearchResult;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace Application.Tests.Queries.GetStaticSearchResult
{
    public class GetStaticSearchResultQueryTests
    {
        private static readonly Fixture Fixture = new Fixture();
        private static readonly CancellationToken CancellationToken = CancellationToken.None;
        public GetStaticSearchResultQueryTests()
        {

        }

        [Fact]
        public async void WhenRequestIsNull_ShouldReturnRequirementListTypeAsNull()
        {
            var query = Fixture.Build<GetStaticSearchResultQuery>()
                .Create();

            var handler = new GetStaticSearchResultQuery.Handler(new StaticSearch(null));
        }
    }
}
