using AngleSharp.Html.Dom;
using EasyParser.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyParser.Core.Cian
{
    class CianParser : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {
            List<string> list = new List<string>();
            var items = document.QuerySelectorAll(".c6e8ba5398--info-container--A11gU").Where(x => x.ClassName != null);

            foreach (var item in items)
            {
                list.Add(item.TextContent);
            }

            return list.ToArray();
        }

            
    }
}
