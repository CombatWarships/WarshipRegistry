using ShipDomain;

namespace WarshipRegistryAPI.Warships
{
	public interface IAddWarshipAPI
	{
		Task PostWarship(Ship ship);
		Task PostWarships(IEnumerable<Ship> ships);
	}
}