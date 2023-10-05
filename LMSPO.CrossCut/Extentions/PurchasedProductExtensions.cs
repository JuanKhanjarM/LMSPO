using LMSPO.CoreBusiness.Entities;
using LMSPO.CrossCut.Dtos;

namespace LMSPO.CrossCut.Extentions
{
    public static class PurchasedProductExtensions
    {
        public static PurchasedProductDto ToDto(this PurchasedProduct purchasedProduct)
        {
            if (purchasedProduct != null)
            {
                return new PurchasedProductDto
                {
                    PurchasedProductId = purchasedProduct.PurchasedProductId,
                    ProductName = purchasedProduct.ProductName,
                    PurchasedQty = purchasedProduct.PurchasedQty,
                    ProductPrice = purchasedProduct.ProductPrice,
                    TotalCost= purchasedProduct.CalculateTotalCost(),
                    CustomerId = purchasedProduct.CustomerId,
                    PPInputQty =0
                };
            }
            return new PurchasedProductDto();
        }

        public static IEnumerable<PurchasedProductDto> ToDto(this IEnumerable<PurchasedProduct> purchasedProducts)
        {
            return purchasedProducts.Select(purchasedProduct => new PurchasedProductDto
            {
                PurchasedProductId = purchasedProduct.PurchasedProductId,
                ProductName = purchasedProduct.ProductName.ToUpper(),
                PurchasedQty = purchasedProduct.PurchasedQty,
                ProductPrice = purchasedProduct.ProductPrice,
                TotalCost = purchasedProduct.CalculateTotalCost(),
                CustomerId = purchasedProduct.CustomerId,
                PPInputQty = 0,
                Customer=purchasedProduct.Customer.ToDto()
            });
        }
    }
}
