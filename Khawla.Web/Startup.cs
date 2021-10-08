using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Khawla.Web.Startup))]
namespace Khawla.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
