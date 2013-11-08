#region Comments

// 功能：管理和存储系统所有界面的控件帮助和出错信息，提供全局唯一实例。
// 作者：Anders Cui
// 日期：2007-03-26

// 最近修改：Anders Cui
// 最近修改日期：2007-03-31
// 修改内容：Check注释。

#endregion

using System;
using System.Text;
using System.Web.UI;

namespace Andersc.CodeLib.Common.WebForm
{
	/// <summary>
	/// 返回Javascript脚本的通用工具类
	/// 但不会执行脚本
	/// 要执行脚本，请使用PageScriptUtil类中的方法。
	/// </summary>
	public static class ScriptLibrary
	{
        private static string scriptBlockFormat = "<script language='javascript'>{0}</script>";

		#region 通用脚本相关的方法

		/// <summary>
		/// 获取javascript脚本的通用方法
		/// </summary>
		/// <param name="input">输入字符串</param>
		/// <returns></returns>
		public static string GetScriptString(string input)
		{
            return string.Format(scriptBlockFormat, input);
		}

		#endregion 通用脚本相关的方法

		#region 警告、确认、提示脚本相关的方法

		/// <summary>
		/// 返回警告框消息的javascript脚本块。
		/// </summary>
		/// <param name="message">消息内容。</param>
        /// <returns>包含警告消息的完整javascript脚本块。</returns>
		public static string AlertMsg(string message)
		{
            string format = string.Format(scriptBlockFormat, "alert('{0}');");
			return string.Format(format, message);
		}

		/// <summary>
        /// 返回包含确认框消息的javascript脚本块。
		/// </summary>
		/// <param name="message">消息内容。</param>
        /// <returns>包含确认框消息的javascript脚本块。</returns>
        /// <remarks>TODO：如何取得返回值。</remarks>
		public static string ConfirmMsg(string message)
		{
            string format = string.Format(scriptBlockFormat, "confirm('{0}');");
			return string.Format(format, message);
		}

		/// <summary>
		/// 返回先显示警告框然后关闭窗体的javascript脚本。
		/// </summary>
		/// <param name="message">消息内容。</param>
        /// <returns>包含先显示警告框然后关闭窗体的javascript脚本。</returns>
		public static string AlertMsgThenClose(string message)
		{
            string format = string.Format(scriptBlockFormat, "alert('{0}');window.close();");
            return string.Format(format, message);
		}

		/// <summary>
		/// 返回先显示警告框然后刷新本窗体的javascript脚本。
		/// </summary>
		/// <param name="message">消息内容。</param>
        /// <returns>包含先显示警告框然后刷新本窗体的javascript脚本。</returns>
		public static string AlertMsgThenRefreshSelf(string message)
		{
            string format = string.Format(scriptBlockFormat, "alert('{0}');window.location=window.location.href;");
            return string.Format(format, message);
		}

		/// <summary>
		/// 返回显示警告框，刷新父窗体，然后关闭自身窗体的javascript脚本。
		/// </summary>
        /// <param name="message">消息内容。</param>
        /// <returns>javascript脚本。</returns>
        public static string AlertMsgRefreshParentWindowThenCloseSelf(string message)
		{
            string format = string.Format(scriptBlockFormat, "alert('{0}');window.opener.location=window.opener.location.href;window.close();");
            return string.Format(format, message);
		}

		/// <summary>
		/// 返回显示提示框 的javascript脚本。
		/// </summary>
        /// <param name="message">消息内容。</param>
        /// <returns>相应的javascript脚本。</returns>
        /// <remarks>TODO：如何取得返回值。</remarks>
        public static string PromptMsg(string message)
		{
            string format = string.Format(scriptBlockFormat, "window.prompt('{0}');");
            return string.Format(format, message);
		}

		#endregion 警告、确认脚本相关的方法

		#region 页面控制的脚本相关的方法

        private static string OpenPageWindow(string url, string windowName, int width, int height, int xPos, int yPos)
        {
            StringBuilder script = new StringBuilder();
            string paras = "resizable,left=" + xPos.ToString() + ",top=" + yPos.ToString() + ",screenX=" + xPos.ToString() + ",screenY=" + yPos.ToString() + ",scrollbars=yes,width=" + width.ToString() + ",height=" + height.ToString();
            script.AppendLine("var win;");
            script.AppendFormat("win=window.open('{0}','{1}','{2}');win.focus();", url, windowName, paras);

            return script.ToString();
        }

