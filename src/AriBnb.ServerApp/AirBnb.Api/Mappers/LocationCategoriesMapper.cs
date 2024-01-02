using AirBnb.Api.Models.Dtos;
using AirBnb.Domain.Entities;
using AutoMapper;

namespace AirBnb.Api.Mappers;

public class LocationCategoriesMapper : Profile
{
    public LocationCategoriesMapper()
    {
        CreateMap<LocationCategories, LocationCategoriesDto>().ReverseMap();
    }
}