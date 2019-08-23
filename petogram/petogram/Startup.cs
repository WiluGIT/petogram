using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(petogram.Startup))]
namespace petogram
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
