using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ASAlloc
{
    class studentDescriptor
    {
        public studentDescriptor(string rBook_, int prevPlace)
        {
            rBook = rBook_;
            prevPlaceId = prevPlace;
            place = 0;
        }
        public studentDescriptor(studentDescriptor src)
        {
            rBook = src.rBook;
            prevPlaceId = src.prevPlaceId;
            place = src.place;
            benefits = src.benefits;
            violations = src.violations;
        }
        string rBook { get; set; }
        int prevPlaceId { get; set; }
        int place { get; set; }
        Dictionary<string, List<string>> benefits = new Dictionary<string,List<string>>();
        Dictionary<string, List<string>> violations = new Dictionary<string,List<string>>();

    }
    class AllocatingSystem
    {
    }
}
