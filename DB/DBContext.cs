using DoItAllList_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetAuthentication.DB
{
    public class DIALDBContext : DbContext
    {

        //entities
        // public DbSet<User> Users { get; set; }
        // public DbSet<LocalListt> LocalList { get; set; }        

        // public DbSet<LocalListItem> LocalListItem { get; set; }

        // public DIALDBContext(DbContextOptions<DIALDBContext> options): base(options)
        // {

        // }
        

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<User>().HasKey(u => new {
        //         u.UserId
        //     });

        //     modelBuilder.Entity<Player>().HasKey(p => new {
        //         p.Player_key
        //     });
        //     modelBuilder.Entity<Team>().HasKey(t => new
        //     {
        //         t.TeamName
        //     });

        //     modelBuilder.Entity<PlayerSelection>().HasKey(p => new
        //     {
        //         p.TeamName,
        //         p.Player_key,
        //         p.UserId
        //     });

        //     modelBuilder.Entity<ColumnHeaders>().HasNoKey();

        //     modelBuilder.Entity<DtrScores>().HasNoKey();
        // }
    }
}
