using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(michaeltroth.blog.Startup))]
namespace michaeltroth.blog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
