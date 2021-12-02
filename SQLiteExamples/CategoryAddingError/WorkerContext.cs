using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CategoryAddingError
{
    public class WorkerContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Occupation> Occupations { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Person> People { get; set; }


        private readonly string ConnectionString;
        public WorkerContext(string dbFullName)
        {
            ConnectionString = $"Filename={dbFullName}";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>().HasData(GetInitCountryEntities());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = optionsBuilder.UseSqlite(ConnectionString);
        }

        private static IList<Country> GetInitCountryEntities()
        {
            string countriesString = "Беларусь Польша Россия Украина";

            List<Country> countries = countriesString
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select((title, i) => new Country() { Id = i + 1, Title = title })
                .ToList();

            return countries;
        }

    }
}