		/// <summary>
		/// 返回弹出新页面的javascript脚本。
		/// </summary>
        /// <param name="url">新页面的url。</param>
        /// <returns>相应的javascript脚本。</returns>
        /// <remarks>TODO：好像不能将新页面前置，还是调用js代码。另：提供更多的重载方法。</remarks>
		public static string OpenPageWindow(string url)
		{
            return string.Format(scriptBlockFormat, OpenPageWindow(url, "subWindow", 800, 600, 200, 200));
		}

		/// <summary>
		/// 返回关闭当前页面的javascript脚本。
		/// </summary>
        /// <returns>关闭当前页面的javascript脚本。</returns>
		public static string CloseWindow()
		{
            return string.Format(scriptBlockFormat, "window.close();");
		}

		/// <summary>
		/// 返回刷新当前页面的javascript脚本。
		/// </summary>
        /// <returns>刷新当前页面的javascript脚本。</returns>
		public static string RefreshWindow()
		{
			return "<script language='javascript'>window.location=window.location.href;</script>";
		}

		/// <summary>
        /// 获取刷新父窗体的javascript脚本。
		/// </summary>
        /// <returns>刷新父窗体的javascript脚本。</returns>
		public static string RefreshParentWindow()
		{
			return "<script language='javascript'>window.opener.location=window.opener.location.href;</script>";
		}

        /// <summary>
        /// 返回刷新父窗口，并关闭当前窗口的javascript脚本。
        /// </summary>
        /// <returns>刷新父窗口，并关闭当前窗口的javascript脚本。</returns>
		public static string RefreshParentThenCloseWindow()
		{
			return "<script language='javascript'>window.opener.location=window.opener.location.href;window.close();</script>";
		}

		/// <summary>
		/// 获取弹出模式对话框web页面的javascript脚本。
		/// </summary>
        /// <param name="url">弹出页面的url。</param>
        /// <param name="height">弹出页面的高度。</param>
        /// <param name="width">弹出页面的宽度。</param>
        /// <returns>弹出模式对话框web页面的javascript脚本。</returns>
        /// <remarks>TODO：大小如何控制？</remarks>
		public static string ShowModalDialog(string url, double height, double width)
		{
            string format = string.Format(scriptBlockFormat, "window.showModalDialog('{0}', '_blank', 'dialogHeight={1};dialogWidth={2}');");
            return string.Format(format, url, height, width);
		}

		/// <summary>
		/// 获取弹出模式对话框web页面，关闭后重定向至另一页面的javascript脚本。
		/// </summary>
        /// <param name="url">弹出页面的url。</param>
        /// <param name="redirect">弹出框后转到的url。</param>
        /// <param name="height">弹出页面的高度。</param>
        /// <param name="width">弹出页面的宽度。</param>
        /// <returns>含script标记的javascript脚本。</returns>
		public static string ShowModalDialogThenRedirect(string url, string redirect, double height, double width)
		{
            string format = string.Format(scriptBlockFormat, "window.showModalDialog('{0}', '_blank', 'dialogHeight={1};dialogWidth={2}');window.navigate ('{3}');");
            return string.Format(format, url, height, width, redirect);
		}

		#endregion 页面控制的脚本相关的方法

		#region 页面跳转脚本相关的方法

		/// <summary>
		/// 获取跳转到指定页面的javascript脚本。
		/// </summary>
		/// <param name="position">目标页面在历史列表中的相对位置。</param>
		/// <returns></returns>
        /// <remarks>TODO：当前还不可用！</remarks>
		public static string GoToPage(int position)
		{
            string format = GetScriptString("window.history.go({0});");
            return string.Format(format, position);
		}

		/// <summary>
		/// 返回 跳转到指定页面 的javascript脚本
		/// </summary>
		/// <param name="url">目标页面在历史列表中的确切url</param>
		/// <returns></returns>
		public static string GoToPage(string url)
		{
            string format = GetScriptString("window.history.go('{0}');");
            return string.Format(format, url);
		}

		#endregion 页面跳转脚本相关的方法
	}
}
