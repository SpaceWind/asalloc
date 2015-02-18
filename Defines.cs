using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ASAlloc
{
    class Defines
    {
        List<string> l = new List<string>();
        public Defines(string filename)
        {
            using (StreamReader sr = new StreamReader("config.txt"))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    l.Add(line);
                }
            }
        }
        public void loadFromFile(string filename)
        {
            l.Clear();
            using (StreamReader sr = new StreamReader("config.txt"))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    l.Add(line);
                }
            }
        }
        public void loadFromString(string str)
        {
            string[] strings = str.Split(new Char[] {'\n'});
            foreach (string s in strings)
                l.Add(s);
        }
        public void saveToFile(string filename)
        {
            File.WriteAllLines(filename, l);
        }
        public string toString()
        {
            string buildString = "";
            foreach (string s in l)
                buildString = buildString + s + "\n";
            return buildString;
        }
        //
        public string get(string key)
        {
            return get(key, "", "");
        }
        public string get(string key, string prefix, string postfix)
        {
            int index = -1;
            for (int i =0; i<l.Count; i++)
                if (l.ElementAt(i) == "#" + key)
                {
                    index = i + 1;
                    break;
                }
            if (index == -1)
                return "";
            return prefix + l.ElementAt(index) + postfix;
        }
        public void set(string key, string value)
        {
            int index = -1;
            for (int i=0; i<l.Count; i++)
                if (l.ElementAt(i) == "#" + key)
                {
                    index = i + 1;
                    break;
                }
            if (index == -1)
            {
                l.Add("#" + key);
                l.Add(value);
            }
            else
                l[index] = value;
        }
        public bool isDefined(string key)
        {
            return get(key) != "";
        }
        public void define(string key)
        {
            if (!isDefined(key))
                set(key, "!");
        }
        public void undefine(string key)
        {
            if (isDefined(key))
                set(key, "");
        }
        public void setList(string key, List<string> data)
        {
            clearList(key);
           // addToList(key, "");
            int index = l.IndexOf("@" + key) + 1;
            foreach (string s in data)
            {
                l.Insert(index,s);
                index++;
            }
        }
        public List<string> getList(string key)
        {
            int indexS = -1, indexF = -1;
            for (int i = 0; i < l.Count; i++)
            {
                switch (indexS)
                {
                    case -1:
                        if (l.ElementAt(i) == "@" + key)
                            indexS = i;
                        break;
                    default:
                        if (l.ElementAt(i) == "/" + key)
                        {
                            indexF = i;
                            i = l.Count;
                        }
                        break;
                }
            }
            List<string> result = new List<string>();
            if (indexS !=-1 && indexF !=-1)
            {
                for (int j=0; j<indexF-indexS-1; j++)
                    result.Add(l.ElementAt(indexS+1+j));
                return result;
            }
            else
                return result;
        }
        public void removeList(string key)
        {
            int iStart = l.IndexOf("@" + key);
            int iStop = l.IndexOf("/" + key);
            for (int i = iStart; i <= iStop; i++)
                l.RemoveAt(iStart);
        }
        public void clearList(string key)
        {
            int iStart = l.IndexOf("@" + key)+1;
            int iStop = l.IndexOf("/" + key)-1;
            if (iStart > iStop)
                return;
            for (int i = iStart; i <= iStop; i++)
                l.RemoveAt(iStart);
        }
        public bool listExist(string key)
        {
            int iStart = l.IndexOf("@" + key);
            int iStop = l.IndexOf("/" + key);
            if (iStart != -1 && iStop != -1 && iStart < iStop)
                return true;
            return false;
        }
        public bool addToList(string key, string data)
        {
            if (!listExist(key))
                return false;
            var s = getList(key);
            s.Add(data);
            setList(key, s);
            return true;
        }
        public void removeFromList(string key, string data)
        {
            if (!listExist(key))
                return;
            var s = getList(key);
            int i = s.IndexOf(data);
            if (i >= 0)
                s.RemoveAt(i);
            setList(key, s);
        }
    }
}
