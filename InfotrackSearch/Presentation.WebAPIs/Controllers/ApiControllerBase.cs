using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;


namespace Presentation.WebAPIs.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        private ISender _mediator;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
    }
}
