using System.Net.Http;
using System.Threading.Tasks;
using DurableFunctionsAggregatorPattern.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace DurableFunctionsAggregatorPattern
{
    public class Aggregator
    {
        [FunctionName("QueueTrigger")]
        public async Task Run(
            [QueueTrigger(Constants.Queue)] string candidate,
            [DurableClient] IDurableEntityClient entityClient)
        {
            var entityId = new EntityId(Constants.Entity, Constants.Entity);
            await entityClient.SignalEntityAsync(entityId, "add", candidate);
        }

        [FunctionName("HTTPTrigger")]
        public async Task<IActionResult> GetEntities(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestMessage req,
            [DurableClient] IDurableEntityClient entityClient)
        {
            var entity =
                await entityClient.ReadEntityStateAsync<VotingCounter>(new EntityId(Constants.Entity,
                    Constants.Entity));

            return (ActionResult) new OkObjectResult(entity);
        }
    }
}