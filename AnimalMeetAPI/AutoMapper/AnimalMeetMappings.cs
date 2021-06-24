using AnimalMeetAPI.Models;
using AnimalMeetAPI.Models.Dtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetAPI.AutoMapper
{
    public class AnimalMeetMappings : Profile
    {
        public AnimalMeetMappings()
        {
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<AnimalType, AnimalTypeDto>().ReverseMap();
            CreateMap<AnimalSubtype, AnimalSubtypeDto>().ReverseMap();
            CreateMap<Pets, PetsDto>().ReverseMap();
        }
    }
}
