using HackerNewsAPI.Interfaces;
using HackerNewsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace HackerNewsAPIAzureFunction;

public class StoriesFunction
{
    private readonly IStoriesService _storiesService;

    public StoriesFunction(IStoriesService storiesService)
    {
        _storiesService = storiesService;
    }

    [FunctionName("StoriesFunction")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "stoires/{totalStories}")] HttpRequest req,
        ILogger log,int totalStories)
    {
        log.LogInformation("Request received to get stories from Hacker api.");

        try
        {  

            var response = await _storiesService.GetStories(totalStories);

            log.LogInformation("Sending response.");
            return new OkObjectResult(response);
        }
        catch (Exception e)
        {
            log.LogInformation("Something went wrong {0}", e);
            return new BadRequestObjectResult(e);
        }
    }
}
