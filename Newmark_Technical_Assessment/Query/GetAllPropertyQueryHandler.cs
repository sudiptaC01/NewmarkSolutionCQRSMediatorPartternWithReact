using MediatR;
using Microsoft.Extensions.Options;
using Newmark_Technical_Assessment.Entity;
using Newmark_Technical_Assessment.Utility;
using Newtonsoft.Json;

namespace Newmark_Technical_Assessment.Query
{
    public class GetAllPropertyQueryHandler : IRequestHandler<GetAllPropertyQuery, List<PropertyDetails>>
    {
        private readonly BlobDetailsAppSettings _blobDetailsAppSettings;
        public GetAllPropertyQueryHandler(IOptions<BlobDetailsAppSettings> blobDetailsAppSettings)
        {
            _blobDetailsAppSettings = blobDetailsAppSettings.Value;
        }

        public async Task<List<PropertyDetails>> Handle(GetAllPropertyQuery request, CancellationToken cancellationToken)
        {
            string sasUrl = string.Concat(_blobDetailsAppSettings.BlobURL, _blobDetailsAppSettings.SASToken);
            string jsonContent = string.Empty;
            List<PropertyDetails>? property = null;

            //try
            //{
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(sasUrl);

                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {
                        jsonContent = await response.Content.ReadAsStringAsync();
                        property = JsonConvert.DeserializeObject<List<PropertyDetails>>(jsonContent);
                    }
                }
            //}
            //catch (Exception)
            //{
            //    throw;
            //}

            return property;

        }
    }
}
