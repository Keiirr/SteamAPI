using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gambling.Startup))]
namespace Gambling
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
