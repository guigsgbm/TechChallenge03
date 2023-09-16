using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace App.Domain;

public class News
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IdentityUser Author { get; set; }
    public DateTime PublishDate { get; set; } = DateTime.Now;

    public News(string title, string description, IdentityUser author)
    {
        if (string.IsNullOrEmpty(title))
        {
            throw new ArgumentException("Title cannot be empty");
        }

        if (description.Length <= 30)
        {
            throw new ArgumentException("Description must contain 30 or more characters");
        }

        Title = title;
        Description = description;
        Author = author;
    }
}
