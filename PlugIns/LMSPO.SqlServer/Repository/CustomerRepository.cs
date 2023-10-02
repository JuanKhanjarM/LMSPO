using LMSPO.CoreBusiness.Entities;
using LMSPO.SqlServer.Data;
using LMSPO.UseCase.PluginsInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LMSPO.SqlServer.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbContextFactory<LMSDbContext> _dbContextFactory;

        public CustomerRepository(IDbContextFactory<LMSDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Task<Customer> GetCustomerById(string CustomerId)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer?> GetCustomerWithGroupsAndProductsAsync(int customerId)
        {
            using LMSDbContext _dbContext = _dbContextFactory.CreateDbContext();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var customer = await _dbContext.Customers
                .Include(c => c.Groups)
                .ThenInclude(g => g.GroupProducts)
                .ThenInclude(gp => gp.PurchasedProduct)
                .Where(c => c.CustomerId == customerId)
                .Select(c => new Customer
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    Groups = c.Groups.Select(g => new Group
                    {
                        GroupId = g.GroupId,
                        GroupName = g.GroupName,
                        GroupProducts = g.GroupProducts
                            .Where(gp => gp.AddedQuantity > 0)
                            .ToList()
                    }).ToList()
                })
                .FirstOrDefaultAsync();
            stopwatch.Stop();
            // Get the elapsed time
            TimeSpan elapsedTime = stopwatch.Elapsed;

            Console.WriteLine($"Method execution time: {elapsedTime}");
            return customer;
        }
    }
}
