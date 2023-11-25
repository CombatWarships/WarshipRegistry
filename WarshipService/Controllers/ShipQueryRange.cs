using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog.Context;
using Serilog;
using WarshipSearchAPI.DTO;
using WarshipSearchAPI.Interfaces;

namespace WarshipSearchAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ShipQueryRange : ControllerBase
	{
		//private readonly IWarshipDatabase _database;

		public ShipQueryRange()//IWarshipDatabase database)
		{
		//	_database = database;
		}

		[HttpGet(Name = "GetRanges")]
		public QueryRange Get()
		{
			Log.Information("Get Range Information");
			//var result = _database.Query(query);
			//Log.Information($"{result.Count()} results were found.");

			// TODO: Send through automapper
			return new QueryRange
			{
				MinUnits = 1,
				MaxUnits = 8,
				MinSpeedIrcwcc = 23,
				MaxSpeedIrcwcc = 34,
				MinSpeedKnots = 22,
				MaxSpeedKnots = 50,
				MinLength = 100,
				MaxLength = 900,
				MinBeam = 20,
				MaxBeam = 150
			};
		}
	}
}