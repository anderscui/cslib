using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;

namespace Andersc.CodeLib.Common.WebForm
{
	/// <summary>
	/// WebForm�����Ͽؼ�������ص�ͨ�÷���
	/// </summary>
	public sealed class ControlHelper
	{
		private ControlHelper()
		{
		}

		#region ListBox��ط���

        /// <summary>
        /// ��ListBox������Դ��
        /// </summary>
        /// <param name="lbx">Ҫ�󶨵�Listbox�ؼ���</param>
        /// <param name="dataSource">ʹ�õ�����Դ��</param>
        /// <param name="textField">��ʾ���е����ơ�</param>
        /// <param name="valueField">��Ϊֵ���е����ơ�</param>
		public static void BindListBox(ListBox lbx, IList dataSource, string textField, string valueField)
		{
			lbx.DataSource = dataSource;
			lbx.DataTextField = textField;
			lbx.DataValueField = valueField;
			lbx.DataBind();
		}

        /// <summary>
        /// ���ַ�����Ϊ����Դ�󶨵�ListBox��
        /// </summary>
        /// <param name="lbx">Ҫ�󶨵�Listbox�ؼ���</param>
        /// <param name="text">�����󶨵��ַ�����</param>
		public static void BindListBoxWithString(ListBox lbx, string text)
		{
			BindListBoxWithString(lbx, text, ';');
		}

        /// <summary>
        /// ���ַ�����Ϊ����Դ�󶨵�ListBox��
        /// </summary>
        /// <param name="lbx">Ҫ�󶨵�Listbox�ؼ�</param>
        /// <param name="text">�����󶨵��ַ�����</param>
        /// <param name="split">�ַ����ķָ�����</param>
        /// <remarks>��ʱ�����ԭ�е����ݡ�</remarks>
		public static void BindListBoxWithString(ListBox lbx, string text, char split)
		{
			string[] values = text.Split(split);
			lbx.Items.Clear();
			for(int i = 0; i < values.Length; i++)
			{
				ListItem item = new ListItem(values[i], values[i]);
				lbx.Items.Add(item);
			}
		}

		/// <summary>
		/// ��ʾ���������ListBox��
		/// </summary>
		/// <param name="lbx1">��ʼ����ȫ��</param>
		/// <param name="lbx2">��ʼ��������</param>
		/// <remarks>����ǣ���lbx1����lbx2����ͬ�����Ƴ����������⡣</remarks>
		public static void Display2ListBox(ListBox lbx1, ListBox lbx2)
		{
			for(int i = lbx1.Items.Count - 1;i >= 0;i--)
			{
				string text = lbx1.Items[i].Text;
				for(int j = lbx2.Items.Count-1; j >= 0; j--)
				{
					if(lbx2.Items[j].Text == text)
					{
						lbx1.Items.Remove(lbx1.Items[i]);
					}
				}
				
				lbx2.SelectedIndex = -1;
			}
		}

		/// <summary>
		/// ʵ�� ListBox ֮���ѡ��������ת�ƣ����ݴ� lbox1 ��ӵ� lbox2 �У����Զ�ѡ��ӣ���
		/// </summary>
		/// <param name="lbox1">��ѡ�����ڵ�ListBox��</param>
		/// <param name="lbox2">��ѡ�����ڵ�ListBox��</param>
		public static void ListBoxAdd(ListBox lbox1, ListBox lbox2)
		{
			int flag = 0;
			//ע�⣬Ҫ��items������ʼ�������������������0��ʼѭ���Ļ�����Ϊlbox1��Item���ϼ��٣�
			//ÿ��ѭ����lbox.Items.Count�᲻ͬ����ѡʱ���ܻ�©�������ĳЩѡ����
			for(int i = lbox1.Items.Count - 1; i >= 0; i--)
			{
				ListItem item = lbox1.Items[i];
				if(item.Selected)
				{
					for(int j = lbox2.Items.Count - 1; j >= 0;j--)
					{
						//�ж�lbox2���Ƿ��Ѻ��е�ǰitem����item��Value
						if(lbox2.Items[j].Value == item.Value)
						{
							flag = 1;
						}
					}
					//lbox2û�е�ǰitem����Ӳ�ɾ��lbox1�еĸ�item
					if(flag == 0)
					{
						lbox2.Items.Insert(0, item);
						lbox1.Items.Remove(item);
					}
					lbox2.SelectedIndex = -1;
				}
			}
		}

