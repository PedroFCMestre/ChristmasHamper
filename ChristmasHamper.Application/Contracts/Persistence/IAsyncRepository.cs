﻿namespace ChristmasHamper.Application.Contracts.Persistence;

public interface IAsyncRepository<T> where T: class
{
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<bool> ExistsByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(int id);

}
