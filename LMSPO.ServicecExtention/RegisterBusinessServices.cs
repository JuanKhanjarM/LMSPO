using LMSPO.SqlServer.Data;
using LMSPO.SqlServer.Repository;
using LMSPO.UseCase.CustomerUC;
using LMSPO.UseCase.CustomerUC.CustomerUCInterfaces;
using LMSPO.UseCase.GroupUCs;
using LMSPO.UseCase.GroupUCs.GroupUCInterfaces;
using LMSPO.UseCase.PluginsInterfaces;
using LMSPO.UseCase.PurchasedProductsUCs;
using LMSPO.UseCase.PurchasedProductsUCs.PurchasedProductsUCsInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
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

            services.AddScoped<IPurchasedProductRepository, PurchasedProductRepository>();
            services.AddTransient<IGetPurchasedProductsByCustomerIdUC, GetPurchasedProductsByCustomerIdUC>();

            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddTransient<ICreateGroupUC, CreateGroupUC>();
            services.AddTransient<IGetAllGroupsByCustomerIdUC, GetAllGroupsByCustomerIdUC>();
            services.AddTransient<IGetGroupByIdAndCustomerIdUC, GetGroupByIdAndCustomerIdUC>();
            services.AddTransient<IDeleteGroupByIdAndCustomerIdUC, DeleteGroupByIdAndCustomerIdUC>();
            services.AddTransient<IDeleteSelectedGroupProductsUC, DeleteSelectedGroupProductsUC>();
            services.AddTransient<IAddGroupProductsToGroupUC, AddGroupProductsToGroupUC>();

            
            //services.AddTransient<IUpdateGroupNameUC, UpdateGroupNameUC>();
            //services.AddTransient<IDeleteGroupWithProductsUC, DeleteGroupWithProductsUC>();
            //services.AddTransient<IAddPurchasedQtysToGroupProductsUC, AddPurchasedQtysToGroupProductsUC>();

            //// Auto Mapper Configurations
            //var mapperConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(typeof(MappingProfile));
            //});

            //IMapper mapper = mapperConfig.CreateMapper();
            //services.AddSingleton(mapper);
        }
    }
}
