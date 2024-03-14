namespace ChristmasHamper.Application.Contracts.Persistence;

public interface IAsyncRepository<T> where T: class
{
    Task<T?> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<bool> ExistsByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task<int> UpdateAsync(T entity);
    Task<int> DeleteAsync(T entity);

}

