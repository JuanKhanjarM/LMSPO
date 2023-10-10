using LMSPO.BlazorServerApp.Areas.Identity;
using LMSPO.BlazorServerApp.Data;
using LMSPO.BlazorServerApp.WebApiConnection;
using LMSPO.BlazorServerApp.WebApiConnection.Customers;
using LMSPO.BlazorServerApp.WebApiConnection.PurchasedProducts;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LMSPO.BlazorServerApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            builder.Services.AddSingleton<WeatherForecastService>();

            //add httpclinetfactory
            builder.Services.AddHttpClient("Customers", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5047/api/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            builder.Services.AddTransient<ICustomersWS, CustomersWS>();
            

            builder.Services.AddHttpClient("PurchasedProducts", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5047/api/PurchasedProducts/");
                client.DefaultRequestHeaders.Add("Accept","application/json");
            });
            builder.Services.AddTransient<IPurchasedProdutsWS, PurchasedProdutsWS>();




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}