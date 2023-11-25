using Microsoft.AspNetCore.Mvc;
using Serilog;
using WarshipRegistryAPI.Nationality;
using Warships.Interfaces;

namespace Warships.Controllers
{
    [ApiController]
	[Route("api/[controller]")]
	public class NationalityController : ControllerBase
	{
		private readonly INationalityDB _nationalityDB;

		public NationalityController(INationalityDB nationalityDB)
		{
			_nationalityDB = nationalityDB;
		}

		[HttpGet]
		public async Task<List<Nationality>> Get()
		{
			Log.Information($"Retrieving nationality list.");

			var list = _nationalityDB.GetFullList() ?? new List<Nationality>();

			Log.Information($"Returned {list.Count} nationalities");

			return list;
		}
	}
}