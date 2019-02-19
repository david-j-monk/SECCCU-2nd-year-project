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
            MonitorSystem monitorSystem = new MonitorSystem();





            using (SqlCommand command = new SqlCommand("SELECT * FROM student;", monitorSystem.Database.Connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                reader.ToString();
                while (reader.Read())
                {
                    //Console.WriteLine(reader.GetValue(0).ToString());
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           Reader reader = new Reader();
           uiCardNumberLabel.Text = reader.ScanCard();
        }
    }
}
