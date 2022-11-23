using System;
using System.Collections.Generic;

namespace LordOfQuotes.Dtos
{
    public class Doc
    {
        public string _id { get; set; }
        public string dialog { get; set; }
        public string movie { get; set; }
        public string character { get; set; }
        public string id { get; set; }
    }

    public class Root
    {
        public List<Doc> docs { get; set; }
        public int total { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public int page { get; set; }
        public int pages { get; set; }
    }
}
