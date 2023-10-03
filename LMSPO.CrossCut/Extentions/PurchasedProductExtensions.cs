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
                    ProductPrice = purchasedProduct.ProductPrice,
                    CustomerId = purchasedProduct.CustomerId,
                    PurchasedQty = purchasedProduct.PurchasedQty,
                    PPInputQty=0,
                    GetIndividualAddedQuantityForGroupProduct = purchasedProduct.GetIndividualAddedQuantityForGroupProduct()
                };
            }
            return new PurchasedProductDto();
        }

        public static IEnumerable<PurchasedProductDto> ToDto(this IEnumerable<PurchasedProduct> purchasedProducts)
        {
            return purchasedProducts.Select(purchasedProduct => new PurchasedProductDto
            {
                PurchasedProductId = purchasedProduct.PurchasedProductId,
                ProductName = purchasedProduct.ProductName,
                ProductPrice = purchasedProduct.ProductPrice,
                CustomerId = purchasedProduct.CustomerId,
                PurchasedQty = purchasedProduct.PurchasedQty,
                PPInputQty = 0,
                GetIndividualAddedQuantityForGroupProduct = purchasedProduct.GetIndividualAddedQuantityForGroupProduct()
            });
        }
    }
}
