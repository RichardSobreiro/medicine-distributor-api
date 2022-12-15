using AutoMapper;
using MedicinesDistributorApi.Dtos;
using MedicinesDistributorApi.Dtos.Payments;
using MedicinesDistributorApi.Models;
using MedicinesDistributorApi.Models.Payments;
using static MedicinesDistributorApi.Dtos.Payments.PaymentDto;
using static MedicinesDistributorApi.Models.Payments.Payment;

namespace MedicinesDistributorApi.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<MeasurementUnit, MeasurementUnitDto>().ReverseMap();
            CreateMap<Concentration, ConcentrationDto>().ReverseMap();
            CreateMap<Payment, PaymentDto>().ReverseMap().ForMember("BoletoResponse", opt => opt.Ignore());
            CreateMap<ShoppingCartModel, ShoppingCartDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<ProductInCart, ProductInCartDto>().ReverseMap();
            CreateMap<ConcentrationInCart, ConcentrationInCartDto>().ReverseMap();

            //CreateMap<BoletoResponse, BoletoResponseDto>().ReverseMap();
            //CreateMap<BoletoResponse, BoletoResponseDto>().ReverseMap();
            //CreateMap<BoletoResponse, BoletoResponseDto>().ReverseMap();
            //CreateMap<BoletoResponse, BoletoResponseDto>().ReverseMap();
            //CreateMap<BoletoResponse, BoletoResponseDto>().ReverseMap();
        }
    }
}
