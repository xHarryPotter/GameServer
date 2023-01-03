using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using GameServer.Database.Models;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using GameServer.Module.Faction.Models;

namespace GameServer.Database
{
    public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Bans> Bans { get; set; }
        public virtual DbSet<FactionModel> Factions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql($"Server=localhost;Database=gameserver;User=root;Password=aabbccdd;",
                     Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.1.37-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
