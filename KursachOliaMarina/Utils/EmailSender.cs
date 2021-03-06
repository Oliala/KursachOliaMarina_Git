﻿using System;
using System.Net;
using System.Net.Mail;

namespace KursachOliaMarina.Utils
{
    public struct EmailSource
    {
        public string host;
        public Int32 port;
        public string address;
        public string password;
        public string displayName;
        public EmailSource(string host, Int32 port, string address, string password, string displayName)
        {
            this.host = host;
            this.port = port;
            this.address = address;
            this.password = password;
            this.displayName = displayName;
        }
    }

    public class EmailSender
    {
        private string to = null;
        //private string user = null;
        private string DateOfMenu = null;

        private static string defautEmailHost = "smtp.gmail.com";
        private static Int32 defautEmailPort = 587;
        private static string defautEmaiFrom = "sssr.stolovaya@gmail.com";
        private static string defautEmailPassword = "canteen_123";
        private static string defautEmailDisplayName = "Canteen";

        private EmailSource source;

        private string subject = null;
        private string body = null;

        private string defaultSubject = "У нас обновилось меню!";
        private string defaultBody;

        //public EmailSender(string to, string user, string orderDatetime)
        //{
        //    this.to = to;
        //    this.user = user;
        //    this.orderDatetime = orderDatetime;
        //    source = new EmailSource(defautEmailHost, defautEmailPort, defautEmaiFrom, defautEmailPassword, defautEmailDisplayName);
        //    defaultBody = createDefaultBody(user, orderDatetime);
        //}
        public EmailSender(string to, string DateOfMenu)
        {
            this.to = to;
            //this.user = user;
            this.DateOfMenu = DateOfMenu;
            source = new EmailSource(defautEmailHost, defautEmailPort, defautEmaiFrom, defautEmailPassword, defautEmailDisplayName);
            defaultBody = createDefaultBody(DateOfMenu);
        }

        public EmailSender setSource(EmailSource source)
        {
            this.source = source;
            return this;
        }

        public EmailSender setSubject(string subject)
        {
            this.subject = subject;
            return this;
        }

        public EmailSender setBody(string body)
        {
            this.body = body;
            return this;
        }

        public bool send()
        {
            SmtpClient client = new SmtpClient(source.host, source.port);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = true;
            client.Credentials = new NetworkCredential(source.address, source.password);
            // Specify the e-mail sender.
            MailAddress from = new MailAddress(source.address,
               source.displayName, System.Text.Encoding.UTF8);
            // Set destinations for the e-mail message.
            MailAddress sendto = new MailAddress(this.to);

            // Specify the message content.
            MailMessage message = new MailMessage(from, sendto);
            if (subject == null)
                message.Subject = defaultSubject ;
            else
                message.Subject = subject;
            message.SubjectEncoding = System.Text.Encoding.UTF8;

            if (body == null)
                message.Body = defaultBody;
            else
                message.Body = body;
            message.BodyEncoding = System.Text.Encoding.UTF8;

            // Set the method that is called back when the send operation ends.
            /*client.SendCompleted += new
            SendCompletedEventHandler(SendCompletedCallback);*/
            // The userState can be any object that allows your callback 
            // method to identify this send operation.

            client.Send(message);

            message.Dispose();
            return true;
        }

        private static string createDefaultBody( string DateOfMenu)
        {
            string body = "Здравствуйте! Проголодались?" +
                "\n\n У нас обновилось меню на  "+ DateOfMenu +" так что предлагаем посетить наш веб-сайт!" +
                "\n Ждем Вас в нашей столовой! :)";
            return body;

        }
    }
}