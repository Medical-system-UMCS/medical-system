using System;
using System.Collections.Generic;
using System.Windows;
using Newtonsoft.Json.Linq;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using WpfAppMedicalSystemsDraft.Enums;
using WpfAppMedicalSystemsDraft.Models;

namespace WpfAppMedicalSystemsDraft.Services
{
    class EmailService
    {
        private readonly TransactionalEmailsApi apiIstance;
        private readonly EmailTemplates emailTemplates;
        private readonly string senderName;
        private readonly string senderEmail;
        public EmailService(string apiKey) {
            Configuration.Default.ApiKey.Add("api-key",
            apiKey);
            apiIstance = new TransactionalEmailsApi();
            emailTemplates = new EmailTemplates();
            senderName = "Medical System MFII";
            senderEmail = "medical.system.mfii@proton.me";
        }

        public void SendEmail(string email, string name, string emailType)
        {
            SendSmtpEmailSender emailSender = new(senderName, senderEmail);
            JObject Headers = new()
            {
                { "", ""}
            };
            SendSmtpEmailTo smtpEmailTo = new(email, name);
            List<SendSmtpEmailTo> To = new()
            {
                smtpEmailTo
            };           
            Template? template = emailTemplates.GetTemplateByEmailType(emailType);
            if (template == null)
            {
                return;
            }
            string htmlContent = template.Content;
            string? TextContent = null;
            string Subject = template.Header;

            long? TemplateId = null;
            // zamienić parametry w zależności od wzorców
            JObject Params = new()
            {
                { "login", "admin" }
            };
            SendSmtpEmailTo1 smtpEmailTo1 = new(email, name);
            List<SendSmtpEmailTo1> sendList = new()
            {
                smtpEmailTo1
            };
            Dictionary<string, object> _params = new()
            {
                { "params", Params }
            };
            SendSmtpEmailMessageVersions messageVersion =
                new(sendList, _params, null, null, null, Subject);
            List<SendSmtpEmailMessageVersions> messageVersiopns = new()
            {
                messageVersion
            };
            try
            {
                var sendSmtpEmail = new SendSmtpEmail(emailSender, To, null, null, htmlContent, TextContent, Subject, null,
                    null, Headers, TemplateId, Params, messageVersiopns, null);

                CreateSmtpEmail result = apiIstance.SendTransacEmail(sendSmtpEmail);              
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

    }
}
