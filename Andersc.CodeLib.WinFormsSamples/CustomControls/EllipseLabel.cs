using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Andersc.CodeLib.Common;
using Andersc.CodeLib.WinFormsSamples.Components;

namespace Andersc.CodeLib.WinFormsSamples.CustomControls
{
    public partial class EllipseLabel : Control
    {
        public event EventHandler<ValueChangedEventArgs<string>> PrefixChanged;

        private string _prefix = string.Empty;
        private bool mouseDown = false;

        public EllipseLabel()
        {
            InitializeComponent();

            this.TextChanged += new EventHandler(EllipseLabel_TextChanged);

        }

        #region Event Handlers

        private void EllipseLabel_TextChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            Brush foreBrush = new SolidBrush(ForeColor);
            Brush backBrush = new SolidBrush(BackColor);
            g.FillEllipse(foreBrush, ClientRectangle);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            g.DrawString(Prefix + Text, Font, backBrush, ClientRectangle, format);

            base.OnPaint(pe);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            mouseDown = true;
            SetMouseForeColor(e);

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (mouseDown)
            {
                SetMouseForeColor(e);
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            SetMouseForeColor(e);
            mouseDown = false;

            base.OnMouseUp(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            Point newLocation = new Point(Left, Top);

            switch (e.KeyChar)
            {
                case 'i':
                    newLocation.Y -= 1;
                    break;
                case 'j':
                    newLocation.X -= 1;
                    break;
                case 'k':
                    newLocation.Y += 1;
                    break;
                case 'l':
                    newLocation.Y += 1;
                    break;

                default:
                    break;
            }

            this.Location = newLocation;
            base.OnKeyPress(e);
        }

        #endregion

        #region Private Methods

        private void SetMouseForeColor(MouseEventArgs e)
        {
            int red = (e.X * 255 / (this.ClientRectangle.Width - e.X)) % 256;
            red = Math.Abs(red);
            int green = 0;
            int blue = (e.Y * 255 / (this.ClientRectangle.Height - e.Y)) % 256;
            blue = Math.Abs(blue);

            this.ForeColor = Color.FromArgb(red, green, blue);
        }

        #endregion

        public string Prefix
        {
            get { return _prefix; }
            set
            {
                if (_prefix != value)
                {
                    ValueChangedEventArgs<string> e = new ValueChangedEventArgs<string>(_prefix, value);

                    _prefix = value;
                    // This is required.
                    Invalidate();

                    if (PrefixChanged.IsNotNull())
                    {
                        PrefixChanged(this, e);
                    }
                }
            }
        }

        public void ResetPrefix()
        {
            Prefix = string.Empty;
        }
    }
}
