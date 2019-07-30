﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace ElementarySchoolProject.Utilities
{
    public static class EmailSenders
    {
        public static void TestSendEmail(string toEmail)
        {
            string subject = "TestToTempmail";
            string body = "Email body";
            string FromMail = ConfigurationManager.AppSettings["from"];
            string emailTo = toEmail;
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["smtpServer"]);
            mail.From = new MailAddress(FromMail);
            mail.To.Add(emailTo);
            mail.Subject = subject;
            mail.Body = body;            
            SmtpServer.Port = int.Parse(ConfigurationManager.AppSettings["smtpPort"]);
            SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["password"]);
            //SmtpServer.Credentials = new System.Net.NetworkCredential("d65f17fc8b19f7", "db511567a7b18c");
            SmtpServer.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["smtpSsl"]);
            SmtpServer.Send(mail);
        }
    }
}