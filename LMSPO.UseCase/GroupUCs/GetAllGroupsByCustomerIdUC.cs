using LMSPO.CoreBusiness.Entities;
using LMSPO.UseCase.Exceptions;
using LMSPO.UseCase.GroupUCs.GroupUCInterfaces;
using LMSPO.UseCase.PluginsInterfaces;
using Microsoft.Extensions.Logging;

namespace LMSPO.UseCase.GroupUCs
{
    public class GetAllGroupsByCustomerIdUC : IGetAllGroupsByCustomerIdUC
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ILogger<GetAllGroupsByCustomerIdUC> _logger;
        public GetAllGroupsByCustomerIdUC(IGroupRepository groupRepository, ILogger<GetAllGroupsByCustomerIdUC> logger)
        {
            _groupRepository = groupRepository ?? throw new ArgumentNullException(nameof(groupRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<IEnumerable<Group>> ExcecuteAsync(int customerId)
        {

            if (customerId <= 0)
            {
                _logger.LogError("Invalid customerId: {CustomerId}", customerId);
                throw new InvalidCustomerIdException("customerId must be a positive integer.");
            }
            try
            {
                IEnumerable<Group> groups = await _groupRepository.GetAllGroupsByCustomerIdAsync(customerId);
                if (groups == null)
                {
                    _logger.LogError("Failed to retrive the groups for customer with Id : {customerId}", customerId);
                    //throw new GroupCreationException("Failed to create a new group.");
                    return Enumerable.Empty<Group>();
                }
                return groups;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while retriving a group/s for CustomerId: {CustomerId}", customerId);
                throw;
            }
        }
    }
}
