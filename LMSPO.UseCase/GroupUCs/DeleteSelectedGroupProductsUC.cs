using LMSPO.UseCase.Exceptions.GroupEX;
using LMSPO.UseCase.GroupUCs.GroupUCInterfaces;
using LMSPO.UseCase.PluginsInterfaces;
using Microsoft.Extensions.Logging;

namespace LMSPO.UseCase.GroupUCs
{
    public class DeleteSelectedGroupProductsUC : IDeleteSelectedGroupProductsUC
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ILogger<DeleteSelectedGroupProductsUC> _logger;
        public DeleteSelectedGroupProductsUC(IGroupRepository groupRepository, ILogger<DeleteSelectedGroupProductsUC> logger)
        {
            _groupRepository = groupRepository ?? throw new ArgumentNullException(nameof(groupRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<bool> ExecuteAsync(int groupId, List<int> selectedGroupProductIds)
        {
            // Validate groupId
            if (groupId <= 0)
            {
                _logger.LogError("Invalid groupId: {GroupId}", groupId);
                throw new InvalidGroupIdException("Group ID must be a positive integer.");
            }

            // Validate selectedGroupProductIds
            if (selectedGroupProductIds == null || selectedGroupProductIds.Count == 0)
            {
                _logger.LogError("Invalid selectedGroupProductIds: {SelectedGroupProductIds}", selectedGroupProductIds);
                throw new InvalidSelectedGroupProductIdsException("At least one GroupProduct ID must be selected for deletion.");
            }

            try
            {
                bool result = await _groupRepository.DeleteSelectedGroupProductsAsync(groupId, selectedGroupProductIds);

                if (result)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting group products for Group: {GroupId}", groupId);
                throw; // Re-throw the exception for proper error handling higher up the call stack.
            }
        }
    }
}
