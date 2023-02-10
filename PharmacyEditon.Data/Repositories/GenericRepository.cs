using Newtonsoft.Json;
using PharmacyEdition.Constants;
using PharmacyEdition.Data.IRepositories;
using PharmacyEdition.Domain.Commons;
using PharmacyEdition.Domain.Entities;
using PharmacyEdition.Models;

namespace PharmacyEdition.Data.Repositories;

public class GenericRepository<TResult> : IGenericRepository<TResult> where TResult : Auditable
{
    private string path;
    private long lastId;

    public GenericRepository()
    {
        if (typeof(TResult) == typeof(Medicine))
        {
            path = DatabasePath.MEDCINE_PATH;
        }
        else if (typeof(TResult) == typeof(Order))
        {
            path = DatabasePath.ORDER_PATH;
        }
        else if (typeof(TResult) == typeof(Payment))
        {
            path = DatabasePath.PAYMENT_PATH;
        }
    }

    public async Task<TResult> CreateAsync(TResult value)
    {
        value.Id = ++lastId;
        value.CreatedAt = DateTime.UtcNow;

        var values = await GetAllAsync();
        values.Add(value);

        var json = JsonConvert.SerializeObject(values, Formatting.Indented);
        await File.WriteAllTextAsync(path, json);
        return value;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var values = await GetAllAsync();
        var value = values.FirstOrDefault(x => x.Id == id);

        if (value is null)
            return false;

        values.Remove(value);
        var json = JsonConvert.SerializeObject(values, Formatting.Indented);
        await File.WriteAllTextAsync(path, json);

        return true;
    }

    public async Task<List<TResult>> GetAllAsync()
    {
        string models = await File.ReadAllTextAsync(path);
        if (string.IsNullOrEmpty(models))
            models = "[]";

        List<TResult> results = JsonConvert.DeserializeObject<List<TResult>>(models);
        return results;
    }

    public async Task<TResult> GetByIdAsync(long id)
    {
        var values = await GetAllAsync();
        return values.FirstOrDefault(x => x.Id == id);
    }

    public async Task<TResult> UpdateAsync(long id, TResult value)
    {
        var values = await GetAllAsync();
        var model = values.FirstOrDefault(x => x.Id == id);
        if (model is not null)
        {
            var index = values.IndexOf(model);
            values.Remove(model);

            value.CreatedAt = model.CreatedAt;
            value.UpdatedAt = DateTime.UtcNow;

            values.Insert(index, value);
            var json = JsonConvert.SerializeObject(values, Formatting.Indented);
            await File.WriteAllTextAsync(path, json);
            return model;
        }

        return model;
    }
}
