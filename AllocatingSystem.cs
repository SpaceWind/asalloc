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
            findBenefits();
            findViolationCount();
        }
        public studentDescriptor(studentDescriptor src)
        {
            rBook = src.rBook;
            prevPlaceId = src.prevPlaceId;
            place = src.place;
            benefits_ = src.benefits_;
            violationCount = src.violationCount;
        }
        public void findBenefits()
        {
            SqlConnection objConn = mainForm.createDBConnection();
            QueryResult qr = SqlCommandBuilder.getQueryResult(new GetStudentBenefitsId(rBook, objConn).buildCommand());
            for (int i = 0; i < qr.getRowCount(); i++)
                benefits_.Add(Convert.ToInt32(qr.getValue(i, 0)));
            objConn.Close();
        }
        public void findViolationCount()
        {
            SqlConnection objConn = mainForm.createDBConnection();
            violationCount = Convert.ToInt32(new GetViolationCountForStudent(rBook, objConn).buildCommand().ExecuteScalar().ToString());
            objConn.Close();
        }
        public string rBook { get; set; }
        public int prevPlaceId { get; set; }
        public int place { get; set; }
        public List<int> benefits_ = new List<int>();
        // Dictionary<string, List<int>> benefits_ = new Dictionary<string,List<int>>();
        public int violationCount;
        public int range { get; set; }
        public int budget { get; set; }
    }
    class AllocatingSystem
    {
        public List<studentDescriptor> allocatedStudents
        {
            get;
            private set;
        }
        public List<studentDescriptor> deniedStudents
        {
            get;
            private set;
        }
        private List<studentDescriptor> studentsToAllocate;

        /// <summary>
        /// Функция, которая ищет всех студентов данного факультета, ждущих заселения
        /// Находит койку, в которой они лежали в прошлом году
        /// Считывает их нарушения и льготы
        /// И заполняет дескрипторы студента
        /// </summary>
        /// <param name="faculty">Имя факультета, студентов которого будем искать</param>
        /// <returns>Список дескрипторов студентов, которых мы нашли</returns>
        private List<studentDescriptor> getStudentsList(string faculty)
        {
            SqlConnection objConn = mainForm.createDBConnection();
            QueryResult qr = SqlCommandBuilder.getQueryResult(new GetPrevPlacesForStudents(faculty, objConn).buildCommand());
            List<studentDescriptor> students = new List<studentDescriptor>();
            for (int i = 0; i < qr.getRowCount(); i++)
            {
                string rBook = qr.getValue(i, 0);
                string strPrevPlace = qr.getValue(i,1);
                int place = (strPrevPlace == null)?0:Convert.ToInt32(strPrevPlace);
                
                var student = new studentDescriptor(rBook, place);
                string rangeStr = qr.getValue(i, 2);
                student.range = (rangeStr == null) ? -1 : Convert.ToInt32(rangeStr);
                string budgetStr = qr.getValue(i, 3);
                student.budget = (budgetStr == null) ? -1 : Convert.ToInt32(budgetStr);
                students.Add(student);
            }
            objConn.Close();
            return students;
        }
        struct doubleStudentList
        {
            public List<studentDescriptor> primaryList;
            public List<studentDescriptor> slaveList;
        }
        private List<studentDescriptor> sortStudentsByBenefit(List<studentDescriptor> allStudents)
        {
            SqlConnection objConn = mainForm.createDBConnection();
            QueryResult qr = SqlCommandBuilder.getQueryResult(new GetBenefitCoefs(objConn).buildCommand());
            return new List<studentDescriptor>();
            objConn.Close();
        }
        private doubleStudentList filterStudentsByViolationCount(List<studentDescriptor> allStudents)
        {
            List<studentDescriptor> allowed = new List<studentDescriptor>();
            List<studentDescriptor> notAllowed = new List<studentDescriptor>();

            foreach (studentDescriptor student in allStudents)
                if (student.violationCount > 2)
                    notAllowed.Add(student);
                else
                    allowed.Add(student);
            doubleStudentList result = new doubleStudentList();
            result.primaryList = allowed;
            result.slaveList = notAllowed;
            return result;
        }
        private doubleStudentList filterStudentsByPrevPlaces(List<studentDescriptor> allStudents)
        {
            List<studentDescriptor> highPriority = new List<studentDescriptor>();
            List<studentDescriptor> lowPriority = new List<studentDescriptor>();
            foreach (studentDescriptor student in allStudents)
                if (student.prevPlaceId == 0)
                    lowPriority.Add(student);
                else
                    highPriority.Add(student);
            doubleStudentList result = new doubleStudentList();
            result.primaryList = highPriority;
            result.slaveList = lowPriority;
            return result;
        }

        public AllocatingSystem()
        {
            ;//
        }
        public void allocateStudents(string faculty)
        {
            studentsToAllocate = getStudentsList(faculty);
            doubleStudentList filteredByViolationStudentLists = filterStudentsByViolationCount(studentsToAllocate);
            deniedStudents.AddRange(filteredByViolationStudentLists.slaveList);
        }
        

        

    }
}
