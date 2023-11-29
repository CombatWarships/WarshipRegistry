using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Serilog;
using Serilog.Context;
using ShipDomain;
using System.Text.Json;
using WarshipRegistryAPI.Warships;
using WarshipSearchAPI.DTO;
using WarshipSearchAPI.Interfaces;
using WarshipService.Processors;

namespace WarshipSearchAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class SearchWarshipsController : ControllerBase
	{
		private readonly IWarshipDatabase _database;
		private readonly IQueryRangeProcessor _queryRangeProcessor;

		public SearchWarshipsController(IWarshipDatabase database, IQueryRangeProcessor queryRangeProcessor)
		{
			_database = database;
			_queryRangeProcessor = queryRangeProcessor;
		}

		[HttpPost(Name = "Search")]
		public async Task<IEnumerable<Ship>> Search(ShipQuery query)
		{
			string jsonString = JsonSerializer.Serialize(query);
			using (LogContext.PushProperty("Query", jsonString))
			{
				Log.Information("Search Request");

				query = _queryRangeProcessor.RemoveFullRangeQueryConstraints(query);

				Log.Information("Removed full range values from query");

				
				var result = await _database.Query(query);

				Log.Information($"{result.Count()} results were found.");

				// TODO: Send through automapper
				return result;
			}
		}

	}
}