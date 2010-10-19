using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataScraper
{
    public class ScriptConsts
    {
        public const string DOC_TAG_NAME = "ACTIONS";
        public const string TAG_NAME = "ACTION";

        public const string ATTR_NAME = "name";
        public const string ATTR_TYPE = "type";

        public const string TYPE_TEXT = "text";
        public const string TYPE_COOK = "cookie";
        public const string TYPE_DATE = "date";
        public const string TYPE_SAVE = "save";
        public const string TYPE_FIND = "find";
        public const string TYPE_REPL = "replace";
        public const string TYPE_LOAD = "load";
        public const string TYPE_DOWNLOAD = "download";
        public const string TYPE_SET_VAR = "set-var";
        public const string TYPE_GET_VAR = "get-var";
        public const string TYPE_NEXT_PAGE = "next-page";

        public static string EXCEPT_PROCESSED_KEY = "processed";

        public const int MAX_LOAD_ATTEMPT = 3;
        public const int LOAD_INTERVAL_SEC = 5;
    }
}
