using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections.Specialized;


namespace ASAlloc
{
    abstract class SqlCommandBuilder
    {
        abstract public SqlCommand buildCommand();

        static public bool isQueryNotEmpty(SqlCommand command)
        {
            try
            {
                return (command.ExecuteScalar() != null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
                return false;
            }
        }
        static public QueryResult getQueryResult(SqlCommand command)
        {
            try
            {
                QueryResult result = new QueryResult();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    OrderedDictionary sd = new OrderedDictionary();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        sd[reader.GetName(i)] = reader[i].ToString();
                    }
                    result.addRow(sd);
                }
                reader.Close();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
                return null;
            }

        }

        static public int getCorpusCount()
        {
            SqlConnection conn = mainForm.createDBConnection();
            string query = "SELECT MAX (corpus) FROM Floor";
            SqlCommand comm = new SqlCommand(query, conn);
            int count = Convert.ToInt32(comm.ExecuteScalar().ToString());
            conn.Close();
            return count;
        }

        protected SqlConnection conn;
    }
    // /// //////////////////////////////////////////////////////////////////////////////////////////////////

    class GetStudentListCommand : SqlCommandBuilder
    {
        public GetStudentListCommand(string author, bool type, string desc, SqlConnection connection)
        {
            author_ = author;
            type_ = type ? "True" : "False";
            desc_ = desc;
            conn = connection;
        }
        public override SqlCommand buildCommand()
        {
            string sql = "select name, rbook, gender, faculty, yos, place, accomm_range, budget "+
                         "from Student where id in (select student from RT_Student_Lists where idList = " +
                         "(select id from Lists where type = '"+ type_ +"' and author = '"+ author_ +"' and description = '"+ desc_ +"' ))";
            return new SqlCommand(sql, conn);
        }
        private string author_, type_, desc_;
    }
    class GetOrderCommand : SqlCommandBuilder
    {
        public GetOrderCommand(string date, SqlConnection connection)
        {
            date_ = date;
            conn = connection;
        }
        public override SqlCommand buildCommand()
        {
            string sql = "select Student.name, Student.rbook, Student.faculty, Room.number, Floor.corpus from Student " +
                         "full join RT_Student_Lists on RT_Student_Lists.student = Student.id " +
                         "full join Room on Room.id = (select room from Place where id = RT_Student_Lists.place) " +
                         "full join Floor on Floor.id = Room.floor " +
                         "where Student.id in (select RT_Student_Lists.student from RT_Student_Lists where RT_Student_Lists.idList = " +
                         "(select idList from Orders where Orders.date = CONVERT (smalldatetime, '" + date_ + "',120)))";
            return new SqlCommand(sql, conn);
        }
        private string date_;
    }
    class GetAllUsersExceptAdminCommand : SqlCommandBuilder
    {
        public GetAllUsersExceptAdminCommand(SqlConnection connection)
        {
            conn = connection;
        }
        public override SqlCommand buildCommand()
        {
            string sql = "SELECT login FROM login WHERE role <> 1";
            return new SqlCommand(sql, conn);
        }
    }
    class AuthCommand : SqlCommandBuilder
    {
        public AuthCommand(string login_, string pass_, SqlConnection connection)
        {
            login = login_;
            pass = md5Hash.GetMd5Hash(pass_);
            conn = connection;
        }
        override public SqlCommand buildCommand()
        {
            string sql = "select* from login where login = '" + login + "' and password='" + pass + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            return cmd;
        }
        private string login;
        private string pass;
    }
    class ReadAllRowsCommand : SqlCommandBuilder
    {
        public ReadAllRowsCommand(string tableName, SqlConnection connection)
        {
            conn = connection;
            table = tableName;
        }
        public ReadAllRowsCommand(string tableName, string order, SqlConnection connection)
        {
            conn = connection;
            table = tableName;
            order_ = order;
        }
        
        override public SqlCommand buildCommand()
        {
            string sql = "select " + order_ + " from " + table;
            SqlCommand cmd = new SqlCommand(sql, conn);
            return cmd;
        }
        private string table = "";
        private string order_ = "*";
    }
    class SelectAllRowsByTwoColumnsCommand : SqlCommandBuilder
    {
        public SelectAllRowsByTwoColumnsCommand(string tableName, string columnName1, string columnValue1,
                                                                  string columnName2, string columnValue2, SqlConnection connection)
        {
            table = tableName;
            column1 = columnName1;
            column2 = columnName2;
            value1 = columnValue1;
            value2 = columnValue2;
            conn = connection;
        }
        public SelectAllRowsByTwoColumnsCommand(string tableName, string order,  string columnName1, string columnValue1,
                                                                  string columnName2, string columnValue2, SqlConnection connection)
        {
            table = tableName;
            column1 = columnName1;
            column2 = columnName2;
            value1 = columnValue1;
            value2 = columnValue2;
            conn = connection;
            order_ = order;
        }
        override public SqlCommand buildCommand()
        {
            string sql = "SELECT " + order_ + " FROM " + table + " WHERE " + column1 + " = ('" + value1 + "') AND " + column2 + " = ('" + value2+"')";
            SqlCommand cmd = new SqlCommand(sql, conn);
            return cmd;
        }
        private string table;
        private string column1,column2;
        private string value1, value2;
        private string order_ = "*";
    }
    class SelectAllRowsByColumnValueCommand : SqlCommandBuilder
    {
        public SelectAllRowsByColumnValueCommand(string tableName, string columnName, string columnValue, SqlConnection connection)
        {
            conn = connection;
            table = tableName;
            column = columnName;
            value = columnValue;
        }
        public SelectAllRowsByColumnValueCommand(string tableName, string order, string columnName, string columnValue, SqlConnection connection)
        {
            conn = connection;
            table = tableName;
            column = columnName;
            value = columnValue;
            order_ = order;
        }
        override public SqlCommand buildCommand()
        { 
            string sql = "SELECT " + order_ + " FROM " + table + " WHERE " + column + " = '" + value + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            return cmd;
        }
        private string table;
        private string column;
        private string value;
        private string order_ = "*";
    }
    class GetPlaceRowCommand : SqlCommandBuilder
    {
        public GetPlaceRowCommand(string room, string floor, string building, string order, SqlConnection connection)
        {
            conn = connection;
            order_ = order;
            joins_ = " FULL JOIN Room ON Room.id = Place.room FULL JOIN Floor ON Floor.id = Room.floor FULL JOIN Student ON Student.place = Place.id";
            value = "SELECT id FROM Room WHERE number = " + room + " AND floor = " +
                    "(SELECT id FROM Floor WHERE floor_level = " + floor + " AND corpus = " + building + ")";           
        }
        override public SqlCommand buildCommand()
        { 
            string sql = "SELECT " + order_ + " FROM Place" + joins_ + " WHERE room = (" + value + ")";
            SqlCommand cmd = new SqlCommand(sql, conn);
            return cmd;
        }
        private string value;
        private string order_ = "*";
        private string joins_ = "";
    }
    class GetRoomsRowCommand : SqlCommandBuilder
    {
        public GetRoomsRowCommand(int building, int floor, SqlConnection connection)
        {
            conn = connection;
            building_ = building;
            floor_ = floor;
        }
        public GetRoomsRowCommand(int building, int floor, bool faculty, bool state, SqlConnection connection)
        {
            conn = connection;
            building_ = building;
            floor_ = floor;
            tail = faculty ? " AND id IN (SELECT room FROM Place WHERE owner = '" + mainForm.name + (state ? "'" : "' AND state = 'False'") + ")" : "";
        }
        public GetRoomsRowCommand(int building, string columnOrder, SqlConnection connection)
        {
            conn = connection;
            building_ = building;
            columnOrder_ = columnOrder;
            joins = "FULL JOIN Floor ON Floor.id = Room.floor";
        }
        public GetRoomsRowCommand(int building, int floor, string columnOrder, SqlConnection connection)
        {
            conn = connection;
            building_ = building;
            floor_ = floor;
            columnOrder_ = columnOrder;
            joins = "FULL JOIN Floor ON Floor.id = Room.floor";
        }
        public override SqlCommand buildCommand()
        {
            string sql = "SELECT " + columnOrder_ + " FROM Room " + joins + " WHERE floor = (SELECT id FROM Floor WHERE floor_level = '" + 
                                                                                                (floor_).ToString() + "' AND corpus = '" + (building_).ToString() + "')" + tail;
            if (floor_ < 0)
                sql = "SELECT " + columnOrder_ + " FROM Room " + joins + " WHERE floor IN (SELECT id FROM Floor WHERE corpus  = " + building_.ToString() + ")";
                
            return new SqlCommand(sql, conn);
        }
        private int building_;
        private int floor_ = -1;
        private string joins = "";
        private string columnOrder_ = "*";
        private string tail = "";
    }
    class UserExistCommand : SqlCommandBuilder
    {
        public UserExistCommand(string login_, SqlConnection connection)
        {
            login = login_;
            conn = connection;
        }
        override public SqlCommand buildCommand()
        {
            string sql = "SELECT* FROM login WHERE login = '" + login +"'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            return cmd;
        }
        private string login;
    }
    class AddStudentCommand : SqlCommandBuilder
    {
        public AddStudentCommand(string name, string rbook, bool gender, string faculty, int yos, int aRange, int budget, SqlConnection connection)
        {
            name_ = name;
            rbook_ = rbook;
            gender_ = gender;
            faculty_ = faculty;
            yos_ = yos;
            aRange_ = aRange;
            budget_ = budget;
            conn = connection;
        }
        public override SqlCommand buildCommand()
        {
            //insert into Student (name, rbook, gender, faculty, yos, accomm_range, budget) values (,,,,,,)
            string[] tail = { ", acomm_range", ", budget",", @arange",", @budget" };
            string sql = "INSERT INTO Student (name, rbook, gender, faculty, yos"+((aRange_==-1)?"":tail[0]) + ((budget_ == -1)?"":tail[1]) +") "+
                         "VALUES (@name, @rbook, @gender, @faculty, @yos" + ((aRange_ == -1) ? "" : tail[2]) + ((budget_ == -1) ? "" : tail[3]) + ")";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = name_;
            cmd.Parameters.Add("@rbook", SqlDbType.NVarChar).Value = rbook_;
            cmd.Parameters.Add("@gender", SqlDbType.Bit).Value = gender_;
            cmd.Parameters.Add("@faculty", SqlDbType.NVarChar).Value = faculty_;
            cmd.Parameters.Add("@yos", SqlDbType.Int).Value = yos_;
            if (aRange_ !=-1)
                cmd.Parameters.Add("@arange", SqlDbType.Int).Value = aRange_;
            if (budget_ !=-1)
                cmd.Parameters.Add("@budget", SqlDbType.Int).Value = budget_;
            return cmd;
        }

        private string name_, rbook_, faculty_;
        private int yos_, aRange_, budget_;
        private bool gender_;
    }
    class AddUserCommand : SqlCommandBuilder
    {
        public AddUserCommand(string login_, string passHash, int role_, string name_, SqlConnection connection)
        {
            login = login_;
            conn = connection;
            pass = passHash;
            name = name_;
            role = role_;

        }
        override public SqlCommand buildCommand()
        {
            string sql = "INSERT INTO login (login,password,role,name) VALUES (@login, @pass, @role, @name)";
            
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.Parameters.Add("@login", SqlDbType.NVarChar, 15).Value = login;
            cmd.Parameters.Add("@pass", SqlDbType.NVarChar, 32).Value = pass;
            cmd.Parameters.Add("@role", SqlDbType.Int).Value = role;
            cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;
            
            return cmd;
        }
        private string login;
        private string pass;
        private string name;
        private int role;
    }
    class UpdateUserCommand : SqlCommandBuilder
    {
        public UpdateUserCommand(string prevLogin_, string login_, string passHash, int role_, string name_, SqlConnection connection)
        {
            login = login_;
            prevLogin = prevLogin_;
            conn = connection;
            pass = passHash;
            name = name_;
            role = role_;

        }
        override public SqlCommand buildCommand()
        {
            string sql = "UPDATE login SET login = @login, password = @pass, role = @role, name = @name " +
                         "WHERE login = @prevLogin";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.Parameters.Add("@login", SqlDbType.NVarChar, 15).Value = login;
            cmd.Parameters.Add("@prevLogin", SqlDbType.NVarChar, 15).Value = prevLogin;
            cmd.Parameters.Add("@pass", SqlDbType.NVarChar, 32).Value = pass;
            cmd.Parameters.Add("@role", SqlDbType.Int).Value = role;
            cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;

            return cmd;
        }
        private string login;
        private string prevLogin;
        private string pass;
        private string name;
        private int role;
    }
    class RemoveUserCommand : SqlCommandBuilder
    {
        public RemoveUserCommand(string login_, SqlConnection connection)
        {
            login = login_;
            conn = connection;
        }
        override public SqlCommand buildCommand()
        {
            string sql = "DELETE FROM login WHERE login = @login";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.Parameters.Add("@login", SqlDbType.NVarChar, 15).Value = login;
            return cmd;
        }
        private string login;
    }
    class SetBenefitValueCommand : SqlCommandBuilder
    {
        public SetBenefitValueCommand(string desc_, int value_, SqlConnection connection)
        {
            desc = desc_;
            value = value_;
            conn = connection;
        }
        override public SqlCommand buildCommand()
        {
            string sql = "UPDATE Primary_Benefit SET priority = @val1 " +
                         "WHERE description = @desc";
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.Parameters.Add("@val1", SqlDbType.Int).Value = value;
            cmd.Parameters.Add("@desc", SqlDbType.NVarChar).Value = desc;
            return cmd;
        }
        private string desc;
        private int value;
    }
    class SetBenefitStateCommand : SqlCommandBuilder
    {
        public SetBenefitStateCommand(string desc_, bool enabled_, SqlConnection connection)
        {
            desc = desc_;
            enabled = enabled_;
            conn = connection;
        }
        override public SqlCommand buildCommand()
        { 
            string sql = "UPDATE Primary_Benefit SET enabled = @val1 " +
                               "WHERE description = @desc";
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.Parameters.Add("@val1", SqlDbType.Bit).Value = enabled;
            cmd.Parameters.Add("@desc", SqlDbType.NVarChar).Value = desc;
            return cmd;
        }
        private string desc;
        private bool enabled;
    }
    class GetFacultyStudentsCommand : SqlCommandBuilder
    {
        public GetFacultyStudentsCommand(string faculty_, int yearOfStudy_, SqlConnection connection)
        {
            conn = connection;
            yearOfStudy = yearOfStudy_;
            faculty = faculty_;
        }
        override public SqlCommand buildCommand()
        {
            string sql  = "SELECT * FROM Student WHERE faculty = '" + faculty + "' AND yos = '" + yearOfStudy.ToString() + "'";
            return new SqlCommand(sql, conn);
        }
        private string faculty;
        private int yearOfStudy;
    }
    class GetFacultyListCommand : SqlCommandBuilder
    {
        public GetFacultyListCommand(SqlConnection connection)
        {
            conn = connection;
        }
        override public SqlCommand buildCommand()
        {
            string sql = "select* from login where role = '3'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            return cmd;
        }
    }
    class BindBenefitToStudent : SqlCommandBuilder
    {
        public BindBenefitToStudent (string rBook, List<Object> benefits, SqlConnection connection)
        {
            rBook_ = rBook;
            benefits_ = benefits;
            conn = connection;
        }
        public override SqlCommand buildCommand()
        {
            string sql = "INSERT INTO RT_Student_Benefit (student, benefit) VALUES ";
            SqlCommand findStudentCmd = new SqlCommand("SELECT id FROM Student WHERE rbook = '" + rBook_+"'", conn);
            string studId = findStudentCmd.ExecuteScalar().ToString();

            for (int i = 0; i < benefits_.Count; i++)
            {
                ListViewItem item = (ListViewItem)benefits_.ElementAt(i);
                string sqlTail = "('" + studId + "', (SELECT id FROM Primary_Benefit WHERE description = '" + item.Text + "'))";
                if (i != benefits_.Count - 1)
                    sqlTail += ",";
                sql += sqlTail;
            }
            return new SqlCommand(sql, conn);
        }
        private string rBook_;
        private List<Object> benefits_;
    }
    class SelectStudentCommand : SqlCommandBuilder
    {
        public SelectStudentCommand(string faculty, string gender, List<int> yos, string isHavePlace, SqlConnection connection)
        {
            faculty_ = faculty;
            gender_ = gender;
            yos_ = yos;
            isHavePlace_ = isHavePlace;
            conn = connection;
        }
        public override SqlCommand buildCommand()
        {
            string yosList = "(";
            foreach(int i in yos_)
                yosList += i.ToString() + ",";
            yosList = yosList.Substring(0,yosList.Length-1)+")";
            string sql = "SELECT name, rbook, gender, faculty, yos, place, accomm_range, budget FROM Student WHERE faculty = '" + faculty_ + "'"
                + ((gender_ !="")?" AND gender = '"+gender_+"' ":"")
                + ((isHavePlace_ !="")?((isHavePlace_ == "True")?" AND place IS NOT NULL ":" AND place IS NULL "):"")
                + ((yos_.Count!=0)?" AND yos IN "+yosList+" ":"");
            return new SqlCommand(sql, conn);
        }
        private string faculty_, gender_;
        List<int> yos_;
        string isHavePlace_;

    }
    class RenameListCommand : SqlCommandBuilder
    {
        public RenameListCommand(string oldDescription, string newDescription, SqlConnection connection)
        {
            new_ = newDescription;
            old_ = oldDescription;
            conn = connection;
        }
        public override SqlCommand buildCommand()
        {
            string sql = "UPDATE Lists SET description='" + new_ + "' WHERE description = '" + old_ + "'";
            return new SqlCommand(sql, conn);
        }
        private string old_, new_;
    }
    class GetListIDCommand : SqlCommandBuilder
    {
        public GetListIDCommand(string description, SqlConnection connection)
        {
            desc_ = description;
            conn = connection;
        }
        public override SqlCommand buildCommand()
        {
            string sql = "SELECT id FROM Lists WHERE description = '" + desc_ + "'";
            return new SqlCommand(sql, conn);
        }
        private string desc_;
    }
    class RemoveListCommand : SqlCommandBuilder
    {
        public RemoveListCommand(int ID, SqlConnection connection)
        {
            id = ID;
            conn = connection;
        }
        public override SqlCommand buildCommand()
        {   
            new SqlCommand("DELETE FROM RT_Student_Lists WHERE idList = " + id, conn).ExecuteNonQuery();
            string sql = "DELETE FROM Lists WHERE id = " + id;
            return new SqlCommand(sql, conn);
        }
        private int id;
    }
    class AddListCommand : SqlCommandBuilder
    {
        public AddListCommand(bool type, DateTime date, string description, List<string> rBooksEnum, SqlConnection connection)
        {
            type_ = type;
            date_ = date.Year.ToString() + "-" + date.Month.ToString() + "-" + date.Day.ToString() + " " +
                    date.Hour.ToString() + ":" + date.Minute.ToString() + ":" + date.Second.ToString();
            desc_ = description;
            rBooksEnum_ = rBooksEnum;
            conn = connection;
        }
        public override SqlCommand buildCommand()
        {
            string sql = "INSERT INTO Lists (type, date, author, description) VALUES (" + ((type_) ? "'True', " : "'False', ") + "CONVERT (smalldatetime, '" + date_ + "',120), '" +
                         mainForm.name + "', '" + desc_ + "')";
            new SqlCommand(sql, conn).ExecuteNonQuery();
            int listID = Convert.ToInt32(SqlCommandBuilder.getQueryResult(new GetListIDCommand(desc_, conn).buildCommand()).getValue(0,0));
            string rBooks = "";
            foreach (string currentStr in rBooksEnum_)
                rBooks += "'" + currentStr + "', ";
            rBooks = rBooks.Substring(0, rBooks.Length - 2);
            sql = "SELECT id FROM Student WHERE rbook in (" + rBooks + ")";
            QueryResult qr = SqlCommandBuilder.getQueryResult(new SqlCommand(sql, conn));
            string values = "";
            for (int i = 0; i < qr.getRowCount(); i++)
                values += qr.getValue(i, 0) + ", " + listID + "), (";
            values = values.Substring(0, values.Length - 4);
            sql = "INSERT INTO RT_Student_Lists (student, idList) VALUES (" + values + ")";

            return new SqlCommand(sql, conn);
        }        
        private bool type_;
        private string date_;
        private string desc_;
        private List<string> rBooksEnum_;
    }
}
