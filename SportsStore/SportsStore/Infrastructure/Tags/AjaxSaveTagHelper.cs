using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Tags
{
    [HtmlTargetElement("select", Attributes = "ss-ajax-save")]
    public class AjaxSaveTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public string SsAjaxSave { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var controller = ViewContext.RouteData.Values["controller"].ToString();

            output.Attributes.Add("ss-ajax-save", "true");

            TagBuilder btnTag = new TagBuilder("button");
            btnTag.Attributes.Add("asp-controller", controller);
            btnTag.Attributes.Add("asp-action", SsAjaxSave);
            btnTag.Attributes.Add("style", "display:none");
            btnTag.AddCssClass("btn btn-success mt-1");
            btnTag.InnerHtml.AppendHtml("Zapisz");

            output.PostElement.SetHtmlContent(btnTag);
        }
    }
}
