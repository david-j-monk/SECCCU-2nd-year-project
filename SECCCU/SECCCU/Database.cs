using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace SECCCU
{
    class Database
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

            sb.Append("DROP TABLE STAFF;");
            sb.Append("DROP TABLE STUDENT;");
            sb.Append("DROP TABLE LECTURE;");
            sb.Append("DROP TABLE PROGRAMME;");
            sb.Append("CREATE TABLE Programme(");
            sb.Append("ProgrammeID		CHAR(9) 	NOT NULL,");
            sb.Append("ProgrammeName	VARCHAR(40)	NOT NULL,");
            sb.Append("	PRIMARY KEY (programmeID)");
            sb.Append(")");
            sb.Append("CREATE TABLE Staff(");
            sb.Append("StaffID 	    CHAR(11)		NOT NULL,");
            sb.Append("Surname 		VARCHAR (40)	NOT NULL,");
            sb.Append("FirstName	VARCHAR (40)	NOT NULL,");
            sb.Append("ProgrammeID  CHAR(9),");
            sb.Append("	FOREIGN KEY (programmeID) REFERENCES Programme(ProgrammeID)");
            sb.Append(")");
            sb.Append("CREATE TABLE Lecture(");
            sb.Append("LectureID 	CHAR(12)	    NOT NULL,");
            sb.Append("LectureName	CHAR(40),");
            sb.Append("LectureStart	TIME		    NOT NULL,");
            sb.Append("LectureEnd	TIME		    NOT NULL,");
            sb.Append("Location	    CHAR(5)		    NOT NULL,");
            sb.Append("ProgrammeID	CHAR(9)		    NOT NULL,");
            sb.Append("	FOREIGN KEY (programmeID)   REFERENCES Programme(ProgrammeID)");
            sb.Append(")");
            sb.Append("CREATE TABLE Student(");
            sb.Append("StudentID	CHAR(11)	    NOT NULL,");
            sb.Append("Surname	    VARCHAR(40)	    NOT NULL,");
            sb.Append("FirstName    VARCHAR(40)	    NOT NULL,");
            sb.Append("Dob			DATE,");
            sb.Append("Email		VARCHAR(50),");
            sb.Append("PhoneNumber	CHAR(11),");
            sb.Append("ProgrammeID	CHAR(9)		    NOT NULL,");
            sb.Append("	FOREIGN KEY (programmeID) REFERENCES Programme(ProgrammeID)");
            sb.Append(")");

            string[][] rows = File.ReadAllLines("programmes.csv").Select(l => l.Split(',').ToArray()).ToArray();
            for (int i = 0; i < rows.GetLength(0); i++)
            {
                sb.Append("INSERT INTO Programme (ProgrammeID, ProgrammeName)");
                sb.Append($"VALUES ('{rows[i][0]}', '{rows[i][1]}');");
            }

            rows = File.ReadAllLines("staff.csv").Select(l => l.Split(',').ToArray()).ToArray();
            for (int i = 0; i < rows.GetLength(0); i++)
            {
                sb.Append("INSERT INTO Staff (StaffID, Surname, FirstName, ProgrammeID)");
                sb.Append($"VALUES ('{rows[i][0]}', '{rows[i][1]}', '{rows[i][2]}', '{rows[i][3]}');");
            }
            
            rows = File.ReadAllLines("lectures.csv").Select(l => l.Split(',').ToArray()).ToArray();
            for (int i = 0; i < rows.GetLength(0); i++)
            {
                sb.Append("INSERT INTO Lecture (LectureID, LectureName, LectureStart, LectureEnd, Location, ProgrammeID)");
                sb.Append($"VALUES ('{rows[i][0]}', '{rows[i][1]}', '{rows[i][2]}', '{rows[i][3]}', '{rows[i][4]}', '{rows[i][5]}');");
            }

            rows = File.ReadAllLines("students.csv").Select(l => l.Split(',').ToArray()).ToArray();
            for (int i = 0; i < rows.GetLength(0); i++)
            {
                sb.Append("INSERT INTO Student (StudentID, Surname, FirstName, Dob, Email, PhoneNumber, ProgrammeID)");
                sb.Append($"VALUES ('{rows[i][0]}', '{rows[i][1]}', '{rows[i][2]}', '{rows[i][3]}', '{rows[i][4]}', '{rows[i][5]}', '{rows[i][6]}');");
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
    }
}
