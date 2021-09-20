using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task5.Models
{
    public class RulesContext : DbContext
    {
        public DbSet<RulesModel> Rules { get; set; }
        public RulesContext(DbContextOptions<RulesContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
