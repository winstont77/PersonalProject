using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BetServer.Models
{
    public class Bet
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string DateTime { get; set; } 
        public int Money { get; set; }
        public string AwayTeamName { get; set; }
        public string HomeTeamName { get; set; }
        public float AwayTeamOdds { get; set; }
        public float HomeTeamOdds { get; set; }
        public string Sports { get; set; }
        public string BetTeamName { get; set; }
        public float BetTeamOdds { get;set; }
        public bool BetStatus { get; set; } = false;
    }
}
