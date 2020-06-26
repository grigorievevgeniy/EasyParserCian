using AngleSharp.Browser;
using AngleSharp.Html.Parser;
using EasyParser.Core.Cian;
using EasyParser.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyParser.Core
{
    class ParserWorker<T> where T : class
    {
        HtmlLoader loader;

        IParser<T> parser { get; set; }
        IParserSettings settings { 
            get { return settings; }
            set
            {
                settings = value;
                loader = new HtmlLoader(value);
            }
        }

        bool IsActive { get; set; }

        public event Action<object, T> OnNewData;
        public event Action<object> OnComplited;

        public ParserWorker(IParser<T> parser)
        {
            this.parser = parser;
        }

        public ParserWorker(IParser<T> parser, IParserSettings settings) : this(parser)
        {
            this.settings = settings;
        }

        public void Start()
        {
            IsActive = true;
            Worker();
        }

        public void Stop()
        {
            IsActive = false;
        }

        private async void Worker()
        {
            for (int i = settings.StartPage; i <= settings.EndPage; i++)
            {
                if (!IsActive) 
                {
                    OnComplited?.Invoke(this);
                    return;
                }

                var source = await loader.GetSourceByPage(i);
                var domParser = new HtmlParser();

                var document = await domParser.ParseDocumentAsync(source);

                var result = parser.Parse(document);

                OnNewData?.Invoke(this, result);
            }

            OnComplited?.Invoke(this);
            IsActive = false;
        }
    }
}
