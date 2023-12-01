using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Serilog;
using ShipDomain;
using WarshipSearchAPI.DTO;
using WarshipSearchAPI.Interfaces;

namespace Warships.Databases
{
	public class WarshipDatabase : DbContext, IWarshipDatabase
	{
		private readonly IConfiguration _configuration;

		public WarshipDatabase(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public DbSet<Ship> Ships { get; set; }

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


		public async Task<Ship> Create(Ship ship)
		{
			if (ship == null)
				throw new ArgumentNullException(nameof(ship));

			var existingShip = Ships.Where(s => s.ID == ship.ID).FirstOrDefault();
			if (existingShip != null)
				throw new ArgumentException($"Ship with ID {ship.ID} already exists.");

			EntityEntry<Ship> result = await Ships.AddAsync(ship);

			await SaveChangesAsync();

			return result.Entity;
		}

		public async Task<bool> CreateOrUpdate(Ship ship)
		{
			if (ship == null)
				throw new ArgumentNullException(nameof(ship));

			var existingShip = Ships.Where(s => s.ID == ship.ID).AsNoTracking().FirstOrDefault();

			EntityEntry<Ship> result;
			if (existingShip == null)
				result = await Ships.AddAsync(ship);
			else
				result = Ships.Update(ship);

			await SaveChangesAsync();

			return result.Entity != null;
		}


		public async Task<Ship?> FindShip(Guid? shipId, int? shiplistKey, string? wikiLink)
		{
			if (shipId == null && shiplistKey == null && string.IsNullOrEmpty(wikiLink))
				throw new ArgumentException("No identity provided, all fields are null");

			IQueryable<Ship> linq = Ships;

			if (shipId != null)
				linq = linq.Where(s => s.ID == shipId.Value);
			if (shiplistKey != null)
				linq = linq.Where(s => s.ShiplistKey == shiplistKey);
			if (shipId != null)
				linq = linq.Where(s => string.Equals(s.WikiLink, wikiLink.ToLowerInvariant(), StringComparison.OrdinalIgnoreCase));

			return linq.FirstOrDefault();
		}

		public async Task<IEnumerable<Ship>> Query(ShipQuery query)
		{
			IQueryable<Ship> linq = Ships;

			if (!string.IsNullOrEmpty(query.ClassName))
				linq = linq.Where(s => s.ClassName != null && s.ClassName.Contains(query.ClassName));

			if (!string.IsNullOrEmpty(query.Nation))
				linq = linq.Where(s => s.Nation != null && s.Nation.Contains(query.Nation));

			if (query.MinUnits != null)
				linq = linq.Where(s => s.Units >= query.MinUnits);

			if (query.MaxUnits != null)
				linq = linq.Where(s => s.Units <= query.MaxUnits);


			if (query.MinSpeedIrcwcc != null)
				linq = linq.Where(s => s.SpeedIrcwcc >= query.MinSpeedIrcwcc);

			if (query.MaxSpeedIrcwcc != null)
				linq = linq.Where(s => s.SpeedIrcwcc <= query.MaxSpeedIrcwcc);


			if (query.MinSpeedKnots != null)
				linq = linq.Where(s =>  s.SpeedKnots >= query.MinSpeedKnots);

			if (query.MaxSpeedKnots != null)
				linq = linq.Where(s => s.SpeedKnots <= query.MaxSpeedKnots);


			if (query.MinLength != null)
				linq = linq.Where(s => s.LengthFt >= query.MinLength);

			if (query.MaxLength != null)
				linq = linq.Where(s => s.LengthFt <= query.MaxLength);


			if (query.MinBeam != null)
				linq = linq.Where(s => s.BeamFt >= query.MinBeam);

			if (query.MaxBeam != null)
				linq = linq.Where(s => s.BeamFt <= query.MaxBeam);


			if (query.MinRudders != null)
				linq = linq.Where(s => s.Rudders >= query.MinRudders);

			if (query.MaxRudders != null)
				linq = linq.Where(s => s.Shafts <= query.MaxRudders);


			if (query.MinShafts != null)
				linq = linq.Where(s => s.Shafts >= query.MinShafts);

			if (query.MaxShafts != null)
				linq = linq.Where(s => s.Shafts <= query.MaxShafts);


			if (query.ShiplistKey != null)
				linq = linq.Where(s => s.ShiplistKey == query.ShiplistKey);

			if (query.Skip != null)
				linq = linq.Skip(query.Skip.Value);

			if (query.Take == null)
				query.Take = 25;

			return linq.Take(query.Take.Value).ToList();
		}
	}
}
