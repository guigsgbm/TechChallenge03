using App.Application.DTOs;
using App.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace App.Services.Mapper;

public interface INewsMapper : IMapper
{
    public News MapCreateNewsDtoToNews(CreateNewsDto sourceEntity, IdentityUser user)
    {
        News destinationEntity = Map<CreateNewsDto, News>(sourceEntity);
        destinationEntity.AuthorName = user.UserName;
        destinationEntity.AuthorId = user.Id;
        return destinationEntity;
    }

    public GetNewsDto MapNewsToGetNewsDto(News news)
    {
        var destinationEntity = Map<News, GetNewsDto>(news);
        return destinationEntity;
    }
}
