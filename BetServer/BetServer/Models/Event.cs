using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BetServer.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string AwayTeamName { get; set; }
        public string HomeTeamName { get; set; }
        public float AwayTeamOdds { get; set; }
        public float HomeTeamOdds { get; set; }
        public float AwayTeamPoint { get; set; }
        public float HomeTeamPoint { get; set; }
        public string Sports { get; set; }
        public string DateTime { get; set; }
        public bool CloseEvent { get; set; } = false;
        public string WinTeamName { get; set; } = string.Empty;
        public string LoseTeamName { get; set; } = string.Empty;
        public int? AwayTeamScore { get; set; }
        public int? HomeTeamScore { get; set; }
        public int AwayTeamMoney { get; set; } = 0;
        public int HomeTeamMoney { get; set; } = 0;
        public bool? Point { get; set; } = false;
    }
}
