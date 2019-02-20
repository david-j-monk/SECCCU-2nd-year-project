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
        MonitorSystem monitorSystem = new MonitorSystem();

        public Form1()
        {
            InitializeComponent();






            //using (SqlCommand command = new SqlCommand("SELECT * FROM student;", monitorSystem.Database.Connection))
            //{
            //    monitorSystem.Database.Connection.Open();
            //    SqlDataReader reader = command.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        //Console.WriteLine(reader.GetValue(0).ToString());
            //    }
            //    monitorSystem.Database.Connection.Close();

            //}
        }


        private void uiScanCardButton_click(object sender, EventArgs e)
        {
            uiScanCardButton.Enabled = false;
            Reader reader = new Reader();
            string cardNumber = reader.ScanCard();
            uiCardNumberLabel.Text = cardNumber;

            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO log (student_id, scan_time)");
            sb.Append($"VALUES ('{cardNumber}', '{DateTime.UtcNow:yyyy-MM-dd hh:mm:ss.fffffff}');");
            try
            {
                using (SqlCommand command = new SqlCommand(sb.ToString(), monitorSystem.Database.Connection))
                {

                    Debug.WriteLine("CODE: Executing command");
                    monitorSystem.Database.Connection.Open();
                    command.ExecuteNonQuery();

                }

                using (SqlCommand command = new SqlCommand($"SELECT first_name, surname FROM student WHERE student_id = '{cardNumber}';", monitorSystem.Database.Connection))
                {
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        MessageBox.Show($"{dataReader.GetString(0)} {dataReader.GetString(1)}", "Swipe Success");
                        //Console.WriteLine(reader.GetValue(0).ToString());
                    }
                }
            }
            catch (SqlException exception)
            {
                switch (exception.Message)
                {
                    case "The INSERT statement conflicted with the FOREIGN KEY constraint \"fk_student_id\". The conflict occurred in database \"secccusql\", table \"dbo.student\", column 'student_id'.\r\nThe statement has been terminated.":
                        MessageBox.Show("Card Read Error", "Error");
                        break;
                    default:
                        throw;

                }
            }

            monitorSystem.Database.Connection.Close();
            uiScanCardButton.Enabled = true;

        }
    }
}