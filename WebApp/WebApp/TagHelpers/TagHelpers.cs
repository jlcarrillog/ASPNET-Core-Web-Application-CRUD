using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;

namespace WebApp.TagHelpers
{
    [HtmlTargetElement("li", Attributes = "asp-controller")]
    public class LiMenuRuteControllerTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-controller")]
        public string controller { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var currentController = ViewContext.RouteData.Values["controller"] as string;

            if (controller == currentController)
            {
                output.AddClass("active", HtmlEncoder.Default);
            }
        }
    }
    [HtmlTargetElement("li", Attributes = "asp-treeview-controller, asp-treeview-action")]
    public class LiMenuRuteControllerActionTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-treeview-controller")]
        public string controller { get; set; }
        [HtmlAttributeName("asp-treeview-action")]
        public string action { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var currentController = ViewContext.RouteData.Values["controller"] as string;
            var currentAction = ViewContext.RouteData.Values["action"] as string;

            if (controller == currentController && action == currentAction)
            {
                output.AddClass("active", HtmlEncoder.Default);
            }
        }
    }
}
