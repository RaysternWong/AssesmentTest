using Api;
using Config.Net;
using ConsoleDataSync.Model;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace ConsoleDataSync
{
    class Program
    {
        static void Main(string[] args)
        {
            IAppSettings settings = new ConfigurationBuilder<IAppSettings>()
                .UseJsonFile("appsettings.json")
                .Build();

            var login = new PostLogin(settings.Api.username, settings.Api.password, settings.Api.Url);
            var bearerToken = login.GetBearerToken();
            bearerToken = bearerToken.Trim('\"');

            var options = new DbContextOptionsBuilder<MyContext>()
                .UseSqlServer(settings.Sql.connection)
                .Options;

            while (true)
            {
                StorePlatforms(settings, bearerToken, options);
                Console.WriteLine();
                Thread.Sleep(3000);
            }
        }

        public static void StorePlatforms(IAppSettings settings , string bearerToken, DbContextOptions<MyContext> options)
        {
            var responseActual = new GetPlatformWellActual(bearerToken, settings.Api.Url);
            var responseDummy = new GetPlatformWellDummy(bearerToken, settings.Api.Url);

            var platforms = responseActual.Response();
            //var platforms = responseDummy.Response();  //To test dummy response, please uncomment this line , and comment line above

            using (var context = new MyContext(options))
            {
                foreach (var platform in platforms)
                {
                    try
                    {
                        var platformwelldataSq = platform.ToPlatformwelldataSq();
                        context.AddOrUpdate(platformwelldataSq);
                        Console.WriteLine($"Stored {platform.uniqueName}");
                    }
                    catch (Exception ex)
                    {
                        string mss = ex.Message;
                        Console.WriteLine($"Failed to add this data : {JsonConvert.SerializeObject(platform)}\n, error : {ex.Message}");
                        continue;
                    }
                    finally
                    {
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
