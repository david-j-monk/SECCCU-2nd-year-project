using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SECCCU
{
    class Database
    {

        private SqlConnection Connection;
        
        public bool CreateConnection()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "secccuserver.database.windows.net";
                builder.UserID = "secccuadmin";
                builder.Password = "SeccuPass1337$!";
                builder.InitialCatalog = "secccusql";
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
            sb.Append("StaffID 	CHAR(11)		NOT NULL,");
            sb.Append("Name 		VARCHAR (40)	NOT NULL,");
            sb.Append("ProgrammeID CHAR(9),");
            sb.Append("	FOREIGN KEY (programmeID) REFERENCES Programme(ProgrammeID)");
            sb.Append(")");
            sb.Append("CREATE TABLE Lecture(");
            sb.Append("LectureID 	CHAR(12)	NOT NULL,");
            sb.Append("LectureName	CHAR(40),");
            sb.Append("LectureTime	DATE		NOT NULL,");
            sb.Append("Location	CHAR(5)		NOT NULL,");
            sb.Append("ProgrammeID	CHAR(9)		NOT NULL,");
            sb.Append("	FOREIGN KEY (programmeID) REFERENCES Programme(ProgrammeID)");
            sb.Append(")");
            sb.Append("CREATE TABLE Student(");
            sb.Append("StudentID	CHAR(11)	NOT NULL,");
            sb.Append("Name		VARCHAR(40)	NOT NULL,");
            sb.Append("Dob			DATE,");
            sb.Append("Email		VARCHAR(50),");
            sb.Append("PhoneNumber	CHAR(11),");
            sb.Append("ProgrammeID	CHAR(9)		NOT NULL,");
            sb.Append("	FOREIGN KEY (programmeID) REFERENCES Programme(ProgrammeID)");
            sb.Append(")");

            string Query = sb.ToString();
            int rowsAffected;
            try
            {
                using (SqlCommand command = new SqlCommand(Query, Connection))
                {
                    Debug.WriteLine("CODE: Executing command");
                    Connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("ERROR: " + e);
                return false;
            }
            Debug.WriteLine("CODE: Command executed succesfully \n \t" + rowsAffected + " Rows Affected!");
            return true;

        }
    }
}
