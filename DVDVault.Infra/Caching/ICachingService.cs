namespace DVDVault.Infra.Caching;
public interface ICachingService
{
    Task<string> GetAsync(string key);

    public Task SetAsync(string key, string value);
}
