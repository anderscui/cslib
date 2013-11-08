using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Andersc.CodeLib.WinFormsSamples.StandardControls
{
    public partial class OwnerDrawCtrlForm : Form
    {
        public OwnerDrawCtrlForm()
        {
            InitializeComponent();

            InitControls();
        }

        private void InitControls()
        {
            //ltbOwnerDraw.DrawMode = DrawMode.OwnerDrawFixed;
            ltbOwnerDraw.DrawMode = DrawMode.OwnerDrawVariable;
            ltbOwnerDraw.DrawItem += new DrawItemEventHandler(ltbOwnerDraw_DrawItem);
            ltbOwnerDraw.MeasureItem += new MeasureItemEventHandler(ltbOwnerDraw_MeasureItem);

            BindListBox();
        }

        private void BindListBox()
        {
            string[] items = { "Anders", "Bill", "Carl", "Dull" };
            ltbOwnerDraw.Items.AddRange(items);
        }

        private void ltbOwnerDraw_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            // Get the default color
            Font drawFont = e.Font;
            Color foreColor = e.ForeColor;
            bool useOurFont = false;

            // Draw italics if selected
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                useOurFont = true;
                drawFont = new Font(drawFont, FontStyle.Italic | FontStyle.Bold);
                foreColor = Color.Blue;
            }

            // Draw the item.
            e.Graphics.DrawString(ltbOwnerDraw.Items[e.Index].ToString(),
                drawFont, new SolidBrush(foreColor), e.Bounds);

            if (useOurFont)
            {
                drawFont.Dispose();
            }

            e.DrawFocusRectangle();
        }

        private void ltbOwnerDraw_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index % 2 == 0)
            {
                e.ItemHeight *= 2;
            }
        }
    }
}
