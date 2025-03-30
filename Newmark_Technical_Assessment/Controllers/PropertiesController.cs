using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newmark_Technical_Assessment.Entity;
using Newmark_Technical_Assessment.Query;
using Newmark_Technical_Assessment.Utility;
using Newtonsoft.Json;

namespace Newmark_Technical_Assessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : Controller
    {
        private readonly IMediator _mediator;

        public PropertiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProperty()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response =  await _mediator.Send(new GetAllPropertyQuery());

            return Ok(response);
        }
    }
}
