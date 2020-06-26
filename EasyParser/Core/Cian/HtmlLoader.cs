using EasyParser.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EasyParser.Core.Cian
{
    class HtmlLoader
    {
        readonly HttpClient client;
        readonly string url;

        public HtmlLoader(IParserSettings settings)
        {
            client = new HttpClient();
            url = settings.BaseUrl + settings.Prefix;
        }

        public async Task<string> GetSourceByPage(int id)
        {
            string currentUrl = url.Replace("&p=1", "&p=" + id);
            var response = await client.GetAsync(currentUrl);
            string source = default;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }
    }
}
