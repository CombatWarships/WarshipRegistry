using ServiceBus.Core;
using ShipDomain;

namespace WarshipRegistryAPI.Warships
{
	public sealed class AddWarshipAPI : ServiceBusProducer<Ship>, IAddWarshipAPI
	{
		public AddWarshipAPI(string connectionString) :
			base(connectionString, "AddOrUpdateShips")
		{ }

		public Task PostWarship(Ship ship)
		{
			return PostMessage(ship);
		}

		public Task PostWarships(IEnumerable<Ship> ships)
		{
			return PostMessages(ships);
		}
	}
}
