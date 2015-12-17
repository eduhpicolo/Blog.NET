using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlogEdu.Startup))]
namespace BlogEdu
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
