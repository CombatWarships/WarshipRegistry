using Microsoft.AspNetCore.Mvc;
using Serilog;
using WarshipSearchAPI.DTO;
using WarshipService.Processors;

namespace WarshipSearchAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ShipQueryRange : ControllerBase
	{
		private readonly IQueryRangeProcessor _queryRangeProcessor;

		public ShipQueryRange(IQueryRangeProcessor queryRangeProcessor)
		{
			_queryRangeProcessor = queryRangeProcessor;
		}

		[HttpGet(Name = "GetRanges")]
		public async Task<QueryRange> Get()
		{
			Log.Information("Get Range Information");

			return await _queryRangeProcessor.GetRange();

			// TODO: Send through automapper
		}
	}
}