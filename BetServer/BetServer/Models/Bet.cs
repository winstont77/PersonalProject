using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BetServer.Models
{
    public class Bet
    {
        public int Id { get; set; }
        public Event Events { get; set; }
        public string BetTeam { get; set; }
    }
}
