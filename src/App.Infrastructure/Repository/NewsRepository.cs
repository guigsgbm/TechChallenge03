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
        _context.News.Remove(_context.News.FirstOrDefault(x => x.Id == id));
        _context.SaveChanges();
    }

    public IEnumerable<News> GetAll(int skip, int take)
    {
        return _context.News.ToList()
            .Skip(skip)
            .Take(take);
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
