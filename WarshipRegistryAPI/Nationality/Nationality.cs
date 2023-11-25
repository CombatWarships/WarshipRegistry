namespace WarshipRegistryAPI.Nationality
{
    public class Nationality
    {
        public string ID { get; set; }
        public string DisplayName { get; set; }
        public int Tier { get; set; }
        public string? Aliases { get; set; }

        public override string ToString()
        {
            return ID;
        }
    }
}
