﻿using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs;
using DurableFunctionsAggregatorPattern.Models;

namespace DurableFunctionsAggregatorPattern
{
    public class Functions
    {
        [FunctionName("Counter")]
        public void Counter([EntityTrigger] IDurableEntityContext ctx)
        {
            var vote = ctx.GetState<VotingCounter>() ?? new VotingCounter();
            switch (ctx.OperationName.ToLowerInvariant())
            {
                case "add":
                    var candidateToAdd = ctx.GetInput<string>();
                    vote.Add(candidateToAdd);
                    ctx.SetState(vote);
                    break;
                case "reset":
                    var candidateToReset = ctx.GetInput<string>();
                    vote.Reset(candidateToReset);
                    ctx.SetState(vote);
                    break;
                case "get":
                    ctx.Return(vote);
                    break;
                case "delete":
                    ctx.DeleteState();
                    break;
            }
        }
    }
}
