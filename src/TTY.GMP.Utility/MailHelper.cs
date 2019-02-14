using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using TTY.GMP.Entity.Config;

namespace TTY.GMP.Utility
{
    /// <summary>
    /// 电子邮件帮助类
    /// </summary>
    public class MailHelper
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailConfig"></param>
        /// <param name="mailInfo"></param>
        /// <returns></returns>
        public static bool Send(MailConfig mailConfig, MailInfo mailInfo)
        {
            var message = new MailMessage();
            if (!string.IsNullOrEmpty(mailInfo.AttachmentAddress))
            {
                message.Attachments.Add(new Attachment(mailInfo.AttachmentAddress));
            }
            foreach (var address in mailInfo.Recipients)
            {
                message.To.Add(new MailAddress(address));
            }
            message.From = new MailAddress(mailConfig.SenderAddress, mailConfig.SenderDisplayName);
            message.BodyEncoding = Encoding.GetEncoding(mailInfo.Encoding);
            message.Body = mailInfo.Body;
            message.SubjectEncoding = Encoding.GetEncoding(mailInfo.Encoding);
            message.Subject = mailInfo.Subject;
            message.IsBodyHtml = mailInfo.IsBodyHtml;
            var smtpclient = new SmtpClient(mailConfig.MailHost, mailConfig.MailPort.ToInt());
            smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpclient.Credentials = new System.Net.NetworkCredential(mailConfig.SenderUserName, mailConfig.SenderPassword);
            smtpclient.EnableSsl = mailInfo.EnableSsl;
            smtpclient.Send(message);
            return true;
        }
    }

    /// <summary>
    /// 邮件信息
    /// </summary>
    public class MailInfo
    {
        /// <summary>
        /// 收件人
        /// </summary>
        public string[] Recipients { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 附件地址
        /// </summary>
        public string AttachmentAddress { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Encoding = "UTF-8";

        /// <summary>
        /// 邮件消息是html格式
        /// </summary>
        public bool IsBodyHtml = true;

        /// <summary>
        /// 启用安全套接字层(SSL)
        /// </summary>
        public bool EnableSsl = true;
    }
}
