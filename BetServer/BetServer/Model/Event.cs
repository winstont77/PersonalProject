namespace BetServer.Model
{
    public class Event
    {
        public int Id { get; set; }
        public double AwayTeamOdds { get; set; }
        public string AwayTeamName { get; set; }
        public int AwayTeamScore { get; set; } = 0;
        public double HomeTeamOdds { get; set; }
        public string HomeTeamName { get; set; }
        public int HomeTeamScore { get; set; } = 0;
        public string Sports { get; set; }
        public string DateTime { get; set; }
        public bool CloseEvent { get; set; } = false;
        public string WinTeamName { get; set; } = string.Empty;
        public string LoseTeamName { get; set; } = string.Empty;
        public int AwayTeamMoney { get; set; } = 0;
        public int HomeTeamMoney { get; set; } = 0;
        public bool PointStatus { get; set; } = false;
        public ICollection<Bet>? Bets { get; set; }
    }
}
