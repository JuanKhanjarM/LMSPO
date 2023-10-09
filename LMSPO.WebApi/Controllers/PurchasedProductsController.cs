﻿using AutoMapper;
using LMSPO.CoreBusiness.Entities;
using LMSPO.UseCase.PurchasedProductsUCs.PurchasedProductsUCsInterfaces;
using LMSPO.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LMSPO.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasedProductsController : ControllerBase
    {
        private IMapper _mapper;
        private readonly IGetPurchasedProductsByCustomerIdUC _getPurchasedProductsByCustomerIdUC;

        public PurchasedProductsController(IGetPurchasedProductsByCustomerIdUC getPurchasedProductsByCustomerIdUC, IMapper mapper)
        {
            _getPurchasedProductsByCustomerIdUC = getPurchasedProductsByCustomerIdUC;
            _mapper = mapper;
        }

        [HttpGet("{customerId:int}")]
        public async Task<IActionResult> GetCustomersPurchasedProducts(int customerId)
        {
            IEnumerable<PurchasedProduct> PurchasedProducts = await _getPurchasedProductsByCustomerIdUC.ExecuteAsync(customerId);

            if (PurchasedProducts == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<PurchasedProductDto>>(PurchasedProducts));
        }
        //[HttpPost]
        //public async Task<IActionResult> CreatePurchasedProduct([FromBody] PurchasedProductDto purchasedProduct)
        //{
        //    if (purchasedProduct == null)
        //    {
        //        return BadRequest();
        //    }

        //    // Your logic to create the purchased product goes here, e.g., using _createPurchasedProductUC

        //    var createdProduct = await _createPurchasedProductUC.ExecuteAsync(purchasedProduct);

        //    if (createdProduct == null)
        //    {
        //        return BadRequest("Failed to create the purchased product.");
        //    }

        //    return CreatedAtAction("GetCustomersPurchasedProducts", new { customerId = createdProduct.CustomerId }, createdProduct);
        //}

        //[HttpPut("{purchasedProductId:int}")]
        //public async Task<IActionResult> UpdatePurchasedProduct(int purchasedProductId, [FromBody] PurchasedProductDto updatedProduct)
        //{
        //    if (updatedProduct == null || purchasedProductId != updatedProduct.PurchasedProductId)
        //    {
        //        return BadRequest();
        //    }

        //    // Your logic to update the purchased product goes here, e.g., using _updatePurchasedProductUC

        //    var result = await _updatePurchasedProductUC.ExecuteAsync(updatedProduct);

        //    if (!result)
        //    {
        //        return NotFound("Purchased product not found or update failed.");
        //    }

        //    return NoContent();
        //}

        //[HttpDelete("{purchasedProductId:int}")]
        //public async Task<IActionResult> DeletePurchasedProduct(int purchasedProductId)
        //{
        //    // Your logic to delete the purchased product goes here, e.g., using _deletePurchasedProductUC

        //    var result = await _deletePurchasedProductUC.ExecuteAsync(purchasedProductId);

        //    if (!result)
        //    {
        //        return NotFound("Purchased product not found or delete failed.");
        //    }

        //    return NoContent();
        //}
    }
}