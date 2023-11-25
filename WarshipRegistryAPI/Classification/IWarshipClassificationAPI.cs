namespace WarshipRegistryAPI.Classification
{
    public interface IWarshipClassificationAPI
    {
        Task<IEnumerable<WarshipClassification>> GetAll();
    }
}