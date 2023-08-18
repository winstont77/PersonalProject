using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetServer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Money { get; set; }
        public virtual ICollection<Bet>? Bets { get; set; }  = null;
    }
}
