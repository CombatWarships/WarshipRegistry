using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using ShipDomain;
using System.Text.Json;
using WarshipRegistryAPI.Warships;
using WarshipSearchAPI.DTO;
using WarshipSearchAPI.Interfaces;

namespace WarshipSearchAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class SearchWarshipsController : ControllerBase
	{
		private readonly IWarshipDatabase _database;

		public SearchWarshipsController(IWarshipDatabase database)
		{
			_database = database;
		}

		[HttpPost(Name = "Search")]
		public async Task<IEnumerable<Ship>> Search(ShipQuery query)
		{
			string jsonString = JsonSerializer.Serialize(query);
			using (LogContext.PushProperty("Query", jsonString))
			{
				Log.Information("Search Request");
				
				var result = await _database.Query(query);

				Log.Information($"{result.Count()} results were found.");

				// TODO: Send through automapper
				return result;
			}
		}
	}
}