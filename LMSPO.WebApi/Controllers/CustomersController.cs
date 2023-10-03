using LMSPO.CrossCut.Dtos;
using LMSPO.UseCase.CustomerUC.CustomerUCInterfaces;
using LMSPO.UseCase.PluginsInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMSPO.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IGetCustomerWithGroupsAndProductsUC _getCustomerWithGroupsAndProductsUC;

        public CustomersController(IGetCustomerWithGroupsAndProductsUC getCustomerWithGroupsAndProductsUC)
        {
            _getCustomerWithGroupsAndProductsUC = getCustomerWithGroupsAndProductsUC ?? throw new ArgumentNullException(nameof(getCustomerWithGroupsAndProductsUC));
        }

        [HttpGet("{id}")]
        public async Task< IActionResult> GetCustomer(int id)
        {
            CustomerDto? customer =await _getCustomerWithGroupsAndProductsUC.ExecuteAsync(id);

            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
    }
}
