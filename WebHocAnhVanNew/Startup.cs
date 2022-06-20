using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebHocAnhVanNew.Startup))]
namespace WebHocAnhVanNew
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
