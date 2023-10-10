
using AutoMapper;
using LMSPO.CoreBusiness.Entities;
using LMSPO.UseCase.CustomerUC.CustomerUCInterfaces;
using LMSPO.WebApi.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSPO.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IGetCustomerWithGroupsAndProductsUC _getCustomerWithGroupsAndProductsUC;
        
        private readonly IMapper _mapper;
        public CustomersController(IGetCustomerWithGroupsAndProductsUC getCustomerWithGroupsAndProductsUC,
            IMapper mapper)
        {
            _getCustomerWithGroupsAndProductsUC = getCustomerWithGroupsAndProductsUC ?? throw new ArgumentNullException(nameof(getCustomerWithGroupsAndProductsUC));
           
            this._mapper = mapper;
        }

        [HttpGet("{customerId:int}")]
        public async Task< IActionResult> GetCustomer(int customerId)
        {
            Customer? customer =await _getCustomerWithGroupsAndProductsUC.ExecuteAsync(customerId);


            if (customer == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CustomerDto>(customer));
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
