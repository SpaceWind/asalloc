using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ASAlloc
{

    class roomDescriptor : IComparable
    {        
        public enum Gender { undefined = 0, man = 1, woman = 2, mixed = 3 };
        public int id_ = 0;
        public int number = 0;
        public int maxPlaces = 0;
        public int currentPlaces = 0;
        public Gender gender = Gender.undefined;
        public int minYos = 0;
        public int maxYos = 0;
        public bool yosViolation = false;
        public bool genderViolation = false;
        public List<int> places = new List<int>();
        //
        int IComparable.CompareTo(object obj)
        {
            var room = (roomDescriptor)obj;
            double fillPercent = ((double)currentPlaces) /
                                 ((double)maxPlaces);
            double roomFillPercent = ((double)room.currentPlaces) /
                                     ((double)room.maxPlaces);
            if (fillPercent > roomFillPercent)
                return 1;
            if (fillPercent < roomFillPercent)
                return -1;
            else
            {
                if (maxPlaces > room.maxPlaces)
                    return 1;
                if (maxPlaces < room.maxPlaces)
                    return -1;
                return 0;
            }
        }
        public bool checkGenderComp(studentDescriptor student)
        {
            if (gender == Gender.mixed ||
                gender == Gender.undefined)
                return true;
            if (student.gender)
            {
                if (gender == Gender.man)
                    return true;
            }
            else if (gender == Gender.woman)
                return true;
            return false;
        }
        public roomDescriptor()
        {
            ;
        }
        public roomDescriptor(int id, Gender g, int maxplaces)
        {
            id_ = id;
            gender = g;
            maxPlaces = maxplaces;
        }
        public void addPlace(int idPlace)
        {
            places.Add(idPlace);
            maxPlaces++;
        }
        public bool isStudentAddingLegal(studentDescriptor student)
        {
            bool sameYosInRoom = (maxYos - minYos == 2);
            if ((student.yos == minYos) && (currentPlaces < maxPlaces))
            {
                if (!checkGenderComp(student))
                    return false;
                return true;
            }
            else if ((student.yos == maxYos) && (currentPlaces < maxPlaces))
            {
                if (!checkGenderComp(student))
                    return false;
                return true;
            }
            else if ((student.yos == (minYos + 1)) && sameYosInRoom)
            {
                if (!checkGenderComp(student))
                    return false;
                return true;
            }
            else if (currentPlaces < maxPlaces)
            {
                if (!checkGenderComp(student))
                    return false;
                return true;
            }
            return false;

        }
        public bool addStudent(studentDescriptor student)
        {
            if (minYos == 0 && maxYos == 0)
            {
                minYos = student.yos - 1;
                maxYos = student.yos + 1;
                if (gender == Gender.undefined)
                    switch (student.gender)
                    {
                        case true:
                            gender = Gender.man;
                            break;
                        case false:
                            gender = Gender.woman;
                            break;
                        default:
                            break;
                    }
            }
            bool sameYosInRoom = (maxYos - minYos == 2);
            if ((student.yos == minYos) && (currentPlaces < maxPlaces))
            {
                if (sameYosInRoom)
                    maxYos--;
                genderViolation = !checkGenderComp(student);
                currentPlaces++;
                return true;
            }
            else if ((student.yos == maxYos) && (currentPlaces < maxPlaces))
            {
                if (sameYosInRoom)
                    minYos++;
                genderViolation = !checkGenderComp(student);
                currentPlaces++;
                return true;
            }
            else if ((student.yos == (minYos + 1)) && sameYosInRoom)
            {
                currentPlaces++;
                genderViolation = !checkGenderComp(student);
                return true;
            }
            else if (currentPlaces < maxPlaces)
            {
                currentPlaces++;
                yosViolation = true;
                genderViolation = !checkGenderComp(student);
                return true;
            }
            return false;
        }
    }
    class studentDescriptor : IComparable
    {
        int IComparable.CompareTo(object obj)
        {
            studentDescriptor sd = (studentDescriptor)obj;
            if (benefitCoef > sd.benefitCoef)
                return 1;
            if (benefitCoef < sd.benefitCoef)
                return -1;
            else
            {
                double selfSecondaryBenefitCoef = normalizeCoefs(studentDescriptor.coefs.minRange, studentDescriptor.coefs.maxRange,
                                                                 studentDescriptor.coefs.minBudget, studentDescriptor.coefs.maxBudget,
                                                                 studentDescriptor.coefs.coef, range, budget);
                double objectSecondaryBenefitCoef = normalizeCoefs(studentDescriptor.coefs.minRange, studentDescriptor.coefs.maxRange,
                                                                   studentDescriptor.coefs.minBudget, studentDescriptor.coefs.maxBudget,
                                                                   studentDescriptor.coefs.coef, sd.range, sd.budget);
                if (selfSecondaryBenefitCoef > objectSecondaryBenefitCoef)
                    return 1;
                if (selfSecondaryBenefitCoef < objectSecondaryBenefitCoef)
                    return -1;
                else
                    return 0;
            }

        }
        private double normalizeCoefs(double minRange, double maxRange, double minBudget, double maxBudget, double coef, double range, double budget)
        {
            double budgetCoef = ((budget == 0) || (maxRange - minRange == 1)) ? 0 : (1 - (budget - minBudget) / (maxBudget - minBudget));
            double accommRangeCoef = (range == 0) ? 0 : ((range - minRange) / (maxRange - minRange));
            return budgetCoef - coef * (budgetCoef - accommRangeCoef);
        }

        public studentDescriptor(string rBook_, int prevPlace)
        {
            rBook = rBook_;
            prevPlaceId = prevPlace;
            place = 0;
            findViolationCount();
        }
        public studentDescriptor(studentDescriptor src)
        {
            rBook = src.rBook;
            prevPlaceId = src.prevPlaceId;
            place = src.place;
            violationCount = src.violationCount;
        }

        public void findViolationCount()
        {
            SqlConnection objConn = mainForm.createDBConnection();
            violationCount = Convert.ToInt32(new GetViolationCountForStudent(rBook.Trim(), objConn).buildCommand().ExecuteScalar().ToString());
            objConn.Close();
        }
        public class secondaryBenefitCoefs
        {
            public secondaryBenefitCoefs()
            {
                minBudget = maxBudget = minRange = maxRange = coef = 0;
            }
            public double minRange;
            public double maxRange;
            public double minBudget;
            public double maxBudget;
            public double coef;
        }
        public static secondaryBenefitCoefs coefs = new secondaryBenefitCoefs();
        public string rBook { get; set; }
        public int prevPlaceId { get; set; }
        public int place { get; set; }
        public int violationCount;
        public int range { get; set; }
        public int budget { get; set; }
        public int benefitCoef { get; set; }
        public int yos { get; set; }
        public bool gender { get; set; }
    }
    class AllocatingSystem
    {
        public List<studentDescriptor> allocatedStudents { get; private set; }
        public List<studentDescriptor> deniedStudents { get; private set; }
        private List<studentDescriptor> studentsToAllocate;
        private Dictionary<int, int> placeToRoomMap = new Dictionary<int,int>();
        private List<int> placeOccupated = new List<int>();

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
            QueryResult qr = SqlCommandBuilder.getQueryResult(new GetResidentsInfoCommand(faculty, objConn).buildCommand());
            studentDescriptor.coefs = getSecondaryBenefitsCoefs(faculty, objConn);
            List<studentDescriptor> students = new List<studentDescriptor>();
            for (int i = 0; i < qr.getRowCount(); i++)
            {
                string rBook = qr.getValue(i, 0);
                string strPrevPlace = qr.getValue(i,1);
                int place = (strPrevPlace == null)?0:Convert.ToInt32(strPrevPlace);
                
                var student = new studentDescriptor(rBook, place);
                string rangeStr = qr.getValue(i, 2);
                student.range = (rangeStr == "") ? -1 : Convert.ToInt32(rangeStr);
                string budgetStr = qr.getValue(i, 3);
                student.budget = (budgetStr == "") ? -1 : Convert.ToInt32(budgetStr);
                string benefitCoefString = qr.getValue(i, 6);
                student.benefitCoef = (benefitCoefString == "") ? 0 : Convert.ToInt32(benefitCoefString);
                student.yos = Convert.ToInt32(qr.getValue(i, 4));
                string genderStr = qr.getValue(i, 5);
                student.gender = (genderStr == "True");
                students.Add(student);
            }
            objConn.Close();
            return students;
        }
        studentDescriptor.secondaryBenefitCoefs getSecondaryBenefitsCoefs(string faculty, SqlConnection objConn)
        {
            QueryResult benefitCoefsResult = SqlCommandBuilder.getQueryResult(new GetSecondaryBenefitsCommand(faculty, objConn).buildCommand());
            studentDescriptor.secondaryBenefitCoefs secBenefitCoefs = new studentDescriptor.secondaryBenefitCoefs();
            secBenefitCoefs.minRange = (benefitCoefsResult.getValue(0, 0) == "") ? 0 : Convert.ToDouble(benefitCoefsResult.getValue(0, 0));
            secBenefitCoefs.maxRange = (benefitCoefsResult.getValue(0, 1) == "") ? 0 : Convert.ToDouble(benefitCoefsResult.getValue(0, 1));
            if (secBenefitCoefs.maxRange == secBenefitCoefs.minBudget)
                secBenefitCoefs.maxRange += 1.0;
            secBenefitCoefs.minBudget = (benefitCoefsResult.getValue(0, 2) == "") ? 0 : Convert.ToDouble(benefitCoefsResult.getValue(0, 2));
            secBenefitCoefs.maxBudget = (benefitCoefsResult.getValue(0, 3) == "") ? 0 : Convert.ToDouble(benefitCoefsResult.getValue(0, 3));
            if (secBenefitCoefs.maxBudget == secBenefitCoefs.minBudget)
                secBenefitCoefs.maxBudget += 1.0;
            return secBenefitCoefs;

        }
        struct doubleStudentList
        {
            public List<studentDescriptor> primaryList;
            public List<studentDescriptor> slaveList;
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

        int getRoomIndexById(List<roomDescriptor> rooms, int id)
        {
            int index = 0;
            foreach (roomDescriptor room in rooms)
            {
                if (room.id_ == id)
                    return index;
                index++;
            }
            return -1;
        }
        List<roomDescriptor> getRoomsAvailable(string faculty)
        {
            List<roomDescriptor> rooms = new List<roomDescriptor>();
            SqlConnection objConn = mainForm.createDBConnection();
            QueryResult qr = SqlCommandBuilder.getQueryResult(new GetRoomInfoCommand(faculty, objConn).buildCommand());
            for (int i = 0; i < qr.getRowCount(); i++)
            {
                int roomId = Convert.ToInt32(qr.getValue(i, 0));
                string genderStr = qr.getValue(i, 1);
                roomDescriptor.Gender gender;
                if (genderStr.ToLower() == "мужская")
                    gender = roomDescriptor.Gender.man;
                else if (genderStr.ToLower() == "женская")
                    gender = roomDescriptor.Gender.woman;
                else if (genderStr.ToLower() == "смешанная")
                    gender = roomDescriptor.Gender.mixed;
                else
                    gender = roomDescriptor.Gender.undefined;
                int maxPlaces = Convert.ToInt32(qr.getValue(i, 2));
                roomDescriptor room = new roomDescriptor(roomId, gender, maxPlaces);
                rooms.Add(room);
            }
            objConn.Close();
            return rooms;
        }
        Dictionary<int,List<int>> setupRoomToPlaceListMap(Dictionary<int,int> map)
        {
            int[] keys = new int[map.Count];
            map.Keys.CopyTo(keys, 0);
            int[] values = new int[map.Count];
            map.Values.CopyTo(values, 0);
            List<int> uniqueValues = new List<int>();
            foreach (int v in values)
                if (!uniqueValues.Contains(v))
                    uniqueValues.Add(v);
            Dictionary<int, List<int>> result = new Dictionary<int,List<int>>();
            foreach (int uv in uniqueValues)
            {
                List<int> places = new List<int>();
                int index = 0;
                foreach(int value in values)
                {
                    if (uv == value)
                        places.Add(keys[index]);
                    index++;
                }
                result[uv] = places;
            }
            return result;
        }
        void setupPlaceToRoomMap(string faculty)
        {
            SqlConnection objConn = mainForm.createDBConnection();
            QueryResult qr = SqlCommandBuilder.getQueryResult(new SelectAllRowsByColumnValueCommand("Place", "Place.id, Place.room", "owner", faculty, objConn).buildCommand());

            for (int i = 0; i < qr.getRowCount(); i++)
                placeToRoomMap[Convert.ToInt32(qr.getValue(i, 0))] = Convert.ToInt32(qr.getValue(i, 1));

            objConn.Close();
        }
        bool isYosAllowed(List<int> yoses, int newYos)
        {
            int minYos, maxYos;
            bool singleYosStudent = (yoses.Count == 2);
            minYos = yoses[0];
            maxYos = yoses[1];
            if (newYos >= minYos && newYos <= maxYos)
                return true;
            return false;

        }
        List<int> updateYosAllowed(List<int> yosAllowed, int newYos)
        {
            if (yosAllowed.Count == 0)
            {
                yosAllowed.Add(newYos - 1);
                yosAllowed.Add(newYos + 1);
                yosAllowed.Add(newYos);
                return yosAllowed;
            }
            if (isYosAllowed(yosAllowed,newYos))
            {
                if (yosAllowed.Count == 3)
                {
                    if (newYos == yosAllowed[2])
                        return yosAllowed;
                    else
                    {            
                        List<int> result = new List<int>();
                        result.Add(Math.Min(yosAllowed[2], newYos));
                        result.Add(Math.Max(yosAllowed[2], newYos));
                        return result;
                    }
                }
                else if (yosAllowed.Count == 2)
                    return yosAllowed;
            }
            return yosAllowed;

        }
        doubleStudentList FindStudentGroup(List<studentDescriptor> allStudents, int groupSize)
        {
            doubleStudentList result = new doubleStudentList();
            result.primaryList = new List<studentDescriptor>();
            foreach (studentDescriptor student in allStudents)
            {
                result.slaveList = allStudents;
                List<studentDescriptor> currentGroup = new List<studentDescriptor>();
                List<int> yosAllowed = new List<int>();
                yosAllowed = updateYosAllowed(yosAllowed, student.yos);
                currentGroup.Add(student);
                result.slaveList.Remove(student);
                bool groupFound = false;
                foreach (studentDescriptor secondStudent in allStudents)
                {
                    if (student == secondStudent)
                        continue;
                    if ((secondStudent.gender == student.gender) && isYosAllowed(yosAllowed, secondStudent.yos))
                    {
                        yosAllowed = updateYosAllowed(yosAllowed, secondStudent.yos);
                        if (currentGroup.Count != groupSize)
                        {
                            result.slaveList.Remove(secondStudent);
                            currentGroup.Add(secondStudent);
                        }
                    }
                    if (currentGroup.Count == groupSize)
                    {
                        result.primaryList = currentGroup;
                        groupFound = true;
                        break;
                    }
                }
                if (groupFound)
                    break;
            }
            if (result.primaryList.Count == 0)
                result.slaveList = allStudents;
            return result;
        }

        bool settle(roomDescriptor room, studentDescriptor student, Dictionary<int,List<int>> roomsToPlaceMap)
        {
            List<int> placesInRoom = roomsToPlaceMap[room.id_];
            foreach (int idPlace in placesInRoom)
                if (!placeOccupated.Contains(idPlace))
                {
                    student.place = idPlace;
                    room.addStudent(student);
                    allocatedStudents.Add(student);
                    placeOccupated.Add(idPlace);
                    return true;
                }
            return false;
        }
        public void allocateStudents(string faculty)
        {
            studentsToAllocate = getStudentsList(faculty);
            doubleStudentList filteredByViolationStudentLists = filterStudentsByViolationCount(studentsToAllocate);
            deniedStudents = new List<studentDescriptor>();
            deniedStudents.AddRange(filteredByViolationStudentLists.slaveList);

            List<studentDescriptor> sortedStudents = filteredByViolationStudentLists.primaryList;
            sortedStudents.Sort();
            sortedStudents.Reverse();
            List<roomDescriptor> allRooms = getRoomsAvailable(faculty);
            setupPlaceToRoomMap(faculty);
            //1. Заселить старых в их койки
            doubleStudentList filteredStudentsByPrevPlaces = filterStudentsByPrevPlaces(sortedStudents);
            List<studentDescriptor> oldStudents = filteredStudentsByPrevPlaces.primaryList;
            List<studentDescriptor> newStudents = filteredStudentsByPrevPlaces.slaveList;
            List<studentDescriptor> restOfStudents = new List<studentDescriptor>();
            var roomsToPlaceMap = setupRoomToPlaceListMap(placeToRoomMap);
            //Заселяем старых
            List<roomDescriptor> oldStudentRooms = new List<roomDescriptor>();
            allocatedStudents = new List<studentDescriptor>();
            foreach (studentDescriptor student in oldStudents)
            {
                student.place = student.prevPlaceId;
                roomDescriptor room = allRooms.ElementAt(getRoomIndexById(allRooms, placeToRoomMap[student.place]));
                room.addStudent(student);
                allocatedStudents.Add(student);
                placeOccupated.Add(student.place);
                oldStudentRooms.Add(room);
            }
            //2. Заселить как можно больше новых студентов в комнаты со старыми студентами
            studentsToAllocate = newStudents;
            foreach (studentDescriptor student in studentsToAllocate)
            {
                bool isStudentAllocated = false;
                foreach (roomDescriptor room in oldStudentRooms)
                {
                    
                    if (room.isStudentAddingLegal(student))
                        isStudentAllocated = settle(room,student,roomsToPlaceMap);
                }
                if (!isStudentAllocated)
                    restOfStudents.Add(student);
            }
            //3. Найти и отсортировать комнаты по размеру.
            studentsToAllocate = restOfStudents;
            var emptyRooms = findEmptyRooms(allRooms);
            emptyRooms.Sort();
            emptyRooms.Reverse();
            //3.0 Пытаемся заселить пустые комнаты в цикле от полной плотности заселения до 1 человека
            for (int roomSizeDecrease = 0; ; roomSizeDecrease++)
            {
                bool AtLeastOneRoomValid = false;
                foreach (roomDescriptor room in emptyRooms)
                {
                    if (room.currentPlaces != 0)
                        continue;
                    int currentRoomSize = room.maxPlaces - roomSizeDecrease;
                    if (currentRoomSize > 0)
                        AtLeastOneRoomValid = true;
                    else
                        continue;
                    doubleStudentList studentGroup = FindStudentGroup(studentsToAllocate, currentRoomSize);
                    if (studentGroup.primaryList.Count != 0)
                    {
                        room.currentPlaces += studentGroup.primaryList.Count;
                        foreach (studentDescriptor settleStudent in studentGroup.primaryList)
                        {
                            settle(room, settleStudent, roomsToPlaceMap);
                        }
                        studentsToAllocate = studentGroup.slaveList;
                    }
                }
                if (!AtLeastOneRoomValid)
                    break;
            }

            //5. Заселсяем всех оставшихся студентов в комнаты с ошибками.
            List<roomDescriptor> sortedRooms = new List<roomDescriptor>();
            List<roomDescriptor> trueEmptyRooms = new List<roomDescriptor>();
            sortedRooms.AddRange(oldStudentRooms);
            foreach (roomDescriptor room in emptyRooms)
                if (room.currentPlaces != 0)
                    sortedRooms.Add(room);
                else
                    trueEmptyRooms.Add(room);
            sortedRooms.Sort();
            sortedRooms.Reverse();
            trueEmptyRooms.Sort();
            sortedRooms.AddRange(trueEmptyRooms);

            bool allRoomsOccupated = false;
            while (!allRoomsOccupated)
            {
                studentDescriptor student;
                if (studentsToAllocate.Count == 0)
                    break;
                student = studentsToAllocate.First();
                allRoomsOccupated = true;
                foreach (roomDescriptor room in sortedRooms)
                    if (room.currentPlaces != room.maxPlaces)
                    {
                        allRoomsOccupated = false;
                        settle(room, student, roomsToPlaceMap);
                        studentsToAllocate.RemoveAt(0);
                        break;
                    }
            }
            deniedStudents.AddRange(studentsToAllocate);


        }
        List<roomDescriptor> findEmptyRooms(List<roomDescriptor> allRooms)
        {
            var result = new List<roomDescriptor>();
            foreach (roomDescriptor room in allRooms)
                if (room.currentPlaces == 0)
                    result.Add(room);
            return result;
        }
        

        

    }
}
