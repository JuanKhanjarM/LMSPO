using LMSPO.CoreBusiness.Entities;
using LMSPO.CrossCut.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSPO.CrossCut.Extentions
{
    public static class CustomerExtensions
    {
        public static CustomerDto ToDto(this Customer customer)
        {
            if (customer != null)
            {
                return new CustomerDto
                {
                    CustomerId = customer.CustomerId,
                    CustomerName = customer.CustomerName,
                    TotalSpent = customer.CalculateTotalSpent(),
                    PurchasedProducts = customer.PurchasedProducts.Select(pp => pp.ToDto()).ToList(),
                    Groups = customer.Groups.Select(group => group.ToDto()).ToList()
                };
            }
            return new CustomerDto();
        }
    }
}
