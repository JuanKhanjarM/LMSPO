using LMSPO.CoreBusiness.Entities;
using LMSPO.CrossCut.Dtos;
using LMSPO.CrossCut.Extentions;
using LMSPO.UseCase.Exceptions;
using LMSPO.UseCase.GroupUCs.GroupUCInterfaces;
using LMSPO.UseCase.PluginsInterfaces;
using Microsoft.Extensions.Logging;

namespace LMSPO.UseCase.GroupUCs
{
    public class CreateGroupUC : ICreateGroupUC
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ILogger<CreateGroupUC> _logger;

        public CreateGroupUC(IGroupRepository groupRepository, ILogger<CreateGroupUC> logger)
        {
            _groupRepository = groupRepository ?? throw new ArgumentNullException(nameof(groupRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); // Validate that logger is not null
        }
        public async Task<GroupDto?> ExcecuteAsync( int customerId, GroupDto groupDto)
        {
            if (customerId <= 0)
            {
                _logger.LogError("Invalid customerId: {CustomerId}", customerId);
                throw new InvalidCustomerIdException("customerId must be a positive integer.");
            }

            try
            {
                Group? createdGroup = await _groupRepository.CreateGroupAsync(customerId, groupDto.ToEntity());

                if (createdGroup == null)
                {
                    _logger.LogError("Failed to create a new group.");
                    //throw new GroupCreationException("Failed to create a new group.");
                }

                return createdGroup.ToDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a group for CustomerId: {CustomerId}", customerId);
                throw; // Re-throw the exception for proper error handling higher up the call stack.
            }
        }
    }
}
