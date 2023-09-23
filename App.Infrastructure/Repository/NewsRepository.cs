using App.Domain;

namespace App.Infrastructure.Repository;

public class NewsRepository : IRepository<News>
{
    private readonly AppIdentityDbContext _context;
    public NewsRepository(AppIdentityDbContext context)
    {
        _context = context;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public News[] GetAll()
    {
        throw new NotImplementedException();
    }

    public News GetById(int id)
    {
        return _context.News.FirstOrDefault(x => x.Id == id);
    }

    public void Save(News news)
    {
        _context.News.AddAsync(news);
        _context.SaveChanges();
    }
}
