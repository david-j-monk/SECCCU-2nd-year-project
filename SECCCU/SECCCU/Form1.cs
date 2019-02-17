using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SECCCU
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            try 
            { 
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "secccuserver.database.windows.net"; 
                builder.UserID = "secccuadmin";            
                builder.Password = "SeccuPass1337$!";     
                builder.InitialCatalog = "secccusql";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();
                    DatabaseCreation DBC = new DatabaseCreation(connection);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("DROP TABLE Persons;");
                                        
                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        //using (SqlDataReader reader = command.ExecuteReader())
                        //{
                        //    while (reader.Read())
                        //    {
                        //        Console.WriteLine("{0}", reader.GetString(0));
                        //    }
                        //}
                    }
                    Debug.WriteLineIf(DBC.DropAndCreateTables(), "DROP AND CREATE SUCCESS");
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine("ERROR: " + e.ToString());
            }
            Console.ReadLine();
        }
    }
}
