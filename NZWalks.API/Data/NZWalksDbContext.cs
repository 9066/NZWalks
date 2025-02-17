using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;
using System.Data;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        //public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options) : base(options)
        //{
        //    // Optional: Add this to get more detailed connection errors
        //    this.Database.GetDbConnection().StateChange += (sender, e) =>
        //    {
        //        if (e.CurrentState == ConnectionState.Broken)
        //        {
        //            Console.WriteLine($"Database connection state changed to: {e.CurrentState}");
        //        }
        //    };
        //}
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
