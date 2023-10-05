using LMSPO.CoreBusiness.Entities;
using LMSPO.CrossCut.Dtos;
using LMSPO.CrossCut.Extentions;
using LMSPO.UseCase.Exceptions;
using LMSPO.UseCase.PluginsInterfaces;
using LMSPO.UseCase.PurchasedProductsUCs.PurchasedProductsUCsInterfaces;
using Microsoft.Extensions.Logging;

namespace LMSPO.UseCase.PurchasedProductsUCs
{
    public class GetPurchasedProductsByCustomerIdUC : IGetPurchasedProductsByCustomerIdUC
    {
        private readonly IPurchasedProductRepository _purchasedProductRepository;
        private readonly ILogger<GetPurchasedProductsByCustomerIdUC> _logger;

        public GetPurchasedProductsByCustomerIdUC(IPurchasedProductRepository purchasedProductRepository, ILogger<GetPurchasedProductsByCustomerIdUC> logger)
        {
            _purchasedProductRepository = purchasedProductRepository ?? throw new ArgumentNullException(nameof(purchasedProductRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
        }
        public async Task<IEnumerable<PurchasedProductDto>> ExecuteAsync(int customerId)
        {
            if (customerId <= 0)
            {
                _logger.LogError("Invalid customerId: {CustomerId}", customerId); 
                throw new InvalidCustomerIdException("customerId must be a positive integer.");
            }

            try
            {

                IEnumerable<PurchasedProduct>? purchasedProducts = await _purchasedProductRepository.GetPurchasedProductsByCustomerIdAsync(customerId);

                if (purchasedProducts == null)
                {
                    _logger.LogWarning("PurchasedProducts for Customer with ID {CustomerId} not found.", customerId);
                }

                return purchasedProducts.ToDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching purchasedProducts for  customer data for CustomerId: {CustomerId}", customerId);
                throw; // Re-throw the exception for proper error handling higher up the call stack.
            }
        }
    }
}
