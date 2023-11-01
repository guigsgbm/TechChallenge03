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
    }
}