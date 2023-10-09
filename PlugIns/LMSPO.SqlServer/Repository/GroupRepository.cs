using LMSPO.CoreBusiness.Entities;
using LMSPO.SqlServer.Data;
using LMSPO.UseCase.PluginsInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LMSPO.SqlServer.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly IDbContextFactory<LMSDbContext> _dbContextFactory;
        private readonly ILogger<GroupRepository> _logger;

        public GroupRepository(IDbContextFactory<LMSDbContext> dbContextFactory, ILogger<GroupRepository> logger)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
        }
        public async Task<Group?> CreateGroupAsync(int customerId, Group group)
        {
            if (string.IsNullOrWhiteSpace(group.GroupName))
            {
                _logger.LogError("Invalid group name provided.");
                throw new ArgumentException("Group name is required.", nameof(group.GroupName));
            }

            using (LMSDbContext _dbContext = _dbContextFactory.CreateDbContext())
            {
                // Check if a group with the same name already exists for the customer
                bool groupExists = await _dbContext.Groups
                    .AnyAsync(g => g.CustomerId == customerId && g.GroupName.Equals(group.GroupName));

                if (groupExists)
                {
                    _logger.LogError("Group with the same name already exists for customer.");
                    throw new InvalidOperationException("A group with the same name already exists for this customer.");
                }
                // Create a new group
                var Newgroup = Group.CreateNewGroup(group.GroupName, customerId);
                
                // Add the group to the database
                _dbContext.Groups.Add(Newgroup);
                await _dbContext.SaveChangesAsync();

                return Newgroup;
            }
        }
        // Helper method to generate EAN based on the specified format
        //private string GenerateEAN(string groupName)
        //{
        //    Random random = new Random();
        //    int randomDigits = random.Next(1000, 10000);
        //    string lastLetter = groupName.Substring(groupName.Length - 1).ToUpper();
        //    string year = DateTime.Now.Year.ToString().Substring(2);

        //    return $"{char.ToUpper(groupName[0])}{randomDigits}{lastLetter}-{year}";
        //}
    }
}
