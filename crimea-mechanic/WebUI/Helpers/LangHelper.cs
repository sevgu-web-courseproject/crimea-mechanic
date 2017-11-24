using System.Web.Mvc;
using System.Web.Routing;

namespace WebUI.Helpers
{
    public static class LangHelper
    {
        public static MvcHtmlString LangSwitch(this UrlHelper url, RouteData routeData, string lang)
        {
            var routeValueDictionary = new RouteValueDictionary(routeData.Values);
            if (routeValueDictionary.ContainsKey("lang"))
            {
                routeValueDictionary["lang"] = lang;
            }
            return new MvcHtmlString(url.RouteUrl(routeValueDictionary));
        }
    }
}