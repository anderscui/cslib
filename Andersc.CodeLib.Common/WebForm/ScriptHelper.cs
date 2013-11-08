#region Comments

// ���ܣ�����ʹ洢ϵͳ���н���Ŀؼ������ͳ�����Ϣ���ṩȫ��Ψһʵ����
// ���ߣ�Anders Cui
// ���ڣ�2007-03-26

// ����޸ģ�Anders Cui
// ����޸����ڣ�2007-03-31
// �޸����ݣ�Checkע�͡�

#endregion

using System;
using System.Web.UI;

namespace Andersc.CodeLib.Common.WebForm
{
	/// <summary>
	/// ��ָ��ҳ��ִ��ָ�����ܵ�Javascript�ű��Ĺ�����
	/// </summary>
	public static class ScriptHelper
	{
		#region ִ��ͨ�ýű�����ط���

        /// <summary>
        /// ִ��javascript�ű���ͨ�÷�����
        /// </summary>
        /// <param name="page">Ҫִ�нű���ҳ�����</param>
        /// <param name="key">Ҫִ�нű��ļ�ֵ��</param>
        /// <param name="input">Ҫִ�еĽű���</param>
		public static void ExecuteScript(Page page, string key, string input)
		{
            ExecuteScript(page, key, input, false);
		}

