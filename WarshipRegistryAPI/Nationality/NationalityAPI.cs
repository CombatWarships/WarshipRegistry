using System.Text.Json;

namespace WarshipRegistryAPI.Nationality
{
    public class NationalityAPI : INationalityAPI
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly string _connectionString;
        private List<Nationality> _cache;
        private TaskCompletionSource _cacheBuilderTask;
        private readonly object _lock = new object();

        public NationalityAPI(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException($"'{nameof(connectionString)}' cannot be null or empty.", nameof(connectionString));

            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Nationality>> GetAll()
        {
            if (_cache != null)
                return _cache;

            bool buildCacheOnThisThread = false;
            lock (_lock)
            {
                if (_cache != null)
                    return _cache;

                if (_cacheBuilderTask == null)
                {
                    buildCacheOnThisThread = true;
                    _cacheBuilderTask = new TaskCompletionSource();
                }
            }

            if (buildCacheOnThisThread)
            {
                await CreateCache();
                _cacheBuilderTask.SetResult();
                _cacheBuilderTask = null;
            }
            else
            {
                await _cacheBuilderTask.Task;
            }

            return _cache;
        }


        private async Task CreateCache()
        {
            var result = await _client.GetAsync(_connectionString);

            if (!result.IsSuccessStatusCode)
                return;

            var jsonShipData = await result.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var classes = JsonSerializer.Deserialize<List<Nationality>>(jsonShipData, options);

            if (classes?.Count > 0)
                _cache = classes;
        }
    }
}
