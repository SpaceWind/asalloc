using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace ASAlloc
{
    class tabDescriptor
    {
        public class columnParserDescriptor
        {
            public int index;
            public StringDictionary parser;
        }
        public tabDescriptor(QueryResult queryResult, bool isTabEditable_, StringDictionary nameParser)
        {
            columnParsers = new List<columnParserDescriptor>();
            qr = queryResult;
            isTabEditable = isTabEditable_;
            columnNameParser = nameParser;
        }
        public void addColumnParser(int index, StringDictionary parser)
        {
            columnParserDescriptor cpd = new columnParserDescriptor();
            cpd.index = index;
            cpd.parser = parser;
            columnParsers.Add(cpd);
        }

        public QueryResult qr;
        public QueryResult qr1;
        public bool isTabEditable;
        public StringDictionary columnNameParser;
        public List<columnParserDescriptor> columnParsers;

    }
}
