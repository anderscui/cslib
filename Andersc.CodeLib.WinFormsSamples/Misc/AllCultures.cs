using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Andersc.CodeLib.WinFormsSamples.Misc
{
    public partial class AllCultures : Form
    {
        public AllCultures()
        {
            InitializeComponent();
        }

        private void AllCultures_Load(object sender, EventArgs e)
        {
            double amount = 4.52;
            DateTime now = DateTime.Now;

            var cultures = from ci in CultureInfo.GetCultures(CultureTypes.AllCultures)
                           orderby ci.EnglishName
                           select new
                           {
                               EnglishName = ci.EnglishName,
                               Name = ci.Name,
                               Currency = amount.ToString("C", ci.NumberFormat),
                               Date = now.ToString("d", ci.DateTimeFormat),
                               IsNeutralCulture = ci.IsNeutralCulture
                           };

            cultures.ToList().ForEach(ci =>
            {
                ListViewItem item = lvAllCultures.Items.Add(ci.EnglishName);
                item.SubItems.Add(ci.Name);
                if (!ci.IsNeutralCulture)
                {
                    item.SubItems.Add(ci.Currency);
                    item.SubItems.Add(ci.Date);
                }
            });

            //foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.AllCultures))
            //{
            //    ListViewItem item = lvAllCultures.Items.Add(ci.EnglishName);
            //    item.SubItems.Add(ci.Name);
            //    if (!ci.IsNeutralCulture)
            //    {
            //        item.SubItems.Add(amount.ToString("C", ci.NumberFormat));
            //        item.SubItems.Add(now.ToString("d", ci.DateTimeFormat));
            //    }
            //}
        }
    }
}
