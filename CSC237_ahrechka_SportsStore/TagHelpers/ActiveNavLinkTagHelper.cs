using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.TagHelpers
{
    [HtmlTargetElement("a")]
    public class ActiveNavLinkTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewCtx { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            /* since class attribute also contains the CSS class nav-item
             difficult to select using Attributes property of HtmlTargetElement attribute.
             So use AllAttributes dictionary of TagHelperContext class instead to make sure
             tag helper applies to correct <a> element.
             */

            var css = context.AllAttributes["class"]?.Value?.ToString() ?? "";
            if (css.Contains("nav-link"))
            {
                // Grabs what in a rout:
                string area = ViewCtx.RouteData.Values["area"]?.ToString() ?? "";
                string ctlr = ViewCtx.RouteData.Values["controller"]?.ToString() ?? "";
                string action = ViewCtx.RouteData.Values["action"]?.ToString() ?? "";

                // Get attributes from the page:
                string aspArea = context.AllAttributes["asp-area"]?.ToString() ?? "";
                string aspCtlr = context.AllAttributes["asp-controller"]?.ToString() ?? "";
                string aspAction = context.AllAttributes["asp-action"]?.ToString() ?? "";

                if (area == aspArea && ctlr == aspCtlr && action == aspAction)
                {
                    output.Attributes.AppendCssClass("active");
                }
            }
        }
    }
}
