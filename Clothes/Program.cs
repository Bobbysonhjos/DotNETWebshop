using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Clothes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //int random = 0;  
            //Random rand = new Random();
            //random = rand.Next(1, 100);

            CreateHostBuilder(args).Build().Run();
        } 

        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    } 
}
