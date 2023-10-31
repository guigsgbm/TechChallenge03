using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using App.Application.DTOs;
using Microsoft.AspNetCore.Identity;
using App.Services.Mapper;
using App.Infrastructure.Repository;

namespace NewsAPI.Controllers
{
    [Route("api/news")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly NewsMapper _newsMapper;
        private readonly NewsRepository _newsRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public NewsController(NewsRepository newsRepository, NewsMapper newsMapper, UserManager<IdentityUser> userManager)
        {
            _newsRepository = newsRepository;
            _userManager = userManager;
            _newsMapper = newsMapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Uploadnews([FromBody] CreateNewsDto newsDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            IdentityUser user = await _userManager.GetUserAsync(HttpContext.User);
            var news = _newsMapper.MapCreateNewsDtoToNews(newsDto, user);
            _newsRepository.Save(news);

            return Ok(news);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNews(int id)
        {
            var news = _newsRepository.GetById(id);

            if (news == null)
                return NotFound("News not found");

            var newsDto = _newsMapper.MapNewsToGetNewsDto(news);

            return Ok(newsDto);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllNews([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            var news = _newsRepository.GetAll(skip, take);

            if (!news.Any())
                return NotFound("News aren't found");

            return Ok(news);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletenews(int id)
        {
            var news = _newsRepository.GetById(id);

            if (news == null)
                return NotFound();

            if (_userManager.GetUserAsync(HttpContext.User).Result.Id == news.AuthorId || User.IsInRole("admin"))
            {
                _newsRepository.Delete(id);
                Console.WriteLine($"Delete successfully notice {news.Id}.");
                return NoContent();
            }

            return StatusCode(403, "Authors can only delete your own news");
        }

    }
}
