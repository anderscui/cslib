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
	/// WebForm窗体上控件操作相关的通用方法
	/// </summary>
	public sealed class ControlHelper
	{
		private ControlHelper()
		{
		}

		#region ListBox相关方法

        /// <summary>
        /// 绑定ListBox到数据源。
        /// </summary>
        /// <param name="lbx">要绑定的Listbox控件。</param>
        /// <param name="dataSource">使用的数据源。</param>
        /// <param name="textField">显示的列的名称。</param>
        /// <param name="valueField">作为值的列的名称。</param>
		public static void BindListBox(ListBox lbx, IList dataSource, string textField, string valueField)
		{
			lbx.DataSource = dataSource;
			lbx.DataTextField = textField;
			lbx.DataValueField = valueField;
			lbx.DataBind();
		}

        /// <summary>
        /// 将字符串作为数据源绑定到ListBox。
        /// </summary>
        /// <param name="lbx">要绑定的Listbox控件。</param>
        /// <param name="text">用来绑定的字符串。</param>
		public static void BindListBoxWithString(ListBox lbx, string text)
		{
			BindListBoxWithString(lbx, text, ';');
		}

        /// <summary>
        /// 将字符串作为数据源绑定到ListBox。
        /// </summary>
        /// <param name="lbx">要绑定的Listbox控件</param>
        /// <param name="text">用来绑定的字符串。</param>
        /// <param name="split">字符串的分隔符。</param>
        /// <remarks>绑定时会清除原有的数据。</remarks>
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
		/// 显示两个互斥的ListBox。
		/// </summary>
		/// <param name="lbx1">初始包含全部</param>
		/// <param name="lbx2">初始包含部分</param>
		/// <remarks>结果是，将lbx1中与lbx2中相同的项移除，两个互斥。</remarks>
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
		/// 实现 ListBox 之间的选中数据项转移，数据从 lbox1 添加到 lbox2 中（可以多选添加）。
		/// </summary>
		/// <param name="lbox1">候选项所在的ListBox。</param>
		/// <param name="lbox2">被选项所在的ListBox。</param>
		public static void ListBoxAdd(ListBox lbox1, ListBox lbox2)
		{
			int flag = 0;
			//注意，要从items总数开始倒减，否则若是正序从0开始循环的话，因为lbox1的Item不断减少，
			//每次循环的lbox.Items.Count会不同，多选时可能会漏掉后面的某些选中项
			for(int i = lbox1.Items.Count - 1; i >= 0; i--)
			{
				ListItem item = lbox1.Items[i];
				if(item.Selected)
				{
					for(int j = lbox2.Items.Count - 1; j >= 0;j--)
					{
						//判断lbox2中是否已含有当前item，用item的Value
						if(lbox2.Items[j].Value == item.Value)
						{
							flag = 1;
						}
					}
					//lbox2没有当前item怎添加并删除lbox1中的该item
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
		/// 实现 ListBox 之间的全部数据项转移，数据从 lbox1 添加到 lbox2 中（可以多选添加）。
		/// </summary>
		/// <param name="lbox1">候选项所在的ListBox。</param>
		/// <param name="lbox2">被选项所在的ListBox。</param>
		public static void ListBoxAddAll(ListBox lbox1, ListBox lbox2)
		{
			int flag = 0;
			//注意，要从items总数开始倒减，否则若是正序从0开始循环的话，因为lbox1的Item不断减少，
			//每次循环的lbox.Items.Count会不同，多选时可能会漏掉后面的某些选中项
			for(int i = lbox1.Items.Count - 1; i >= 0; i--)
			{
				ListItem item = lbox1.Items[i];

				for(int j = lbox2.Items.Count - 1; j >= 0;j--)
				{
					//判断lbox2中是否已含有当前item，用item的Value
					if(lbox2.Items[j].Value == item.Value)
					{
						flag = 1;
					}
				}
				//lbox2没有当前item怎添加并删除lbox1中的该item
				if(flag == 0)
				{
					lbox2.Items.Insert(0, item);
					lbox1.Items.Remove(item);
				}
				lbox2.SelectedIndex = -1;
			}
		}

        /// <summary>
        /// 将ListBox控件中每一项的值以字符';'连接。
        /// </summary>
        /// <param name="lbx">要操作的ListBox。</param>
        /// <returns>返回连接后的字符串。</returns>
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

		#endregion ListBox相关方法

		#region DropDownList相关方法

		/// <summary>
		/// 统一风格，每个DropDownList都有一个值为""的默认项
		/// </summary>
		/// <param name="ddl"></param>
		/// <param name="dataSource"></param>
		/// <param name="textField">项的显示值对应的字段名</param>
		/// <param name="valueField">项的值对应的字段名</param>
		/// <param name="promptItem">默认项的显示文本，如"请选择种类"</param>
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
        /// 查看DropDownList控件是否得到选择（即默认项之外的项是否选中）。
        /// </summary>
        /// <param name="ddl">要查看的DropDownList控件。</param>
        /// <returns>如果默认项选中，返回false，否则返回true。</returns>
		public static bool IsDropDownListSelected(DropDownList ddl)
		{
			return IsDropDownListSelected(ddl, "");
		}

        /// <summary>
        /// 查看DropDownList控件是否得到选择（即默认项之外的项是否选中）。
        /// </summary>
        /// <param name="ddl">要查看的DropDownList控件。</param>
        /// <param name="defaultValue">DropDownList的默认项的值。</param>
        /// <returns>如果默认项选中，返回false，否则返回true。</returns>
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
        /// 定位DropDownList的一项。
        /// </summary>
        /// <param name="ddl">要定位的DropDownList。</param>
        /// <param name="searchItem">用于定位的值。</param>
		public static void LocateDropDownList(DropDownList ddl, string searchItem)
		{
			ListItem expectedItem = ddl.Items.FindByValue(searchItem);
			if(expectedItem != null)
			{
				ddl.SelectedItem.Selected = false;
				expectedItem.Selected = true;
			}
		}


		#endregion DropDownList相关方法

		#region 页面控件控制

        /// <summary>
        /// 使指定页面的所有控件不可编辑。
        /// </summary>
        /// <param name="page">指定页面。</param>
		public static void MakeCtrlUnchangeable(Page page)
		{
			foreach(Control ctrl in page.Controls)
			{
				MakeCtrlUnchangeable(ctrl);
			}
		}
		
		/// <summary>
		/// 使控件及其子控件不可编辑，使DropDownList的Enable成为false。
		/// </summary>
		public static void MakeCtrlUnchangeable(Control ctrl)
		{
			if (ctrl.Controls.Count > 0)
			{
				//CheckBoxList要直接设为Enabled=false
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

				//RadioButtonList不同于CheckBoxList，被视为一个单独控件
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

		#endregion 页面控件控制

        
	}
}
