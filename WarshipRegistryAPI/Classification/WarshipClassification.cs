namespace WarshipRegistryAPI.Classification
{
    public class WarshipClassification
    {
        public string ID { get; set; }
        public string Family { get; set; }
        public string Abbreviation { get; set; }
        public string DisplayName { get; set; }
        public int ClassRank { get; set; }
        public string Aliases { get; set; }


        public override string ToString()
        {
            return DisplayName;
        }
    }
}