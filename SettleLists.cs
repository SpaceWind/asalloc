using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASAlloc
{
    public partial class SettleLists : UserControl
    {
        public SettleLists()
        {
            InitializeComponent();
        }

        public SettleLists(List<studentDescriptor> allocated, List<studentDescriptor> denied)
        {            
            InitializeComponent();
            allocated_ = new List<studentDescriptor>(allocated);
            denied_ = new List<studentDescriptor>(denied);
            placeMap = new Dictionary<int,int>();

            setupPlaceToRoomMap(mainForm.name);
            fillDataGridView();
        }
        
        public void fillDataGridView()
        {            
            if (allocated_.Count != 0)
            {
                allocatedList.ColumnCount = 11;

                allocatedList.Columns[0].Name = "№";
                allocatedList.Columns[1].Name = "Зачётка";
                allocatedList.Columns[2].Name = "Место";
                allocatedList.Columns[3].Name = "Новое место";
                allocatedList.Columns[4].Name = "Комната";
                allocatedList.Columns[5].Name = "Нарушения";
                allocatedList.Columns[6].Name = "Дальность";
                allocatedList.Columns[7].Name = "Бюджет";
                allocatedList.Columns[8].Name = "Коэффициент льгот";
                allocatedList.Columns[9].Name = "Год обучения";
                allocatedList.Columns[10].Name = "Пол";

                allocatedList.ColumnHeadersVisible = true;
                for (int rowIndex = 0; rowIndex < allocated_.Count; rowIndex++)
                {
                    string[] row = new string[allocatedList.ColumnCount];
                    row[0] = rowIndex.ToString();
                    row[1] = allocated_[rowIndex].rBook;
                    row[2] = allocated_[rowIndex].prevPlaceId.ToString();
                    row[3] = allocated_[rowIndex].place.ToString();
                    row[4] = placeMap[allocated_[rowIndex].place].ToString();
                    row[5] = allocated_[rowIndex].violationCount.ToString();
                    row[6] = allocated_[rowIndex].range.ToString();
                    row[7] = allocated_[rowIndex].budget.ToString();
                    row[8] = allocated_[rowIndex].benefitCoef.ToString();
                    row[9] = allocated_[rowIndex].yos.ToString();
                    row[10] = allocated_[rowIndex].gender.ToString();
                    allocatedList.Rows.Add(row);
                }
            }
            else allocatedList.Rows.Clear();
            
            if (denied_.Count != 0)
            {
                deniedList.ColumnCount = 11;

                deniedList.Columns[0].Name = "№";
                deniedList.Columns[1].Name = "Зачётка";
                deniedList.Columns[2].Name = "Место";
                deniedList.Columns[3].Name = "Новое место";
                deniedList.Columns[4].Name = "Комната";
                deniedList.Columns[5].Name = "Нарушения";
                deniedList.Columns[6].Name = "Дальность";
                deniedList.Columns[7].Name = "Бюджет";
                deniedList.Columns[8].Name = "Коэффициент льгот";
                deniedList.Columns[9].Name = "Год обучения";
                deniedList.Columns[10].Name = "Пол";

                deniedList.ColumnHeadersVisible = true;
                for (int rowIndex = 0; rowIndex < denied_.Count; rowIndex++)
                {
                    string[] row = new string[deniedList.ColumnCount];
                    row[0] = rowIndex.ToString();
                    row[1] = denied_[rowIndex].rBook;
                    row[2] = denied_[rowIndex].prevPlaceId.ToString();
                    row[3] = denied_[rowIndex].place.ToString();
                    row[4] = denied_[rowIndex].place == 0 ? null : placeMap[denied_[rowIndex].place].ToString();
                    row[5] = denied_[rowIndex].violationCount.ToString();
                    row[6] = denied_[rowIndex].range.ToString();
                    row[7] = denied_[rowIndex].budget.ToString();
                    row[8] = denied_[rowIndex].benefitCoef.ToString();
                    row[9] = denied_[rowIndex].yos.ToString();
                    row[10] = denied_[rowIndex].gender.ToString();
                    deniedList.Rows.Add(row);
                }
            }
            else deniedList.Rows.Clear();
        }
        void setupPlaceToRoomMap(string faculty)
        {
            SqlConnection objConn = mainForm.createDBConnection();
            QueryResult qr = SqlCommandBuilder.getQueryResult(new SelectAllRowsByColumnValueCommand("Place", "Place.id, Place.room", "owner", faculty, objConn).buildCommand());

            placeMap = new Dictionary<int, int>();
            for (int i = 0; i < qr.getRowCount(); i++)
                placeMap[Convert.ToInt32(qr.getValue(i, 0))] = Convert.ToInt32(qr.getValue(i, 1));

            objConn.Close();
        }
        
        List<studentDescriptor> allocated_;
        List<studentDescriptor> denied_;
        Dictionary<int, int> placeMap;
    }
}
