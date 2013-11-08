#region Comments

// ���ܣ�����ʹ洢ϵͳ���н���Ŀؼ������ͳ�����Ϣ���ṩȫ��Ψһʵ����
// ���ߣ�Anders Cui
// ���ڣ�2007-03-26

// ����޸ģ�Anders Cui
// ����޸����ڣ�2007-03-31
// �޸����ݣ�Checkע�͡�

#endregion

using System;
using System.Text;
using System.Web.UI;

namespace Andersc.CodeLib.Common.WebForm
{
	/// <summary>
	/// ����Javascript�ű���ͨ�ù�����
	/// ������ִ�нű�
	/// Ҫִ�нű�����ʹ��PageScriptUtil���еķ�����
	/// </summary>
	public static class ScriptLibrary
	{
        private static string scriptBlockFormat = "<script language='javascript'>{0}</script>";

		#region ͨ�ýű���صķ���

		/// <summary>
		/// ��ȡjavascript�ű���ͨ�÷���
		/// </summary>
		/// <param name="input">�����ַ���</param>
		/// <returns></returns>
		public static string GetScriptString(string input)
		{
            return string.Format(scriptBlockFormat, input);
		}

		#endregion ͨ�ýű���صķ���

		#region ���桢ȷ�ϡ���ʾ�ű���صķ���

		/// <summary>
		/// ���ؾ������Ϣ��javascript�ű��顣
		/// </summary>
		/// <param name="message">��Ϣ���ݡ�</param>
        /// <returns>����������Ϣ������javascript�ű��顣</returns>
		public static string AlertMsg(string message)
		{
            string format = string.Format(scriptBlockFormat, "alert('{0}');");
			return string.Format(format, message);
		}

		/// <summary>
        /// ���ذ���ȷ�Ͽ���Ϣ��javascript�ű��顣
		/// </summary>
		/// <param name="message">��Ϣ���ݡ�</param>
        /// <returns>����ȷ�Ͽ���Ϣ��javascript�ű��顣</returns>
        /// <remarks>TODO�����ȡ�÷���ֵ��</remarks>
		public static string ConfirmMsg(string message)
		{
            string format = string.Format(scriptBlockFormat, "confirm('{0}');");
			return string.Format(format, message);
		}

		/// <summary>
		/// ��������ʾ�����Ȼ��رմ����javascript�ű���
		/// </summary>
		/// <param name="message">��Ϣ���ݡ�</param>
        /// <returns>��������ʾ�����Ȼ��رմ����javascript�ű���</returns>
		public static string AlertMsgThenClose(string message)
		{
            string format = string.Format(scriptBlockFormat, "alert('{0}');window.close();");
            return string.Format(format, message);
		}

		/// <summary>
		/// ��������ʾ�����Ȼ��ˢ�±������javascript�ű���
		/// </summary>
		/// <param name="message">��Ϣ���ݡ�</param>
        /// <returns>��������ʾ�����Ȼ��ˢ�±������javascript�ű���</returns>
		public static string AlertMsgThenRefreshSelf(string message)
		{
            string format = string.Format(scriptBlockFormat, "alert('{0}');window.location=window.location.href;");
            return string.Format(format, message);
		}

		/// <summary>
		/// ������ʾ�����ˢ�¸����壬Ȼ��ر��������javascript�ű���
		/// </summary>
        /// <param name="message">��Ϣ���ݡ�</param>
        /// <returns>javascript�ű���</returns>
        public static string AlertMsgRefreshParentWindowThenCloseSelf(string message)
		{
            string format = string.Format(scriptBlockFormat, "alert('{0}');window.opener.location=window.opener.location.href;window.close();");
            return string.Format(format, message);
		}

		/// <summary>
		/// ������ʾ��ʾ�� ��javascript�ű���
		/// </summary>
        /// <param name="message">��Ϣ���ݡ�</param>
        /// <returns>��Ӧ��javascript�ű���</returns>
        /// <remarks>TODO�����ȡ�÷���ֵ��</remarks>
        public static string PromptMsg(string message)
		{
            string format = string.Format(scriptBlockFormat, "window.prompt('{0}');");
            return string.Format(format, message);
		}

		#endregion ���桢ȷ�Ͻű���صķ���

		#region ҳ����ƵĽű���صķ���

        private static string OpenPageWindow(string url, string windowName, int width, int height, int xPos, int yPos)
        {
            StringBuilder script = new StringBuilder();
            string paras = "resizable,left=" + xPos.ToString() + ",top=" + yPos.ToString() + ",screenX=" + xPos.ToString() + ",screenY=" + yPos.ToString() + ",scrollbars=yes,width=" + width.ToString() + ",height=" + height.ToString();
            script.AppendLine("var win;");
            script.AppendFormat("win=window.open('{0}','{1}','{2}');win.focus();", url, windowName, paras);

            return script.ToString();
        }

