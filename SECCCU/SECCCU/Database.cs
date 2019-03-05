using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SECCCU
{
    public class Database
    {
        private SqlConnection Connection { get; set; }



        public bool CreateConnection()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
                {
                    DataSource = "secccuserver.database.windows.net",
                    UserID = "secccuadmin",
                    Password = "SeccuPass1337$!",
                    InitialCatalog = "secccusql"
                };
                Connection = new SqlConnection(builder.ConnectionString);
                Connection.Open();
                Connection.Close();
                return true;
            }
            catch (SqlException e)
            {
                Debug.WriteLine("ERROR: " + e);
                return false;
            }
        }

        public bool InitializeDatabase()
        {
            bool success = true;

            Console.WriteLine("\nStart database initialization DEBUG:");
            Console.WriteLine("=========================================\n");


            StringBuilder sb = new StringBuilder();

            Debug.WriteLine("CODE: Dropping tables");

            sb.Append("DROP TABLE IF EXISTS LOG;");
            sb.Append("DROP TABLE IF EXISTS LECTURES;");
            sb.Append("DROP TABLE IF EXISTS LECTURERS;");
            sb.Append("DROP TABLE IF EXISTS SCANNERS;");
            sb.Append("DROP TABLE IF EXISTS ROOMS;");
            sb.Append("DROP TABLE IF EXISTS STUDENTS;");
            sb.Append("DROP TABLE IF EXISTS PROGRAMMES;");

            success = SendQueryToDatabase(sb.ToString());
            if (!success) return success;
            sb = new StringBuilder();

            Debug.WriteLine("CODE: CREATE TABLE programmes");
            sb.Append("CREATE TABLE programmes(");
            sb.Append("programme_id		char(9) 	    PRIMARY KEY,");
            sb.Append("programme_name	varchar(40)	    NOT NULL");
            sb.Append(")");

            success = SendQueryToDatabase(sb.ToString());
            if (!success) return success;

            sb = new StringBuilder();

            Debug.WriteLine("CODE: CREATE TABLE lecturers");
            sb.Append("CREATE TABLE lecturers(");
            sb.Append("lecturer_id 	    char(12)		PRIMARY KEY,");
            sb.Append("surname 		    varchar (40)	NOT NULL,");
            sb.Append("first_name	    varchar (40)	NOT NULL");
            sb.Append(")");

            success = SendQueryToDatabase(sb.ToString());
            if (!success) return success;

            sb = new StringBuilder();

            Debug.WriteLine("CODE: CREATE TABLE rooms");
            sb.Append("CREATE TABLE rooms(");
            sb.Append("room_id 	        int             IDENTITY(1,1)    PRIMARY KEY,");
            sb.Append("room_name 		varchar (5) 	UNIQUE              NOT NULL");
            sb.Append(")");

            success = SendQueryToDatabase(sb.ToString());
            if (!success) return success;

            sb = new StringBuilder();

            Debug.WriteLine("CODE: CREATE TABLE scanners");
            sb.Append("CREATE TABLE scanners(");
            sb.Append("scanner_id 	    int             IDENTITY(1,1)    PRIMARY KEY,");
            sb.Append("room_id 		    int	            FOREIGN KEY      REFERENCES rooms(room_id)");
            sb.Append(")");

            success = SendQueryToDatabase(sb.ToString());
            if (!success) return success;

            sb = new StringBuilder();

            Debug.WriteLine("CODE: CREATE TABLE lectures");
            sb.Append("CREATE TABLE lectures(");
            sb.Append("lecture_id 	    int	            IDENTITY(1,1)   PRIMARY KEY,");
            sb.Append("lecture_name	    varchar(40)                 ,");
            sb.Append("lecture_start    datetime2		    NOT NULL,");
            sb.Append("lecture_end	    datetime2		    NOT NULL,");
            sb.Append("room_id	        int             FOREIGN KEY     REFERENCES      rooms(room_id),");
            sb.Append("programme_id	    char(9)		    FOREIGN KEY     REFERENCES      programmes(programme_id),");
            sb.Append("lecturer_id	    char(12)		FOREIGN KEY     REFERENCES      lecturers(lecturer_id)");
            sb.Append(")");

            success = SendQueryToDatabase(sb.ToString());
            if (!success) return success;

            sb = new StringBuilder();

            Debug.WriteLine("CODE: CREATE TABLE students");
            sb.Append("CREATE TABLE students(");
            sb.Append("student_id	    char(12)        PRIMARY KEY,");
            sb.Append("surname	        varchar(40)	    NOT NULL,");
            sb.Append("first_name       varchar(40)	    NOT NULL,");
            sb.Append("programme_id	    char(9)		    FOREIGN KEY     REFERENCES      programmes(programme_id),");
            sb.Append("CONSTRAINT       CK_first_name_Length            CHECK (LEN(first_name) >= 3),");
            sb.Append("CONSTRAINT       CK_surname_Length               CHECK (LEN(surname) >= 3)");
            sb.Append(");");

            success = SendQueryToDatabase(sb.ToString());
            if (!success) return success;

            sb = new StringBuilder();

            Debug.WriteLine("CODE: CREATE TABLE log");
            sb.Append("CREATE TABLE log(");
            sb.Append("log_id           int	            PRIMARY KEY     IDENTITY(1,1),");
            sb.Append("student_id       char(12)        FOREIGN KEY     REFERENCES students(student_id),");
            sb.Append("scan_time	    datetime2		NOT NULL,");
            sb.Append("scanner_id	    int		        FOREIGN KEY     REFERENCES scanners(scanner_id)");
            sb.Append(");");

            success = SendQueryToDatabase(sb.ToString());
            if (!success) return success;

            sb = new StringBuilder();

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
                sb.Append("INSERT INTO students (surname, first_name, student_id, programme_id)");
                sb.Append($"VALUES ('{rows[i][0]}', '{rows[i][1]}','{rows[i][2]}','{rows[i][3]}');");
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
            return success;
        }

        private bool SendQueryToDatabase(string query)
        {
            try
            {
                int rowsAffected;
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    Connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                    Connection.Close();
                }
                Debug.WriteLine("CODE: Command executed successfully \n \t" + rowsAffected + " Rows Affected!");
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("ERROR: " + e);
                return false;
            }
        }


        public string[] LogCardSwipe(string cardNumber)
        {
            string[] returnString = new string[2];
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO log (student_id, scan_time, scanner_id)");
            sb.Append($"VALUES ('{cardNumber}', GETDATE(),1);");
            try
            {
                using (SqlCommand command = new SqlCommand(sb.ToString(), Connection))
                {
                    Debug.WriteLine("CODE: Executing command");
                    Connection.Open();
                    command.ExecuteNonQuery();
                }

                using (SqlCommand command = new SqlCommand($"SELECT first_name, surname  FROM students WHERE student_id = '{cardNumber}';", Connection))
                {
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        returnString[0] = string.Format($"{dataReader.GetString(0)} {dataReader.GetString(1)}");
                        returnString[1] = "Swipe Success";
                    }
                }
            }
            catch (SqlException exception)
            {

                switch (exception.Number)
                {
                    case 547:
                        returnString[0] = "Card Read Error";
                        returnString[1] = "Error";
                        break;
                    case 8152:
                        returnString[0] = "Card Read Error";
                        returnString[1] = "Error";
                        break;
                    default:
                        throw;
                }
            }

            Connection.Close();
            return returnString;
        }

        public string[] DidUserSwipeInCurrentLecture(string cardNumber)
        {
            string[] returnString = new String[3];

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT students.first_name, students.surname, lectures.lecture_name FROM log ");
            sb.Append(" JOIN students ON log.student_id = students.student_id ");
            sb.Append(" JOIN programmes ON students.programme_id = programmes.programme_id ");
            sb.Append(" JOIN lectures ON programmes.programme_id = lectures.programme_id ");
            sb.Append(" JOIN rooms ON lectures.room_id = rooms.room_id ");
            sb.Append(" JOIN scanners ON log.scanner_id = scanners.scanner_id ");
            sb.Append($"WHERE log.student_id = '{cardNumber}' ");
            sb.Append("AND scan_time > DATEADD(minute, -15, lecture_start) ");
            sb.Append("AND scan_time < lecture_end ");
            sb.Append("AND GETDATE() > DATEADD(minute, -15, lecture_start) ");
            sb.Append("AND GETDATE() < lecture_end; ");
            try
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand(sb.ToString(), Connection))
                {
                    SqlDataReader dataReader = command.ExecuteReader();
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
                        using (SqlCommand getNameCommand =
                            new SqlCommand(
                                $"SELECT first_name, surname FROM students WHERE student_id = '{cardNumber}';",
                                Connection))
                        {
                            SqlDataReader dataReader2 = getNameCommand.ExecuteReader();
                            if (dataReader2.Read())
                            {
                                returnString[0] = dataReader2.GetString(0);
                                returnString[1] = dataReader2.GetString(1);
                            }
                        }
                    }
                }
            }
            catch (SqlException exception)
            {

                switch (exception.Number)
                {
                    case 547:
                        break;
                    default:
                        throw;
                }
            }

            Connection.Close();
            return returnString;
        }

        public List<string> GetProgrammeTitles()
        {
            List<string> programmes = new List<string>();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT programme_id FROM programmes;");

            try
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand(sb.ToString(), Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            programmes.Add(reader.GetString(0));
                        }
                    }
                }

            }
            catch (SqlException exception)
            {

                switch (exception.Number)
                {
                    case 547:
                        break;
                    default:
                        throw;
                }
            }
            Connection.Close();
            return programmes;
        }

        public List<string> GetProgrammeReport(string programmeID)
        {
            List<string> report = new List<string>();

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT students.first_name, students.surname, lectures.lecture_name, lectures.lecture_start FROM log ");
            sb.Append(" JOIN students   ON log.student_id = students.student_id ");
            sb.Append(" JOIN programmes ON students.programme_id = programmes.programme_id ");
            sb.Append(" JOIN lectures   ON programmes.programme_id = lectures.programme_id ");
            sb.Append(" JOIN rooms      ON lectures.room_id = rooms.room_id ");
            sb.Append(" JOIN scanners   ON log.scanner_id = scanners.scanner_id ");
            sb.Append($" WHERE programmes.programme_id = '{programmeID}' AND");
            sb.Append(" (log.scan_time < lectures.lecture_start OR log.scan_time > lectures.lecture_end);");

            try
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand(sb.ToString(), Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string tempString = String.Format($"{reader.GetString(0)} {reader.GetString(1)} did not attend the {reader.GetDateTime(3).ToString("dd/MMM/yy HH:mm")} {reader.GetString(2)} lecture");
                            report.Add(tempString);
                        }
                    }
                }

            }
            catch (SqlException exception)
            {

                switch (exception.Number)
                {
                    case 547:
                        break;
                    default:
                        throw;
                }
            }

            sb = new StringBuilder();
            sb.Append(" SELECT students.first_name, students.surname, lectures.lecture_name, lectures.lecture_start, log.scan_time FROM log ");
            sb.Append(" JOIN students   ON log.student_id = students.student_id ");
            sb.Append(" JOIN programmes ON students.programme_id = programmes.programme_id ");
            sb.Append(" JOIN lectures   ON programmes.programme_id = lectures.programme_id ");
            sb.Append(" JOIN rooms      ON lectures.room_id = rooms.room_id ");
            sb.Append(" JOIN scanners   ON log.scanner_id = scanners.scanner_id ");
            sb.Append($" WHERE programmes.programme_id = '{programmeID}' AND");
            sb.Append(" log.scan_time > lectures.lecture_start AND log.scan_time < lectures.lecture_end;");

            try
            {
                using (SqlCommand command = new SqlCommand(sb.ToString(), Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string tempString = String.Format($"{reader.GetString(0)} {reader.GetString(1)} attended the {reader.GetDateTime(3).ToString("dd/MMM/yy HH:mm")} {reader.GetString(2)} lecture at {reader.GetDateTime(4).ToString("HH:mm")}");
                            report.Add(tempString);
                        }
                    }
                }

            }
            catch (SqlException exception)
            {

                switch (exception.Number)
                {
                    case 547:
                        break;
                    default:
                        throw;
                }
            }
            Connection.Close();


            return report;
        }
    }
}
