using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MEL.Web.TagHelpers
{
    [HtmlTargetElement("div", Attributes = EnabledValueAttributeName)]
    public class EnabledTagHelper : TagHelper
    {
        private const string EnabledValueAttributeName = "asp-enabled";

        [HtmlAttributeName(EnabledValueAttributeName)]
        public string Enabled { get; set; }

        private readonly IConfiguration _configuration = null;

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public EnabledTagHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string currentController = ViewContext.RouteData.Values["Controller"].ToString();

            // appsettings.json - setting: "disabled":"Controller":"Property" == "disabled"
            string configuration = String.Concat("disabled", ":", currentController, ":", Enabled);

            if (_configuration[configuration] == "disabled")
            {
                output.SuppressOutput();
            }
        }

    }
}


