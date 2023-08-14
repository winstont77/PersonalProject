using BetServer.Models;
using Microsoft.EntityFrameworkCore;

namespace BetServer.Data
{
    public class DemoDBContext:DbContext
    {
        public DemoDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Bet> Bets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}
