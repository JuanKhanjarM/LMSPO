using LMSPO.CoreBusiness.Entities;
using LMSPO.UseCase.Exceptions;
using LMSPO.UseCase.GroupUCs.GroupUCInterfaces;
using LMSPO.UseCase.PluginsInterfaces;
using Microsoft.Extensions.Logging;

namespace LMSPO.UseCase.GroupUCs
{
    public class AddGroupProductsToGroupUC : IAddGroupProductsToGroupUC
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ILogger<AddGroupProductsToGroupUC> _logger;

        public AddGroupProductsToGroupUC(IGroupRepository groupRepository, ILogger<AddGroupProductsToGroupUC> logger)
        {
            _groupRepository = groupRepository ?? throw new ArgumentNullException(nameof(groupRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<bool> ExecuteAsync(int groupId, List<GroupProduct> groupProducts)
        {
            if (groupId <= 0)
            {
                _logger.LogError("Invalid groupId: {GroupId}", groupId);
                throw new InvalidCustomerIdException("groupId must be a positive integer.");
            }

            try
            {
                bool result = await _groupRepository.AddGroupProductsToGroupAsync(groupId, groupProducts);

                return result; // Return the result directly, no need to re-throw exceptions.

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding products to a group: {GroupId}", groupId);
                return false; // Return a failure status or message instead of re-throwing the exception.
            }
        }
    }
}
