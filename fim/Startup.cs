using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(fim.Startup))]
namespace fim
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
