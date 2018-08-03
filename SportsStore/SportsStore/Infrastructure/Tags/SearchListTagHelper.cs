using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Tags
{
    [HtmlTargetElement("div", Attributes = "search")]
    public class SearchListTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        private IUrlHelperFactory _urlHelperFactory;

        public string LabelText { get; set; }

        public SearchListTagHelper(IUrlHelperFactory factory)
        {
            _urlHelperFactory = factory;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            TagBuilder formTag = new TagBuilder("form");
            formTag.Attributes["asp-action"] = "List";
            formTag.Attributes["method"] = "post";

            //<div class="form-inline">
            TagBuilder divTag = new TagBuilder("div");
            divTag.AddCssClass("form-inline");

            //<label class="mr-1" asp-for="SearchData">Login lub email:</label>
            TagBuilder labelTag = new TagBuilder("label");
            labelTag.Attributes["asp-for"] = "SearchData";
            labelTag.AddCssClass("mr-1");
            labelTag.InnerHtml.Append(LabelText);

            //<input class="form-control mr-1" type="text" asp-for="SearchData"/>
            TagBuilder inputTag = new TagBuilder("input");
            inputTag.Attributes["type"] = "text";
            inputTag.Attributes["id"] = "SearchData";
            inputTag.Attributes["name"] = "SearchData";
            inputTag.AddCssClass("form-control mr-1");

            //<button type="submit" class="btn btn-primary mr-1">Szukaj</button>
            TagBuilder btnTag = new TagBuilder("button");
            btnTag.Attributes["type"] = "submit";
            btnTag.AddCssClass("btn btn-primary mr-1");
            btnTag.InnerHtml.Append("Szukaj");

            divTag.InnerHtml.AppendHtml(labelTag);
            divTag.InnerHtml.AppendHtml(inputTag);
            divTag.InnerHtml.AppendHtml(btnTag);

            formTag.InnerHtml.AppendHtml(divTag);

            output.Content.AppendHtml(formTag);
    }
    }
}
