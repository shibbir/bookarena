using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BookArena.App.Startup))]

namespace BookArena.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}