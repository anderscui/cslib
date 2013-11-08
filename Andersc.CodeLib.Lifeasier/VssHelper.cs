using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SourceSafeTypeLib;

using Andersc.CodeLib.Common;

namespace Andersc.CodeLib.Lifeasier
{
    public partial class VssHelper : Form
    {
        private const int ITEM_TYPE_FOLDER = 0;
        private const int ITEM_TYPE_FILE = 1;
        private const int CHECKOUT_TYPE_BY_OTHER = 1;
        private const int CHECKOUT_TYPE_BY_CURRENT = 2;
        private const string MESSAGE_ERROR_OCCURED = "Error occured: ";


        private readonly string userName = WinConfigManager.GetAppSetting("userName");
        private readonly string password = WinConfigManager.GetAppSetting("pwd");
        private readonly string serverUrl = WinConfigManager.GetAppSetting("serverUrl");
        private readonly string rootSourceFolder = WinConfigManager.GetAppSetting("rootSourceFolder");

        Dictionary<string, VSSItem> filesCheckedOutByCurrentUser = new Dictionary<string, VSSItem>();
        Dictionary<string, VSSItem> filesCheckedOutByOthers = new Dictionary<string, VSSItem>();

        public VssHelper()
        {
            InitializeComponent();
        }

        private void VssHelper_Load(object sender, EventArgs e)
        {
            lblUserName.Text = userName;
            lblServerPath.Text = serverUrl;
            txtSourcePath.Text = rootSourceFolder;
        }

        private void btnCheckinSelected_Click(object sender, EventArgs e)
        {
            foreach (string itemSpec in ltbFilesCheckedOutItems.SelectedItems)
            {
                if (filesCheckedOutByCurrentUser.ContainsKey(itemSpec))
                {
                    VSSItem item = filesCheckedOutByCurrentUser[itemSpec];
                    item.Checkin(string.Empty, item.LocalSpec, 0);
                }
            }
        }

        private void btnCheckinAll_Click(object sender, EventArgs e)
        {
            foreach (string itemSpec in ltbFilesCheckedOutItems.Items)
            {
                if (filesCheckedOutByCurrentUser.ContainsKey(itemSpec))
                {
                    VSSItem item = filesCheckedOutByCurrentUser[itemSpec];
                    item.Checkin(string.Empty, item.LocalSpec, 0);
                }
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            VSSDatabase vssDB = new VSSDatabase();

            try
            {
                vssDB.Open(serverUrl, userName, password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MESSAGE_ERROR_OCCURED + ex.Message);
                return;
            }

            if (vssDB.IsNull())
            {
                MessageBox.Show("Unable to open VSSDB: " + serverUrl);
                return;
            }

            VSSItem rootSourceFolderItem = null;
            try
            {
                rootSourceFolderItem = vssDB.get_VSSItem(rootSourceFolder, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MESSAGE_ERROR_OCCURED + ex.Message);
                return;
            }

            if (rootSourceFolderItem.IsNull())
            {
                MessageBox.Show(string.Format("The VSS Source Folder doesnt Exists: {0}", rootSourceFolder));
                return;
            }

            filesCheckedOutByCurrentUser.Clear();
            filesCheckedOutByOthers.Clear();
            FindCheckedOutFiles(rootSourceFolderItem);
            BindFilesCheckedOutByCurrentUser();
        }

        void FindCheckedOutFiles(VSSItem vssFolder)
        {
            foreach (VSSItem item in vssFolder.get_Items(false))
            {
                if (IsFileItem(item))
                {
                    if (IsCheckedOutByCurrentUser(item))
                    {
                        filesCheckedOutByCurrentUser.Add(item.Spec, item);
                    }
                    else if (IsCheckedOutByOthers(item))
                    {
                        filesCheckedOutByOthers.Add(item.Spec, item);
                    }
                }
                else if (IsFolderItem(item))
                {
                    FindCheckedOutFiles(item);
                }
            }
        }

        private bool IsFolderItem(VSSItem item)
        {
            return (item.Type == ITEM_TYPE_FOLDER);
        }

        private bool IsFileItem(VSSItem item)
        {
            return (item.Type == ITEM_TYPE_FILE);
        }

        private bool IsCheckedOut(VSSItem item)
        {
            return IsCheckedOutByCurrentUser(item) || IsCheckedOutByOthers(item);
        }

        private bool IsCheckedOutByCurrentUser(VSSItem item)
        {
            return (item.IsCheckedOut == CHECKOUT_TYPE_BY_CURRENT);
        }

        private bool IsCheckedOutByOthers(VSSItem item)
        {
            return (item.IsCheckedOut == CHECKOUT_TYPE_BY_OTHER);
        }

        private void BindFilesCheckedOutByCurrentUser()
        {
            ltbFilesCheckedOutItems.Items.Clear();

            if (filesCheckedOutByCurrentUser.Count > 0)
            {
                ltbFilesCheckedOutItems.Items.Add("-----------Files checkedout by current user-----------");
                filesCheckedOutByCurrentUser.Values
                    .ToList()
                    .ForEach(item => ltbFilesCheckedOutItems.Items.Add(item.Spec));

                ltbFilesCheckedOutItems.Items.Add("");
            }

            if ((filesCheckedOutByOthers.Count > 0)
                && chbIncludingItemsCheckedOutByOthers.Checked)
            {
                ltbFilesCheckedOutItems.Items.Add("-----------Files checkedout by others-----------");
                filesCheckedOutByOthers.Values
                    .ToList()
                    .ForEach(item => ltbFilesCheckedOutItems.Items.Add(item.Spec));
            }
        }

        private void btnCopyPaths_Click(object sender, EventArgs e)
        {
            StringBuilder paths = new StringBuilder();
            ltbFilesCheckedOutItems.SelectedItems.Cast<string>()
                .ToList()
                .FindAll(s => s.StartsWith("$"))
                .ForEach(s => paths.AppendLine(s));

            Clipboard.SetText(paths.ToString());
        }
    }
}
