using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Andersc.CodeLib.WinFormsSamples.Components;

namespace Andersc.CodeLib.WinFormsSamples.StandardControls
{
    public partial class DragDropForm : Form
    {
        public DragDropForm()
        {
            InitializeComponent();
        }

        private void DragDropForm_Load(object sender, EventArgs e)
        {
            ltbLeft.Items.AddRange(new string[] { "L1", "L2", "L3" });
            ltbRight.Items.AddRange(new string[] { "R1", "R2", "R3" });
        }

        private void ltbLeft_MouseDown(object sender, MouseEventArgs e)
        {
            // Start a drag-n-drop operation.
            if (ltbLeft.SelectedIndex >= 0)
            {
                DoDragDrop(ltbLeft.SelectedItem, DragDropEffects.Copy);
            }
        }

        private void ltbRight_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(string)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                MessageBox.Show("You should give a string object, OK?");
            }
        }

        private void ltbRight_DragDrop(object sender, DragEventArgs e)
        {
            string droppedItem = (e.Data.GetData(typeof(string)) as string);
            if (!ltbRight.Items.Contains(droppedItem))
            {
                ltbRight.Items.Add(droppedItem);
                ltbLeft.Items.Remove(droppedItem);
            }
        }

        private void btnDrag_MouseDown(object sender, MouseEventArgs e)
        {
            DragDropEffects effect = DoDragDrop(btnDrag.Text, 
                DragDropEffects.Copy | DragDropEffects.Move);

            if (effect == DragDropEffects.Move)
            {
                btnDrag.Text = string.Empty;
            }
        }

        private void txtDrop_DragEnter(object sender, DragEventArgs e)
        {
            SetDropEffects(e);
        }

        private void txtDrop_DragOver(object sender, DragEventArgs e)
        {
            SetDropEffects(e);
        }

        private void SetDropEffects(DragEventArgs e)
        {
            KeyState ks = (KeyState)e.KeyState;

            if (e.Data.GetDataPresent(typeof(string)))
            {
                if ((ks & KeyState.CtrlKey) == KeyState.CtrlKey)
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.Move;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void txtDrop_DragDrop(object sender, DragEventArgs e)
        {
            string droppedItem = (e.Data.GetData(typeof(string)) as string);
            txtDrop.Text = droppedItem;
        }
    }
}
