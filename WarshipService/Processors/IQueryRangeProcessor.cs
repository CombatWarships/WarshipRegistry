using WarshipSearchAPI.DTO;

namespace WarshipService.Processors
{
	public interface IQueryRangeProcessor
	{
		Task<QueryRange> GetRange();
		ShipQuery RemoveFullRangeQueryConstraints(ShipQuery query);
	}
}