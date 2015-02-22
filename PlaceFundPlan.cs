using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ASAlloc
{
    public partial class PlaceFundPlan : UserControl
    {
        public PlaceFundPlan()
        {
            InitializeComponent();
        }
        public openPlan(string building, string floor)
        {

        }


        //class data:
        MarkupParser markup;

    }


    public class MarkupParser
    {
        public MarkupParser(string filename)
        {
            strings = new List<string>();
            objects = new List<string>();

            maskColors = new Dictionary<string, Color>();
            fillPositions = new Dictionary<string, Point>();
            ownerPositions = new Dictionary<string, Point>();
            genderPositions = new Dictionary<string, Point>();
            captionPositions = new Dictionary<string, Point>();


            loadFromFile(filename);
            parse();
        }

        public void loadFromFile (string filename)
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    strings.Add(line);
                }
            }
        }

        private List<string> parseString(string s)
        {
            List<string> result = new List<string>();
            string[] tokens = s.Split(new string[] { "=>" }, StringSplitOptions.None);
            string tail = tokens[1].Trim();
            string[] tailTokens = tail.Split(' ');
            foreach (string tailToken in tailTokens)
                result.Add(tailToken);
            return result;
        }
        public void parse()
        {
            //find objects in markup
            foreach (string s in strings)
            {
                if (s.IndexOf("#objects") == 0)
                {
                    var values = parseString(s);
                    foreach (string objectName in values)
                        objects.Add(objectName.Trim());
                }
            }

            foreach (string objectName in objects)
            {
                foreach (string str in strings)
                {
                    if (str.IndexOf("#"+objectName) == 0)
                    {
                        var values = parseString(str);
                        Color color = Color.FromArgb(Convert.ToInt32(values[0]),
                                                     Convert.ToInt32(values[1]),
                                                     Convert.ToInt32(values[2]));
                        maskColors[objectName] = color;
                    }
                    else if (str.IndexOf("#caption."+objectName) == 0)
                    {
                        var values = parseString(str);
                        Point point = new Point(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]));
                        captionPositions[objectName] = point;
                    }
                    else if (str.IndexOf("#fill." + objectName) == 0)
                    {

                        var values = parseString(str);
                        Point point = new Point(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]));
                        fillPositions[objectName] = point;
                    }
                    else if (str.IndexOf("#owner." + objectName) == 0)
                    {

                        var values = parseString(str);
                        Point point = new Point(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]));
                        ownerPositions[objectName] = point;
                    }
                    else if (str.IndexOf("#gender." + objectName) == 0)
                    {

                        var values = parseString(str);
                        Point point = new Point(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]));
                        genderPositions[objectName] = point;
                    }
                    
                }
            }

        }

        public List<string> getObjects()
        {
            return objects;
        }
        public Color getMaskColor(string obj)
        {
            if (maskColors.ContainsKey(obj))
                return maskColors[obj];
            return Color.Transparent;
        }
        public Point getCaptionPosition(string obj) 
        {
            if (captionPositions.ContainsKey(obj))
                return captionPositions[obj];
            return new Point(0, 0);
        }
        public Point getFillPosition(string obj) 
        {
            if (fillPositions.ContainsKey(obj))
                return fillPositions[obj];
            return new Point(0, 0);
        }
        public Point getOwnerPosition(string obj) 
        {
            if (ownerPositions.ContainsKey(obj))
                return ownerPositions[obj];
            return new Point(0, 0);
        }
        public Point getGenderPosition(string obj) 
        {
            if (genderPositions.ContainsKey(obj))
                return genderPositions[obj];
            return new Point(0, 0);
        }


        //
        private List<string> strings;
        private List<string> objects;
        private Dictionary<string, Color> maskColors;
        private Dictionary<string, Point> captionPositions;
        private Dictionary<string, Point> fillPositions;
        private Dictionary<string, Point> ownerPositions;
        private Dictionary<string, Point> genderPositions;
    }
}
