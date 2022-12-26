using AutoMapper;
using MedicinesDistributorApi.Dtos;
using MedicinesDistributorApi.Dtos.Payments;
using MedicinesDistributorApi.Dtos.Payments.PagSeguro;
using MedicinesDistributorApi.Models;
using MedicinesDistributorApi.Models.Payments;
using MedicinesDistributorApi.PagSeguro;
using static MedicinesDistributorApi.Dtos.Payments.PaymentRequestDto;
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
            CreateMap<PaymentRequestDto, Payment>().ForMember("BoletoResponse", opt => opt.Ignore()); ;
            CreateMap<PaymentRequestDto.ShoppingCartDto, ShoppingCartModel>();
            CreateMap<PaymentRequestDto.AddressDto, Address>();
            CreateMap<PaymentRequestDto.ProductInCartDto, ProductInCart>();
            CreateMap<PaymentRequestDto.ConcentrationInCartDto, ConcentrationInCart>();

            CreateMap<Payment, PaymentResponseDto>();
            CreateMap<ShoppingCartModel, PaymentResponseDto.ShoppingCartDto>();
            CreateMap<Address, PaymentResponseDto.AddressDto>();
            CreateMap<ProductInCart, PaymentResponseDto.ProductInCartDto>();
            CreateMap<ConcentrationInCart, PaymentResponseDto.ConcentrationInCartDto>();
            CreateMap<BoletoResponse, BoletoResponseDto>().ReverseMap();
            CreateMap<BoletoResponse.Boleto, BoletoResponseDto.BoletoDto>().ReverseMap();

            //CreateMap<BoletoResponse, BoletoResponseDto>().ReverseMap();
            //CreateMap<BoletoResponse, BoletoResponseDto>().ReverseMap();
            //CreateMap<BoletoResponse, BoletoResponseDto>().ReverseMap();
            //CreateMap<BoletoResponse, BoletoResponseDto>().ReverseMap();
            //CreateMap<BoletoResponse, BoletoResponseDto>().ReverseMap();
        }
    }
}
