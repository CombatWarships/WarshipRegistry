using System.Reflection;
using WarshipSearchAPI.DTO;

namespace WarshipService.Processors
{
	public class QueryRangeProcessor : IQueryRangeProcessor
	{
		private readonly QueryRange _queryRange = new QueryRange()
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
		private readonly PropertyInfo[] _shipQueryProps;
		private readonly PropertyInfo[] _queryRangeProps;

		public QueryRangeProcessor()
		{
			_shipQueryProps = typeof(ShipQuery).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
			_queryRangeProps = typeof(QueryRange).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
		}

		public async Task<QueryRange> GetRange()
		{
			return _queryRange;
		}

		public ShipQuery RemoveFullRangeQueryConstraints(ShipQuery query)
		{
			foreach (var property in _shipQueryProps)
			{
				object queryValue = property.GetValue(query);

				if (queryValue == null)
					continue;

				if (IsDefault(queryValue))
				{
					property.SetValue(query, null);
					continue;
				}

				var rangeProperty = _queryRangeProps.FirstOrDefault(p => p.Name == property.Name);

				if (rangeProperty == null)
					continue;

				object defaultValue = rangeProperty.GetValue(_queryRange);

				if (defaultValue.Equals(queryValue))
				{
					property.SetValue(query, null);
					continue;
				}
			}

			return query;
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

			if (value.GetType() == typeof(string))
				return string.IsNullOrWhiteSpace((string)value);

			return false;
		}

	}
}
