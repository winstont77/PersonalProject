using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BetServer.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
