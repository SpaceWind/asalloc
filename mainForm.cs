using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace ASAlloc
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();

            role = 3;
            name = "ФИТ";
        }

        private void setupColNamesDictionary()
        {
            StringDictionary adminDictionary = new StringDictionary();
            //
            //
            colNames["admin"] = adminDictionary;

            StringDictionary studentDictionary = new StringDictionary();
            //
            studentDictionary["name"] = "ФИО";
            studentDictionary["rbook"] = "№ зачетки";
            studentDictionary["gender"] = "Пол";
            studentDictionary["faculty"] = "Факультет";
            studentDictionary["yos"] = "Курс";
            studentDictionary["accomm_range"] = "Дальность проживания";
            studentDictionary["budget"] = "Семейный доход";
            studentDictionary["place"] = "Заселён";
            //
            colNames["student"] = studentDictionary;

            StringDictionary orderDictionary = new StringDictionary();
            //
            orderDictionary["name"] = "ФИО";
            orderDictionary["rbook"] = "№ зачетки";
            orderDictionary["faculty"] = "Факультет";
            orderDictionary["number"] = "№ комнаты";
            orderDictionary["corpus"] = "№ корпуса";
            //
            colNames["order"] = orderDictionary;

            StringDictionary placeDictionary = new StringDictionary();
            //
            placeDictionary["state"] = "Состояние";
            placeDictionary["room"] = "№ комнаты";
            placeDictionary["number"] = "№ комнаты";
            placeDictionary["corpus"] = "№ корпуса";
            placeDictionary["faculty"] = "Владелец";
            placeDictionary["floor_level"] = "№ этажа";
            placeDictionary["yos"] = "Курс";
            placeDictionary["name"] = "Арендатор";
            //
            colNames["place"] = placeDictionary;

            StringDictionary roomDictionary = new StringDictionary();
            //
            roomDictionary["number"] = "№ комнаты";
            roomDictionary["type"] = "Тип";
            roomDictionary["max_places"] = "Макс. Мест";
            roomDictionary["current_places"] = "Мест";
            roomDictionary["floor_level"] = "№ этажа";
            roomDictionary["corpus"] = "№ корпуса";
            //
            colNames["room"] = roomDictionary;

            StringDictionary genderDictionary = new StringDictionary();
            genderDictionary["True"] = "Муж";
            genderDictionary["False"] = "Жен";
            colNames["gender"] = genderDictionary;

            StringDictionary placeStateDictionary = new StringDictionary();
            placeStateDictionary["True"] = "Занято";
            placeStateDictionary["False"] = "Свободно";
            colNames["place_state"] = placeStateDictionary;
        }

        static public SqlConnection createDBConnection()
        {
            return createDBConnection(dataSource,initCatalog,securityMode);
        }
        static public SqlConnection createDBConnection(string initCat)
        {
            return createDBConnection(dataSource, initCat,securityMode);
        }
        static private SqlConnection createDBConnection(string ds, string initCat, string secMode)
        {
            string sConnectionString;
            sConnectionString = "Data Source=" + ds + ";"+initCat + secMode;

            SqlConnection objConn = new SqlConnection(sConnectionString);
            objConn.Open();
            return objConn;
        }
       

        private void mainForm_Activated(object sender, EventArgs e)
        {
            Defines config = new Defines("config.txt");
            dataSources = config.getList("servers");
            securityParams = config.getList("securityMode");
            debugModes = config.getList("DebugMode");
            debugNames = config.getList("DebugLogin");
            initCatalog = config.get("initCatalog");

            var serverNames = config.getList("serverNames");
            comboBox1.Items.Clear();
            foreach (string s in serverNames)
                comboBox1.Items.Add(s);
            comboBox1.Text = serverNames.First();
            colNames = new Dictionary<string, StringDictionary>();
            setupColNamesDictionary();
            comboBox1_SelectedIndexChanged(comboBox1, null);

        }

        static public int role = 0;
        static public string name = null;
        static private string dataSource = @"(local)\ASALLOC";//tcp:192.168.0.103,49172";
        static private string securityMode = "Integrated Security=SSPI;"; //UID=PSA;PWD=123;";
        static private string initCatalog = "Initial Catalog=PSA_base;";

        public static Dictionary<string, StringDictionary> colNames //= new Dictionary<string, StringDictionary>()
        {get; private set;}
        private List<string> dataSources = new List<string>();
        private List<string> securityParams = new List<string>();
        private List<string> debugModes = new List<string>();
        private List<string> debugNames = new List<string>();

        private string secTypeToString(string type)
        {
            if (type == "integrated")
                return "Integrated Security=SSPI;";
            else if (type == "normal")
                return "UID=PSA;PWD=123;";
            return "";
        }
        private string secStringToType(string sec)
        {
            if (sec == "Integrated Security=SSPI;")
                return "Integrated";
            else if (sec == "UID=PSA;PWD=123;")
                return "Normal";
            return "";
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selIndex = ((ComboBox)sender).SelectedIndex;
            dataSource = dataSources.ElementAt(selIndex);
            string secMode = securityParams.ElementAt(selIndex).ToLower();
            securityMode = secTypeToString(secMode); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            int selIndex = comboBox1.SelectedIndex;
            string debugMode = debugModes.ElementAt(comboBox1.SelectedIndex);
            string debugName = debugNames.ElementAt(comboBox1.SelectedIndex);

            if (debugMode == "none")
            {
                Authorisation authForm = new Authorisation();
                authForm.Show();
                authForm.Controls.Find("txtLogin", false).ElementAt(0).Focus();
            }
            else if (debugMode == "faculty")
            {
                role = 3;
                name = debugName;
                facultyMainForm fmf = new facultyMainForm();
                fmf.Show();
            }
            
            //facultyMainForm fmf = new facultyMainForm();
            //fmf.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddServerForm dlg = new AddServerForm();
            dlg.ShowDialog();
            Defines config = new Defines("config.txt");
            config.addToList("servers", dlg.serverAddress);
            config.addToList("securityMode", dlg.securityMode);
            config.addToList("serverNames", dlg.serverName);
            if (dlg.isDebugMode)
            {
                config.addToList("DebugMode",dlg.role);
                debugModes.Add(dlg.role);
                config.addToList("DebugLogin",dlg.login);
                debugNames.Add(dlg.login);
            }
            else
            {
                config.addToList("DebugMode","none");
                debugModes.Add("none");
                config.addToList("DebugLogin","");
                debugNames.Add("");
            }
            config.saveToFile("config.txt");
            var serverNames = config.getList("serverNames");
            comboBox1.Items.Clear();
            foreach (string name in serverNames)
                comboBox1.Items.Add(name);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            AddServerForm dlg = new AddServerForm();
            dlg.serverName = comboBox1.Text;
            dlg.serverAddress = dataSource;
            dlg.isDebugMode = debugModes[index] != "none";
            dlg.securityMode = secStringToType(securityMode);
            if (dlg.isDebugMode)
            {
                dlg.role = debugModes[comboBox1.SelectedIndex];
                dlg.login = debugNames[comboBox1.SelectedIndex];
            }
            dlg.ShowDialog();
            if (dlg.result == false)
                return;
            Defines config = new Defines("config.txt");
            var serversList = config.getList("servers");
            var secModeList = config.getList("securityMode");
            var serverNamesList = config.getList("serverNames");
            var debugModesList = config.getList("DebugMode");
            var debugLoginsList = config.getList("DebugLogin");
            serversList[index] = dlg.serverAddress;
            dataSources[index] = dlg.serverAddress;
            secModeList[index] = dlg.securityMode;
            securityParams[index] = dlg.securityMode;
            serverNamesList[index] = dlg.serverName;

            if (dlg.isDebugMode)
            {
                debugModesList[index] = dlg.role;
                debugModes[index] = dlg.role;
                debugLoginsList[index] = dlg.login;
                debugNames[index] = dlg.login;
            }
            else
            {
                debugModesList[index] = "none";
                debugModes[index] = "none";
                debugLoginsList[index] = "";
                debugNames[index] = "";
            }
            config.setList("servers", serversList);
            config.setList("securityMode", secModeList);
            config.setList("serverNames", serverNamesList);
            config.setList("DebugMode", debugModesList);
            config.setList("DebugLogin", debugLoginsList);

            config.saveToFile("config.txt");
            var serverNames = config.getList("serverNames");
            comboBox1.Items.Clear();
            foreach (string name in serverNames)
                comboBox1.Items.Add(name);
            comboBox1.SelectedIndex = index;
            comboBox1_SelectedIndexChanged(comboBox1, null);
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            Defines config = new Defines("config.txt");
            config.removeFromList("servers", dataSource);
            config.removeFromList("securityMode", secStringToType(securityMode));
            config.removeFromList("serverNames", comboBox1.Text);
            config.removeFromList("DebugMode", debugModes[index]);
            config.removeFromList("DebugLogin", debugNames[index]);
            config.saveToFile("config.txt");
            debugModes.RemoveAt(index);
            debugNames.RemoveAt(index);
            comboBox1.Items.RemoveAt(index);
            if (comboBox1.Items.Count !=0)
                comboBox1.Text = comboBox1.Items[Math.Min(index-1,comboBox1.Items.Count)].ToString();
        }
    }
}
