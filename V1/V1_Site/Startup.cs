using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(V1_Site.Startup))]
namespace V1_Site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
