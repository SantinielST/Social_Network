using Microsoft.EntityFrameworkCore;
using SocialNetwork.DLL.Interfaces;
using SocialNetwork.DLL.UoW;

namespace SocialNetwork.DLL.Repositories.Base;

public class Repository<T> : IRepository<T> where T : class
{
    protected DbContext _db;
    //public UnitOfWork _unitOfWork;

    public DbSet<T> Set
    {
        get;
        private set;
    }

    public Repository(ApplicationDbContext db)
    {
        //_unitOfWork = unitOfWork;
        _db = db;
        var set = _db.Set<T>();
        set.Load();

        Set = set;
    }

    public void Create(T item)
    {
        Set.Add(item);
        _db.SaveChanges();
    }

    public void Delete(T item)
    {
        Set.Remove(item); 
        _db.SaveChanges();
    }

    public void Update(T item)
    {
        Set.Update(item);
        _db.SaveChanges();
    }

    public T Get(int id)
    {
        return Set.Find(id);
    }

    public IEnumerable<T> GetAll()
    {
        return Set;
    }
    
}