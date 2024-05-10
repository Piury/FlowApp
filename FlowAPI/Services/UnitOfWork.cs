using FlowApi.Models.UserModels;
using FlowAPI.FlowDb;
using FlowAPI.FlowDb.UserDb;
using FlowAPI.Services.Context;

namespace FlowAPI.Services;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private Dictionary<Type, object> repositories;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        repositories = new Dictionary<Type, object>();
    }

    public IRepository<T> GetRepository<T>() where T : class
    {

        var repositoryType = typeof(IRepository<>).MakeGenericType(typeof(T));
        if (!repositories.ContainsKey(typeof(T)))
        {
            // Use generic creation for other repository types
            repositories[typeof(T)] = Activator.CreateInstance(repositoryType, _context);
        }
        return (IRepository<T>)repositories[typeof(T)];
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}