using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GitHubUserSearchMain.Startup))]
namespace GitHubUserSearchMain
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
