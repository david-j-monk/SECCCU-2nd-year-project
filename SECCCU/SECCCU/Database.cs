using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SECCCU
{
    public class Database
    {
        private SQLiteConnection Connection { get; set; }



        public bool CreateConnection()
        {
            var databasePath = Path.Combine("SECCCUDB.db3");
            if (!File.Exists(databasePath))
            {
                SQLiteConnection.CreateFile(databasePath);
            }
            try
            {
                Connection = new SQLiteConnection($"data source = {databasePath}");
                Connection.Open();
                Connection.Close();
                return true;
            }
            catch (SQLiteException e)
            {
                Debug.WriteLine("ERROR:" + e);
                return false;
            }
        }

        public bool InitializeDatabase()
        {
            bool success = true;


            Console.WriteLine("\nStart database initialization DEBUG:");
            Console.WriteLine("=========================================\n");

            Debug.WriteLine("CODE: Dropping tables");
            string sqlCommand = @"
            PRAGMA foreign_keys = ON;
            DROP TABLE IF EXISTS LOG;
            DROP TABLE IF EXISTS LECTURES;
            DROP TABLE IF EXISTS LECTURERS;
            DROP TABLE IF EXISTS SCANNERS;
            DROP TABLE IF EXISTS ROOMS;
            DROP TABLE IF EXISTS STUDENTS;
            DROP TABLE IF EXISTS PROGRAMMES;
            ";
            success = SendQueryToDatabase(sqlCommand);
            if (!success) return success;

            Debug.WriteLine("CODE: CREATE TABLE [programmes]");
            sqlCommand = @"CREATE TABLE programmes (
            programme_id    char(9) 	PRIMARY KEY,
            programme_name	varchar(40)	NOT NULL
            )";

            success = SendQueryToDatabase(sqlCommand);
            if (!success) return success;

            Debug.WriteLine("CODE: CREATE TABLE lecturers");
            sqlCommand = @"CREATE TABLE lecturers(
            lecturer_id 	char(12)		PRIMARY KEY,
            surname 		varchar (40)	NOT NULL,
            first_name	    varchar (40)	NOT NULL
            )";

            success = SendQueryToDatabase(sqlCommand);
            if (!success) return success;


            Debug.WriteLine("CODE: CREATE TABLE rooms");
            sqlCommand = @"CREATE TABLE rooms(
            room_id 	    INTEGER         PRIMARY KEY,
            room_name 		varchar (5) 	UNIQUE NOT NULL
            )";

            success = SendQueryToDatabase(sqlCommand);
            if (!success) return success;

            Debug.WriteLine("CODE: CREATE TABLE scanners");
            sqlCommand = @"CREATE TABLE scanners(
            scanner_id 	    INTEGER         PRIMARY KEY,
            room_id 		int,
            FOREIGN KEY     (room_id)       REFERENCES      rooms(room_id)
            )";

            success = SendQueryToDatabase(sqlCommand);
            if (!success) return success;


            Debug.WriteLine("CODE: CREATE TABLE lectures");
            sqlCommand = @"CREATE TABLE lectures(
            lecture_id 	    INTEGER         PRIMARY KEY,
            lecture_name	varchar(40),
            lecture_start   datetime2		NOT NULL,
            lecture_end	    datetime2		NOT NULL,
            room_id	        int,
            programme_id	char(9),
            lecturer_id	    char(12),
            FOREIGN KEY     (room_id)       REFERENCES      rooms(room_id),
		    FOREIGN KEY     (programme_id)  REFERENCES      programmes(programme_id),
            FOREIGN KEY     (lecturer_id)	REFERENCES      lecturers(lecturer_id)
            )";

            success = SendQueryToDatabase(sqlCommand);
            if (!success) return success;



            Debug.WriteLine("CODE: CREATE TABLE students");
            sqlCommand = @"CREATE TABLE students(
            row_id          INTEGER         PRIMARY KEY     AUTOINCREMENT,
            student_id	    char(12)        UNIQUE,
            surname	        varchar(40)	    NOT NULL,
            first_name      varchar(40)	    NOT NULL,
            phone_number    varchar(13),
            programme_id	char(9),
            FOREIGN KEY     (programme_id)                  REFERENCES      programmes(programme_id),
            CONSTRAINT      CK_first_name_Length            CHECK           (LENGTH(first_name) >= 3),
            CONSTRAINT      CK_surname_Length               CHECK           (LENGTH(surname) >= 3)
            );";

            success = SendQueryToDatabase(sqlCommand);
            if (!success) return success;


            Debug.WriteLine("CODE: CREATE TABLE log");
            sqlCommand = @"CREATE TABLE log(
            student_id      char(12),
            scan_time	    datetime		NOT NULL,
            scanner_id	    int,
            FOREIGN KEY     (scanner_id)    REFERENCES      scanners(scanner_id),
            CONSTRAINT FK_studentID         FOREIGN KEY     (student_id)    REFERENCES      students(student_id)
            );";

            success = SendQueryToDatabase(sqlCommand);
            if (!success) return success;
            var sb = new StringBuilder();

            //Programmes to DB
            Debug.WriteLine("CODE: Programmes to DB");
            string[][] rows = File.ReadAllLines("csvFiles\\programmes.csv").Select(l => l.Split(',').ToArray()).ToArray();
            for (int i = 0; i < rows.GetLength(0); i++)
            {
                sb.Append("INSERT INTO programmes (programme_id, programme_name)");
                sb.Append($"VALUES ('{rows[i][0]}', '{rows[i][1]}');");
            }

            success = SendQueryToDatabase(sb.ToString());
            if (!success) return success;

            sb = new StringBuilder();

            //Lecturers to DB
            Debug.WriteLine("CODE: Lecturers to DB");
            rows = File.ReadAllLines("csvFiles\\lecturers.csv").Select(l => l.Split(',').ToArray()).ToArray();
            for (int i = 0; i < rows.GetLength(0); i++)
            {
                sb.Append("INSERT INTO lecturers (lecturer_id, surname, first_name)");
                sb.Append($"VALUES ('{rows[i][0]}', '{rows[i][1]}', '{rows[i][2]}');");
            }

            success = SendQueryToDatabase(sb.ToString());
            if (!success) return success;

            sb = new StringBuilder();

            //Rooms to DB
            Debug.WriteLine("CODE: Rooms to DB");
            rows = File.ReadAllLines("csvFiles\\rooms.csv").Select(l => l.Split(',').ToArray()).ToArray();
            for (int i = 0; i < rows.GetLength(0); i++)
            {
                sb.Append("INSERT INTO rooms (room_name)");
                sb.Append($"VALUES ('{rows[i][0]}');");
            }


            success = SendQueryToDatabase(sb.ToString());
            if (!success) return success;

            sb = new StringBuilder();

            //Students to DB
            Debug.WriteLine("CODE: Students to DB");
            rows = File.ReadAllLines("csvFiles\\students.csv").Select(l => l.Split(',').ToArray()).ToArray();
            for (int i = 0; i < rows.GetLength(0); i++)
            {
                sb.Append("INSERT INTO students (surname, first_name, student_id, programme_id, phone_number )");
                sb.Append($"VALUES ('{rows[i][0]}', '{rows[i][1]}','{rows[i][2]}','{rows[i][3]}','{rows[i][4]}' );");
            }

            success = SendQueryToDatabase(sb.ToString());
            if (!success) return success;

            sb = new StringBuilder();

            //Scanners to DB
            Debug.WriteLine("CODE: Scanners to DB");
            rows = File.ReadAllLines("csvFiles\\scanners.csv").Select(l => l.Split(',').ToArray()).ToArray();
            for (int i = 0; i < rows.GetLength(0); i++)
            {
                sb.Append("INSERT INTO scanners (room_id)");
                sb.Append($"VALUES ('{rows[i][0]}');");
            }

            success = SendQueryToDatabase(sb.ToString());
            if (!success) return success;

            sb = new StringBuilder();

            //Lectures to DB
            Debug.WriteLine("CODE: Lectures to DB");
            rows = File.ReadAllLines("csvFiles\\lectures.csv").Select(l => l.Split(',').ToArray()).ToArray();
            for (int i = 0; i < rows.GetLength(0); i++)
            {
                sb.Append("INSERT INTO lectures (lecture_name, lecture_start, lecture_end, room_id, programme_id, lecturer_id)");
                sb.Append($"VALUES ('{rows[i][0]}', '{rows[i][1]}', '{rows[i][2]}', '{rows[i][3]}', '{rows[i][4]}', '{rows[i][5]}');");
            }

            success = SendQueryToDatabase(sb.ToString());

            //if (!success) return success;
            //sb = new StringBuilder();

            ////Students to DB
            //Debug.WriteLine("CODE: Students to DB");
            //rows = File.ReadAllLines("csvFiles\\students.csv").Select(l => l.Split(',').ToArray()).ToArray();
            //for (int i = 0; i < rows.GetLength(0); i++)
            //{
            //    sb.Append("INSERT INTO log (student_id, scan_time, scanner_id )");
            //    sb.Append($"VALUES ('{rows[i][2]}', '1970-01-01', 1); ");
            //}
            //success = SendQueryToDatabase(sb.ToString());

            return success;
        }

        private bool SendQueryToDatabase(string query)
        {
            try
            {
                int rowsAffected;

                using (SQLiteCommand sqLiteCommand = new SQLiteCommand(query, Connection))
                {
                    Connection.Open();
                    rowsAffected = sqLiteCommand.ExecuteNonQuery();
                    Connection.Close();
                }

                Debug.WriteLine("CODE: Command executed successfully \n \t" + rowsAffected + " " + "Rows Affected!");
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("ERROR: " + e);
                return false;
            }
        }

        public string AddStudent(string first, string surname, string phone, string programme)
        {
            int lastID = 0;
            string programmeID = "";
            try
            {
                using (SQLiteCommand command = new SQLiteCommand("SELECT row_id from students order by ROWID DESC limit 1", Connection))
                {
                    Connection.Open();
                    SQLiteDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        lastID = dataReader.GetInt32(0) + 1;

                    }
                    Connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "False";
            }

            try
            {

                using (SQLiteCommand command = new SQLiteCommand($"SELECT programme_id from programmes WHERE programme_name = '{programme}' limit 1", Connection))
                {
                    Connection.Open();
                    SQLiteDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        programmeID = dataReader.GetString(0);
                    }
                    Connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "False";
            }
            string student_id =
                String.Format($"{surname.Substring(0, 3).ToUpper()}{first.Substring(0, 3).ToUpper()}{lastID:D6}");

            if (SendQueryToDatabase(String.Format($"INSERT INTO students (surname, first_name, student_id, programme_id, phone_number) VALUES ('{surname}', '{first}', '{student_id}', '{programmeID}', '{phone}');")))
            {
                return student_id;
            }
            else
            {
                
                return "False";
            }

        }

        public string[] LogCardSwipe(string cardNumber)
        {
            string[] returnString = new string[3];
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO log (student_id, scan_time, scanner_id)");
            sb.Append($"VALUES ('{cardNumber}', datetime('now'),1);");
            bool cardNumberExists = false;
            try
            {
                using (SQLiteCommand command = new SQLiteCommand($"SELECT * FROM students WHERE student_id = '{cardNumber}'", Connection))
                {
                    Debug.WriteLine("CODE: Executing command");
                    Connection.Open();
                    SQLiteDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        cardNumberExists = true;
                    }

                    if (!cardNumberExists)
                    {
                        returnString[0] = "Card Read Error";
                        returnString[1] = "Error";
                    }
                  Connection.Close();
                }

                if (cardNumberExists)
                {
                    using (SQLiteCommand command = new SQLiteCommand(sb.ToString(), Connection))
                    {
                        Debug.WriteLine("CODE: Executing command");
                        Connection.Open();
                        command.ExecuteNonQuery();
                        Connection.Close();
                    }
                }



                using (SQLiteCommand command = new SQLiteCommand($"SELECT first_name, surname, phone_number FROM students WHERE student_id = '{cardNumber}';", Connection))
                {
                    Connection.Open();
                    SQLiteDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        returnString[0] = String.Format($"{dataReader.GetString(0)} {dataReader.GetString(1)}");
                        returnString[1] = "Swipe Success";
                        returnString[2] = dataReader.GetString(2);
                    }
                    Connection.Close();
                }
            }
            catch (SQLiteException exception)
            {
                returnString[0] = "Card Read Error";
                returnString[1] = "Error";
                Console.WriteLine(exception);
            }


            return returnString;
        }

        public string[] DidUserSwipeInCurrentLecture(string cardNumber)
        {
            string[] returnString = new String[3] { "", "", "" };


            string sqlCommand = $@" SELECT students.first_name, students.surname, lectures.lecture_name 
            FROM log, students, programmes, lectures, scanners, rooms
            WHERE   log.student_id = students.student_id
            AND     students.programme_id = programmes.programme_id
            AND     programmes.programme_id = lectures.programme_id
            AND     lectures.room_id = rooms.room_id
            AND     log.scanner_id = scanners.scanner_id
            AND     log.student_id = '{cardNumber}'
            AND     scan_time > datetime(lecture_start, '-15 minutes')
            AND     scan_time < lecture_end
            AND     datetime('now') > datetime(lecture_start, '-15 minutes')
            AND     datetime('now') < lecture_end; ";
            try
            {
                Connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(sqlCommand, Connection))
                {
                    SQLiteDataReader dataReader = command.ExecuteReader();
                    if (dataReader.Read())
                    {
                        returnString[0] = dataReader.GetString(0);
                        returnString[1] = dataReader.GetString(1);
                        returnString[2] = dataReader.GetString(2);
                    }
                    else
                    {
                        Connection.Close();
                        Connection.Open();
                        using (SQLiteCommand getNameCommand =
                        new SQLiteCommand(
                        $"SELECT first_name, surname FROM students WHERE student_id = '{cardNumber}';",
                        Connection))
                        {
                            SQLiteDataReader dataReader2 = getNameCommand.ExecuteReader();
                            if (dataReader2.Read())
                            {
                                returnString[0] = dataReader2.GetString(0);
                                returnString[1] = dataReader2.GetString(1);
                            }
                        }
                    }
                }
            }
            catch (SQLiteException exception)
            {

                Console.WriteLine(exception);
            }

            Connection.Close();
            return returnString;
        }

        public List<string> GetProgrammeTitles()
        {
            List<string> programmes = new List<string>();
            string sqlCommand = "SELECT programme_name FROM programmes;";

            try
            {
                Connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(sqlCommand, Connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            programmes.Add(reader.GetString(0));
                        }
                    }
                }

            }
            catch (SQLiteException exception)
            {

                Console.WriteLine(exception);
            }
            Connection.Close();
            return programmes;
        }

        public List<string> GetReport(string programmeID, string module, string dateFrom, string dateTo, string studentID)
        {
            bool studentExists = false;
            Connection.Open();
            using (SQLiteCommand command = new SQLiteCommand(
                $@"SELECT * FROM students WHERE student_id = '{studentID}'", Connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        studentExists = true;
                    }
                }
                Connection.Close();
            }

            StringBuilder sb = new StringBuilder();
            StringBuilder csvBuilder = new StringBuilder();
            List<string> report = new List<string>();
            sb.Append("SELECT students.student_id, students.first_name, students.surname, lectures.lecture_name, lectures.lecture_start, programmes.programme_name");
            sb.Append(" FROM students, scanners, programmes, lectures, rooms");
            sb.Append(" WHERE students.programme_id = programmes.programme_id  ");
            sb.Append(" AND programmes.programme_id = lectures.programme_id  ");
            if (studentExists)
            {
                sb.Append($" AND '{studentID}' = students.student_id  ");
            }
            sb.Append(programmeID == "*"
                ? $" AND programmes.programme_name IS NOT NULL "
                : $" AND programmes.programme_name = '{programmeID}' ");
            sb.Append(module == "*"
                ? $" AND lectures.lecture_name IS NOT NULL "
                : $" AND lectures.lecture_name = '{module}' ");
            sb.Append($" AND lectures.lecture_start >= '{dateFrom}' ");
            sb.Append($" AND lectures.lecture_end <= '{dateTo} 23:59:59' ");
            sb.Append(" EXCEPT  ");
            sb.Append(" SELECT students.student_id, students.first_name, students.surname, lectures.lecture_name, lectures.lecture_start, programmes.programme_name ");
            sb.Append(" FROM students, log, scanners, programmes, lectures, rooms");
            sb.Append(" WHERE ");
            sb.Append(" students.student_id = log.student_id");
            if (studentExists)
            {
                sb.Append($" AND '{studentID}' = students.student_id  ");
            }
            sb.Append(" AND log.scanner_id = scanners.scanner_id");
            sb.Append(" AND students.programme_id = programmes.programme_id");
            sb.Append(" AND programmes.programme_id = lectures.programme_id  ");
            sb.Append(" AND lectures.room_id = rooms.room_id  ");
            sb.Append(programmeID == "*"
                ? $" AND programmes.programme_name IS NOT NULL "
                : $" AND programmes.programme_name = '{programmeID}' ");
            sb.Append(module == "*"
                ? $" AND lectures.lecture_name IS NOT NULL "
                : $" AND lectures.lecture_name = '{module}' ");
            sb.Append($" AND lectures.lecture_start >= '{dateFrom}' ");
            sb.Append($" AND lectures.lecture_end <= '{dateTo} 23:59:59' ");
            sb.Append(" AND log.scan_time > datetime(lectures.lecture_start, '-15 minutes') ");
            sb.Append(" AND log.scan_time < datetime(lectures.lecture_start, '+15 minutes');");


            try
            {
                Connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(sb.ToString(), Connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        bool firstLine = true;
                        while (reader.Read())
                        {
                            if (firstLine)
                            {
                                report.Add(String.Format("Non-Attending"));
                                csvBuilder.AppendLine(String.Format("Date Time,Student ID,Programme,Module,Surname,First Name,Attended?"));
                                firstLine = false;

                            }
                            csvBuilder.AppendLine(String.Format($"{reader.GetDateTime(4).ToString("dd/MMM/yy HH:mm")},{reader.GetString(0)},{reader.GetString(5)},{reader.GetString(3)},{reader.GetString(2)},{reader.GetString(1)},No"));
                            string tempString = String.Format($"{reader.GetDateTime(4).ToString("dd/MMM/yy HH:mm")}\t{reader.GetString(0)}\t{reader.GetString(1)} {reader.GetString(2)}");
                            if (programmeID == "*" || module == "*")
                            {
                                tempString += String.Format($"\t{reader.GetString(5)}\t{reader.GetString(3)}");
                            }
                            report.Add(tempString);
                        }
                    }
                }

            }
            catch (SQLiteException exception)
            {


                Console.WriteLine(exception);
            }


            sb = new StringBuilder();
            //sb.Append(" SELECT students.student_id, students.first_name, students.surname, lectures.lecture_name, lectures.lecture_start, programmes.programme_name FROM log ");
            //sb.Append(" JOIN students   ON log.student_id = students.student_id ");
            //sb.Append(" JOIN programmes ON students.programme_id = programmes.programme_id ");
            //sb.Append(" JOIN lectures   ON programmes.programme_id = lectures.programme_id ");
            //sb.Append(" JOIN rooms      ON lectures.room_id = rooms.room_id ");
            //sb.Append(" JOIN scanners ON log.scanner_id = scanners.scanner_id ");
            //sb.Append(programmeID == "*"
            //? $" WHERE programmes.programme_name IS NOT NULL AND "
            //: $" WHERE programmes.programme_name = '{programmeID}' AND ");
            //sb.Append(module == "*"
            //? $" lectures.lecture_name IS NOT NULL AND"
            //: $" lectures.lecture_name = '{module}' AND");
            //sb.Append($" lectures.lecture_start >= '{dateFrom}' AND");
            //sb.Append($" lectures.lecture_end <= '{dateTo} 23:59:59' AND");
            //sb.Append(" log.scan_time > datetime(lectures.lecture_start, '-15 minutes') AND log.scan_time < datetime(lectures.lecture_start, '+15 minutes');");
            sb.Append(" SELECT students.student_id, students.first_name, students.surname, lectures.lecture_name, lectures.lecture_start, programmes.programme_name ");
            sb.Append(" FROM students, log, scanners, programmes, lectures, rooms");
            sb.Append(" WHERE ");
            sb.Append(" students.student_id = log.student_id");
            sb.Append(" AND log.scanner_id = scanners.scanner_id");
            if (studentExists)
            {
                sb.Append($" AND '{studentID}' = students.student_id  ");
            }
            sb.Append(" AND students.programme_id = programmes.programme_id");
            sb.Append(" AND programmes.programme_id = lectures.programme_id  ");
            sb.Append(" AND lectures.room_id = rooms.room_id  ");
            sb.Append(programmeID == "*"
                ? $" AND programmes.programme_name IS NOT NULL "
                : $" AND programmes.programme_name = '{programmeID}' ");
            sb.Append(module == "*"
                ? $" AND lectures.lecture_name IS NOT NULL "
                : $" AND lectures.lecture_name = '{module}' ");
            sb.Append($" AND lectures.lecture_start >= '{dateFrom}' ");
            sb.Append($" AND lectures.lecture_end <= '{dateTo} 23:59:59' ");
            sb.Append(" AND log.scan_time > datetime(lectures.lecture_start, '-15 minutes') ");
            sb.Append(" AND log.scan_time < datetime(lectures.lecture_start, '+15 minutes');");

            try
            {
                using (SQLiteCommand command = new SQLiteCommand(sb.ToString(), Connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        bool firstLine = true;
                        while (reader.Read())
                        {
                            if (firstLine)
                            {
                                report.Add(String.Format($"Attended"));
                                firstLine = false;

                            }
                            csvBuilder.AppendLine(String.Format($"{reader.GetDateTime(4).ToString("dd/MMM/yy HH:mm")},{reader.GetString(0)},{reader.GetString(5)},{reader.GetString(3)},{reader.GetString(2)},{reader.GetString(1)},Yes"));
                            string tempString = String.Format($"{reader.GetDateTime(4).ToString("dd/MMM/yy HH:mm")}\t{reader.GetString(0)}\t{reader.GetString(1)} {reader.GetString(2)}");
                            if (programmeID == "*" || module == "*")
                            {
                                tempString += String.Format($"\t{reader.GetString(5)}\t{reader.GetString(3)}");
                            }
                            report.Add(tempString);
                        }
                    }
                }

            }
            catch (SQLiteException exception)
            {

                Console.WriteLine(exception);
            }
            Connection.Close();

            File.WriteAllText("csvFiles\\Report.csv", csvBuilder.ToString());
            return report;
        }

        public List<string> GetModules(string programme)
        {
            List<string> modulesList = new List<string>();

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT DISTINCT lecture_name FROM lectures ");
            sb.Append(" JOIN programmes ON programmes.programme_id = lectures.programme_id ");
            sb.Append(programme == "*"
            ? $" WHERE programmes.programme_name IS NOT NULL;"
            : $" WHERE programmes.programme_name = '{programme}';");

            try
            {
                Connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(sb.ToString(), Connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string tempString = reader.GetString(0);
                            modulesList.Add(tempString);
                        }
                    }
                }

            }
            catch (SQLiteException exception)
            {

                Console.WriteLine(exception);
            }
            Connection.Close();

            return modulesList;
        }
    }
}
