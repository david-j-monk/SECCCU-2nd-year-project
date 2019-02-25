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

            Console.WriteLine("\nQuery data example:");
            Console.WriteLine("=========================================\n");

            Debug.WriteLine("CODE: Dropping and creating database tables.");

            StringBuilder sb = new StringBuilder();

            Debug.WriteLine("CODE: Creating query");
            sb.Append("DROP TABLE log;");

            //sb.Append("DROP TABLE STAFF;");
            sb.Append("DROP TABLE student;");
            //sb.Append("DROP TABLE LECTURE;");
            //sb.Append("DROP TABLE PROGRAMME;");


            //sb.Append("CREATE TABLE programme(");
            //sb.Append("programme_id		CHAR(9) 	    NOT NULL,");
            //sb.Append("programme_name	VARCHAR(40)	    NOT NULL,");
            //sb.Append("PRIMARY KEY (programme_id)");
            //sb.Append(")");

            //sb.Append("CREATE TABLE staff(");
            //sb.Append("staff_id 	    CHAR(11)		NOT NULL,");
            //sb.Append("surname 		    VARCHAR (40)	NOT NULL,");
            //sb.Append("first_name	    VARCHAR (40)	NOT NULL,");
            //sb.Append("programme_id     CHAR(9),                 ");
            //sb.Append("FOREIGN KEY (programme_id) REFERENCES programme(programme_id)");
            //sb.Append(")");

            //sb.Append("CREATE TABLE lecture(");
            //sb.Append("lecture_id 	    CHAR(12)	    NOT NULL,");
            //sb.Append("lecture_name	    CHAR(40)                ,");
            //sb.Append("lecture_start    TIME		    NOT NULL,");
            //sb.Append("lecture_end	    TIME		    NOT NULL,");
            //sb.Append("location	        CHAR(5)		    NOT NULL,");
            //sb.Append("programme_id	    CHAR(9)		    NOT NULL,");
            //sb.Append("	FOREIGN KEY (programme_id)      REFERENCES programme(programme_id)");
            //sb.Append(")");

            sb.Append("CREATE TABLE student(");
            sb.Append("row_id	        INT	            NOT NULL        IDENTITY(1,1),");
            sb.Append("student_id	    VARCHAR(40)     PRIMARY KEY,");
            sb.Append("surname	        VARCHAR(40)	    NOT NULL,");
            sb.Append("first_name       VARCHAR(40)	    NOT NULL,");

            //sb.Append("dob			    DATE,                    ");
            //sb.Append("email		    VARCHAR(50)             ,");
            //sb.Append("phone_number	    CHAR(11)                ,");
            //sb.Append("programme_id	    CHAR(9)		    NOT NULL,");
            sb.Append("CONSTRAINT       CK_first_name_Length            CHECK (LEN(first_name) >= 3),");
            sb.Append("CONSTRAINT       CK_surname_Length               CHECK (LEN(surname) >= 3)");
            //sb.Append("	FOREIGN KEY (programme_id) REFERENCES programme(programme_id)");

            sb.Append(");");


            sb.Append("CREATE TABLE log(");
            sb.Append("log_id           INT	            PRIMARY KEY     IDENTITY(1,1),");
            sb.Append("student_id       VARCHAR(40)     ,");
            sb.Append("scan_time	    DATETIME2		NOT NULL,");
            sb.Append("CONSTRAINT       fk_student_id   FOREIGN KEY (student_id) REFERENCES student(student_id),");

            sb.Append(");");

            //string[][] rows = File.ReadAllLines("csvFiles\\programmes.csv").Select(l => l.Split(',').ToArray()).ToArray();

            //for (int i = 0; i < rows.GetLength(0); i++)
            //{
            //    sb.Append("INSERT INTO programme (programme_id, programme_name)");
            //    sb.Append($"VALUES ('{rows[i][0]}', '{rows[i][1]}');");
            //}

            //rows = File.ReadAllLines("csvFiles\\staff.csv").Select(l => l.Split(',').ToArray()).ToArray();
            //for (int i = 0; i < rows.GetLength(0); i++)
            //{
            //    sb.Append("INSERT INTO staff (staff_id, surname, first_name, programme_id)");
            //    sb.Append($"VALUES ('{rows[i][0]}', '{rows[i][1]}', '{rows[i][2]}', '{rows[i][3]}');");
            //}

            //rows = File.ReadAllLines("csvFiles\\lectures.csv").Select(l => l.Split(',').ToArray()).ToArray();
            //for (int i = 0; i < rows.GetLength(0); i++)
            //{
            //    sb.Append("INSERT INTO lecture (lecture_id, lecture_name, lecture_start, lecture_end, location, programme_id)");
            //    sb.Append($"VALUES ('{rows[i][0]}', '{rows[i][1]}', '{rows[i][2]}', '{rows[i][3]}', '{rows[i][4]}', '{rows[i][5]}');");
            //}

            //rows = File.ReadAllLines("csvFiles\\students.csv").Select(l => l.Split(',').ToArray()).ToArray();
            //for (int i = 0; i < rows.GetLength(0); i++)
            //{
            //    sb.Append("INSERT INTO student (student_id, surname, first_name, dob, email, phone_number, programme_id)");
            //    sb.Append($"VALUES ('{rows[i][0]}', '{rows[i][1]}', '{rows[i][2]}', '{rows[i][3]}', '{rows[i][4]}', '{rows[i][5]}', '{rows[i][6]}');");
            //}
            string[][] rows = File.ReadAllLines("csvFiles\\students.csv").Select(l => l.Split(',').ToArray()).ToArray();
            for (int i = 0; i < rows.GetLength(0); i++)
            {
                sb.Append("INSERT INTO student (surname, first_name, student_id)");
                sb.Append($"VALUES ('{rows[i][0]}', '{rows[i][1]}','{rows[i][2]}');");
            }


            string query = sb.ToString();
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
