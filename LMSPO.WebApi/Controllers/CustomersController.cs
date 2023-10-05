using LMSPO.CrossCut.Dtos;
using LMSPO.UseCase.CustomerUC.CustomerUCInterfaces;
using LMSPO.UseCase.GroupUCs.GroupUCInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace LMSPO.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IGetCustomerWithGroupsAndProductsUC _getCustomerWithGroupsAndProductsUC;
        private readonly ICreateGroupUC _createGroupUC;
        public CustomersController(IGetCustomerWithGroupsAndProductsUC getCustomerWithGroupsAndProductsUC,
            ICreateGroupUC createGroupUC)
        {
            _getCustomerWithGroupsAndProductsUC = getCustomerWithGroupsAndProductsUC ?? throw new ArgumentNullException(nameof(getCustomerWithGroupsAndProductsUC));
            _createGroupUC = createGroupUC ?? throw new ArgumentNullException(nameof(createGroupUC));
        }

        [HttpGet("{customerId:int}")]
        public async Task< IActionResult> GetCustomer(int customerId)
        {
            CustomerDto? customer =await _getCustomerWithGroupsAndProductsUC.ExecuteAsync(customerId);

            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

       
        //[HttpPost("{customerId:int}")]
        //public async Task<IActionResult> CreateGroup(int customerId, [FromBody] GroupDto groupDto)
        //{
        //    if (groupDto == null)
        //    {
        //        return BadRequest("Invalid group data");
        //    }

        //    // You can add validation logic here to ensure the data is valid

        //    GroupDto createdGroup = await _createGroupUC.ExcecuteAsync(customerId, groupDto);

        //    if (createdGroup == null)
        //    {
        //        return BadRequest("Failed to create the group");
        //    }

        //    //return CreatedAtAction(nameof(GetGroup), new { customerId, groupId = createdGroup.GroupId }, createdGroup);
        //    return Ok(createdGroup);
        //}



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
