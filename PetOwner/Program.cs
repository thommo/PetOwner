using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using PetOwner.Application;
using PetOwner.Configuration;
using PetOwner.Repositories.Contracts;
using PetOwner.Repositories.Implementations;
using PetOwner.Services.Contracts;
using PetOwner.Services.Implementations;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PetOwner
{
    class Program
    {
        private static IConfiguration _config;
        public static async Task Main(string[] args)
        {
            LoadConfig();

            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            await serviceProvider.GetService<App>().RunAsync();
        }

        /// <summary>
        /// Load application settings from config.json
        /// </summary>
        private static void LoadConfig()
        {
            var builder = new ConfigurationBuilder();
            builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", true);
            _config = builder.Build();
        }

        /// <summary>
        /// ConfigureServices
        /// Builds up the services to enable dependency injection 
        /// to resolve required dependencies
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IPetOwnerService, PetOwnerService>();
            services.AddTransient<IPetOwnerRepository, PetOwnerRepository>();
            services.Configure<PetOwnerRepositorySettings>(_config.GetSection("PetOwnerDatabase"));
            services.AddSingleton(_config.GetSection("PetOwnerDatabase").Get<PetOwnerRepositorySettings>());
            services.AddLogging(builder =>
            {
                builder.AddConfiguration(_config.GetSection("Logging")).AddConsole().AddDebug();
            });
            services.AddTransient<App>();
        }
    }
}
