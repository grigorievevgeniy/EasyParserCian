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

        IParser<T> parser;
        IParserSettings settings;

        bool isActive;

        public IParser<T> Parser
        {
            get
            {
                return parser;
            }
            set
            {
                parser = value;
            }
        }

        public IParserSettings Settings
        {
            get
            {
                return settings;
            }
            set
            {
                settings = value;
                loader = new HtmlLoader(value);
            }
        }

        public bool IsActive
        {
            get
            {
                return isActive;
            }
        }



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
            isActive = true;
            Worker();
        }

        public void Stop()
        {
            isActive = false;
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
            isActive = false;
        }
    }
}
