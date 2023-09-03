using BetServer.Model;
using Microsoft.EntityFrameworkCore;

namespace BetServer.Data
{
    public class DemoDBContext : DbContext
    {
        public DemoDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Bet> Bets { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
