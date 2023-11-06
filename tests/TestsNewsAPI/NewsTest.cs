using App.Domain;
using App.Infrastructure;
using Castle.Core.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace TestsNewsAPI;

public class NewsTest : IClassFixture<WebApplicationFactory<Program>>
{
    private DbContextOptions<AppIdentityDbContext> _options;

    public NewsTest(WebApplicationFactory<Program> factory)
    {
        _options = new DbContextOptionsBuilder<AppIdentityDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public void Constructor_WithValidData_SetsProperties()
    {
        // Arrange
        var author = new IdentityUser { Id = "1", UserName = "JohnDoe" };
        var title = "Sample Title";
        var description = "This is a sample description with more than 30 characters.";

        // Act
        var news = new News(title, description, author);

        // Assert
        Assert.Equal(title, news.Title);
        Assert.Equal(description, news.Description);
        Assert.Equal(author.Id, news.AuthorId);
        Assert.Equal(author.UserName, news.AuthorName);
    }

    [Fact]
    public void Constructor_WithEmptyTitle_ThrowsArgumentException()
    {
        // Arrange
        var author = new IdentityUser();
        var title = "";
        var description = "Sample Description";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new News(title, description, author));
    }

    [Fact]
    public void Constructor_WithShortDescription_ThrowsArgumentException()
    {
        // Arrange
        var author = new IdentityUser();
        var title = "Sample Title";
        var description = "Short Description";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new News(title, description, author));
    }

    [Fact]
    public void TestUploadValidNews()
    {
        // Arrange
        var author = new IdentityUser();
        var title = "Sample Title";
        var description = "Sample valid description with 30 or more characters";

        // Act
        using (var context = new AppIdentityDbContext(_options))
        {
            var news = new News(title, description, author);

            context.News.Add(news);
            context.SaveChanges();
        }

        // Assert
        using (var context = new AppIdentityDbContext(_options))
        {
            var insertNews = context.News.FirstOrDefault(n => n.Title == title);

            Assert.NotNull(insertNews);
            Assert.Equal(title, insertNews.Title);
            Assert.Equal(description, insertNews.Description);
        }
    }

    [Fact]
    public async Task TestGetAllNewsEndpoint()
    {
        // Arrange
        var factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var projectPath = Path.Combine("..", "src/Services/NewsAPI");

                        config
                            .SetBasePath(projectPath)
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    });

                    builder.ConfigureServices(services =>
                    {
                        var serviceProvider = services.BuildServiceProvider();
                        var configuration = serviceProvider.GetRequiredService<Microsoft.Extensions.Configuration.IConfiguration>();
                        var connection = configuration.GetConnectionString("AzureDB");

                        services.AddDbContext<AppIdentityDbContext>(options =>
                            options.UseSqlServer(connection));

                        services.AddIdentity<IdentityUser, IdentityRole>()
                        .AddRoles<IdentityRole>()
                        .AddEntityFrameworkStores<AppIdentityDbContext>();

                    });
                });
        var client = factory.CreateClient();
        string url = "/api/news";

        // Act
        var response = await client.GetAsync(url);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

}