		/// <summary>
		/// ʵ�� ListBox ֮���ȫ��������ת�ƣ����ݴ� lbox1 ��ӵ� lbox2 �У����Զ�ѡ��ӣ���
		/// </summary>
		/// <param name="lbox1">��ѡ�����ڵ�ListBox��</param>
		/// <param name="lbox2">��ѡ�����ڵ�ListBox��</param>
		public static void ListBoxAddAll(ListBox lbox1, ListBox lbox2)
		{
			int flag = 0;
			//ע�⣬Ҫ��items������ʼ�������������������0��ʼѭ���Ļ�����Ϊlbox1��Item���ϼ��٣�
			//ÿ��ѭ����lbox.Items.Count�᲻ͬ����ѡʱ���ܻ�©�������ĳЩѡ����
			for(int i = lbox1.Items.Count - 1; i >= 0; i--)
			{
				ListItem item = lbox1.Items[i];

				for(int j = lbox2.Items.Count - 1; j >= 0;j--)
				{
					//�ж�lbox2���Ƿ��Ѻ��е�ǰitem����item��Value
					if(lbox2.Items[j].Value == item.Value)
					{
						flag = 1;
					}
				}
				//lbox2û�е�ǰitem����Ӳ�ɾ��lbox1�еĸ�item
				if(flag == 0)
				{
					lbox2.Items.Insert(0, item);
					lbox1.Items.Remove(item);
				}
				lbox2.SelectedIndex = -1;
			}
		}

        /// <summary>
        /// ��ListBox�ؼ���ÿһ���ֵ���ַ�';'���ӡ�
        /// </summary>
        /// <param name="lbx">Ҫ������ListBox��</param>
        /// <returns>�������Ӻ���ַ�����</returns>
		public static string GetListBoxValues(ListBox lbx)
		{
			string result = string.Empty;
			foreach(ListItem item in lbx.Items)
			{
				if(item.Value.Trim().Length > 0)
				{
					result += item.Value + ";";
				}
			}
			result = result.TrimEnd(';');

			return result;
		}

		#endregion ListBox��ط���

		#region DropDownList��ط���

		/// <summary>
		/// ͳһ���ÿ��DropDownList����һ��ֵΪ""��Ĭ����
		/// </summary>
		/// <param name="ddl"></param>
		/// <param name="dataSource"></param>
		/// <param name="textField">�����ʾֵ��Ӧ���ֶ���</param>
		/// <param name="valueField">���ֵ��Ӧ���ֶ���</param>
		/// <param name="promptItem">Ĭ�������ʾ�ı�����"��ѡ������"</param>
		public static void BindDropDownList(DropDownList ddl, IList dataSource, string textField, string valueField, string promptItem)
		{
			ddl.DataSource = dataSource;
			ddl.DataTextField = textField;
			ddl.DataValueField = valueField;
			ddl.DataBind();
			ddl.Items.Insert(0, new ListItem(promptItem, ""));
			ddl.Items.FindByValue("").Selected = true;
		}

        public static void BindDropDownList(DropDownList ddl, IDictionary dictionary)
        {
            foreach (object key in dictionary.Keys)
            {
                ListItem item = new ListItem(dictionary[key].ToString(), key.ToString());
                ddl.Items.Add(item);
            }
            //ddl.Items.Insert(0, new ListItem(promptItem, ""));
        }

