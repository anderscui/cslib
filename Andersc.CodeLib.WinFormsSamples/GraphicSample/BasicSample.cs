using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Andersc.CodeLib.WinFormsSamples.GraphicSample
{
    public partial class BasicSample : DialogForm
    {
        private bool _drawEllipse = false;

        public BasicSample()
        {
            InitializeComponent();
        }

        //private void btnDrawEllipse_Click(object sender, EventArgs e)
        //{
        //    drawEllipse = !drawEllipse;

        //    using (Graphics g = this.CreateGraphics())
        //    {
        //        if (drawEllipse)
        //        {
        //            g.DrawEllipse(SystemPens.ActiveCaption, this.ClientRectangle);
        //        }
        //        else
        //        {
        //            g.DrawEllipse(SystemPens.Control, this.ClientRectangle);
        //        }
        //    }
        //}

        private void btnDrawEllipse_Click(object sender, EventArgs e)
        {
            _drawEllipse = !_drawEllipse;
            //Invalidate(true);
            //Update();
            Refresh();
        }

        private void BasicSample_Paint(object sender, PaintEventArgs e)
        {
            if (_drawEllipse)
            {
                e.Graphics.FillEllipse(Brushes.DarkBlue, ClientRectangle);
            }
        }
    }
}
