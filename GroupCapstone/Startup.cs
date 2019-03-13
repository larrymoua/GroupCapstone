using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GroupCapstone.Startup))]
namespace GroupCapstone
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
