using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Andersc.CodeLib.Lifeasier.Tools
{
    public partial class Pop3Mail : Form
    {
        // TODO: Move to base form class.
        protected bool Cancellable { get; set; }

        // TODO: Find better pattern;
        public TcpClient Server;
        public NetworkStream NetStrm;
        public StreamReader RdStrm;

        public string Data;
        public byte[] szData;

        private readonly string CRLF = "\r\n";

        public Pop3Mail()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;
            this.Cancellable = true;
            this.KeyDown += new KeyEventHandler(Pop3Mail_KeyDown);

            txtPop3Server.Text = "pop3.163.com";
            txtUser.Text = "flareboy@163.com";
            txtPassword.Text = "flare";
        }

        void Pop3Mail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private string MailServer
        {
            get { return txtPop3Server.Text.Trim(); }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            //string error = string.Empty;
            //MessageBox.Show(ValidateEmailAccount(MailServer, 110, "flareboy@163.com", "flare20!", out error).ToString());

            Cursor orin = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            Server = new TcpClient(MailServer, 110);
            lstStatus.Items.Clear();

            try
            {
                NetStrm = Server.GetStream();
                RdStrm = new StreamReader(Server.GetStream());
                lstStatus.Items.Add(RdStrm.ReadLine());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Cursor.Current = orin;
        }

        static bool ValidateEmailAccount(string server, int port, string userName, string password, out string ErrorMessage)
        {
            ErrorMessage = "";

            //create a tcp connection 
            TcpClient _server = new TcpClient(server, port);

            //prepare  
            NetworkStream netStream = _server.GetStream();
            StreamReader reader = new StreamReader(_server.GetStream());

            if (!reader.ReadLine().Contains("+OK"))
            {
                //失败 
                ErrorMessage = "server链接失败";
                return false;
            }

            string data;
            byte[] charData;
            string CRLF = "\r\n";

            //login 
            data = "USER " + userName + CRLF;
            charData = System.Text.Encoding.ASCII.GetBytes(data);
            netStream.Write(charData, 0, charData.Length);
            if (!reader.ReadLine().Contains("+OK"))
            {
                //账户错误 
                ErrorMessage = "账户错误";
                return false;
            }
            data = "PASS " + password + CRLF;
            charData = System.Text.Encoding.ASCII.GetBytes(data);
            netStream.Write(charData, 0, charData.Length);
            if (!reader.ReadLine().Contains("+OK"))
            {
                //密码错误 
                ErrorMessage = "密码错误";
                return false;
            }
            return true;
        } 
    }
}
