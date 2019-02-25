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
    }
}