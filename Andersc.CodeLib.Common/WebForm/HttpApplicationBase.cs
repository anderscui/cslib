#region Comments

// ���ܣ�ASP.NETӦ�ó���Ļ��࣬�ṩ�쳣�����ܡ�
// ���ߣ�Anders Cui
// ���ڣ�2007-03-16

// ����޸ģ�Anders Cui
// ����޸����ڣ�2007-03-27
// �޸����ݣ��޸������ռ䡣
// TODO: �ҵ�һ��ͨ�õķ�ʽ������������ʹ��log4net?ͬʱҪ�ҵ�Console��WinForm���쳣����ʽ��
// ��Ŀǰ���ִ���ʽ����Ҫ�������ļ��м���Logger�����ƺʹ���ҳ���ַ��Ҳ���Խ�����д��

#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Andersc.CodeLib.Common.WebForm
{
    /// <summary>
    /// Custom HttpApplication subclass.
    /// TODO:
    /// </summary>
    public class HttpApplicationBase : HttpApplication
    {
        ///// <summary>
        ///// ���캯��<see cref="HttpApplicationBase"/>��
        ///// </summary>
        //public HttpApplicationBase()
        //{
        //    this.Error += new EventHandler(HttpApplicationBase_Error);
        //}

        ///// <summary>
        ///// ��������еĳ��ֵ�δ�����쳣��
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        //protected void HttpApplicationBase_Error(object sender, EventArgs e)
        //{
        //    string errorMessage = string.Empty;

        //    Exception exception = Server.GetLastError().InnerException;
        //    while (exception.InnerException != null)
        //    {
        //        exception = exception.InnerException;
        //    }

        //    string loggerName = ConfigManager.GetAppSetting("AppErrorLoggerName");
        //    Logger logger = LoggerManager.GetLogger(loggerName);
        //    logger.Error("����������", exception);

        //    string errorPage = ConfigManager.GetAppSetting("AppErrorPage");
        //    Server.Transfer(errorPage);
        //}
    }
}
