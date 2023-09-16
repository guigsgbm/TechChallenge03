using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using App.Domain;
using App.Infrastructure;
using App.Application.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace NewsAPI.Controllers
{
    [Route("api/news")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppIdentityDbContext _context;

        public NewsController(IMapper mapper, AppIdentityDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Uploadnews([FromBody] CreateNewsDto newsDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var news = _mapper.Map<News>(newsDto);

            var result = await _context.News.AddAsync(news);
            _context.SaveChanges();

            return Ok(result.Entity);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNews(int id)
        {
            var news = _context.News.FirstOrDefault(x => x.Id == id);

            if (news == null)
                return NotFound("News not found");

            var newsDto = _mapper.Map<GetNewsDto>(news);

            return Ok(newsDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNews()
        {
            var news = _context.News.ToList();

            if (!news.Any())
                return NotFound("News aren't found");

            return Ok(news);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletenews(int id)
        {
            var news = _context.News.FirstOrDefault(x => x.Id == id);

            if (news == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != news.AuthorId.ToString())
                return Forbid("Authors can only delete your own news");

            var result = _context.News.Remove(news);
            await _context.SaveChangesAsync();

            Console.WriteLine($"Delete successfully.");
            return NoContent();
        }

    }
}
