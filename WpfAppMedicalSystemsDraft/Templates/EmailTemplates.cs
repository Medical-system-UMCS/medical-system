﻿using Newtonsoft.Json.Linq;
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
                        </html>",
                Params = new()
                {
                    { ParamType.LOGIN, "" },
                    { ParamType.FULL_NAME, "" }
                }
            });

            emailTemplates.Add(EmailType.DOCTOR_REGISTRATION, new Template
            {
                Header = "Rejestracja konta lekarza",
                Content = @"<html>
                            <body>
                                <p>Dzień dobry!</p>
                                <p>Konto '{{params.login}}' zostało zarejestrowane. Czekaj na potwierdzenie, aby korzystać z aplikacji.
                                <p>Z poważaniem,<br>
                                Administrator systemu medycznego MFII</p>                
                            </body>
                        </html>",
                Params = new()
                {
                    { ParamType.LOGIN, "" },
                    { ParamType.FULL_NAME, "" }
                }
            });


            emailTemplates.Add(EmailType.NEW_APPOINTMENT_CONFIRMATION, new Template
            {
                Header = "Potwierdzenie wizyty",
                Content = @"<html>
                            <body>
                                <p>Dzień dobry!</p>
                                <p>Umówiona wizyta została potwierdzona. Data i czas: {{params.data}}, lekarz: {{params.doctor_name}}, gabinet: {{params.examin_room}}.</p>                                
                                <p>Z poważaniem,<br>
                                Administrator systemu medycznego MFII</p>                
                            </body>
                        </html>",
                Params = new()
                {                    
                    { ParamType.DATA, "" },
                    { ParamType.DOCTOR_NAME, "" },
                    { ParamType.EXAMIN_ROOM, "" }
                }
            });

            emailTemplates.Add(EmailType.EXAMINATION_RESULT, new Template
            {
                Header = "Wyniki badań",
                Content = @"<html>
                            <body>
                                <p>Dzień dobry!</p>
                                <p>Został dodany wynik badania {{params.data}}. Możesz zobaczyć go w aplikacji.</p>                                			
                                <p>Z poważaniem,<br>
                                Administrator systemu medycznego MFII</p>                
                            </body>
                        </html>",
                Params = new()
                {
                    { ParamType.DATA, ""}
                }
            });

            emailTemplates.Add(EmailType.NEW_APPOINTMENT_DOCTOR, new Template
            {
                Header = "Nowa wizyta",
                Content = @"<html>
                            <body>
                                <p>Dzień dobry!</p>
                                <p>Nowa wizyta została umówiona. Data i czas: {{params.data}}, pacjent: {{params.patient_name}}, gabinet: {{params.examin_room}}.</p>                                
                                <p>Z poważaniem,<br>
                                Administrator systemu medycznego MFII</p>                
                            </body>
                        </html>",
                Params = new()
                {
                    { ParamType.DATA, "" },
                    { ParamType.PATIENT_NAME, "" },
                    { ParamType.EXAMIN_ROOM, "" }
                }
            });
            // Dodanie kolejnych wzorców
        }

        public Template? GetTemplateByEmailType(string emailType) => emailTemplates.ContainsKey(emailType) ? emailTemplates[emailType] : null;

    }
}
