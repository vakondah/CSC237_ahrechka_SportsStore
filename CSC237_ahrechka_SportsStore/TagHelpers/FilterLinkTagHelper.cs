using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.TagHelpers
{
    [HtmlTargetElement(Attributes = "my-filter")]
    public class FilterLinkTagHelper: TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewCtx { get; set; }

        [HtmlAttributeName("my-filter")]
        public string Filter { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string id = context.AllAttributes["asp-route-id"]?.Value?.ToString() ?? "";

            if (id == Filter)
            {
                output.Attributes.AppendCssClass("active");
            }
        }

        
    }
}
