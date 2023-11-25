using ShipDomain;
using System.Text.Json;
using System.Web;

namespace WarshipRegistryAPI.Warships;


public class WarshipAPI : IWarshipAPI
{
	private readonly HttpClient _client = new HttpClient();
	private readonly string _connectionString;

	public WarshipAPI(string connectionString)
	{
		if (string.IsNullOrEmpty(connectionString))
			throw new ArgumentException($"'{nameof(connectionString)}' cannot be null or empty.", nameof(connectionString));

		_connectionString = connectionString;
	}

	public async Task<Ship> GetShip(Guid? shipId, int? shiplistKey, string? wikiLink)
	{
		var uriBuilder = new UriBuilder(_connectionString);
		var query = HttpUtility.ParseQueryString(uriBuilder.Query);
		if (shipId != null)
			query["shipId"] = shipId.ToString();
		if (shiplistKey != null)
			query["shiplistKey"] = shiplistKey.ToString();
		if (wikiLink != null)
			query["wikiLink"] = wikiLink;
		uriBuilder.Query = query.ToString();
		var shipUrl = uriBuilder.Uri;

		var result = await _client.GetAsync(shipUrl);

		if (!result.IsSuccessStatusCode)
			return null;

		var jsonShipData = await result.Content.ReadAsStringAsync();
		if (string.IsNullOrEmpty(jsonShipData))
			return null;

		var options = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		};
		var ship = JsonSerializer.Deserialize<Ship>(jsonShipData, options);

		return ship;
	}
}