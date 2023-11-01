using App.Application.DTOs;
using App.Infrastructure;
using App.Infrastructure.Repository;
using App.Services.Mapper;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NewsAPI.Controllers;
using System.Security.Claims;

namespace TestProject1;

[TestClass]
public class NewsAPIUnitTest
{
    [TestMethod]
    public async Task Uploadnews_ValidModel_ReturnsOkResult()
    {
        var contextMock = new Mock<AppIdentityDbContext>();
        var newsRepositoryMock = new Mock<NewsRepository>(contextMock.Object);

        var userManager = new Mock<UserManager<IdentityUser>>();

        var mapper = new Mock<IMapper>();
        var newsMapper = new NewsMapper(mapper.Object);
        var controller = new NewsController(newsRepositoryMock.Object, newsMapper, userManager.Object);

        var createNewsDto = new CreateNewsDto
        {
            // Preencha os campos do DTO com dados válidos para o teste
        };

        var user = new IdentityUser
        {
            // Defina as propriedades do usuário, como o ID
        };

        userManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);

        // Act
        var result = await controller.Uploadnews(createNewsDto);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
    }
}