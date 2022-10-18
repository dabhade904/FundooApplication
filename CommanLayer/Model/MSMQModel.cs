using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommanLayer.Model
{
    public class MSMQModel
    {
        MessageQueue messageQueue = new MessageQueue();
        public void sendData2Queue(string tokan)
        {
            messageQueue.Path = @".\private$\Tokan";
            if(!MessageQueue.Exists(messageQueue.Path))
            {
                // Creates the new queue named "Tokan"
                MessageQueue.Create(messageQueue.Path);
            }
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQueue.ReceiveCompleted += MessageQueue_ReceiveCompleted;
            messageQueue.Send(tokan);
            messageQueue.BeginReceive();
            messageQueue.Close();
        }
        private void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = messageQueue.EndReceive(e.AsyncResult);
                string tokan = msg.Body.ToString();
                string subject = "Fundoo Note App Reset Link";
                string body = tokan;
                var smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("dabhade904@gmail.com", "obilwizikyohjdis"),
                    EnableSsl = true
                };
                smtp.Send("dabhade904@gmail.com", "dabhade904@gmail.com", subject,body);
                messageQueue.BeginReceive();
            }
            catch(Exception )
            {
                throw ;
            }
        }
    }
}
