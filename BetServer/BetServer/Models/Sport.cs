using System.ComponentModel.DataAnnotations;

namespace BetServer.Models
{
    public class Sport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
