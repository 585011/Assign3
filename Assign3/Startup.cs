using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Assign3.Startup))]
namespace Assign3
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
