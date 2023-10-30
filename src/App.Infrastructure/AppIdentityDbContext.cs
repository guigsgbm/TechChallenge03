using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using App.Domain;

namespace App.Infrastructure;

public class AppIdentityDbContext : IdentityDbContext
{
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
        : base(options)
    {
    }

    public DbSet<News>? News { get; set; }
}
