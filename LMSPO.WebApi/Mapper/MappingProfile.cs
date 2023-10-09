using AutoMapper;
using LMSPO.CoreBusiness.Entities;
using LMSPO.WebApi.Dtos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LMSPO.WebApi.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<PurchasedProduct, PurchasedProductDto>()
                .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => src.CalculateTotalCost()))
                .ReverseMap();


            CreateMap<Group, GroupDto>().ReverseMap();
            CreateMap<GroupProduct, GroupProductDto>().ReverseMap();

            CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.TotalSpent, opt => opt.MapFrom(src => src.CalculateTotalSpent()))
            .ForMember(dest => dest.TotalGroups, opt => opt.MapFrom(src => src.GetTotalGroups()))
            .ForMember(dest => dest.TotalGroupSpent, opt => opt.MapFrom(src => src.CalculateTotalCostForGroup()))
            .ReverseMap();
        }
    }
}
