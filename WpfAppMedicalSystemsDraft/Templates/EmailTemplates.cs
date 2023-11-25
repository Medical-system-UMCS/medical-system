using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppMedicalSystemsDraft.Enums;
using WpfAppMedicalSystemsDraft.Models;

namespace WpfAppMedicalSystemsDraft.Services
{
    class EmailTemplates
    {
        private Dictionary<string, Template> emailTemplates = new Dictionary<string, Template>();

        public EmailTemplates()
        {
            emailTemplates.Add(EmailType.ACCOUNT_CONFIRMATION, new Template
            {
                Header = "Potwierdzenie konta",
                Content = @"<html>
                            <body>
                                <p>Dzień dobry!</p>
                                <p>Konto '{{params.login}}' zostało potwierdzone. Teraz możesz korzystać z aplikacji</p>
                                <p>Z poważaniem,<br>
                                Administrator systemu medycznego MFII</p>                
                            </body>
                        </html>"
            });

            // Dodanie kolejnych wzorców
        }

        public Template? GetTemplateByEmailType(string emailType) => emailTemplates.ContainsKey(emailType) ? emailTemplates[emailType] : null;

    }
}
