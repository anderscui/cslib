using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Andersc.CodeLib.Common.Web
{
    /// <summary>
    /// ʹ��GridView�����ݵĹ����ࡣ
    /// </summary>
    public class GridViewUtil
    {
        private enum OperationType
        {
            LimitLength,
            AddWarningInfo,
            PageIndexer,
            HoverColor
        };

        //Ҫ���õ��е�����
        private int columnIndex = -1;
        //�����ݵĳ������ƣ�Ĭ��Ϊ15
        private int columnLimit = 15;
        //Ҫ��ʾ�ľ����ַ���
        private string warningString;
        // �����ͣʱ�еı�����ɫ����"#FFFF00"��
        private string onMouseOverColor = null;
        // ����Ƴ�ʱ�еı�����ɫ����"#FF0000"��
        private string onMouseOutColor = null;
        //��ǰ��������
        private OperationType operationType;

        // Represents a field that is displayed as text in a data-bound control.  
        private const string BoundField = "System.Web.UI.WebControls.BoundField";
        // Represents a field that is displayed as a button in a data-bound control.
        private const string ButtonField = "System.Web.UI.WebControls.ButtonField";
        // Represents a Boolean field that is displayed as a check box in a data-bound control. 
        private const string CheckBoxField = "System.Web.UI.WebControls.CheckBoxField";
        // Represents a special field that displays command buttons to perform selecting, editing, inserting, or deleting operations in a data-bound control.
        private const string CommandField = "System.Web.UI.WebControls.CommandField";
        // Represents a field that is displayed as a hyperlink in a data-bound control.
        private const string HyperLinkField = "System.Web.UI.WebControls.HyperLinkField";
        // Represents a field that is displayed as a hyperlink in a data-bound control.
        private const string ImageField = "System.Web.UI.WebControls.ImageField";
        // Represents a field that displays custom content in a data-bound control.
        private const string TemplateField = "System.Web.UI.WebControls.TemplateField";

        #region ���캯��

        /// <summary>
        /// ��ʼ����<see cref="GridViewHelper"/>��
        /// </summary>
        /// <param name="columnIndex">Ҫ���õ��е�������</param>
        /// <param name="columnLimit">Ҫ���õ��г������ơ�</param>
        public GridViewUtil(int columnIndex, int columnLimit)
        {
            this.columnIndex = columnIndex;
            this.columnLimit = columnLimit;
            operationType = OperationType.LimitLength;
        }

        /// <summary>
        /// ��ʼ����<see cref="GridViewHelper"/>��
        /// </summary>
        /// <param name="columnIndex">Ҫ���õ��е�������</param>
        /// <param name="warnInfo">Ҫ���õľ�����Ϣ���ݡ�</param>
        public GridViewUtil(int columnIndex, string warnInfo)
        {
            //this.columnIndex = columnIndex;
            //operationType = OperationType.AddWarningInfo;
            //this.warningString = warnInfo;
            throw new NotImplementedException("�÷�����δʵ�֡�");
        }

        /// <summary>
        /// ��ʼ����<see cref="GridViewHelper"/>��
        /// </summary>
        /// <param name="mouseOverColor">��꾭��ʱ����ɫ��</param>
        /// <param name="mouseOutColor">����Ƴ�ʱ����ɫ��</param>
        public GridViewUtil(string mouseOverColor, string mouseOutColor)
        {
            onMouseOverColor = mouseOverColor;
            onMouseOutColor = mouseOutColor;
            operationType = OperationType.HoverColor;
        }

        #endregion ���캯��

        /// <summary>
        /// ����GridView�еĳ��ȡ�
        /// </summary>
        /// <param name="sender">�����¼���GridView��</param>
        /// <param name="e"><see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/>���͵�ʵ���������¼������ݡ�</param>
        /// <remarks>ʹ��ʱͨ��<c>public GridViewHelper(int columnIndex, int columnLimit)</c>���캯������ʵ����</remarks>
        /// <example>
        /// <code>
        /// // GridView1�ĵ�5�н���ʾ����15���ַ���
        /// GridView1.RowDataBound += new GridViewRowEventHandler((new GridViewHelper(5, 15)).SetColumnLengthLimit);
        /// </code>
        /// </example>
        public void SetColumnLengthLimit(object sender, GridViewRowEventArgs e)
        {
            if (operationType != OperationType.LimitLength)
            {
                throw new InvalidOperationException("����Ĳ������ͣ�" + operationType.ToString());
            }

            if (columnLimit < 1)
            {
                return;
            }

            GridView gv = sender as GridView;
            if (gv == null) { return; }

            if ((columnIndex < 0) || (columnIndex >= gv.Columns.Count))
            {
                throw new IndexOutOfRangeException("GridView����Խ�硣");
            }

            if (e.Row.RowType != DataControlRowType.DataRow)
            {
                return;
            }

            string columnType = gv.Columns[columnIndex].GetType().ToString();
            if (columnType == GridViewUtil.BoundField)
            {
                string input = e.Row.Cells[columnIndex].Text;
                if (input.Length > columnLimit)
                {
                    e.Row.Cells[columnIndex].Text = StringHelper.TruncateWithEllipsis(input, columnLimit);
                    e.Row.Cells[columnIndex].ToolTip = input;
                }
            }
            else if (columnType == GridViewUtil.ButtonField)
            {
                if (e.Row.RowIndex != -1)
                {
                    Control ctrl = e.Row.Cells[columnIndex].Controls[0];
                    string text = (ctrl as LinkButton).Text;
                    if (text.Length > columnLimit)
                    {
                        (ctrl as LinkButton).Text = StringHelper.TruncateWithEllipsis((ctrl as LinkButton).Text, columnLimit);
                        (ctrl as LinkButton).ToolTip = text;
                    }
                }
            }
            else if (columnType == GridViewUtil.HyperLinkField)
            {
                Control ctrl = new Control();
                if (e.Row.Cells[columnIndex].Controls.Count > 0)
                {
                    ctrl = e.Row.Cells[columnIndex].Controls[0];
                    string text = (ctrl as HyperLink).Text;
                    if (text.Length > columnLimit)
                    {
                        (ctrl as HyperLink).Text = StringHelper.TruncateWithEllipsis((ctrl as HyperLink).Text, columnLimit);
                        (ctrl as HyperLink).ToolTip = text;
                    }
                }
            }
            else if (columnType == GridViewUtil.TemplateField)
            {
                //TODO:
                throw new ArgumentException("����Ϊģ���������г���");
            }
        }

        /// <summary>
        /// ����GridView�е���ͣ��ɫ��
        /// </summary>
        /// <param name="sender">�����¼���GridView��</param>
        /// <param name="e"><see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/>���͵�ʵ���������¼������ݡ�</param>
        /// <remarks>ʹ��ʱͨ��<c>public GridViewHelper(string mouseOverColor, string mouseOutColor)</c>���캯������ʵ����</remarks>
        /// <example>
        /// <code>
        /// GridView1.RowDataBound += new GridViewRowEventHandler((new GridViewHelper("#B4DFE8", "#F4F4F4")).SetHoverColor);
        /// </code>
        /// </example>
        public void SetHoverColor(object sender, GridViewRowEventArgs e)
        {
            if (operationType != OperationType.HoverColor)
            {
                throw new InvalidOperationException("����Ĳ������ͣ�" + operationType.ToString());
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // �������ʱ���б���ɫ��
                e.Row.Attributes.Add("onmouseover", string.Format("this.style.backgroundColor='{0}'", onMouseOverColor));

                // ����Ƴ�ʱ���б���ɫ��
                e.Row.Attributes.Add("onmouseout", string.Format("this.style.backgroundColor='{0}'", onMouseOutColor));
            }
        }
    }
}
