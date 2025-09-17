namespace SocialNetwork.DLL.Interfaces;

public interface IRepository<T> where T : class //IRepository описывает CRUD-операции для наших моделей.
    //В Identity все CRUD-операции над пользователями уже реализованы, и мы можем просто их вызывать. Нужен для наших сущностей, например, Friend
{
    IQueryable<T> GetAll();
    Task<T> Get(int id);
    Task Create(T item);
    Task Update(T item);
    Task Delete(T item);
}