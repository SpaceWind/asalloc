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
            this.Hide();
            //Authorisation authForm = new Authorisation();
            //authForm.Show();
            //authForm.Controls.Find("txtLogin", false).ElementAt(0).Focus();
            colNames = new Dictionary<string,StringDictionary>();
            setupColNamesDictionary();
            facultyMainForm fmf = new facultyMainForm();
            fmf.Show();
        }

        static public int role = 0;
        static public string name = null;
        static private string dataSource = @"(local)\ASALLOC";//tcp:192.168.0.103,49172";
        static private string securityMode = "Integrated Security=SSPI;"; //UID=PSA;PWD=123;";
        static private string initCatalog = "Initial Catalog=PSA_base;";

        public static Dictionary<string, StringDictionary> colNames //= new Dictionary<string, StringDictionary>()
        {get; private set;}
    }
}
