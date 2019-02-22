using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;
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

            if (controller.ToLower() == currentController.ToLower())
            {
                output.AddClass("active", HtmlEncoder.Default);
            }
        }
    }
    [HtmlTargetElement("li", Attributes = "asp-treeview-controllers")]
    public class LiMenuRuteControllerActionTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-treeview-controllers")]
        public string controllers { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var currentController = ViewContext.RouteData.Values["controller"] as string;

            string[] acceptedControllers = controllers.Trim().Split(',').Distinct().ToArray();
            //if (acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController))
            if (acceptedControllers.Contains(currentController))
            {
                output.AddClass("active", HtmlEncoder.Default);
            }
        }
    }
}
