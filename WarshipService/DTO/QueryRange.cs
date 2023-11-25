namespace WarshipSearchAPI.DTO
{
    public class QueryRange
    {
		public double? MinUnits { get; set; }
		public double? MaxUnits { get; set; }


		public int? MinSpeedIrcwcc { get; set; }
		public int? MaxSpeedIrcwcc { get; set; }

		public double? MinSpeedKnots { get; set; }
		public double? MaxSpeedKnots { get; set; }


		public int? MinLength { get; set; }
		public int? MaxLength { get; set; }

		public int? MinBeam { get; set; }
		public int? MaxBeam { get; set; }
	}
}