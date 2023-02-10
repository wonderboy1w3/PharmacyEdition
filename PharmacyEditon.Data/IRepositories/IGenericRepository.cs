namespace PharmacyEdition.Data.IRepositories;

public interface IGenericRepository<TResult>
{
    Task<TResult> CreateAsync(TResult value);
    Task<TResult> UpdateAsync(long id, TResult value);
    Task<bool> DeleteAsync(long id);
    Task<TResult> GetByIdAsync(long id);
    Task<List<TResult>> GetAllAsync();
}
