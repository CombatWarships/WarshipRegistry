using Microsoft.EntityFrameworkCore;
using Serilog;
using WarshipRegistryAPI.Nationality;
using Warships.Interfaces;

namespace Warships.Databases
{
    public class NationalityDB : DbContext, INationalityDB
   {
      private readonly IConfiguration _configuration;

      private List<Nationality> _localCache;

      public NationalityDB(IConfiguration configuration)
      {
         _configuration = configuration;
      }

      public DbSet<Nationality> Nationality { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         var connectionString = _configuration["DBConnection"];
         if (string.IsNullOrEmpty(connectionString))
         {
            Log.Error($"Database connection string is NULL");
            return;
         }
         Log.Information("Connecting to SQL");
         optionsBuilder.UseSqlServer(connectionString);
      }

      public List<Nationality> GetFullList()
      {
         if (_localCache == null)
            _localCache = Nationality.ToList();
         return _localCache;
      }
   }
}