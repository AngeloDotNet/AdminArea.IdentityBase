using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(AdminArea_IdentityBase.Areas.Identity.IdentityHostingStartup))]
namespace AdminArea_IdentityBase.Areas.Identity
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