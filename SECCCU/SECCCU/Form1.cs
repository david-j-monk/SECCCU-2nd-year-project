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
            Debug.WriteLineIf(database.CreateConnection(), "Connection Successful");
            Debug.WriteLineIf(database.InitializeDatabase(), "DROP AND CREATE SUCCESS");

        }
    }
}
