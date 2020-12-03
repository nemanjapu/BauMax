using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace BauMax.Controllers
{
    public class HomeController : Controller
    {
        #region Sitemap

        public class SitemapNode
        {
            public SitemapFrequency? Frequency { get; set; }
            public DateTime? LastModified { get; set; }
            public double? Priority { get; set; }
            public string Url { get; set; }
        }

        public enum SitemapFrequency
        {
            Never,
            Yearly,
            Monthly,
            Weekly,
            Daily,
            Hourly,
            Always
        }

        public IReadOnlyCollection<SitemapNode> GetSitemapNodes(UrlHelper urlHelper)
        {
            List<SitemapNode> nodes = new List<SitemapNode>();

            nodes.Add(
                new SitemapNode()
                {
                    Url = "https://www.baumax.ba/",
                    Priority = 1,
                    Frequency = SitemapFrequency.Daily
                });
            nodes.Add(
               new SitemapNode()
               {
                   Url = "https://www.baumax.ba/o-nama",
                   Priority = 1,
                   Frequency = SitemapFrequency.Daily
               });
            nodes.Add(
               new SitemapNode()
               {
                   Url = "https://www.baumax.ba/usluge",
                   Priority = 1,
                   Frequency = SitemapFrequency.Monthly
               });
            nodes.Add(
               new SitemapNode()
               {
                   Url = "https://www.baumax.ba/kontakt",
                   Priority = 1,
                   Frequency = SitemapFrequency.Monthly
               });

            return nodes;
        }

        public string GetSitemapDocument(IEnumerable<SitemapNode> sitemapNodes)
        {
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement root = new XElement(xmlns + "urlset");

            foreach (SitemapNode sitemapNode in sitemapNodes)
            {
                XElement urlElement = new XElement(
                    xmlns + "url",
                    new XElement(xmlns + "loc", Uri.EscapeUriString(sitemapNode.Url)),
                    sitemapNode.LastModified == null ? null : new XElement(
                        xmlns + "lastmod",
                        sitemapNode.LastModified.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),
                    sitemapNode.Frequency == null ? null : new XElement(
                        xmlns + "changefreq",
                        sitemapNode.Frequency.Value.ToString().ToLowerInvariant()),
                    sitemapNode.Priority == null ? null : new XElement(
                        xmlns + "priority",
                        sitemapNode.Priority.Value.ToString("F1", CultureInfo.InvariantCulture)));
                root.Add(urlElement);
            }

            XDocument document = new XDocument(root);
            return document.ToString();
        }

        public ActionResult SitemapXml()
        {
            var sitemapNodes = GetSitemapNodes(this.Url);
            string xml = GetSitemapDocument(sitemapNodes);
            return this.Content(xml, "xml", Encoding.UTF8);
        }

        #endregion

        [OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, Duration = 86400)]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ClearCache()
        {
            var urlToRemove = Url.Action("Index", "Home");
            List<string> urlsToRemove = new List<string>()
            {
                Url.Action("Index", "Home"),
                Url.Action("Index", "About"),
                Url.Action("Index", "Contact"),
                Url.Action("Index", "Services")
            };
            foreach (var item in urlsToRemove)
            {
                Response.RemoveOutputCacheItem(item);
            }

            return RedirectToAction("Index");
        }
    }
}