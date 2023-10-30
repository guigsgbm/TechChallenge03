using App.Application.DTOs;
using App.Domain;
using AutoMapper;

namespace App.Application.Profiles;

public class NewsProfile : Profile
{
    public NewsProfile()
    {
        CreateMap<CreateNewsDto, News>();
        CreateMap<News, GetNewsDto>();
    }
}
