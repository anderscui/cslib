using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Andersc.CodeLib.Common.Data;

namespace Andersc.CodeLib.GenGen
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //var connStr = "Server=CNS-E1DW;Database=OdinPlus;UID=ram;PWD=ram1818;";
            var connStr = "Server=10.17.4.64;Database=OrpheusDb;UID=sa;PWD=P@ssw0rd1;";
            var conn = new SqlConnection(connStr);
            conn.Open();
            try
            {
                var output = new StringBuilder();
                var restrictions = new string[4];
                restrictions[2] = "Country";
                var dbSchema = conn.GetSchema("Tables", restrictions);
                foreach (DataRow row in dbSchema.Rows)
                {
                    foreach (DataColumn col in dbSchema.Columns)
                    {
                        output.AppendFormat("{0}: {1}", col.ColumnName, row[col]);
                    }
                    output.AppendLine();
                }
                txtOutput.Text = output.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
