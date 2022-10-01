using System.Collections.Generic;
using Newtonsoft.Json;

namespace DurableFunctionsAggregatorPattern.Models
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class VotingCounter : IVotingCounter
    {
        [JsonProperty("voting")]
        public Dictionary<string, int> Voting { get; set; } = new();

        public void Add(string candidate)
        {
            if (this.Voting.ContainsKey(candidate))
            {
                this.Voting[candidate]++;
            }
            else
            {
                this.Voting.Add(candidate, 1);
            }
        }

        public void Reset(string candidate) => this.Voting[candidate] = 0;

        public Dictionary<string, int> Get() => this.Voting;
    }
}
