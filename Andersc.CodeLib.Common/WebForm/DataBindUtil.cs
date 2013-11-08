using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

namespace CommonUtil.WebForm
{
    /// <summary>
    /// ���ݰ󶨵�һЩͨ�ò�����
    /// </summary>
    /// <remarks>TODO���鿴.NET 2.0�е��¿ؼ���</remarks>
    public class DataBindUtil
    {
        private enum OperationType { LimitLength, AddWarnInfo, PageIndexer };

        public delegate void DataBinder();

        //Ҫ���õ��е�����
        private int colItemIndex;
        //�����ݵĳ�������
        private int columnLimit = 0;
        //Ҫ��ʾ�ľ����ַ���
        private string warnStr;
        //��ǰGrid�󶨵ķ���
        private DataBinder binder;
        //��ǰ�������ͣ��ǳ������ƣ�������ʾ������Ϣ��
        private OperationType operationType;

        private const string BoundColumn = "System.Web.UI.WebControls.BoundColumn"; //��ʾ�󶨵�����Դ�е��ֶε��С������ı���ʽ��ʾ�ֶ��е�ÿ������� DataGrid �ؼ���Ĭ�������͡� 
        private const string ButtonColumn = "System.Web.UI.WebControls.ButtonColumn";  //  Ϊ����ÿ������ʾһ�����ť����ʹ�����Դ���һ���Զ��尴ť�ؼ����� Add ��ť�� Remove ��ť�� 
        private const string EditCommandColumn = "System.Web.UI.WebControls.EditCommandColumn"; //  ��ʾһ�У����а������и�����ı༭��� 
        private const string HyperLinkColumn = "System.Web.UI.WebControls.HyperLinkColumn"; // �����и����������ʾΪ�������ӡ��е����ݿ��԰󶨵�����Դ��̬�ı��е��ֶΡ� 
        private const string TemplateColumn = "System.Web.UI.WebControls.TemplateColumn"; //  ����ָ����ģ����ʾ���еĸ����ʹ�������������ṩ�Զ���ؼ��� 

        #region ���캯��

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

        #endregion ���캯��

        #region ΪDataGrid�е�ĳ��������ʾ���ԡ�

        /// <summary>
        /// ��Ӿ�����Ϣȷ�϶Ի������ӵ�ItemDataBound�¼���
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
                    throw new ArgumentOutOfRangeException("columnIndex", "�е����������˷�Χ");
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
            //��datagrid����ʾ���ַ�������
            int charNum = 20;
            if (this.columnLimit != 0)
            {
                charNum = this.columnLimit;
            }
            int itemIndex = this.colItemIndex;

            DataGrid dgd = (DataGrid)sender;
            if ((itemIndex < 0) || (itemIndex > dgd.Columns.Count - 1))
            {
                throw new ArgumentOutOfRangeException("columnIndex", "�е����������˷�Χ");
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
                    throw new ArgumentException("����Ϊģ���������г���");
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

        #region ��TextBox��ֵת��Ϊ��Ҫ����ֵ����

        /// <summary>
        /// ��TextBox��ֵת��Ϊ��Ҫ����ֵ���ͣ��������ת�������׳�ControlValueException�쳣��
        /// </summary>
        /// <param name="targetType">ת����Ķ�������</param>
        /// <param name="tbx">TextBox����</param>
        /// <param name="allowNull">�Ƿ�����TextBoxΪ��</param>
        /// <returns>ת����Ķ���</returns>
        internal static object FillFromTextBoxToTarget(Type targetType, TextBox tbx, bool allowNull)
        {
            //TODO:��������ת��Ϊ���������ţ���������ݿ��ѯʱ���õ������Ǹö����ƺ�����Ӧ�ڴ˴�ִ��
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
                    throw new InvalidOperationException("�˴�ת��ֵ������ʱ������Ϊ��ֵ");
                }
            }

            try
            {
                return Convert.ChangeType(value, targetType);
            }
            catch (Exception e)
            {
                throw new InvalidCastException("����ת��ʧ��", e);
            }
        }

        #endregion

        #region �ؼ���ֵ����������ֵ����䡣һ����������2�����أ��ֱ�����TextBox��Dropdownlist�ؼ�

        /// <summary>
        /// ���ؼ��е�ֵת��Ϊʵ����ʵ���Ķ�Ӧ���Ե�ֵ�����ת��ʧ�ܣ����׳��쳣
        /// �����������ڸ���web�ؼ������⻹�и������ط������ֱ������ض��Ŀؼ�������TextBox��ComboBox��
        /// TODO:Ӧ�û��õ����䣬��������½����Ȳ�����
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
        /// ���ذ汾������TextBox�ؼ�����ֵ������ΪTextBox��Text����
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
        /// ���ذ汾������ListControl��Ŀؼ�����ֵ������ΪListControl��SelectValue����
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

        #region ʵ����ʵ��������ֵ���ؼ���ֵ����䡣һ�����������ĸ����أ��ֱ�����TextBox�� Label��

        /// <summary>
        /// TODO:��Object��ĳ�е�ֵ����Ϊ�ؼ���ֵ
        /// �����������ڸ���Window�ؼ������⻹�и������ط������ֱ������ض��Ŀؼ�������TextBox��ComboBox��
        /// ������óɹ����򷵻�ture��������ص���false���������ֿ��ܣ�
        /// 1��DataRow�ĸ��е�ֵΪDBNull���򷽷��ڲ����Կؼ����κθ�ֵ��ֱ�ӷ��ء�����ڵ��ñ�����ǰ��ͨ��Ӧ�����ȸ��ؼ��趨ȱʡֵ���Ը����ֱ���ֶ�ֵΪ�յ������
        /// 2���ؼ���ֵʧ�ܣ����ÿؼ�����������ʾDataRow�ĸ��е�ֵ
        /// </summary>
        /// <param name="obj">DataRow����</param>
        /// <param name="columnName">����</param>
        /// <param name="ctrl">�ؼ�����</param>
        /// <param name="valueMember">�ؼ���ֵ�����������������TextBox�ؼ���"Text"��</param>
        /// <returns>��ֵ�Ƿ�ɹ�</returns>
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
        /// ���ذ汾������TextBox����ֵ�����������Ϊ"Text"
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
        /// ���ذ汾������Label����ֵ�����������Ϊ"Text"
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

        #region �ж������һЩ���ڣ�Ҫ����˳��
        /// <summary>
        /// �߼��жϣ����ڲ���Ҫ���С�����������
        /// </summary>
        /// <param name="Dates"></param>
        /// <returns>1- ok,-1 �����������߼�</returns>
        public static int DateValidateCheck(params DateTime[] Dates)
        {
            //P2Exception .TrueThrow (Dates.Length <2,"�������ѡ����С��2�����������˱Ƚϣ�");

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
