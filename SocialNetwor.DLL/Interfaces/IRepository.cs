namespace SocialNetwork.DLL.Interfaces;

public interface IRepository<T> where T : class //IRepository описывает CRUD-операции для наших моделей.
    //В Identity все CRUD-операции над пользователями уже реализованы, и мы можем просто их вызывать.
{
    IEnumerable<T> GetAll();
    T Get(int id);
    void Create(T item);
    void Update(T item);
    void Delete(int id);
}
