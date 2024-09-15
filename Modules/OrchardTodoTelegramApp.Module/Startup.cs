using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using OrchardCore.Modules;
using OrchardTodoTelegramApp.Module.Repositories;
using Microsoft.AspNetCore.Mvc.Razor;
using OrchardCore.Admin;
using OrchardCore.Navigation;
using OrchardCore.Security.Permissions;
//using OrchardCore.DisplayManagement.Descriptors; // ShapeTableOptions için


namespace OrchardTodoTelegramApp.Module
{
    public class Startup : StartupBase
    {
        private readonly IConfiguration _configuration;

        // IConfiguration'ý enjekte eden constructor
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            // appsettings.json'dan Azure Storage baðlantý dizesini alýyoruz
            string? storageConnectionString = _configuration.GetSection("AzureStorage:ConnectionString").Value;

            if (string.IsNullOrEmpty(storageConnectionString))
            {
                throw new Exception("Azure Storage connection string is null or empty.");
            }

            // CloudTableClient ve Table referansýný oluþturuyoruz
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
            CloudTable table = tableClient.GetTableReference("TodoTable");

            // Eðer tablo yoksa oluþturulmasýný saðlýyoruz
            table.CreateIfNotExists();

            // Azure Table Storage repository sýnýfýný DI ile ekliyoruz
            services.AddSingleton(table);
            services.AddSingleton<TodoRepository>();
            services.AddScoped<INavigationProvider, AdminMenu>();
            services.AddScoped<IPermissionProvider, Permissions>();

            //services.Configure<RazorViewEngineOptions>(options =>
            //{
            //    options.AreaViewLocationFormats.Clear();
            //    options.AreaViewLocationFormats.Add("/Modules/OrchardTodoTelegramApp.Module/Views/{1}/{0}.cshtml");
            //    options.AreaViewLocationFormats.Add("/Modules/OrchardTodoTelegramApp.Module/Views/Shared/{0}.cshtml");
            //});

        }

        public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {



            routes.MapAreaControllerRoute(
                  name: "OrchardTodoTelegramApp.Module",
                  areaName: "OrchardTodoTelegramApp.Module",
                  pattern: "Module/{controller=Todo}/{action=Index}/{id?}"
              );


        }
    }
}
