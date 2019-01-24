using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project_Management.Startup))]
namespace Project_Management
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    } 
}
