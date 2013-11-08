using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

namespace CommonUtil.WebForm
{
    /// <summary>
    /// 数据绑定的一些通用操作。
    /// </summary>
    /// <remarks>TODO：查看.NET 2.0中的新控件。</remarks>
    public class DataBindUtil
    {
        private enum OperationType { LimitLength, AddWarnInfo, PageIndexer };

        public delegate void DataBinder();

        //要设置的列的索引
        private int colItemIndex;
        //列内容的长度限制
        private int columnLimit = 0;
        //要显示的警告字符串
        private string warnStr;
        //当前Grid绑定的方法
        private DataBinder binder;
        //当前操作类型，是长度限制，还是显示警告信息？
        private OperationType operationType;

        private const string BoundColumn = "System.Web.UI.WebControls.BoundColumn"; //显示绑定到数据源中的字段的列。它以文本形式显示字段中的每个项。这是 DataGrid 控件的默认列类型。 
        private const string ButtonColumn = "System.Web.UI.WebControls.ButtonColumn";  //  为列中每个项显示一个命令按钮。这使您可以创建一列自定义按钮控件，如 Add 按钮或 Remove 按钮。 
        private const string EditCommandColumn = "System.Web.UI.WebControls.EditCommandColumn"; //  显示一列，该列包含列中各个项的编辑命令。 
        private const string HyperLinkColumn = "System.Web.UI.WebControls.HyperLinkColumn"; // 将列中各项的内容显示为超级链接。列的内容可以绑定到数据源或静态文本中的字段。 
        private const string TemplateColumn = "System.Web.UI.WebControls.TemplateColumn"; //  按照指定的模板显示列中的各项。这使您可以在列中提供自定义控件。 

        #region 构造函数

        public DataBindUtil(int column) : this(column, 20)
        {
        }

        public DataBindUtil(int column, int columnLimit)
        {
            this.colItemIndex = column;
            this.columnLimit = columnLimit;
            operationType = OperationType.LimitLength;
        }

        public DataBindUtil(int column, string warnInfo)
        {
            this.colItemIndex = column;
            operationType = OperationType.AddWarnInfo;
            this.warnStr = warnInfo;
        }

        public DataBindUtil(DataBinder binder)
        {
            this.binder = binder;
            operationType = OperationType.PageIndexer;
        }

        #endregion 构造函数

        #region 为DataGrid中的某列设置显示属性。

