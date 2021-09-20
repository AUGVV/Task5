using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task5.Models
{
    public class GameContext : DbContext
    {
        public DbSet<GameModel> Games { get; set; }
        public GameContext(DbContextOptions<GameContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
