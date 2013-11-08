#region Comments

// 功能：ASP.NET应用程序的基类，提供异常处理功能。
// 作者：Anders Cui
// 日期：2007-03-16

// 最近修改：Anders Cui
// 最近修改日期：2007-03-27
// 修改内容：修改命名空间。
// TODO: 找到一种通用的方式来处理它，都使用log4net?同时要找到Console和WinForm的异常处理方式。
// 按目前这种处理方式，需要在配置文件中加上Logger的名称和错误页面地址，也可以进行重写。

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
        ///// 构造函数<see cref="HttpApplicationBase"/>。
        ///// </summary>
        //public HttpApplicationBase()
        //{
        //    this.Error += new EventHandler(HttpApplicationBase_Error);
        //}

        ///// <summary>
        ///// 处理程序中的出现的未处理异常。
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
        //    logger.Error("程序发生错误", exception);

        //    string errorPage = ConfigManager.GetAppSetting("AppErrorPage");
        //    Server.Transfer(errorPage);
        //}
    }
}
