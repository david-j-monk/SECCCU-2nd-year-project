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
            Database database = new Database();
            if (database.CreateConnection())
            {
                Debug.WriteLineIf(database.InitializeDatabase(), "Database Initialized");
            }



            using (SqlCommand command = new SqlCommand("SELECT * FROM student;", database.Connection))
            {
                string stem = command.CommandText;
                SqlDataReader reader = command.ExecuteReader();
                reader.ToString();
                while (reader.Read())
                {

                    Console.WriteLine(reader.GetValue(0).ToString());
                    Console.WriteLine(reader.GetValue(1).ToString());
                    Console.WriteLine(reader.GetValue(2).ToString());
                    Console.WriteLine(reader.GetValue(3).ToString());
                    Console.WriteLine(reader.GetValue(4).ToString());
                    Console.WriteLine(reader.GetValue(5).ToString());
                    Console.WriteLine(reader.GetValue(6).ToString());
                }

            }
        }
    }
}
