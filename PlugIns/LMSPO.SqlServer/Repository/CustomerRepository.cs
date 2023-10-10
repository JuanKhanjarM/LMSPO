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
                .ThenInclude(gp => gp.GroupProducts)
                .Include(c => c.PurchasedProducts)
                .Where(c => c.CustomerId == customerId).FirstOrDefaultAsync();

            stopwatch.Stop();

            // Get the elapsed time
            TimeSpan elapsedTime = stopwatch.Elapsed;

            Console.WriteLine($"Method execution time: {elapsedTime}");

            return customer;
        }
    }
}
