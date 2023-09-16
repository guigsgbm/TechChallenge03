using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Net.Http;
using System.Security.Claims;

namespace App.Domain
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorId { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.Now;

        public News(string title, string description, IHttpContextAccessor httpContextAccessor)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("Title cannot be empty");
            }

            if (description.Length <= 30)
            {
                throw new ArgumentException("Description must contain 30 or more characters");
            }

            var userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Title = title;
            Description = description;
            AuthorId = userId;
        }
    }
}
