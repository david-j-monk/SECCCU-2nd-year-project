using System;
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

            Console.WriteLine("\nQuery data example:");
            Console.WriteLine("=========================================\n");

            Debug.WriteLine("CODE: Dropping and creating database tables.");

            StringBuilder sb = new StringBuilder();

            Debug.WriteLine("CODE: Dropping tables");

            sb.Append("DROP TABLE IF EXISTS LECTURE;");
            sb.Append("DROP TABLE IF EXISTS LECTURER;");
            sb.Append("DROP TABLE IF EXISTS SCANNER;");
            sb.Append("DROP TABLE IF EXISTS ROOM;");
            sb.Append("DROP TABLE IF EXISTS STUDENT;");
            sb.Append("DROP TABLE IF EXISTS PROGRAMME;");
            sb.Append("DROP TABLE IF EXISTS LOG;");

            success = SendQueryToDatabase(sb.ToString());
            if (!success) return success;
            sb = new StringBuilder();

            Debug.WriteLine("CODE: Dropping tables");
            sb.Append("CREATE TABLE programme(");
            sb.Append("programme_id		char(9) 	    PRIMARY KEY,");
            sb.Append("programme_name	varchar(40)	    NOT NULL");
            sb.Append(")");

            success = SendQueryToDatabase(sb.ToString());
            if (!success) return success;

            sb = new StringBuilder();

            Debug.WriteLine("CODE: Dropping tables");
            sb.Append("CREATE TABLE lecturer(");
            sb.Append("lecturer_id 	    char(12)		PRIMARY KEY,");
            sb.Append("surname 		    varchar (40)	NOT NULL,");
            sb.Append("first_name	    varchar (40)	NOT NULL");
            sb.Append(")");
            
            success = SendQueryToDatabase(sb.ToString());
            if (!success) return success;

            sb = new StringBuilder();

            Debug.WriteLine("CODE: Dropping tables");
            sb.Append("CREATE TABLE room(");
            sb.Append("room_id 	        int             IDENTITY(1,1)    PRIMARY KEY,");
            sb.Append("room_name 		varchar (5) 	UNIQUE              NOT NULL");
            sb.Append(")");
            
            success = SendQueryToDatabase(sb.ToString());
            if (!success) return success;

            sb = new StringBuilder();

            Debug.WriteLine("CODE: Dropping tables");
            sb.Append("CREATE TABLE scanner(");
            sb.Append("scanner_id 	    int             IDENTITY(1,1)    PRIMARY KEY,");
            sb.Append("room_id 		    int	            FOREIGN KEY      REFERENCES room(room_id)");
            sb.Append(")");

            success = SendQueryToDatabase(sb.ToString());
            if (!success) return success;

            sb = new StringBuilder();

            Debug.WriteLine("CODE: Dropping tables");
            sb.Append("CREATE TABLE lecture(");
            sb.Append("lecture_id 	    int	            IDENTITY(1,1)   PRIMARY KEY,");
            sb.Append("lecture_name	    char(40)                ,");
            sb.Append("lecture_start    time		    NOT NULL,");
            sb.Append("lecture_end	    time		    NOT NULL,");
            sb.Append("location	        int             FOREIGN KEY     REFERENCES      room(room_id),");
            sb.Append("programme_id	    char(9)		    FOREIGN KEY     REFERENCES      programme(programme_id),");
            sb.Append("lecturer_id	    char(12)		FOREIGN KEY     REFERENCES      lecturer(lecturer_id)");
            sb.Append(")");

            success = SendQueryToDatabase(sb.ToString());
            if (!success) return success;

            sb = new StringBuilder();

            Debug.WriteLine("CODE: Dropping tables");
            sb.Append("CREATE TABLE student(");
            sb.Append("student_id	    char(12)        PRIMARY KEY,");
            sb.Append("surname	        varchar(40)	    NOT NULL,");
            sb.Append("first_name       varchar(40)	    NOT NULL,");
            sb.Append("programme_id	    char(9)		    FOREIGN KEY     REFERENCES      programme(programme_id),");
            sb.Append("CONSTRAINT       CK_first_name_Length            CHECK (LEN(first_name) >= 3),");
            sb.Append("CONSTRAINT       CK_surname_Length               CHECK (LEN(surname) >= 3)");
            sb.Append(");");

            success = SendQueryToDatabase(sb.ToString());
            if (!success) return success;

            sb = new StringBuilder();

            Debug.WriteLine("CODE: Dropping tables");
            sb.Append("CREATE TABLE log(");
            sb.Append("log_id           int	            PRIMARY KEY     IDENTITY(1,1),");
            sb.Append("student_id       char(12)        FOREIGN KEY     REFERENCES student(student_id),");
            sb.Append("scan_time	    datetime2		NOT NULL");
            sb.Append(");");

            success = SendQueryToDatabase(sb.ToString());
            if (!success) return success;

            sb = new StringBuilder();

            //Programmes to DB
            Debug.WriteLine("CODE: Dropping tables");
            string[][] rows = File.ReadAllLines("csvFiles\\programmes.csv").Select(l => l.Split(',').ToArray()).ToArray();
            for (int i = 0; i < rows.GetLength(0); i++)
            {
                sb.Append("INSERT INTO programme (programme_id, programme_name)");
                sb.Append($"VALUES ('{rows[i][0]}', '{rows[i][1]}');");
            }

            success = SendQueryToDatabase(sb.ToString());
            if (!success) return success;

            sb = new StringBuilder();

            //Lecturers to DB
            Debug.WriteLine("CODE: Dropping tables");
            rows = File.ReadAllLines("csvFiles\\lecturer.csv").Select(l => l.Split(',').ToArray()).ToArray();
            for (int i = 0; i < rows.GetLength(0); i++)
            {
                sb.Append("INSERT INTO lecturer (lecturer_id, surname, first_name)");
                sb.Append($"VALUES ('{rows[i][0]}', '{rows[i][1]}', '{rows[i][2]}');");
            }

            success = SendQueryToDatabase(sb.ToString());
            if (!success) return success;

            sb = new StringBuilder();

            //Rooms to DB
            Debug.WriteLine("CODE: Dropping tables");
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
            Debug.WriteLine("CODE: Dropping tables");
            rows = File.ReadAllLines("csvFiles\\students.csv").Select(l => l.Split(',').ToArray()).ToArray();
            for (int i = 0; i < rows.GetLength(0); i++)
            {
                sb.Append("INSERT INTO student (surname, first_name, student_id, programme_id)");
                sb.Append($"VALUES ('{rows[i][0]}', '{rows[i][1]}','{rows[i][2]}','{rows[i][3]}');");
            }

            success = SendQueryToDatabase(sb.ToString());
            if (!success) return success;

            sb = new StringBuilder();

            //Scanners to DB
            Debug.WriteLine("CODE: Dropping tables");
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
            Debug.WriteLine("CODE: Dropping tables");
            rows = File.ReadAllLines("csvFiles\\lectures.csv").Select(l => l.Split(',').ToArray()).ToArray();
            for (int i = 0; i < rows.GetLength(0); i++)
            {
                sb.Append("INSERT INTO lecture (lecture_name, lecture_start, lecture_end, location, programme_id, lecturer_id)");
                sb.Append($"VALUES ('{rows[i][0]}', '{rows[i][1]}', '{rows[i][2]}', '{rows[i][3]}', '{rows[i][4]}', '{rows[i][5]}');");
            }

            success = SendQueryToDatabase(sb.ToString());
            return success;


            //string query = sb.ToString();
            //try
            //{
            //    int rowsAffected;
            //    using (SqlCommand command = new SqlCommand(query, Connection))
            //    {
            //        Debug.WriteLine("CODE: Executing command");
            //        Connection.Open();
            //        rowsAffected = command.ExecuteNonQuery();
            //        Connection.Close();
            //    }
            //    Debug.WriteLine("CODE: Command executed successfully \n \t" + rowsAffected + " Rows Affected!");
            //    return true;

            //}
            //catch (Exception e)
            //{
            //    Debug.WriteLine("ERROR: " + e);
            //    return false;
            //}

        }

        private bool SendQueryToDatabase(string query)
        {
            try
            {
                int rowsAffected;
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    Debug.WriteLine("CODE: Executing command");
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
            string[] returnString = new string [2];
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO log (student_id, scan_time)");
            sb.Append($"VALUES ('{cardNumber}', '{DateTime.UtcNow:yyyy-MM-dd hh:mm:ss.fffffff}');");
            try
            {
                using (SqlCommand command = new SqlCommand(sb.ToString(), Connection))
                {
                    Debug.WriteLine("CODE: Executing command");
                    Connection.Open();
                    command.ExecuteNonQuery();
                }

                using (SqlCommand command = new SqlCommand($"SELECT first_name, surname FROM student WHERE student_id = '{cardNumber}';", Connection))
                {
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        returnString[0] = string.Format($"{dataReader.GetString(0)} {dataReader.GetString(1)}");
                        returnString[1] = string.Format($"Swipe Success");
                    }
                }
            }
            catch (SqlException exception)
            {
                
                switch (exception.Number)
                {
                    case 547:
                        returnString[0] =  String.Format("Card Read Error");
                        returnString[1] =  String.Format("Error");
                        break;
                    case 8152:
                        returnString[0] =  String.Format("Card Read Error");
                        returnString[1] =  String.Format("Error");
                        break;
                    default:
                        throw;
                }
            }

            Connection.Close();
            return returnString;
        }

    }
}
