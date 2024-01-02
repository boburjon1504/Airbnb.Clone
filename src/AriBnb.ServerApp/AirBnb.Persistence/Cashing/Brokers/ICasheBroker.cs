using AirBnb.Domain.Common.Cashing;

namespace AirBnb.Persistence.Cashing.Brokers;

public interface ICasheBroker
{
    ValueTask<T?> GetAsync<T>(string key);

    ValueTask<bool> TryGetAsync<T>(string key, out T? value);

    ValueTask<T?> GetOrSetAsync<T>(string key, Func<Task<T>> valueFactory, CasheEntryOptions? entryOptions = default);

    ValueTask SetAsync<T>(string key, T value, CasheEntryOptions? entryOptions = default);

    ValueTask DeleteAsync<T>(string key); 
}