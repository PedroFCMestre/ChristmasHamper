using ChristmasHamper.Application.Contracts.Persistence;
using ChristmasHamper.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;


namespace ChristmasHamper.Persistence.Repositories;

public class BaseRepository<T>(ChristmasHamperDbContext dbContext) : IAsyncRepository<T> where T : class
{
    protected readonly ChristmasHamperDbContext _dbContext = dbContext;

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await _dbContext.Set<T>().AnyAsync(e => (int)e.GetType().GetProperty("Id")!.GetValue(e)! == id);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<int> UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        return await _dbContext.SaveChangesAsync();
    }
}

