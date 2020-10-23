using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IS7.Startup))]
namespace IS7
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
