using LMSPO.CoreBusiness.Entities;
using LMSPO.UseCase.CustomerUC.CustomerUCInterfaces;
using LMSPO.UseCase.Exceptions;
using LMSPO.UseCase.PluginsInterfaces;
using Microsoft.Extensions.Logging;

namespace LMSPO.UseCase.CustomerUC
{
    public class GetCustomerWithGroupsAndProductsUC : IGetCustomerWithGroupsAndProductsUC
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<GetCustomerWithGroupsAndProductsUC> _logger;

        public GetCustomerWithGroupsAndProductsUC(ICustomerRepository customerRepository, ILogger<GetCustomerWithGroupsAndProductsUC> logger)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Customer?> ExecuteAsync(int customerId)
        {
            if (customerId <= 0)
            {
                _logger.LogError("Invalid customerId: {CustomerId}", customerId); // Log an error
                throw new InvalidCustomerIdException("customerId must be a positive integer.");
            }

            try
            {
                // Validate that _customerRepository is properly injected and used to retrieve the data.
                var customer = await _customerRepository.GetCustomerWithGroupsAndProductsAsync(customerId);

                if (customer == null)
                {
                    _logger.LogWarning("Customer with ID {CustomerId} not found.", customerId);
                    throw new CustomerNotFoundException($"Customer with ID {customerId} not found.");
                }

                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching customer data for CustomerId: {CustomerId}", customerId);
                throw;
            }
        }
    }
}