		/// <summary>
		/// ���ص�����ҳ���javascript�ű���
		/// </summary>
        /// <param name="url">��ҳ���url��</param>
        /// <returns>��Ӧ��javascript�ű���</returns>
        /// <remarks>TODO�������ܽ���ҳ��ǰ�ã����ǵ���js���롣���ṩ��������ط�����</remarks>
		public static string OpenPageWindow(string url)
		{
            return string.Format(scriptBlockFormat, OpenPageWindow(url, "subWindow", 800, 600, 200, 200));
		}

		/// <summary>
		/// ���عرյ�ǰҳ���javascript�ű���
		/// </summary>
        /// <returns>�رյ�ǰҳ���javascript�ű���</returns>
		public static string CloseWindow()
		{
            return string.Format(scriptBlockFormat, "window.close();");
		}

		/// <summary>
		/// ����ˢ�µ�ǰҳ���javascript�ű���
		/// </summary>
        /// <returns>ˢ�µ�ǰҳ���javascript�ű���</returns>
		public static string RefreshWindow()
		{
			return "<script language='javascript'>window.location=window.location.href;</script>";
		}

		/// <summary>
        /// ��ȡˢ�¸������javascript�ű���
		/// </summary>
        /// <returns>ˢ�¸������javascript�ű���</returns>
		public static string RefreshParentWindow()
		{
			return "<script language='javascript'>window.opener.location=window.opener.location.href;</script>";
		}

        /// <summary>
        /// ����ˢ�¸����ڣ����رյ�ǰ���ڵ�javascript�ű���
        /// </summary>
        /// <returns>ˢ�¸����ڣ����رյ�ǰ���ڵ�javascript�ű���</returns>
		public static string RefreshParentThenCloseWindow()
		{
			return "<script language='javascript'>window.opener.location=window.opener.location.href;window.close();</script>";
		}

		/// <summary>
		/// ��ȡ����ģʽ�Ի���webҳ���javascript�ű���
		/// </summary>
        /// <param name="url">����ҳ���url��</param>
        /// <param name="height">����ҳ��ĸ߶ȡ�</param>
        /// <param name="width">����ҳ��Ŀ�ȡ�</param>
        /// <returns>����ģʽ�Ի���webҳ���javascript�ű���</returns>
        /// <remarks>TODO����С��ο��ƣ�</remarks>
		public static string ShowModalDialog(string url, double height, double width)
		{
            string format = string.Format(scriptBlockFormat, "window.showModalDialog('{0}', '_blank', 'dialogHeight={1};dialogWidth={2}');");
            return string.Format(format, url, height, width);
		}

		/// <summary>
		/// ��ȡ����ģʽ�Ի���webҳ�棬�رպ��ض�������һҳ���javascript�ű���
		/// </summary>
        /// <param name="url">����ҳ���url��</param>
        /// <param name="redirect">�������ת����url��</param>
        /// <param name="height">����ҳ��ĸ߶ȡ�</param>
        /// <param name="width">����ҳ��Ŀ�ȡ�</param>
        /// <returns>��script��ǵ�javascript�ű���</returns>
		public static string ShowModalDialogThenRedirect(string url, string redirect, double height, double width)
		{
            string format = string.Format(scriptBlockFormat, "window.showModalDialog('{0}', '_blank', 'dialogHeight={1};dialogWidth={2}');window.navigate ('{3}');");
            return string.Format(format, url, height, width, redirect);
		}

		#endregion ҳ����ƵĽű���صķ���

		#region ҳ����ת�ű���صķ���

		/// <summary>
		/// ��ȡ��ת��ָ��ҳ���javascript�ű���
		/// </summary>
		/// <param name="position">Ŀ��ҳ������ʷ�б��е����λ�á�</param>
		/// <returns></returns>
        /// <remarks>TODO����ǰ�������ã�</remarks>
		public static string GoToPage(int position)
		{
            string format = GetScriptString("window.history.go({0});");
            return string.Format(format, position);
		}

		/// <summary>
		/// ���� ��ת��ָ��ҳ�� ��javascript�ű�
		/// </summary>
		/// <param name="url">Ŀ��ҳ������ʷ�б��е�ȷ��url</param>
		/// <returns></returns>
		public static string GoToPage(string url)
		{
            string format = GetScriptString("window.history.go('{0}');");
            return string.Format(format, url);
		}

		#endregion ҳ����ת�ű���صķ���
	}
}
