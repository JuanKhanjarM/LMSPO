using AutoMapper;
using LMSPO.CoreBusiness.Entities;
using LMSPO.WebApi.Dtos;

namespace LMSPO.WebApi.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PurchasedProduct, PurchasedProductDto>()
                .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => src.CalculateTotalCost()))
                .ReverseMap();


            CreateMap<Group, GroupDto>()
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.CalculateTotalPrice()))
               // .ForMember(dest => dest.FirstGroupProductName, opt => opt.MapFrom<FirstProductNameResolver>())
                .ReverseMap();

            CreateMap<GroupProduct, GroupProductDto>()
                .ForMember(dest => dest.SubTotal, opt => opt.MapFrom(src => src.CalculateSubtotal()))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.PurchasedProduct.ProductName))
                .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.PurchasedProduct.ProductPrice))
                .ReverseMap();

            CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.TotalSpent, opt => opt.MapFrom(src => src.CalculateTotalSpent()))
            .ForMember(dest => dest.TotalGroups, opt => opt.MapFrom(src => src.GetTotalGroups()))
            .ForMember(dest => dest.TotalGroupSpent, opt => opt.MapFrom(src => src.CalculateTotalCostForGroup()))
            .ReverseMap();
        }
    }
}
