using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BetServer.Models
{
    public class Event
    {
        public int Id { get; set; }
        public Odds Oddss { get; set; }
        public string AwayTeamName { get; set; }
        public string HomeTeamName { get; set; }
    }
}
