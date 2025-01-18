using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public static class ProcessDataFunction
{
    [FunctionName("ProcessDataFunction")]
    public static IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("Processing HTTP request in C#.");

        string requestBody = new StreamReader(req.Body).ReadToEnd();
        dynamic data = JsonConvert.DeserializeObject(requestBody);

        string name = data?.name;
        int? age = data?.age;

        if (!string.IsNullOrEmpty(name) && age.HasValue)
        {
            return new OkObjectResult(new
            {
                message = $"Hello {name}, you are {age} years old!"
            });
        }
        else
        {
            return new BadRequestObjectResult("Invalid input. Please provide 'name' and 'age'.");
        }
    }
}
