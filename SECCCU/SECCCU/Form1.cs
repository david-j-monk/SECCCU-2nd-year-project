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
            foreach (string programmeTitle in monitorSystem.Database.GetProgrammeTitles())
            {
                comboBox1.Items.Add(programmeTitle);
            }
        }


        private void uiScanCardButton_click(object sender, EventArgs e)
        {
            uiScanCardButton.Enabled = false;
            Reader reader = new Reader();
            string cardNumber = reader.ScanCard();
            uiCardNumberLabel.Text = cardNumber;

            string[] infoSentToDevice = monitorSystem.Database.LogCardSwipe(cardNumber);
            MessageBox.Show(infoSentToDevice[0], infoSentToDevice[1]);
            uiScanCardButton.Enabled = true;
        }

        private void uiCheckLoginStatusButton_Click(object sender, EventArgs e)
        {
            string userID = uiCheckLoginTextBox.Text;
            string[] responseFromDatabase = monitorSystem.Database.DidUserSwipeInCurrentLecture(userID);
            if (responseFromDatabase[2] == null)
            {
                MessageBox.Show(
                    $"{responseFromDatabase[0]} {responseFromDatabase[1]} is not marked as marked as attended");
            }
            else if (responseFromDatabase[0] == null)
            {
                MessageBox.Show(
                    $"{uiCheckLoginTextBox.Text} is not a valid student id", "Error");
            }
            else
            {
                MessageBox.Show($"{responseFromDatabase[0]} {responseFromDatabase[1]} is marked as attended in {responseFromDatabase[2]}", "Log In Status");

            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            foreach (string singleLog in monitorSystem.Database.GetProgrammeReport(comboBox1.Text))
            {
                listBox1.Items.Add(singleLog);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}