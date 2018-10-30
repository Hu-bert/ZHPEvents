using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(ZHPEvents.Areas.Identity.IdentityHostingStartup))]
namespace ZHPEvents.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}