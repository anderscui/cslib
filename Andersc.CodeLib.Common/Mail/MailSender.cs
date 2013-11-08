using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mail;

using log4net;

namespace Andersc.CodeLib.Common.Mail
{
    public class MailSender
    {
        private string mailName;
        private string mailPassword;

        private static ILog logger = LogManager.GetLogger(typeof(MailSender));
        private static string logFormat = "mail from:{0}; mail to:{1};" + Environment.NewLine
            + "mail body:{2};";

        public MailSender(string accountName, string accountPassword)
        {
            this.mailName = accountName;
            this.mailPassword = accountPassword;
        }

        public void SendMail(string mailFrom, string mailTo, string mailSubject, string mailBody, string attachments, bool useHtmlFormat,
            string smtpServer)
        {
            MailMessage mailMessage = Authenticate(mailName, mailPassword);
            mailMessage.From = mailFrom;
            mailMessage.To = mailTo;
            mailMessage.Subject = mailSubject;
            mailMessage.Body = mailBody;
            mailMessage.BodyFormat = useHtmlFormat ? MailFormat.Html : MailFormat.Text;

            if ((attachments != null) && (attachments != ""))
            {
                foreach (string attachment in attachments.Split(','))
                {
                    MailAttachment ma = new MailAttachment(attachment);
                    mailMessage.Attachments.Add(ma);
                }
            }

            if (logger.IsDebugEnabled)
            {
                logger.DebugFormat(logFormat, mailFrom, mailTo, mailBody);
            }

            SmtpMail.SmtpServer = smtpServer;
            SmtpMail.Send(mailMessage);
        }

        private MailMessage Authenticate(string accountName, string accountPassword)
        {
            MailMessage result = new MailMessage();

            if ((accountName != "") && (accountPassword != ""))
            {
                //Need the authenticate
                result.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
                result.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", accountName);
                result.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", accountPassword);
            }
            return result;
        }
    }
}
