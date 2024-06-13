using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PASSION_PROJECT_ORDER_MANAGEMENT_APP.Startup))]
namespace PASSION_PROJECT_ORDER_MANAGEMENT_APP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
