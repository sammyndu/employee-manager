using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EManager3.Areas.Identity.Data;
using EManager3.Models;

namespace EManager3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<EManager3User> EManager3User { get; set; }
        public DbSet<Subordinates> Subordinates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
                optionsBuilder.UseSqlite(@"workstation id=emanager.mssql.somee.com;packet size=4096;user id=samndu_SQLLogin_1;pwd=vy1h4gaewo;MultipleActiveResultSets=true;data source=emanager.mssql.somee.com;persist security info=False;initial catalog=emanager");
            }
        }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Write Fluent API configurations here
    
            modelBuilder.Entity<Subordinates>()
                        .HasOne(e => e.Users)
                        .WithMany(e => e.Subordinates)
                        .HasForeignKey(e => e.UserId)
                        .OnDelete(DeleteBehavior.Cascade);

        }
    
    }
}
