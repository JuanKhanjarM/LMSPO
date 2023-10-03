using LMSPO.CoreBusiness.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSPO.UseCase.PluginsInterfaces
{
    public interface IPurchasedProductRepository
    {
        Task<IEnumerable<PurchasedProduct>?> GetPurchasedProductsByCustomerIdAsync(int customerId);

    }
}
