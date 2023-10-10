using AutoMapper;
using LMSPO.CoreBusiness.Entities;
using LMSPO.WebApi.Dtos;

namespace LMSPO.WebApi.Mapper
{
    public class FirstProductNameResolver : IValueResolver<Group, GroupDto, string>
    {
        public string Resolve(Group source, GroupDto destination, string destMember, ResolutionContext context)
        {
            // Use LINQ to retrieve the first product's name, or provide a default value if the list is empty.
            var firstProductName = source.GroupProducts.FirstOrDefault()?.PurchasedProduct.ProductName ?? "No Products";
            return firstProductName;
        }
    }
}
