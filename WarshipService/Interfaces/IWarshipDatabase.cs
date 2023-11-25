using ShipDomain;
using WarshipSearchAPI.DTO;

namespace WarshipSearchAPI.Interfaces
{
	public interface IWarshipDatabase
	{
		Task<IEnumerable<Ship>> Query(ShipQuery query);
		Task<Ship?> FindShip(Guid? shipId = null, int? shiplistKey = null, string? wikiLink = null);

		Task<Ship> Create(Ship ship);
		Task<bool> CreateOrUpdate(Ship ship);
	}
}