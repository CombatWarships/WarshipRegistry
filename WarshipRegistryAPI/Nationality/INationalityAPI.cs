namespace WarshipRegistryAPI.Nationality
{
    public interface INationalityAPI
    {
        Task<IEnumerable<Nationality>> GetAll();
    }
}