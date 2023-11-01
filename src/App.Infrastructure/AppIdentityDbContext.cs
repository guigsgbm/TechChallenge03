using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using App.Domain;
using System.Diagnostics.CodeAnalysis;

namespace App.Infrastructure;

public class AppIdentityDbContext : IdentityDbContext
{
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
        : base(options)
    {
    }

    [NotNull] public DbSet<News>? News { get; set; }
}
