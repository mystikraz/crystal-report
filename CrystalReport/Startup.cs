using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CrystalReport.Startup))]
namespace CrystalReport
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
