using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAlloc
{
    public class NullableDictionnary : StringDictionary
    {
        string null_value;

        override public string this[string key]
        {
            get
            {
                if (key == null)
                {
                    return null_value;
                }
                return base[key];
            }
            set
            {
                if (key == null)
                {
                    null_value = value;
                }
                else
                {
                    base[key] = value;
                }
            }
        }
    }
}
