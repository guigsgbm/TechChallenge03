namespace App.Infrastructure.Repository;

public interface IRepository<T>
{
    T GetById(int id);
    T[] GetAll();
    void Save(T entity);
    void Delete(int id);
}
