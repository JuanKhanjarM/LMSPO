using LMSPO.UseCase.Exceptions;
using LMSPO.UseCase.GroupUCs.GroupUCInterfaces;
using LMSPO.UseCase.PluginsInterfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSPO.UseCase.GroupUCs
{
    public class DeleteGroupByIdAndCustomerIdUC : IDeleteGroupByIdAndCustomerIdUC
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ILogger<DeleteGroupByIdAndCustomerIdUC> _logger;
        public DeleteGroupByIdAndCustomerIdUC(IGroupRepository groupRepository, ILogger<DeleteGroupByIdAndCustomerIdUC> logger)
        {
            _groupRepository = groupRepository ?? throw new ArgumentNullException(nameof(groupRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<bool> ExecuteAsync(int customerId,int groupId)
        {
            if (customerId <= 0 && groupId <= 0)
            {
                _logger.LogError("Invalid customerId : {CustomerId} and groupId : {GroupId}", customerId, groupId);
                throw new InvalidCustomerIdException("customerId must be a positive integer.");
            }
            try
            {
                bool result = await _groupRepository.DeleteGroupByIdAndCustomerIdAsync(customerId, groupId);
                if (result)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a group for CustomerId: {CustomerId}", customerId);
                throw; // Re-throw the exception for proper error handling higher up the call stack.
            }
        }
    }
}
