# InfotrackSearchService

Architecture is an onion arhitecture with the outermost layer being the Function followed by Application and Domain. (onion arhitecture: https://www.thinktocode.com/2018/08/16/onion-architecture/)

It uses Command Query Repository Pattern (CQRS) (https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs) for sending requests into the application layer.

It is a very simplified Anemic form of Domain Drivern Design (complete DDD implementation can be done if required). (DDD: https://martinfowler.com/tags/domain%20driven%20design.html)

Please make InfotrackSearchService.WebAPIs project as the "startup project" and run.
URL used by service: http://localhost:58947/api/search/

GetStaticSearchResult: this API performs the static search over static search result pages.
http://localhost:58947/api/search/GetStaticSearchResult

GetGoogleSearchResult: this API performs google search.
http://localhost:58947/api/search/GetGoogleSearchResult

Design:
This service is a WebAPI project. For abstraction within Presentation (WebAPI, Azure Functions) and Application layer it uses Command Query Repository Pattern (CQRS). The most important intention behind using CQRS here is to abstract query/command handling from invocation and to keep the function layer clean.

Interfaces:
IStaticSearch: used for performing static search business logic
IGoogleSearch: used for performing google search business logic
IBingSearch: used for performing static search business logic

Arrangements of projects is as follows -

Domain: the inner most layer (standalone)
Common: common project for AutoFac wild card implementation.
Application: This project comprises of the business logic ie. Queries and Commands. Its depends on Domain layer.
Each query in this layer has it's own model classes defined.
Application.Tests: From the perspective of structure, this project is a mirror of Application project. It has unit test classes for queries in the Application project.
Functions/WebAPIs: this is the outmost layer and exposes the required functions.
Validation
For service validation FluentValidation is used. The GetStaticSearchResult function has a validator (GetStaticSearchResultQueryValidator) configured to validate the mandatory fields.
