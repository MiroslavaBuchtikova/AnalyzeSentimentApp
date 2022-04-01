using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Linq;

namespace AnalyzeSentimentApp
{
    using Dtos;

    public static class AnalyzeSentimentFunction
    {
        private static int count = 0;

        private const string baseAddress =
            "https://language.googleapis.com/v1/documents:analyzeSentiment?key=YOUR_KEY";

        [FunctionName("AnalyzeSentimentFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
            HttpRequest req,
            ILogger log)
        {
            count++;
            if (count < 3900)
            {
                var data = await MapRequest(req);

                if (data == null)
                {
                    return new BadRequestObjectResult("Requested data can't be null");
                }


                for (var i = 0; i < data.messages.Count; i++)
                {
                    if (char.IsLetter(data.messages[i].Last()))
                    {
                        data.messages[i] += ".";
                    }
                }

                var messageData = new DocumentDto()
                {
                    document = new AnalyzeTextDto()
                    {
                        content = string.Join(" ", data.messages)
                    }
                };

                var jsonData = JsonConvert.SerializeObject(messageData);
                var contentData = new StringContent(jsonData, Encoding.UTF8, "application/json");


                using var client = new HttpClient();

                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Add(contentType);

                var response = await client.PostAsync(baseAddress, contentData);
                var mappedResponse = MapResponse(response);

                return new OkObjectResult(mappedResponse);
            }

            return new EmptyResult();
        }

        private static async Task<MessageDto> MapRequest(HttpRequest req)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<MessageDto>(requestBody);

            return data;
        }

        private static SentimentResultDto MapResponse(HttpResponseMessage response)
        {
            var result = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<SentimentResultDto>(result);
        }
    }
}