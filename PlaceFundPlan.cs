using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
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
            indicateColors = new Dictionary<int, Color>();
            data = new Dictionary<string, int>();
            description = new Dictionary<string, RoomDescription>();
        }
        public void openPlan(QueryResult qr, int type)
        {
            string path = "plans\\";
            
            data.Clear();
            description.Clear();

            switch (type) 
            {
                case 12:
                    path+=  qr.getValue(0,0) + "\\" + qr.getValue(0,1) + "\\";
                    
                    string roomNumber = "";
                    RoomDescription roomDescr;
                    for(int i = 0; i < qr.getRowCount(); i++)
                    {
                        roomNumber = qr.getValue(i, 3).ToString().Trim();
                        data[roomNumber] = 
                            getIndicateNumber( Convert.ToInt32( Convert.ToDouble(qr.getValue(i, 7)) / Convert.ToDouble(qr.getValue(i, 6))*100));
                        
                        roomDescr = new RoomDescription();
                        roomDescr.capture = roomNumber;
                        roomDescr.owner = (qr.getValue(i, 4) == "") ? "Проректор" : qr.getValue(i, 4);
                        roomDescr.fill = qr.getValue(i, 6)+"/"+qr.getValue(i, 7)+"/"+qr.getValue(i, 8);
                        if(qr.getValue(i, 5) == "") roomDescr.gender = "н/з";
                        else if(qr.getValue(i, 5) == "мужская") roomDescr.gender = "М";
                        else if(qr.getValue(i, 5) == "женская") roomDescr.gender = "Ж";
                        else roomDescr.gender = "C";

                        description[roomNumber] = roomDescr;
                    }
                    break;
                default: break;
            }           
            
            markup = new MarkupParser(path + "markup.txt");

            plan = new Bitmap(path + "plan.bmp");
            mask = new Bitmap(path + "mask.bmp");
            context = new Bitmap(plan.Width, plan.Height);            

            indicateColors[100] = Color.FromArgb(255, 90, 94);
            indicateColors[75] = Color.FromArgb(240, 138, 70);
            indicateColors[50] = Color.FromArgb(228, 180, 107);
            indicateColors[25] = Color.FromArgb(210, 210, 100);
            indicateColors[0] = Color.FromArgb(136, 204, 111);
        }
        private int getIndicateNumber(int value)
        {
            if (value >= 100)
                return 100;
            if (value >= 75)
                return 75;
            if (value >= 50)
                return 50;
            if (value >= 25)
                return 25;
            else
                return 0;
        }

        private Bitmap generateBgMask()
        {
            Bitmap bgMask = new Bitmap(mask);

            ColorMap[] colorMap = new ColorMap[data.Count];
            int i = 0;
            foreach (KeyValuePair<string, int> pair in data)
            {
                colorMap[i] = new ColorMap();
                colorMap[i].OldColor = markup.getMaskColor(pair.Key);
                colorMap[i].NewColor = indicateColors[getIndicateNumber(pair.Value)];
                i++;
            }
            ImageAttributes attr = new ImageAttributes();
            attr.SetRemapTable(colorMap);
            Rectangle rect = new Rectangle(0, 0, bgMask.Width, bgMask.Height);

            Graphics g = Graphics.FromImage(bgMask);
            g.DrawImage(bgMask, rect, 0, 0, rect.Width, rect.Height, GraphicsUnit.Pixel, attr);

            foreach(KeyValuePair<string, RoomDescription> pair in description)
            {
                g.DrawString(pair.Value.owner, this.Font, new SolidBrush(Color.FromArgb(0, 0, 0)), markup.getOwnerPosition(pair.Key));
                g.DrawString(pair.Value.fill, this.Font, new SolidBrush(Color.FromArgb(0, 0, 0)), markup.getFillPosition(pair.Key));
                g.DrawString(pair.Value.gender, this.Font, new SolidBrush(Color.FromArgb(0, 0, 0)), markup.getGenderPosition(pair.Key));
                g.DrawString(pair.Value.capture, this.Font, new SolidBrush(Color.FromArgb(0, 0, 0)), markup.getCaptionPosition(pair.Key));
            }
            bgMask.MakeTransparent(Color.FromArgb(255, 255, 255));
            return bgMask;
        }

        public void render()
        {
            Graphics contextGraphics = Graphics.FromImage(context);
            contextGraphics.Clear(Color.FromArgb(255, 255, 255));

            contextGraphics.DrawImage(plan, new Point(0, 0));
            contextGraphics.DrawImage(generateBgMask(), new Point(0, 0));

            pictureBox1.Image = context;
        }


        //class data:
        MarkupParser markup;


        private Bitmap plan, mask, context;
        Dictionary<int, Color> indicateColors;
        Dictionary<string, int> data;
        Dictionary<string, RoomDescription> description;
    }

    public struct RoomDescription
    {
        public string capture;
        public string owner;
        public string fill;
        public string gender;
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
                    if (str.IndexOf("#"+objectName) == 0 && str.IndexOf(" ") == objectName.Length+1)
                    {
                        var values = parseString(str);
                        Color color = Color.FromArgb(Convert.ToInt32(values[0]),
                                                     Convert.ToInt32(values[1]),
                                                     Convert.ToInt32(values[2]));
                        maskColors[objectName] = color;
                    }
                    else if (str.IndexOf("#caption."+objectName) == 0 && str.IndexOf(" ") == objectName.Length+9)
                    {
                        var values = parseString(str);
                        Point point = new Point(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]));
                        captionPositions[objectName] = point;
                    }
                    else if (str.IndexOf("#fill." + objectName) == 0 && str.IndexOf(" ") == objectName.Length+6)
                    {

                        var values = parseString(str);
                        Point point = new Point(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]));
                        fillPositions[objectName] = point;
                    }
                    else if (str.IndexOf("#owner." + objectName) == 0 && str.IndexOf(" ") == objectName.Length+7)
                    {

                        var values = parseString(str);
                        Point point = new Point(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]));
                        ownerPositions[objectName] = point;
                    }
                    else if (str.IndexOf("#gender." + objectName) == 0 && str.IndexOf(" ") == objectName.Length+8)
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
