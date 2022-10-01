using System.Collections.Generic;

namespace DurableFunctionsAggregatorPattern.Models
{
    public interface IVotingCounter
    {
        Dictionary<string, int> Voting { get; set; }

        void Add(string candidate);

        void Reset(string candidate);

        Dictionary<string, int> Get();
    }
}
