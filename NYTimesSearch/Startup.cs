using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NYTimesSearch.Startup))]
namespace NYTimesSearch
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
