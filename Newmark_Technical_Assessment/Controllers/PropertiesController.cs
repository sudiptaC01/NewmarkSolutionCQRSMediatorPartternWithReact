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

        //private readonly BlobDetailsAppSettings _blobDetailsAppSettings;
        //public PropertiesController(IOptions<BlobDetailsAppSettings> blobDetailsAppSettings)
        //{
        //    _blobDetailsAppSettings = blobDetailsAppSettings.Value;
        //}

        [HttpGet]
        public async Task<ActionResult> GetAllProperty()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response =  await _mediator.Send(new GetAllPropertyQuery());

            return Ok(response);


            ////string sasUrl = "https://nmrkpidev.blob.core.windows.net/dev-test/dev-test.json?sp=r&st=2024-10-28T10:35:48Z&se=2025-10-28T18:35:48Z&spr=https&sv=2022-11-02&sr=b&sig=bdeoPWtefikVgUGFCUs4ihsl22ZhQGu4%2B4cAfoMwd4k%3D";
            ////string sasUrl = "https://nmrkpidev.blob.core.windows.net/dev-test1.json";

            //string sasUrl = string.Concat(_blobDetailsAppSettings.BlobURL, _blobDetailsAppSettings.SASToken);
            //string jsonContent = string.Empty;
            //List<PropertyDetails>? property = null;

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //try
            //{


            //    using (HttpClient client = new HttpClient())
            //    {
            //        HttpResponseMessage response = await client.GetAsync(sasUrl);

            //        switch (Convert.ToString(response.StatusCode))
            //        {
            //            case "Forbidden": return Unauthorized(new { Message = response.ReasonPhrase });

            //            case "NotFound": return NotFound(new { Message = response.ReasonPhrase });

            //            case "OK":
            //                jsonContent = await response.Content.ReadAsStringAsync();
            //                property = JsonConvert.DeserializeObject<List<PropertyDetails>>(jsonContent);
            //                return Ok(property);

            //            default:
            //                return BadRequest(response.ReasonPhrase);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}


        }
    }
}
