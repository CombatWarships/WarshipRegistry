using ShipDomain;

namespace WarshipRegistryAPI.Warships
{
	public interface IWarshipAPI
	{
		Task<Ship> GetShip(Guid? shipId, int? shiplistKey, string? wikiLink);
	}
}