using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace API_FeirasLivresSP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            //É necessario esta criação de host para a publicação no IIS
            var host = new WebHostBuilder()
                      .UseKestrel()
                      .UseContentRoot(Directory.GetCurrentDirectory())
                      .UseIISIntegration()
                      .UseStartup<Startup>()
                      .Build();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
