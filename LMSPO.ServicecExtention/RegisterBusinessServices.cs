using LMSPO.SqlServer.Data;
using LMSPO.SqlServer.Repository;
using LMSPO.UseCase.CustomerUC;
using LMSPO.UseCase.CustomerUC.CustomerUCInterfaces;
using LMSPO.UseCase.PluginsInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LMSPO.ServicecExtention
{
    public static class RegisterBusinessServices
    {
        public static void AddRegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextFactory<LMSDbContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("LMSConnection")));

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IGetCustomerWithGroupsAndProductsUC, GetCustomerWithGroupsAndProductsUC>();

            //services.AddScoped<IPurchasedProductRepository, PurchasedProductRepository>();
            //services.AddTransient<IGetPurchasedProductsByCustomerIdUC, GetPurchasedProductsByCustomerIdUC>();

            //services.AddScoped<IGroupRepository, GroupRepository>();
            //services.AddTransient<ICreateGroupUC, CreateGroupUC>();
            //services.AddTransient<IUpdateGroupNameUC, UpdateGroupNameUC>();
            //services.AddTransient<IDeleteGroupWithProductsUC, DeleteGroupWithProductsUC>();
            //services.AddTransient<IAddPurchasedQtysToGroupProductsUC, AddPurchasedQtysToGroupProductsUC>();
        }
    }
}
