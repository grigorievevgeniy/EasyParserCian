using EasyParser.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyParser.Core.Cian
{
    class CianParserSettings : IParserSettings
    {
        public string BaseUrl { get; set; } = "https://cian.ru/";
        //ToDo Разделить префиксы по категориям
        public string Prefix { get; set; } = "cat.php?deal_type=sale&engine_version=2&offer_type=flat&p=1&region=4777&room3=1&room4=1";
        public int StartPage { get; set; }// = 1;
        public int EndPage { get; set; }// = 1;
    }
}
