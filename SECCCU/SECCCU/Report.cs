using System;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.NetworkInformation;
using System.Text;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace SECCCU
{
    public class Report
    {
        public void StudentReport(string cardNumber)
        {

        }

        public void SendSignInText(string phoneNumber, string name)
        {
            const string accountSid = "AC6fb9f22e315ca4639e23455b7a981c44";
            const string authToken = "268b2aceca9c6812208d9e2e45768335";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: $"Congratulations {name}, you successfully signed in.",
                from: new Twilio.Types.PhoneNumber("+447480535458"),
                to: new Twilio.Types.PhoneNumber(phoneNumber)
            );

            Console.WriteLine(message.Sid);
        }

        public void SendReport(string email)
        {
                try
                { 
                    //Mail Message
                    MailMessage mM = new MailMessage();
                    //Mail Address
                    mM.From = new MailAddress("evlhax@hotmail.com");
                    //receiver email id
                    mM.To.Add($"{email}");
                    //subject of the email
                    mM.Subject = "Attendance Report";
                    //deciding for the attachment
                    mM.Attachments.Add(new Attachment(@"csvFiles\\report.csv"));
                    //add the body of the email
                    mM.Body = "Please see attached report";
                    mM.IsBodyHtml = true;
                    //SMTP client
                    SmtpClient sC = new SmtpClient("smtp.live.com");
                    //port number for Hot mail
                    sC.Port = 25;
                    //credentials to login in to hotmail account
                    sC.Credentials = new NetworkCredential("evlhax@hotmail.com","Parsewerd1!");
                    //enabled SSL
                    sC.EnableSsl = true;
                    //Send an email
                    sC.Send(mM);
                }//end of try block
                catch (Exception ex)
                {
    
                }//end of catch

            
        }
    }
}