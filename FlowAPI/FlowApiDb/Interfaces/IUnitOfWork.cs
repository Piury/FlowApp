namespace FlowAPI.FlowDb;

public interface IUnitOfWork
{
    IRepository<T> GetRepository<T>() where T : class;
    int SaveChanges();
    Task<int> SaveChangesAsync();
}
