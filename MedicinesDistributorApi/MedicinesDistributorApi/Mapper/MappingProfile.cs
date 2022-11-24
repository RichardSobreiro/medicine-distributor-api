using AutoMapper;
using MedicinesDistributorApi.Dtos;
using MedicinesDistributorApi.Models;

namespace MedicinesDistributorApi.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<MeasurementUnit, MeasurementUnitDto>().ReverseMap();
            CreateMap<Concentration, ConcentrationDto>().ReverseMap();
        }
    }
}
