using LMSPO.CoreBusiness.Entities;
using LMSPO.UseCase.Exceptions;
using LMSPO.UseCase.GroupUCs.GroupUCInterfaces;
using LMSPO.UseCase.PluginsInterfaces;
using Microsoft.Extensions.Logging;

namespace LMSPO.UseCase.GroupUCs
{
    public class GetGroupByIdAndCustomerIdUC : IGetGroupByIdAndCustomerIdUC
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ILogger<GetGroupByIdAndCustomerIdUC> _logger;
        public GetGroupByIdAndCustomerIdUC(IGroupRepository groupRepository, ILogger<GetGroupByIdAndCustomerIdUC> logger)
        {
            _groupRepository = groupRepository ?? throw new ArgumentNullException(nameof(groupRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<Group?> ExecuteAsync(int customerId,int groupId)
        {
            if (customerId <= 0&& groupId<=0)
            {
                _logger.LogError("Invalid customerId: {CustomerId} and groupId {GroupId}", customerId,groupId);
                throw new InvalidCustomerIdException("customerId and groupId must be a positive integer.");
            }
            try
            {
                Group? group = await _groupRepository.GetGroupByIdAndCustomerIdAsync(customerId, groupId);
                if (group == null)
                {
                    _logger.LogError("Failed to retrive the group for a customer with Id : {customerId}", customerId);
                    //throw new GroupCreationException("Failed to create a new group.");
                    return new Group();
                }
                return group;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while retriving a group for CustomerId: {CustomerId}", customerId);
                throw;
            }
        }
    }
}