        /// <summary>
        /// 添加警告信息确认对话框，增加到ItemDataBound事件中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddColumnWarningString(object sender, DataGridItemEventArgs e)
        {
            if (this.operationType != OperationType.AddWarnInfo)
            {
                return;
            }

            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                //string result = string.Empty;
                int itemIndex = this.colItemIndex;
                DataGrid myDataGrid = (DataGrid)sender;

                if ((itemIndex < 0) || (itemIndex > myDataGrid.Columns.Count - 1))
                {
                    throw new ArgumentOutOfRangeException("columnIndex", "列的索引超出了范围");
                }
                else
                {
                    try
                    {
                        e.Item.Cells[itemIndex].Attributes.Add("onclick", "return confirm('" + this.warnStr + "');");
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }

        public void SetColumnLengthLimit(object sender, DataGridItemEventArgs e)
        {
            //在datagrid中显示的字符串长度
            int charNum = 20;
            if (this.columnLimit != 0)
            {
                charNum = this.columnLimit;
            }
            int itemIndex = this.colItemIndex;

            DataGrid dgd = (DataGrid)sender;
            if ((itemIndex < 0) || (itemIndex > dgd.Columns.Count - 1))
            {
                throw new ArgumentOutOfRangeException("columnIndex", "列的索引超出了范围");
            }
            else
            {
                string typeStr = dgd.Columns[this.colItemIndex].GetType().ToString();
                if (typeStr == DataBindUtil.BoundColumn)
                {
                    string input = e.Item.Cells[colItemIndex].Text;
                    if (input != string.Empty)
                    {
                        e.Item.Cells[colItemIndex].Text = StringHelper.Truncate(input, charNum);
                        e.Item.Cells[colItemIndex].ToolTip = input;
                    }
                }
                else if (typeStr == DataBindUtil.ButtonColumn)
                {
                    if (e.Item.ItemIndex != -1)
                    {
                        Control ctrl = e.Item.Cells[colItemIndex].Controls[0];
                        string text = (ctrl as LinkButton).Text;
                        (ctrl as LinkButton).Text = StringHelper.Truncate((ctrl as LinkButton).Text, charNum);
                        (ctrl as LinkButton).ToolTip = text;
                    }
                }
                else if (typeStr == DataBindUtil.HyperLinkColumn)
                {
                    Control ctrl = new Control();
                    if (e.Item.Cells[this.colItemIndex].Controls.Count > 0)
                    {
                        ctrl = e.Item.Cells[colItemIndex].Controls[0];
                        string text = (ctrl as HyperLink).Text;
                        (ctrl as HyperLink).Text = StringHelper.Truncate((ctrl as HyperLink).Text, charNum);
                        (ctrl as HyperLink).ToolTip = text;
                    }
                }
                else if (typeStr == DataBindUtil.TemplateColumn)
                {
                    //TODO:
                    throw new ArgumentException("不能为模板列设置列长度");
                }
            }
        }

        public void SetGridIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            if (binder == null)
            {
                return;
            }
            DataGrid dgrd = source as DataGrid;
            dgrd.CurrentPageIndex = e.NewPageIndex;
            this.binder();
        }

        #endregion

        #region 将TextBox的值转换为想要的数值类型

        /// <summary>
        /// 将TextBox的值转换为想要的数值类型，如果不能转换，则抛出ControlValueException异常。
        /// </summary>
        /// <param name="targetType">转换后的对象类型</param>
        /// <param name="tbx">TextBox对象</param>
        /// <param name="allowNull">是否允许TextBox为空</param>
        /// <returns>转换后的对象</returns>
        internal static object FillFromTextBoxToTarget(Type targetType, TextBox tbx, bool allowNull)
        {
            //TODO:将单引号转换为两个单引号，这个在数据库查询时会用到，但是该动作似乎并不应在此处执行
            //string value = tbx.Text.Trim().Replace("'","''");
            string originalValue = tbx.Text.Trim();
            return ConvertObjectToTarget(targetType, tbx, originalValue, allowNull);
        }

        internal static object ConvertObjectToTarget(Type targetType, Control ctrl, object objValue, bool allowNull)
        {
            string value = objValue.ToString();

            if (value.Length == 0)
            {
                if (allowNull == true)
                {
                    return null;
                }
                else
                {
                    throw new InvalidOperationException("此处转换值的类型时不允许为空值");
                }
            }

            try
            {
                return Convert.ChangeType(value, targetType);
            }
            catch (Exception e)
            {
                throw new InvalidCastException("数据转换失败", e);
            }
        }

        #endregion

        #region 控件的值到对象属性值的填充。一个主方法，2个重载，分别用于TextBox，Dropdownlist控件

        /// <summary>
        /// 将控件中的值转换为实体类实例的对应属性的值，如果转换失败，则抛出异常
        /// 本方法适用于各种web控件，另外还有各个重载方法，分别用于特定的控件，例如TextBox，ComboBox等
        /// TODO:应该会用到反射，造成性能下降？先不用了
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="columnName"></param>
        /// <param name="ctrl"></param>
        /// <param name="ctrlValue"></param>
        /// <param name="allowNull"></param>
        /// <param name="checkEnableStatus"></param>
        internal static void FillFromControlToObject(object obj, string columnName, Control ctrl, object ctrlValue, bool allowNull)
        {
            //			object value = ctrlValue;
            //			if(null != value && value is string && ((string)value) == "")
            //			{
            //				value = null;
            //			}
            //			ObjectMap map=ObjectFactory.GetMap (obj);
            //			AttributeMap attMap=map.Fields .FindColumn (columnName);
            //			//attMap.PropertyInfo.PropertyType  
            //			//TODO: int datetime, short not null.
            //			attMap.SetValue (obj,ConvertObjectToTarget(attMap.Type,ctrl,ctrlValue,allowNull));
        }

        /// <summary>
        /// 重载版本，用于TextBox控件，其值对象设为TextBox的Text属性
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="tbx">The TBX.</param>
        /// <param name="allowNull">if set to <c>true</c> [allow null].</param>
        internal static void FillFromControlToObject(object obj, string columnName, TextBox tbx, bool allowNull)
        {
            FillFromControlToObject(obj, columnName, tbx, tbx.Text.Trim(), allowNull);
        }

        /// <summary>
        /// 重载版本，用于ListControl类的控件，其值对象设为ListControl的SelectValue属性
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="lbx">The LBX.</param>
        /// <param name="allowNull">if set to <c>true</c> [allow null].</param>
        internal static void FillFromControlToObject(object obj, string columnName, ListControl lbx, bool allowNull)
        {
            if ((lbx.SelectedIndex != -1) && (lbx.SelectedValue != "-1"))
            {
                FillFromControlToObject(obj, columnName, lbx, lbx.SelectedValue, allowNull);
            }
        }

        #endregion

        #region 实体类实例的属性值到控件的值的填充。一个主方法，四个重载，分别用于TextBox， Label等

        /// <summary>
        /// TODO:将Object的某列的值设置为控件的值
        /// 本方法适用于各种Window控件，另外还有各个重载方法，分别用于特定的控件，例如TextBox，ComboBox等
        /// 如果设置成功，则返回ture。如果返回的是false，则有两种可能：
        /// 1。DataRow的该列的值为DBNull，则方法内部不对控件做任何赋值，直接返回。因此在调用本方法前，通常应当首先给控件设定缺省值，对付这种表的字段值为空的情况。
        /// 2。控件赋值失败，即该控件不能用来表示DataRow的该列的值
        /// </summary>
        /// <param name="obj">DataRow对象</param>
        /// <param name="columnName">列名</param>
        /// <param name="ctrl">控件对象</param>
        /// <param name="valueMember">控件的值对象的属性名。例如TextBox控件是"Text"。</param>
        /// <returns>赋值是否成功</returns>
        internal static bool FillFromObjToCtrl(object obj, string columnName, Control ctrl, string valueMember)
        {
            //			ObjectMap map=ObjectFactory.GetMap (obj);
            //			
            //			AttributeMap attMap=map.Fields .FindColumn (columnName);
            //			//TODO: int datetime, short not null.
            //			if(attMap==null)
            //				return false;
            //
            //			object value = attMap.GetValue(obj);;
            //			if(value == null)
            //				return false;
            //
            //			Type typeOfCtrl = ctrl.GetType();
            //			System.Reflection.PropertyInfo pi = typeOfCtrl.GetProperty(valueMember);
            //			System.Reflection.MethodInfo mi = pi.GetSetMethod();
            //
            //			object convertedValue;
            //			try
            //			{
            //				convertedValue = Convert.ChangeType(value, pi.PropertyType);
            //				object[] param= new object[1];
            //				param[0] = convertedValue;
            //				mi.Invoke(ctrl, param);
            //			}
            //			catch
            //			{
            //				return false;
            //			}
            return true;
        }

        /// <summary>
        /// 重载版本，用于TextBox，其值对象的属性名为"Text"
        /// </summary>
        /// <param name="row"></param>
        /// <param name="columnName"></param>
        /// <param name="tbx"></param>
        /// <returns></returns>
        internal static bool FillFromObjToCtrl(object obj, string columnName, TextBox tbx)
        {
            return FillFromObjToCtrl(obj, columnName, tbx, "Text");
        }

        /// <summary>
        /// 重载版本，用于Label，其值对象的属性名为"Text"
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="columnName"></param>
        /// <param name="lbl"></param>
        /// <returns></returns>
        internal static bool FillFromObjToCtrl(object obj, string columnName, Label lbl)
        {
            return FillFromObjToCtrl(obj, columnName, lbl, "Text");
        }

        #endregion

        #region 判断输入的一些日期，要求按照顺序
        /// <summary>
        /// 逻辑判断，日期参数要求从小到大进行排序。
        /// </summary>
        /// <param name="Dates"></param>
        /// <returns>1- ok,-1 不符合输入逻辑</returns>
        public static int DateValidateCheck(params DateTime[] Dates)
        {
            //P2Exception .TrueThrow (Dates.Length <2,"意外错误，选择了小于2个参数进行了比较！");

            if (Dates.Length > 2)
            {
                for (int i = 0; i < Dates.Length - 1; i++)
                {
                    int va = DateTime.Compare(Dates[i], Dates[i + 1]);
                    if (va > 0)
                        return -1;
                }
                //ok ,pass
                return 1;
            }
            else
            {
                //if only have two param,then 
                return DateTime.Compare(Dates[0], Dates[1]) > 0 ? -1 : 1;
            }
        }
        #endregion
    }
}
