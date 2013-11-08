#region Comments

// 功能：管理和存储系统所有界面的控件帮助和出错信息，提供全局唯一实例。
// 作者：Anders Cui
// 日期：2007-03-26

// 最近修改：Anders Cui
// 最近修改日期：2007-03-31
// 修改内容：Check注释。

#endregion

using System;
using System.Web.UI;

namespace Andersc.CodeLib.Common.WebForm
{
	/// <summary>
	/// 在指定页面执行指定功能的Javascript脚本的工具类
	/// </summary>
	public static class ScriptHelper
	{
		#region 执行通用脚本的相关方法

        /// <summary>
        /// 执行javascript脚本的通用方法。
        /// </summary>
        /// <param name="page">要执行脚本的页面对象。</param>
        /// <param name="key">要执行脚本的键值。</param>
        /// <param name="input">要执行的脚本。</param>
		public static void ExecuteScript(Page page, string key, string input)
		{
            ExecuteScript(page, key, input, false);
		}

        /// <summary>
        /// 执行javascript脚本的通用方法。
        /// </summary>
        /// <param name="page">要执行脚本的页面对象。</param>
        /// <param name="key">要执行脚本的键值。</param>
        /// <param name="input">要执行的脚本。</param>
        /// <param name="addScriptTags">如果为true，则添加Script标签，否则不添加。</param>
        public static void ExecuteScript(Page page, string key, string input, bool addScriptTags)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), key, input, addScriptTags);
        }

		#endregion 执行通用脚本的相关方法

		#region 执行警告、确认、提示脚本的相关方法

        /// <summary>
        /// 在指定页面显示警告框。
        /// </summary>
        /// <param name="page">指定页面。</param>
        /// <param name="key">脚本键值。</param>
        /// <param name="message">消息内容。</param>
		public static void Alert(Page page, string key, string message)
		{
            ExecuteScript(page, key, ScriptLibrary.AlertMsg(message));
		}

        /// <summary>
        /// 在指定页面显示确认框。
        /// </summary>
        /// <param name="page">指定页面。</param>
        /// <param name="key">脚本键值。</param>
        /// <param name="message">消息内容。</param>
		public static void Confirm(Page page, string key, string message)
		{
            ExecuteScript(page, key, ScriptLibrary.ConfirmMsg(message));
		}

        /// <summary>
        /// 在指定页面显示提示框。
        /// </summary>
        /// <param name="page">指定页面。</param>
        /// <param name="key">脚本键值。</param>
        /// <param name="message">消息内容。</param>
		public static void Prompt(Page page, string key, string message)
		{
            ExecuteScript(page, key, ScriptLibrary.PromptMsg(message));
		}

        /// <summary>
        /// 在指定页面先显示警告框然后关闭窗体。
        /// </summary>
        /// <param name="page">指定页面。</param>
        /// <param name="key">脚本键值。</param>
        /// <param name="message">消息内容。</param>
		public static void AlertMsgThenClose(Page page, string key, string message)
		{
            ExecuteScript(page, key, ScriptLibrary.AlertMsgThenClose(message));
		}

        /// <summary>
        /// 在指定页面先显示警告框然后刷新本窗体。
        /// </summary>
        /// <param name="page">指定页面。</param>
        /// <param name="key">脚本键值。</param>
        /// <param name="message">消息内容。</param>
		public static void AlertMsgThenRefreshSelf(Page page, string key, string message)
		{
            ExecuteScript(page, key, ScriptLibrary.AlertMsgThenRefreshSelf(message));
		}

        /// <summary>
        /// 在指定页面显示警告框，刷新父窗体，然后关闭自身窗体。
        /// </summary>
        /// <param name="page">指定页面。</param>
        /// <param name="key">脚本键值。</param>
        /// <param name="message">消息内容。</param>
        public static void AlertMsgRefreshParentWindowThenCloseSelf(Page page, string key, string message)
		{
            ExecuteScript(page, key, ScriptLibrary.AlertMsgRefreshParentWindowThenCloseSelf(message));
		}

		#endregion 执行警告、确认、提示脚本的相关方法

		#region 执行页面控制的脚本相关的方法

        /// <summary>
        /// 关闭指定页面。
        /// </summary>
        /// <param name="page">指定页面。</param>
        /// <param name="key">脚本键值。</param>
		public static void CloseWindow(Page page, string key)
		{
			ExecuteScript(page, key, ScriptLibrary.CloseWindow());
		}

        /// <summary>
        /// 在指定页面打开一个新的页面。
        /// </summary>
        /// <param name="page">指定页面。</param>
        /// <param name="key">脚本键值。</param>
        /// <param name="url">新页面的url。</param>
		public static void OpenWindow(Page page, string key, string url)
		{
            ExecuteScript(page, key, ScriptLibrary.OpenPageWindow(url));
		}

        /// <summary>
        /// 刷新指定页面。
        /// </summary>
        /// <param name="page">指定页面。</param>
        /// <param name="key">脚本键值。</param>
		public static void RefreshWindow(Page page, string key)
		{
            ExecuteScript(page, key, ScriptLibrary.RefreshWindow());
		}

        /// <summary>
        /// 刷新指定页面的父页面。
        /// </summary>
        /// <param name="page">指定页面。</param>
        /// <param name="key">脚本键值。</param>
		public static void RefreshParentWindow(Page page, string key)
		{
            ExecuteScript(page, key, ScriptLibrary.RefreshParentWindow());
		}

        /// <summary>
        /// 刷新指定页面的父页面，并关闭该页面。
        /// </summary>
        /// <param name="page">指定页面</param>
        /// <param name="key">脚本键值。</param>
		public static void RefreshParentThenCloseWindow(Page page, string key)
		{
            ExecuteScript(page, key, ScriptLibrary.RefreshParentThenCloseWindow());
		}

        /// <summary>
        /// 为指定页面显示模式对话框。
        /// </summary>
        /// <param name="page">指定页面。</param>
        /// <param name="key">脚本键值。</param>
        /// <param name="url">显示在对话框的页面url。</param>
        /// <param name="height">对话框高度。</param>
        /// <param name="width">对话框宽度。</param>
        public static void ShowModalDialog(Page page, string key, string url, double height, double width)
		{
            ExecuteScript(page, key, ScriptLibrary.ShowModalDialog(url, height, width));
		}

        /// <summary>
        /// 为指定页面显示模式对话框，并转向另一个页面。
        /// </summary>
        /// <param name="page">指定页面。</param>
        /// <param name="key">脚本键值。</param>
        /// <param name="url">显示在对话框的页面url。</param>
        /// <param name="redirect">转向页面的url。</param>
        /// <param name="height">对话框高度。</param>
        /// <param name="width">对话框宽度。</param>
        public static void ShowModalDialogThenRedirect(Page page, string key, string url, string redirect, double height, double width)
		{
            ExecuteScript(page, key, ScriptLibrary.ShowModalDialogThenRedirect(url, redirect, height, width));
		}

		#endregion 执行页面控制的脚本相关的方法

		#region 执行页面跳转脚本的相关方法

        /// <summary>
        /// 跳转到指定页面的历史列表中指定位置的页面(history.go(position))。
        /// </summary>
        /// <param name="page">指定页面。</param>
        /// <param name="key">脚本键值。</param>
        /// <param name="position">历史列表中的目标页面的相对位置。</param>
		public static void GoToPage(Page page, string key, int position)
		{
            ExecuteScript(page, key, ScriptLibrary.GoToPage(position));
		}

        /// <summary>
        /// 跳转到指定页面的历史列表中指定url的页面(history.go(url))。
        /// </summary>
        /// <param name="page">指定页面。</param>
        /// <param name="key">脚本键值。</param>
        /// <param name="url">历史列表中的目标页面的url。</param>
		public static void GoToPage(Page page, string key, string url)
		{
            ExecuteScript(page, key, ScriptLibrary.GoToPage(url));
		}

        /// <summary>
        /// 在指定页面执行等价于浏览器的后退按钮的功能(history.back())。
        /// </summary>
        /// <param name="page">指定页面。</param>
        /// <param name="key">脚本键值。</param>
		public static void GoBack(Page page, string key)
		{
            ExecuteScript(page, key, ScriptLibrary.GoToPage(-1));
		}

        /// <summary>
        /// 在指定页执行等价于浏览器的前进按钮的功能(history.forward())。
        /// </summary>
        /// <param name="page">指定页面。</param>
        /// <param name="key">脚本键值。</param>
		public static void GoForward(Page page, string key)
		{
            ExecuteScript(page, key, ScriptLibrary.GoToPage(1));
		}

		#endregion 执行页面跳转脚本的相关方法
	}
}
