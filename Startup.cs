using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OctoberProjectCodeFirst.Startup))]
namespace OctoberProjectCodeFirst
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
