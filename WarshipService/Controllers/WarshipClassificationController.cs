using Microsoft.AspNetCore.Mvc;
using Serilog;
using WarshipImport.Interfaces;
using WarshipRegistryAPI.Classification;

namespace WarshipImport.Controllers
{
    [ApiController]
	[Route("api/[controller]")]
	public class WarshipClassificationController : ControllerBase
	{
		private readonly IWarshipClassificationDB _warshipClassificationDB;

		public WarshipClassificationController(IWarshipClassificationDB warshipClassificationDB)
		{
			_warshipClassificationDB = warshipClassificationDB;
		}

		[HttpGet]
		public async Task<List<WarshipClassification>> Get()
		{
			Log.Information($"Retrieving nationality list.");

			var list = _warshipClassificationDB.GetFullList() ?? new List<WarshipClassification>();

			Log.Information($"Returned {list.Count} nationalities");

			return list;
		}
	}
}