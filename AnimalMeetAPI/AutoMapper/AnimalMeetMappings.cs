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
            CreateMap<AnimalSubtype, AnimalSubtypeCreateDto>().ReverseMap();
            CreateMap<AnimalSubtype, AnimalSubtypeUpdateDto>().ReverseMap();

            CreateMap<Pets, PetsDto>()
                .ForMember(dst => dst.Sex, opt => opt.MapFrom(src => src.Sex.ToString()))
                .ReverseMap();

            CreateMap<Pets, PetsCreateDto>().ReverseMap();
            CreateMap<Pets, PetsUpdateDto>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
        }
    }
}
