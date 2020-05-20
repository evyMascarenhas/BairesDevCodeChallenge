using BairesDev.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;



namespace BairesDev.Data
{

    public class MainDataContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<RolesWithPriorities> RolesWithPriorities { get; set; }

        public DbSet<Country> Countries { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=BairesDevDb;Trusted_Connection=True;");

        }
    }

}
