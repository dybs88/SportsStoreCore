using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.Helpers;
using SportsStore.Models.ViewModels;

namespace SportsStore.Infrastructure.Tags
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTag : TagHelper
    {
        private IUrlHelperFactory _urlHelperFactory;

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public PageHelper PageModel { get; set; }

        public string PageAction { get; set; }

        public string PageClass { get; set; }

        public string PageClassNormal { get; set; }

        public string PageClassSelected { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

        public PageLinkTag(IUrlHelperFactory factory)
        {
            _urlHelperFactory = factory;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder divTag = new TagBuilder("div");
            for (int i = 1; i <= PageModel.TotalPages; i++)
            {
                TagBuilder aTag = new TagBuilder("a");
                PageUrlValues["currentPage"] = i;
                aTag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);

                aTag.AddCssClass(PageClass);
                aTag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);

                aTag.InnerHtml.Append(i.ToString());
                divTag.InnerHtml.AppendHtml(aTag);
            }

            output.Content.AppendHtml(divTag);
        }
    }
}
