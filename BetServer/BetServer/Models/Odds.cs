using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BetServer.Models
{
    public class Odds
    {
        public int Id { get; set; }
        public float AwayTeamOdds { get; set; }
        public float HomeTeamOdds { get; set; }
        public Sport Sports { get; set; }
    }
}
