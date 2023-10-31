namespace App.Infrastructure.Repository;

public interface IRepository<T>
{
    T GetById(int id);
    IEnumerable<T> GetAll(int skip, int take);
    void Save(T entity);
    void Delete(int id);
}
