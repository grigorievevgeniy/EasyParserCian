using System;
using System.Collections.Generic;
using System.Text;

namespace EasyParser.Core.Interfaces
{
    interface IParserSettings
    {
        string BaseUrl { get; set; }
        string Prefix { get; set; }

        int StartPage { get; set; }
        int EndPage { get; set; }
    }
}
