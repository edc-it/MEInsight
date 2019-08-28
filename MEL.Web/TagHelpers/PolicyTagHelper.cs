using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MEL.Web.TagHelpers
{
    //https://stackoverflow.com/questions/36068655/any-way-to-use-authorization-policies-in-a-view-in-net-core-1-0-mvc
    [HtmlTargetElement(Attributes = "policy")]
    public class PolicyTagHelper : TagHelper
    {
        private readonly IAuthorizationService _authService;
        private readonly ClaimsPrincipal _principal;

        public PolicyTagHelper(IAuthorizationService authService, IHttpContextAccessor httpContextAccessor)
        {
            _authService = authService;
            _principal = httpContextAccessor.HttpContext.User;
        }

        public string Policy { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!(await _authService.AuthorizeAsync(_principal, Policy)).Succeeded)
                output.SuppressOutput();
        }
    }
}
