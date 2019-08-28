using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MEL.Web.Extensions
{
    public static class HTMLHelperExtensions
    {
        public static object Request { get; private set; }

        /// <summary>
        /// Sets 'active' class to selected navigation menu
        /// </summary>
        /// <param name="html"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="cssClass"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string IsSelected(this IHtmlHelper html, string controller = null, string action = null, string cssClass = null, string type = null)
        {
            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];
            string currentType = (string)html.ViewContext.HttpContext.Request.Query["type"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            if (String.IsNullOrEmpty(type))
                type = currentType;

            return controller == currentController && action == currentAction && type == currentType ?
                cssClass : String.Empty;
        }

        public static string PageClass(this IHtmlHelper htmlHelper)
        {
            string currentAction = (string)htmlHelper.ViewContext.RouteData.Values["action"];
            return currentAction;
        }
    }
}
