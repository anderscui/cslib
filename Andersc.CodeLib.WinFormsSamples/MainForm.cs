using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.Win32;

using Andersc.CodeLib.Common;
using Andersc.CodeLib.WinFormsSamples.CustomControlsForms;
using Andersc.CodeLib.WinFormsSamples.GraphicSample;
using Andersc.CodeLib.WinFormsSamples.Multithread;
using Andersc.CodeLib.WinFormsSamples.StandardControls;
using Andersc.CodeLib.WinFormsSamples.Misc;
using Andersc.CodeLib.WinFormsSamples.Components;

namespace Andersc.CodeLib.WinFormsSamples
{
    public partial class MainForm : Form
    {
        string subKey = @"Software\Andersc\CodeLib.WinSamples";

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormWindowState currentState = this.WindowState;
            this.WindowState = FormWindowState.Normal;

            RegistryKey key = Registry.CurrentUser;
            RegistryHelper.WriteValue(key, subKey, "MainForm.Location", Location.X.ToString() + "," + Location.Y.ToString());
            RegistryHelper.WriteValue(key, subKey, "MainForm.ClientSize", ClientSize.Width.ToString() + "," + ClientSize.Height.ToString());
            RegistryHelper.WriteValue(key, subKey, "MainForm.WindowState", currentState.ToString());
            RegistryHelper.WriteValue(key, subKey, "MainForm.CloseReason", e.CloseReason.ToString());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser;
            string lastLocation = RegistryHelper.ReadValue(subKey, "MainForm.Location");
            if (lastLocation.IsNotNull())
            {
                string[] locParts = lastLocation.Split(',');
                this.Location = new Point(int.Parse(locParts[0]), int.Parse(locParts[1]));
            }

            string lastClientSize = RegistryHelper.ReadValue(subKey, "MainForm.ClientSize");
            if (lastClientSize.IsNotNull())
            {
                string[] csParts = lastClientSize.Split(',');
                this.ClientSize = new Size(int.Parse(csParts[0]), int.Parse(csParts[1]));
            }

            string lastState = RegistryHelper.ReadValue(subKey, "MainForm.WindowState");
            if (lastState.IsNotNull())
            {
                this.WindowState = (FormWindowState)(Enum.Parse(typeof(FormWindowState), lastState));
            }
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void mnuHelpAbout_Click(object sender, EventArgs e)
        {
            // TODO: Use a new form to display about info.
            MessageBox.Show("WinFormsSamples Application");
        }

        private void mnuGraphicsBasic_Click(object sender, EventArgs e)
        {
            BasicSample bs = new BasicSample();
            bs.ShowDialog(this);
        }

        private void mnuMultithreadSync_Click(object sender, EventArgs e)
        {
            SyncOperForm sof = new SyncOperForm();
            sof.ShowDialog(this);
        }

        private void mnuMultithreadAsync_Click(object sender, EventArgs e)
        {
            AsyncOperForm aof = new AsyncOperForm();
            aof.ShowDialog(this);
        }

        private void mnuMultithreadSimpleAsync_Click(object sender, EventArgs e)
        {
            SimpleAsyncOperForm saof = new SimpleAsyncOperForm();
            saof.ShowDialog(this);
        }

        private void mnuMultithreadCancelAsync_Click(object sender, EventArgs e)
        {
            CancelAsyncOperForm caof = new CancelAsyncOperForm();
            caof.ShowDialog(this);
        }

        private void mnuCustomCtrlEllipseLabel_Click(object sender, EventArgs e)
        {
            EllipseLabelForm elf = new EllipseLabelForm();
            elf.ShowDialog(this);
        }

        private void mnuStdCtrlOwnerDraw_Click(object sender, EventArgs e)
        {
            OwnerDrawCtrlForm odcf = new OwnerDrawCtrlForm();
            odcf.ShowDialog(this);
        }

        private void mnuCustomCtrlFtb_Click(object sender, EventArgs e)
        {
            FileTextBoxForm ftbf = new FileTextBoxForm();
            ftbf.ShowDialog(this);
        }

        private void mnuStdCtrlDragDrop_Click(object sender, EventArgs e)
        {
            DragDropForm ddf = new DragDropForm();
            ddf.ShowDialog(this);
        }

        private void mnuMiscManifestRes_Click(object sender, EventArgs e)
        {
            //Assembly currentAsm = this.GetType().Assembly;
            //string resourcePath = "ManifestRes.Image.fans-of-inter.jpg";

            //Stream img = currentAsm.GetManifestResourceStream(this.GetType(), resourcePath);
        }

        private void mnuMiscTypedRes_Click(object sender, EventArgs e)
        {
            // Use file system.
            //ResXResourceReader rr = new ResXResourceReader(Path.Combine(Application.StartupPath, @"..\..\TypedRes\Common.Resx"));
            //foreach (DictionaryEntry entry in rr)
            //{
            //    if (entry.Key.ToString() == "Author")
            //    {
            //        MessageBox.Show(entry.Value.ToString());
            //    }
            //}

            // Use manifest resource(Note that the file extension is changed to resources from resx)
            //Stream resStream = Assembly.GetEntryAssembly().GetManifestResourceStream(this.GetType(), "TypedRes.Common.resources");
            //ResourceReader rr = new ResourceReader(resStream);

            //foreach (DictionaryEntry entry in rr)
            //{
            //    if (entry.Key.ToString() == "Author")
            //    {
            //        MessageBox.Show(entry.Value.ToString());
            //    }
            //}

            //if (resStream.IsNotNull())
            //{
            //    resStream.Dispose();
            //}

            // Use ResourceManager class.
            ResourceManager rm = new ResourceManager("CodeLib.WinFormsSamples.TypedRes.Common", Assembly.GetEntryAssembly());
            string author = rm.GetString("Author");
            MessageBox.Show(author);

            // Use helper class built by VS IDE.
            //string author = TypedRes.Common.Author;
            //MessageBox.Show(author);
        }

        private void mnuMiscAllCultures_Click(object sender, EventArgs e)
        {
            AllCultures ac = new AllCultures();
            ac.ShowDialog(this);
        }

        private void mnuMiscInputLanguages_Click(object sender, EventArgs e)
        {
            foreach (InputLanguage lan in InputLanguage.InstalledInputLanguages)
            {
                Mbox.ShowInfo(lan.LayoutName);
            }
        }

        private void mnuMiscThrowExp_Click(object sender, EventArgs e)
        {
            throw new InvalidOperationException();
        }

        private void mnuMiscCustomConfigSection_Click(object sender, EventArgs e)
        {
            VssInfoSection section = ConfigurationManager.GetSection("VssInfoSection") as VssInfoSection;
            Mbox.ShowInfo(section.DbPath);
        }
    }
}
