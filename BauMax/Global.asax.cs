using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BauMax
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BundleTable.EnableOptimizations = true;
        }

        protected void Application_BeginRequest()
        {
            if (!Request.Url.Host.StartsWith("www") && !Request.Url.IsLoopback)
            {
                UriBuilder builder = new UriBuilder(Request.Url);
                builder.Host = "www." + Request.Url.Host;
                Response.StatusCode = 301;
                Response.AddHeader("Location", builder.ToString());
                Response.End();
            }
            var loadbalancerReceivedSslRequest = string.Equals(Request.Headers["X-Forwarded-Proto"], "https");
            var serverReceivedSslRequest = Request.IsSecureConnection;

            if (loadbalancerReceivedSslRequest || serverReceivedSslRequest) return;

            UriBuilder uri = new UriBuilder(Context.Request.Url);
            if (!uri.Host.Equals("localhost"))
            {
                uri.Port = 443;
                uri.Scheme = "https";
                Response.Redirect(uri.ToString());
            }
        }
    }
}
