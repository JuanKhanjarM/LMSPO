using LMSPO.CoreBusiness.Entities;
using LMSPO.CrossCut.Dtos;
using LMSPO.UseCase.CustomerUC.CustomerUCInterfaces;
using LMSPO.UseCase.GroupUCs.GroupUCInterfaces;
using LMSPO.UseCase.PluginsInterfaces;
using LMSPO.UseCase.PurchasedProductsUCs.PurchasedProductsUCsInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMSPO.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IGetCustomerWithGroupsAndProductsUC _getCustomerWithGroupsAndProductsUC;
        private readonly IGetPurchasedProductsByCustomerIdUC _getPurchasedProductsByCustomerIdUC;
        private readonly ICreateGroupUC _createGroupUC;
        public CustomersController(IGetCustomerWithGroupsAndProductsUC getCustomerWithGroupsAndProductsUC,
            IGetPurchasedProductsByCustomerIdUC getPurchasedProductsByCustomerIdUC,
            ICreateGroupUC createGroupUC)
        {
            _getCustomerWithGroupsAndProductsUC = getCustomerWithGroupsAndProductsUC ?? throw new ArgumentNullException(nameof(getCustomerWithGroupsAndProductsUC));
            _getPurchasedProductsByCustomerIdUC = getPurchasedProductsByCustomerIdUC ?? throw new ArgumentNullException(nameof(_getPurchasedProductsByCustomerIdUC));
            _createGroupUC = createGroupUC ?? throw new ArgumentNullException(nameof(createGroupUC));
        }

        //[HttpGet("customer")]
        ////http://localhost:5047/api/Customers/customer?customerId=1
        [HttpGet("customer/{customerId:int}")]
        public async Task< IActionResult> GetCustomer(/*[FromQuery]*/ int customerId)
        {
            CustomerDto? customer =await _getCustomerWithGroupsAndProductsUC.ExecuteAsync(customerId);

            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        //[HttpGet("purchased-products")]
        ////http://localhost:5047/api/Customers/purchased-products?customerId=1
        [HttpGet("purchased-products/{customerId:int}")]
        public async Task<IActionResult> GetCustomersPurchasedProducts(/*[FromQuery]*/ int customerId)
        {
            IEnumerable<PurchasedProductDto> PurchasedProducts = await _getPurchasedProductsByCustomerIdUC.ExecuteAsync(customerId);

            if (PurchasedProducts == null)
            {
                return NotFound();
            }
            return Ok(PurchasedProducts);
        }

        [HttpPost("{customerId:int}")]
        //http://localhost:5047/api/Customers/1/Sundhed
        public async Task<IActionResult> CreateGroup(int customerId,[FromBody] string groupName)
        {
            try
            {
                // Validate customer ID and group name as needed

                // Example: Call a service to create the group
                GroupDto? createdGroup = await _createGroupUC.ExcecuteAsync(customerId, groupName);

                if (createdGroup == null)
                {
                    return BadRequest("Failed to create the group.");
                }
                return Ok(createdGroup);
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }


        //[HttpPut("customer/{customerId:int}")]
        //public async Task<IActionResult> UpdateGroupProduct(int customerId, [FromBody] CustomerDto customerDto)
        //{
        //    // Validate customerDto and perform any necessary processing
        //    // Example: Call a service to update the customer
        //    var updatedCustomer = await _updateCustomerService.UpdateCustomerAsync(customerId, customerDto);

        //    if (updatedCustomer == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(updatedCustomer);
        //}

        //[HttpDelete("customer/{customerId:int}")]
        //public async Task<IActionResult> DeleteCustomer(int customerId)
        //{
        //    // Example: Call a service to delete the customer by customerId
        //    var isDeleted = await _deleteCustomerService.DeleteCustomerAsync(customerId);

        //    if (!isDeleted)
        //    {
        //        return NotFound();
        //    }

        //    return NoContent(); // Return 204 No Content on successful deletion
        //}

    }
}
