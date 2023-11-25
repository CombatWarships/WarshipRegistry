using ServiceBus.Core;

namespace WarshipImport
{
	public class AddWarshipConsumerHost : ServiceBusConsumerHost
	{
		public AddWarshipConsumerHost(IConfiguration configuration, IMessageProcessor messageProcessor)
			: base(configuration.GetConnectionString("AddShipsServiceBus"), "addorupdateships", messageProcessor)
		{
		}
	}
}
