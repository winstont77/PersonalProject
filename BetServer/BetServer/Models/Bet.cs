using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BetServer.Models
{
    public class Bet
    {
        public int Id { get; set; }
        public Event? Events { get; set; } = null;
        public string DateTime { get; set; } 
        public int Money { get; set; }

    }
}
