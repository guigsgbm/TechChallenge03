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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly INewsMapper _INewsMapper;
        private readonly NewsRepository _newsRepository;

        public NewsController(NewsRepository newsRepository, UserManager<IdentityUser> userManager, INewsMapper INewsMapper)
        {
            _newsRepository = newsRepository;
            _userManager = userManager;
            _INewsMapper = INewsMapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Uploadnews([FromBody] CreateNewsDto newsDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            IdentityUser user = await _userManager.GetUserAsync(HttpContext.User);
            var news = _INewsMapper.MapCreateNewsDtoToNews(newsDto, user);
            _newsRepository.Save(news);

            return Ok(news);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNews(int id)
        {
            var news = _newsRepository.GetById(id);

            if (news == null)
                return NotFound("News not found");

            var newsDto = _INewsMapper.MapNewsToGetNewsDto(news);

            return Ok(newsDto);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllNews()
        {
            var news = _newsRepository.GetAll();

            if (!news.Any())
                return NotFound("News aren't found");

            return Ok(news);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletenews(int id)
        {
            var news = _newsRepository.GetById(id);

            if (news == null)
                return NotFound();

            var userId = _userManager.GetUserAsync(HttpContext.User).Result.Id;
            if (userId != news.AuthorId)
                return Forbid("Authors can only delete your own news");

            _newsRepository.Delete(id);

            Console.WriteLine($"Delete successfully notice {news.Id}.");
            return NoContent();
        }

    }
}
