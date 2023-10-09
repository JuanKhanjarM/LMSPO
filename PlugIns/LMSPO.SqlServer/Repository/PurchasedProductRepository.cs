using LMSPO.CoreBusiness.Entities;
using LMSPO.SqlServer.Data;
using LMSPO.SqlServer.Exceptions;
using LMSPO.UseCase.PluginsInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace LMSPO.SqlServer.Repository
{
    public class PurchasedProductRepository : IPurchasedProductRepository
    {
        private readonly IDbContextFactory<LMSDbContext> _dbContextFactory;
        private readonly ILogger<PurchasedProductRepository> _logger;

        public PurchasedProductRepository(IDbContextFactory<LMSDbContext> dbContextFactory, ILogger<PurchasedProductRepository> logger)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
        }
        public async Task<IEnumerable<PurchasedProduct>> GetPurchasedProductsByCustomerIdAsync(int customerId)
        {
            if (customerId <= 0)
            {
                _logger.LogError("Invalid customerId provided: {CustomerId}", customerId);
                throw new ArgumentException("Invalid customerId provided.", nameof(customerId));
            }

            using (LMSDbContext _dbContext = _dbContextFactory.CreateDbContext())
            {
                try
                {
                    // Retrieve all PurchasedProducts for the specified customer with PurchasedQty > 0
                    var purchasedProducts = await _dbContext.PurchasedProducts
                        .Where(pp => pp.CustomerId == customerId && pp.PurchasedQty > 0)
                        .ToListAsync();

                    return purchasedProducts;
                }
                catch (DbException ex)
                {
                    _logger.LogError(ex, "Database error occurred while fetching PurchasedProducts for customerId: {CustomerId}", customerId);
                    throw new DatabaseException("An error occurred while accessing the database.", ex);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while fetching PurchasedProducts for customerId: {CustomerId}", customerId);
                    throw new CustomException("An error occurred while processing the request.", ex);
                }
            }
        }
    }
}
