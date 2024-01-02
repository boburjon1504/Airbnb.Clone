using AirBnb.Api.Models.Dtos;
using AirBnb.Domain.Entities;
using AutoMapper;

namespace AirBnb.Api.Mappers;

public class LocationMapper: Profile
{
    public LocationMapper()
    {
        CreateMap<Location, LocationDto>().ReverseMap();
    }
}