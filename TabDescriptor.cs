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
        public enum tabType {noTab = 0, unsavedPrivateListTab = 1, savedPrivateListTab = 2, defaultPublicListTab = 3, publicListInTab = 4, publicListOutTab = 5, publicListOutUnplannedTab = 6, violatorsListTab = 7, orderListTab = 8}
        public class columnParserDescriptor
        {
            public int index;
            public StringDictionary parser;
        }
        public tabDescriptor(QueryResult queryResult, tabType type, StringDictionary nameParser)
        {
            columnParsers = new List<columnParserDescriptor>();
            qr = queryResult;
            columnNameParser = nameParser;
            type_ = type;
        }
        public void addColumnParser(int index, StringDictionary parser)
        {
            columnParserDescriptor cpd = new columnParserDescriptor();
            cpd.index = index;
            cpd.parser = parser;
            columnParsers.Add(cpd);
        }
        public QueryResult qr;
        public tabType type_;
        public StringDictionary columnNameParser;
        public List<columnParserDescriptor> columnParsers;
    }
}
