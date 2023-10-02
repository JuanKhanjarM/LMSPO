using LMSPO.CoreBusiness.Entities;
using LMSPO.CrossCut.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSPO.CrossCut.Extentions
{
    public static class GroupProductExtensions
    {
        public static GroupProductDto ToDto(this GroupProduct groupProduct)
        {
            if (groupProduct != null)
            {
                return new GroupProductDto
                {
                    GroupProductId = groupProduct.GroupProductId,
                    GroupId = groupProduct.GroupId,
                    PurchasedProductId = groupProduct.PurchasedProductId,
                    AddedQuantity = groupProduct.AddedQuantity,
                    Group = groupProduct.Group?.ToDto(), // Convert the related Group if available
                    PurchasedProduct = groupProduct.PurchasedProduct?.ToDto() // Convert the related PurchasedProduct if available
                };
            }
            return new GroupProductDto();
        }
    }
}
