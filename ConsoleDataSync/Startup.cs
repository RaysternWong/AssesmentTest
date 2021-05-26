using Config.Net;
using ConsoleDataSync.Model;
using Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace ConsoleDataSync
{
    public class Startup
    {
        IConfigurationRoot Configuration { get; }
        public IAppSettings settings { get; set; }

        public Startup()
        {
             this.settings = new ConfigurationBuilder<IAppSettings>()
                .UseJsonFile("appsettings.json")
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyContext>(option => option.UseSqlServer(settings.Sql.connection));
        }
    }
}
