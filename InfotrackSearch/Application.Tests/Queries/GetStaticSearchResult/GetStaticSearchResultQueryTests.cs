using Application.BusinessLogic;
using Application.Interface;
using Application.Queries.GetStaticSearchResult;
using AutoFixture;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        public async void WhenServiceResultIsBlank_FoundAtShouldBeNullOrEmpty()
        {
            //Arrange
            var query = Fixture.Build<GetStaticSearchResultQuery>()
                .Create();
            var serviceResult = "";
            var serviceMock = Fixture.Freeze<Mock<IStaticSearchService>>();
            serviceMock.Setup(f => f.Search(It.IsAny<string>()))
                .Returns(serviceResult);
            var handler = new GetStaticSearchResultQuery.Handler(new StaticSearch(serviceMock.Object));
            //Act
            var result = await handler.Handle(query, CancellationToken);
            //Assert
            result.FoundAt.ShouldBeNullOrEmpty();
        }

        [Fact]
        public async void WhenServiceResultIsNotBlank_FoundAtShouldNotBeNullOrEmpty()
        {
            //Arrange
            var urlOfInterest = @"https://www.infotrack.com.au";
            var query = Fixture.Build<GetStaticSearchResultQuery>()
                .With(p => p.UrlOfInterest, urlOfInterest)
                .Create();
            var serviceResult = "<li class=#ads-fr# data-bg=#1#>" + urlOfInterest + "</li>";
            var serviceMock = Fixture.Freeze<Mock<IStaticSearchService>>();
            serviceMock.Setup(f => f.Search(It.IsAny<string>()))
                .Returns(serviceResult);
            var handler = new GetStaticSearchResultQuery.Handler(new StaticSearch(serviceMock.Object));
            //Act
            var result = await handler.Handle(query, CancellationToken);
            //Assert
            result.FoundAt.ShouldNotBeNullOrEmpty();
        }

        [Fact]
        public async void WhenServiceResultDoesNotContainUrlOfInterest_FoundAtShouldBeNullOrEmpty()
        {
            //Arrange
            var urlOfInterest = @"https://www.infotrack.com.au";
            var query = Fixture.Build<GetStaticSearchResultQuery>()
                .With(p => p.UrlOfInterest, urlOfInterest)
                .Create();
            var serviceResult = "<li class=#ads-fr# data-bg=#1#>"+ "abcd" + "</li>";
            var serviceMock = Fixture.Freeze<Mock<IStaticSearchService>>();
            serviceMock.Setup(f => f.Search(It.IsAny<string>()))
                .Returns(serviceResult);
            var handler = new GetStaticSearchResultQuery.Handler(new StaticSearch(serviceMock.Object));
            //Act
            var result = await handler.Handle(query, CancellationToken);
            //Assert
            result.FoundAt.ShouldBeNullOrEmpty();
        }

        [Fact]
        public async void WhenUrlOfInterestIsFoundInFirst50Results_IsSignificantShouldBeTrue()
        {
            //Arrange
            var urlOfInterest = @"https://www.infotrack.com.au";
            var query = Fixture.Build<GetStaticSearchResultQuery>()
                .With(p => p.UrlOfInterest, urlOfInterest)
                .Create();
            var serviceResult = "<li class=#ads-fr# data-bg=#1#>" + urlOfInterest + "</li>";
            var serviceMock = Fixture.Freeze<Mock<IStaticSearchService>>();
            serviceMock.Setup(f => f.Search(It.IsAny<string>()))
                .Returns(serviceResult);
            var handler = new GetStaticSearchResultQuery.Handler(new StaticSearch(serviceMock.Object));
            //Act
            var result = await handler.Handle(query, CancellationToken);
            //Assert
            result.IsSignificant.ShouldBe(true);
        }
    }
}
