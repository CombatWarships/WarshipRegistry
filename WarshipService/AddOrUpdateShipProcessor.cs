using Serilog.Context;
using Serilog;
using ServiceBus.Core;
using System.Text.Json;
using Warships.Processors;
using WarshipSearchAPI.Interfaces;
using ShipDomain;

namespace WarshipEnrichment
{

	public class AddOrUpdateShipProcessor : IMessageProcessor
	{
		private readonly IServiceProvider _serviceProvider;

		public AddOrUpdateShipProcessor(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public async Task ProcessMessage(string message)
		{
			using (var scope = _serviceProvider.CreateScope())
			{
				var db = scope.ServiceProvider.GetService<IWarshipDatabase>();
				var warshipProcessor = new WarshipProcessor(db);

				using (LogContext.PushProperty("MessageJSON", message))
				{
					var ship = JsonSerializer.Deserialize<Ship>(message);

					Log.Information("Adding ship to database");

					await warshipProcessor.CreateOrUpdateShip(new Ship[] { ship });
				}
			}
		}
	}
}