using App.Application.DTOs;
using App.Domain;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace App.Services.Mapper;

public class NewsMapper
{
    private readonly IMapper _mapper;

    public NewsMapper(IMapper mapper)
    {
        _mapper = mapper;
    }

    public News MapCreateNewsDtoToNews(CreateNewsDto sourceEntity, IdentityUser user)
    {
        News destinationEntity = _mapper.Map<CreateNewsDto, News>(sourceEntity);
        destinationEntity.AuthorName = user.UserName;
        destinationEntity.AuthorId = user.Id;
        return destinationEntity;
    }

    public GetNewsDto MapNewsToGetNewsDto(News news)
    {
        var destinationEntity = _mapper.Map<News, GetNewsDto>(news);
        return destinationEntity;
    }
}
