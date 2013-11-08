using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

using Andersc.CodeLib.Lifeasier.Components;

namespace Andersc.CodeLib.Lifeasier.Dev
{
    public partial class TestDatabaseConnectivity : DialogForm
    {
        public TestDatabaseConnectivity()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string connString = txtConnString.Text;
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                conn.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Not Connective: " + Environment.NewLine
                    + ex.Message);
                return;
            }

            if (conn.State == ConnectionState.Open)
            {
                MessageBox.Show("OK");
            }
            else
            {
                MessageBox.Show(conn.State.ToString());
            }
        }
    }
}