        /// <summary>
        /// ִ��javascript�ű���ͨ�÷�����
        /// </summary>
        /// <param name="page">Ҫִ�нű���ҳ�����</param>
        /// <param name="key">Ҫִ�нű��ļ�ֵ��</param>
        /// <param name="input">Ҫִ�еĽű���</param>
        /// <param name="addScriptTags">���Ϊtrue�������Script��ǩ��������ӡ�</param>
        public static void ExecuteScript(Page page, string key, string input, bool addScriptTags)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), key, input, addScriptTags);
        }

		#endregion ִ��ͨ�ýű�����ط���

		#region ִ�о��桢ȷ�ϡ���ʾ�ű�����ط���

        /// <summary>
        /// ��ָ��ҳ����ʾ�����
        /// </summary>
        /// <param name="page">ָ��ҳ�档</param>
        /// <param name="key">�ű���ֵ��</param>
        /// <param name="message">��Ϣ���ݡ�</param>
		public static void Alert(Page page, string key, string message)
		{
            ExecuteScript(page, key, ScriptLibrary.AlertMsg(message));
		}

        /// <summary>
        /// ��ָ��ҳ����ʾȷ�Ͽ�
        /// </summary>
        /// <param name="page">ָ��ҳ�档</param>
        /// <param name="key">�ű���ֵ��</param>
        /// <param name="message">��Ϣ���ݡ�</param>
		public static void Confirm(Page page, string key, string message)
		{
            ExecuteScript(page, key, ScriptLibrary.ConfirmMsg(message));
		}

        /// <summary>
        /// ��ָ��ҳ����ʾ��ʾ��
        /// </summary>
        /// <param name="page">ָ��ҳ�档</param>
        /// <param name="key">�ű���ֵ��</param>
        /// <param name="message">��Ϣ���ݡ�</param>
		public static void Prompt(Page page, string key, string message)
		{
            ExecuteScript(page, key, ScriptLibrary.PromptMsg(message));
		}

        /// <summary>
        /// ��ָ��ҳ������ʾ�����Ȼ��رմ��塣
        /// </summary>
        /// <param name="page">ָ��ҳ�档</param>
        /// <param name="key">�ű���ֵ��</param>
        /// <param name="message">��Ϣ���ݡ�</param>
		public static void AlertMsgThenClose(Page page, string key, string message)
		{
            ExecuteScript(page, key, ScriptLibrary.AlertMsgThenClose(message));
		}

        /// <summary>
        /// ��ָ��ҳ������ʾ�����Ȼ��ˢ�±����塣
        /// </summary>
        /// <param name="page">ָ��ҳ�档</param>
        /// <param name="key">�ű���ֵ��</param>
        /// <param name="message">��Ϣ���ݡ�</param>
		public static void AlertMsgThenRefreshSelf(Page page, string key, string message)
		{
            ExecuteScript(page, key, ScriptLibrary.AlertMsgThenRefreshSelf(message));
		}

        /// <summary>
        /// ��ָ��ҳ����ʾ�����ˢ�¸����壬Ȼ��ر������塣
        /// </summary>
        /// <param name="page">ָ��ҳ�档</param>
        /// <param name="key">�ű���ֵ��</param>
        /// <param name="message">��Ϣ���ݡ�</param>
        public static void AlertMsgRefreshParentWindowThenCloseSelf(Page page, string key, string message)
		{
            ExecuteScript(page, key, ScriptLibrary.AlertMsgRefreshParentWindowThenCloseSelf(message));
		}

		#endregion ִ�о��桢ȷ�ϡ���ʾ�ű�����ط���

		#region ִ��ҳ����ƵĽű���صķ���

        /// <summary>
        /// �ر�ָ��ҳ�档
        /// </summary>
        /// <param name="page">ָ��ҳ�档</param>
        /// <param name="key">�ű���ֵ��</param>
		public static void CloseWindow(Page page, string key)
		{
			ExecuteScript(page, key, ScriptLibrary.CloseWindow());
		}

        /// <summary>
        /// ��ָ��ҳ���һ���µ�ҳ�档
        /// </summary>
        /// <param name="page">ָ��ҳ�档</param>
        /// <param name="key">�ű���ֵ��</param>
        /// <param name="url">��ҳ���url��</param>
		public static void OpenWindow(Page page, string key, string url)
		{
            ExecuteScript(page, key, ScriptLibrary.OpenPageWindow(url));
		}

        /// <summary>
        /// ˢ��ָ��ҳ�档
        /// </summary>
        /// <param name="page">ָ��ҳ�档</param>
        /// <param name="key">�ű���ֵ��</param>
		public static void RefreshWindow(Page page, string key)
		{
            ExecuteScript(page, key, ScriptLibrary.RefreshWindow());
		}

        /// <summary>
        /// ˢ��ָ��ҳ��ĸ�ҳ�档
        /// </summary>
        /// <param name="page">ָ��ҳ�档</param>
        /// <param name="key">�ű���ֵ��</param>
		public static void RefreshParentWindow(Page page, string key)
		{
            ExecuteScript(page, key, ScriptLibrary.RefreshParentWindow());
		}

        /// <summary>
        /// ˢ��ָ��ҳ��ĸ�ҳ�棬���رո�ҳ�档
        /// </summary>
        /// <param name="page">ָ��ҳ��</param>
        /// <param name="key">�ű���ֵ��</param>
		public static void RefreshParentThenCloseWindow(Page page, string key)
		{
            ExecuteScript(page, key, ScriptLibrary.RefreshParentThenCloseWindow());
		}

        /// <summary>
        /// Ϊָ��ҳ����ʾģʽ�Ի���
        /// </summary>
        /// <param name="page">ָ��ҳ�档</param>
        /// <param name="key">�ű���ֵ��</param>
        /// <param name="url">��ʾ�ڶԻ����ҳ��url��</param>
        /// <param name="height">�Ի���߶ȡ�</param>
        /// <param name="width">�Ի����ȡ�</param>
        public static void ShowModalDialog(Page page, string key, string url, double height, double width)
		{
            ExecuteScript(page, key, ScriptLibrary.ShowModalDialog(url, height, width));
		}

        /// <summary>
        /// Ϊָ��ҳ����ʾģʽ�Ի��򣬲�ת����һ��ҳ�档
        /// </summary>
        /// <param name="page">ָ��ҳ�档</param>
        /// <param name="key">�ű���ֵ��</param>
        /// <param name="url">��ʾ�ڶԻ����ҳ��url��</param>
        /// <param name="redirect">ת��ҳ���url��</param>
        /// <param name="height">�Ի���߶ȡ�</param>
        /// <param name="width">�Ի����ȡ�</param>
        public static void ShowModalDialogThenRedirect(Page page, string key, string url, string redirect, double height, double width)
		{
            ExecuteScript(page, key, ScriptLibrary.ShowModalDialogThenRedirect(url, redirect, height, width));
		}

		#endregion ִ��ҳ����ƵĽű���صķ���

		#region ִ��ҳ����ת�ű�����ط���

        /// <summary>
        /// ��ת��ָ��ҳ�����ʷ�б���ָ��λ�õ�ҳ��(history.go(position))��
        /// </summary>
        /// <param name="page">ָ��ҳ�档</param>
        /// <param name="key">�ű���ֵ��</param>
        /// <param name="position">��ʷ�б��е�Ŀ��ҳ������λ�á�</param>
		public static void GoToPage(Page page, string key, int position)
		{
            ExecuteScript(page, key, ScriptLibrary.GoToPage(position));
		}

        /// <summary>
        /// ��ת��ָ��ҳ�����ʷ�б���ָ��url��ҳ��(history.go(url))��
        /// </summary>
        /// <param name="page">ָ��ҳ�档</param>
        /// <param name="key">�ű���ֵ��</param>
        /// <param name="url">��ʷ�б��е�Ŀ��ҳ���url��</param>
		public static void GoToPage(Page page, string key, string url)
		{
            ExecuteScript(page, key, ScriptLibrary.GoToPage(url));
		}

        /// <summary>
        /// ��ָ��ҳ��ִ�еȼ���������ĺ��˰�ť�Ĺ���(history.back())��
        /// </summary>
        /// <param name="page">ָ��ҳ�档</param>
        /// <param name="key">�ű���ֵ��</param>
		public static void GoBack(Page page, string key)
		{
            ExecuteScript(page, key, ScriptLibrary.GoToPage(-1));
		}

        /// <summary>
        /// ��ָ��ҳִ�еȼ����������ǰ����ť�Ĺ���(history.forward())��
        /// </summary>
        /// <param name="page">ָ��ҳ�档</param>
        /// <param name="key">�ű���ֵ��</param>
		public static void GoForward(Page page, string key)
		{
            ExecuteScript(page, key, ScriptLibrary.GoToPage(1));
		}

		#endregion ִ��ҳ����ת�ű�����ط���
	}
}