        /// <summary>
        /// �鿴DropDownList�ؼ��Ƿ�õ�ѡ�񣨼�Ĭ����֮������Ƿ�ѡ�У���
        /// </summary>
        /// <param name="ddl">Ҫ�鿴��DropDownList�ؼ���</param>
        /// <returns>���Ĭ����ѡ�У�����false�����򷵻�true��</returns>
		public static bool IsDropDownListSelected(DropDownList ddl)
		{
			return IsDropDownListSelected(ddl, "");
		}

        /// <summary>
        /// �鿴DropDownList�ؼ��Ƿ�õ�ѡ�񣨼�Ĭ����֮������Ƿ�ѡ�У���
        /// </summary>
        /// <param name="ddl">Ҫ�鿴��DropDownList�ؼ���</param>
        /// <param name="defaultValue">DropDownList��Ĭ�����ֵ��</param>
        /// <returns>���Ĭ����ѡ�У�����false�����򷵻�true��</returns>
		public static bool IsDropDownListSelected(DropDownList ddl, string defaultValue)
		{
			ListItem defaultItem = ddl.Items.FindByValue(defaultValue);
			if(defaultItem == null)
			{
				throw new ArgumentOutOfRangeException("not existed default item in dropdownlist");
			}
			else
			{
				return !defaultItem.Selected;
			}
		}

        /// <summary>
        /// ��λDropDownList��һ�
        /// </summary>
        /// <param name="ddl">Ҫ��λ��DropDownList��</param>
        /// <param name="searchItem">���ڶ�λ��ֵ��</param>
		public static void LocateDropDownList(DropDownList ddl, string searchItem)
		{
			ListItem expectedItem = ddl.Items.FindByValue(searchItem);
			if(expectedItem != null)
			{
				ddl.SelectedItem.Selected = false;
				expectedItem.Selected = true;
			}
		}


		#endregion DropDownList��ط���

		#region ҳ��ؼ�����

        /// <summary>
        /// ʹָ��ҳ������пؼ����ɱ༭��
        /// </summary>
        /// <param name="page">ָ��ҳ�档</param>
		public static void MakeCtrlUnchangeable(Page page)
		{
			foreach(Control ctrl in page.Controls)
			{
				MakeCtrlUnchangeable(ctrl);
			}
		}
		
		/// <summary>
		/// ʹ�ؼ������ӿؼ����ɱ༭��ʹDropDownList��Enable��Ϊfalse��
		/// </summary>
		public static void MakeCtrlUnchangeable(Control ctrl)
		{
			if (ctrl.Controls.Count > 0)
			{
				//CheckBoxListҪֱ����ΪEnabled=false
				if(ctrl.GetType().Equals(typeof(CheckBoxList)))
				{
					((CheckBoxList)ctrl).Enabled = false;
				}
				else
				{
					foreach(Control control in ctrl.Controls)
					{
						MakeCtrlUnchangeable(control);
					}
				}
			}
			else
			{
				if(ctrl.GetType().Equals(typeof(TextBox)))
				{
					((TextBox)ctrl).ReadOnly = true;
					((TextBox)ctrl).Enabled = false;
				}
				if(ctrl.GetType().Equals(typeof(DropDownList)))
				{
					((DropDownList)ctrl).Enabled = false;
					//((DropDownList)ctrl).CssClass = "form1";
				}
				if(ctrl.GetType().Equals(typeof(CheckBox)))
				{
					((CheckBox)ctrl).Enabled = false;
				}

				//RadioButtonList��ͬ��CheckBoxList������Ϊһ�������ؼ�
				if(ctrl.GetType().Equals(typeof(RadioButtonList)))
				{
					((RadioButtonList)ctrl).Enabled = false;
				}
				if (ctrl.GetType().Equals(typeof(RadioButton)))
				{
					((RadioButton)ctrl).Enabled = false;
				}
				if (ctrl.GetType().Equals(typeof(Button)))
				{
					((Button)ctrl).Enabled=false;
				}
			}
		}

		#endregion ҳ��ؼ�����

        
	}
}
