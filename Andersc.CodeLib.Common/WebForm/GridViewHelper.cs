using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Andersc.CodeLib.Common.Web
{
    /// <summary>
    /// 使用GridView绑定数据的工具类。
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

        //要设置的列的索引
        private int columnIndex = -1;
        //列内容的长度限制，默认为15
        private int columnLimit = 15;
        //要显示的警告字符串
        private string warningString;
        // 鼠标悬停时行的背景颜色，如"#FFFF00"；
        private string onMouseOverColor = null;
        // 鼠标移出时行的背景颜色，如"#FF0000"；
        private string onMouseOutColor = null;
        //当前操作类型
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

        #region 构造函数

        /// <summary>
        /// 初始化类<see cref="GridViewHelper"/>。
        /// </summary>
        /// <param name="columnIndex">要设置的列的索引。</param>
        /// <param name="columnLimit">要设置的列长度限制。</param>
        public GridViewUtil(int columnIndex, int columnLimit)
        {
            this.columnIndex = columnIndex;
            this.columnLimit = columnLimit;
            operationType = OperationType.LimitLength;
        }

        /// <summary>
        /// 初始化类<see cref="GridViewHelper"/>。
        /// </summary>
        /// <param name="columnIndex">要设置的列的索引。</param>
        /// <param name="warnInfo">要设置的警告消息内容。</param>
        public GridViewUtil(int columnIndex, string warnInfo)
        {
            //this.columnIndex = columnIndex;
            //operationType = OperationType.AddWarningInfo;
            //this.warningString = warnInfo;
            throw new NotImplementedException("该方法尚未实现。");
        }

        /// <summary>
        /// 初始化类<see cref="GridViewHelper"/>。
        /// </summary>
        /// <param name="mouseOverColor">鼠标经过时的颜色。</param>
        /// <param name="mouseOutColor">鼠标移出时的颜色。</param>
        public GridViewUtil(string mouseOverColor, string mouseOutColor)
        {
            onMouseOverColor = mouseOverColor;
            onMouseOutColor = mouseOutColor;
            operationType = OperationType.HoverColor;
        }

        #endregion 构造函数

        /// <summary>
        /// 设置GridView列的长度。
        /// </summary>
        /// <param name="sender">引发事件的GridView。</param>
        /// <param name="e"><see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/>类型的实例，包含事件的数据。</param>
        /// <remarks>使用时通过<c>public GridViewHelper(int columnIndex, int columnLimit)</c>构造函数创建实例。</remarks>
        /// <example>
        /// <code>
        /// // GridView1的第5列将显示至多15个字符。
        /// GridView1.RowDataBound += new GridViewRowEventHandler((new GridViewHelper(5, 15)).SetColumnLengthLimit);
        /// </code>
        /// </example>
        public void SetColumnLengthLimit(object sender, GridViewRowEventArgs e)
        {
            if (operationType != OperationType.LimitLength)
            {
                throw new InvalidOperationException("错误的操作类型：" + operationType.ToString());
            }

            if (columnLimit < 1)
            {
                return;
            }

            GridView gv = sender as GridView;
            if (gv == null) { return; }

            if ((columnIndex < 0) || (columnIndex >= gv.Columns.Count))
            {
                throw new IndexOutOfRangeException("GridView索引越界。");
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
                throw new ArgumentException("不能为模板列设置列长度");
            }
        }

        /// <summary>
        /// 设置GridView行的悬停颜色。
        /// </summary>
        /// <param name="sender">引发事件的GridView。</param>
        /// <param name="e"><see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/>类型的实例，包含事件的数据。</param>
        /// <remarks>使用时通过<c>public GridViewHelper(string mouseOverColor, string mouseOutColor)</c>构造函数创建实例。</remarks>
        /// <example>
        /// <code>
        /// GridView1.RowDataBound += new GridViewRowEventHandler((new GridViewHelper("#B4DFE8", "#F4F4F4")).SetHoverColor);
        /// </code>
        /// </example>
        public void SetHoverColor(object sender, GridViewRowEventArgs e)
        {
            if (operationType != OperationType.HoverColor)
            {
                throw new InvalidOperationException("错误的操作类型：" + operationType.ToString());
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // 鼠标移入时，行背景色变
                e.Row.Attributes.Add("onmouseover", string.Format("this.style.backgroundColor='{0}'", onMouseOverColor));

                // 鼠标移出时，行背景色变
                e.Row.Attributes.Add("onmouseout", string.Format("this.style.backgroundColor='{0}'", onMouseOutColor));
            }
        }
    }
}
