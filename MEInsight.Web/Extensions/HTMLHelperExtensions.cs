using Microsoft.AspNetCore.Mvc.Rendering;

namespace MEInsight.Web.Extensions
{
    public static class HTMLHelperExtensions
    {
        public static object? Request { get; private set; }

        /// <summary>
        /// Sets 'active' class to selected navigation menu
        /// </summary>
        /// <param name="html"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="cssClass"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string? IsSelected(this IHtmlHelper html, string? controller = null, string? action = null, string? cssClass = null, string? type = null)
        {
            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";
            string? currentAction = html.ViewContext.RouteData.Values["action"] as string;
            string? currentController = html.ViewContext.RouteData.Values["controller"] as string;
            string? currentType = (string)html.ViewContext.HttpContext.Request.Query["type"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            if (String.IsNullOrEmpty(type))
                type = currentType;

            return controller == currentController && action == currentAction && type == currentType ?
                cssClass : String.Empty;
        }

        public static string? PageClass(this IHtmlHelper htmlHelper)
        {
            string? currentAction = htmlHelper.ViewContext.RouteData.Values["action"] as string;
            return currentAction;
        }
    }
}
