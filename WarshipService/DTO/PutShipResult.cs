using ShipDomain;
using WarshipRegistryAPI.Warships;

namespace Warships.DTO
{
	public class PutShipResult
    {
        public List<Ship> ImportedShips { get; } = new List<Ship>();

        public List<Ship> FailedShips { get; } = new List<Ship>();
    }
}