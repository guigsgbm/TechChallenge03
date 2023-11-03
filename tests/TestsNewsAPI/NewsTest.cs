using App.Domain;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace TestsNewsAPI;

public class NewsTest
{
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
    public void Constructor_WithoutPublishDate_ThrowsArgumentException()
    {
        // Arrange
        var author = new IdentityUser();
        var title = "";
        var description = "Sample Description";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new News(title, description, author));
    }
}