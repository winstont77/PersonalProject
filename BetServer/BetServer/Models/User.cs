using System.ComponentModel.DataAnnotations;

namespace BetServer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public ICollection<Bet> Bets { get; set; }
    }
}
