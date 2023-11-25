using WarshipRegistryAPI.Classification;

namespace WarshipImport.Interfaces
{
    public interface IWarshipClassificationDB
	{
		List<WarshipClassification> GetFullList();
	}
}
