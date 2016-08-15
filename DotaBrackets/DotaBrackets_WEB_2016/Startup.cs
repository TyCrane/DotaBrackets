using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DotaBrackets_WEB_2016.Startup))]
namespace DotaBrackets_WEB_2016
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
