using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json;
using WarshipImport.Interfaces;
using WarshipRegistryAPI.Classification;

namespace Warships.Databases
{
    public class WarshipClassificationDB : DbContext, IWarshipClassificationDB
    {
        private readonly IConfiguration _configuration;

        private List<WarshipClassification> _localCache;

        public WarshipClassificationDB(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<WarshipClassification> WarshipClassification { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DBConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                Log.Error($"Database connection string is NULL");
                return;
            }
            Log.Information("Connecting to SQL");
            optionsBuilder.UseSqlServer(connectionString);
        }

        public List<WarshipClassification> GetFullList()
        {
            if (_localCache == null)
                _localCache = WarshipClassification.ToList();
            return _localCache;
        }
    }
}
