using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookArena.Web.Startup))]
namespace BookArena.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
