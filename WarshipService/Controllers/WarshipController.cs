using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using ShipDomain;
using System.Reflection;
using System.Text.Json;
using Warships.DTO;
using WarshipSearchAPI.Interfaces;

namespace WarshipSearchAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class WarshipController : ControllerBase
	{
		private readonly IWarshipDatabase _database;

		public WarshipController(IWarshipDatabase database)
		{
			_database = database;
		}


		[HttpGet]
		public async Task<Ship> GetShip(Guid? shipId, int? shiplistKey, string? wikiLink)
		{
			using (LogContext.PushProperty("Requested Ship ID", shipId))
			{
				Log.Information("Get Ship");

				var result = await _database.FindShip(shipId, shiplistKey, wikiLink);

				if (result == null)
					Log.Information($"Ship was not found.");
				else
					Log.Information($"Ship found and returned.");

				// TODO: Send through automapper
				return result;
			}
		}

		[HttpPut(Name = "Put")]
		public async Task<PutShipResult> PutShip(Ship[] newShips)
		{
			string jsonString = JsonSerializer.Serialize(newShips);
			using (LogContext.PushProperty("PutShip", jsonString))
			{
				Log.Information("Put Ship called");

				var result = new PutShipResult();

				foreach (Ship ship in newShips)
				{
					using (LogContext.PushProperty("Ship", ship))
					{
						try
						{
							if (ship.ShiplistKey == null)
							{
								result.FailedShips.Add(ship);
								continue;
							}

							var existingShip = await _database.FindShip(shiplistKey: ship.ShiplistKey);

							if (existingShip == null)
							{
								var createdShip = await _database.Create(ship);
								if (createdShip != null)
									result.ImportedShips.Add(createdShip);
							}
							else
							{
								bool isMatch = true;
								bool needsUpdated = false;
								var properties = typeof(Ship).GetProperties(BindingFlags.Public | BindingFlags.Instance);
								foreach (var property in properties)
								{
									var existingValue = property.GetValue(existingShip);
									var newValue = property.GetValue(ship);

									if (IsDefault(newValue))
										continue;

									if (IsDefault(existingValue))
									{
										property.SetValue(existingShip, newValue);
										needsUpdated = true;
										continue;
									}

									if (newValue.Equals(existingValue))
										continue;

									isMatch = false;
									break;
								}

								if (needsUpdated)
									isMatch = await _database.CreateOrUpdate(existingShip);

								if (isMatch)
									result.ImportedShips.Add(ship);
								else
									result.FailedShips.Add(ship);
							}
						}
						catch (Exception ex)
						{
							Log.Error(ex, "Error importing ship");
							result.FailedShips.Add(ship);
						}
					}
				}

				Log.Information($"Ship add complete Imported: {result.ImportedShips.Count} - Failed: {result.FailedShips.Count}");

				return result;
			}
		}


		private static bool IsDefault(object? value)
		{
			if (value == null)
				return true;

			var type = value.GetType();
			if (type.IsValueType)
			{
				var defaultValue = Activator.CreateInstance(type);
				return value.Equals(defaultValue);
			}

			return false;
		}
	}
}