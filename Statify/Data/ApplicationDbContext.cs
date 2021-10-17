using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StatTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Statify.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<GameDetail> GameDetails {get; set;}
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
