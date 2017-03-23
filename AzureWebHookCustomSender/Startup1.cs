using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.AspNet.SignalR;
using System.Web.Http;
using AzureWebHookCustomSender.WebHooks;
using Microsoft.AspNet.WebHooks.Config;
using Microsoft.AspNet.WebHooks.Services;
using Microsoft.AspNet.WebHooks;
using System.Security.Claims;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http.Filters;
using System.Threading;
using System.Web.Http.Controllers;
using System.Security.Principal;
using System.Diagnostics;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Http.Dependencies;
using System.Linq;
using Microsoft.AspNet.WebHooks.Diagnostics;
using System.Net.Http;

[assembly: OwinStartup(typeof(AzureWebHookCustomSender.Startup1))]

namespace AzureWebHookCustomSender
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configure(config =>
            {
                config.Filters.Add(new MyAuthorizeAttribute());

                config.MapHttpAttributeRoutes();

                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional });

                config.DependencyResolver = new MyResolver();

                CustomApiServices.SetIdValidator(new MyWebHookIdValidator());

                config.InitializeCustomWebHooks();
                config.InitializeCustomWebHooksAzureStorage(false);
                //config.InitializeCustomWebHooksAzureQueueSender();
                config.InitializeCustomWebHooksApis();
            });
        }
    }

    public class MyAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        public override async Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                actionContext.RequestContext.Principal = new GenericPrincipal(new GenericIdentity("self"), new string[] { "Admin", "PowerUser" });
            });
        }
    }

    public class MyResolver : System.Web.Http.Dependencies.IDependencyResolver
    {
        public IDependencyScope BeginScope() => this;

        public void Dispose()
        {
        }

        public object GetService(Type serviceType)
        {
            if (typeof(IWebHookManager).Equals(serviceType))
            {
                IWebHookStore store = this.GetStore();
                IWebHookSender sender = this.GetSender();
                ILogger logger = this.GetLogger();
                IWebHookManager instance = new MyWebHookManager(store, sender, logger);

                return instance;
            }

            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType) => Enumerable.Empty<object>();
    }
}
