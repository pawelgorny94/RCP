using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RCP.Startup))]
namespace RCP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
