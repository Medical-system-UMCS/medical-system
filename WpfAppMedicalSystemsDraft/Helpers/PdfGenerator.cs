using System;
using System.Windows;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Kernel.Font;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Font.Constants;
using WpfAppMedicalSystemsDraft.Models;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace WpfAppMedicalSystemsDraft.Helpers
{
    public class PdfGenerator
    {
        public void GeneratePdf(Patient currentPatient, Doctor doctorData, string date, Examination examinationData)
        {

            string dateNoSpaces = date.Replace(" ", "");
            dateNoSpaces = dateNoSpaces.Substring(0, dateNoSpaces.Length - 8);
            string filename = "WynikiBadań_" + dateNoSpaces + ".pdf";
            var folderDialog = new CommonOpenFileDialog();
            folderDialog.IsFolderPicker = true;
            CommonFileDialogResult commonOpenFileDialogResult = folderDialog.ShowDialog();
            if (commonOpenFileDialogResult.Equals(CommonFileDialogResult.Cancel))
            {
                return;
            }            
            filename = string.Join('/', folderDialog.FileName, filename);
            MessageBox.Show(filename);
            PdfFont fontArial = PdfFontFactory.CreateFont(@"C:\Windows\Fonts\Arial.ttf");
            PdfFont fontArialBold = PdfFontFactory.CreateFont(@"C:\Windows\Fonts\Arialbd.ttf");


            using (PdfWriter writer = new PdfWriter(filename))
            {
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    Document document = new Document(pdf);

                    document.SetFont(fontArial);


                    Paragraph headline = new Paragraph("WYNIKI BADAŃ").SetFontSize(20).SetFont(fontArial);
                    headline.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    headline.SetMarginTop(40);
                    headline.SetMarginBottom(80);
                    document.Add(headline);

                    Paragraph patientParagraph = new Paragraph().Add(new Text("Pacjent: ").SetFont(fontArialBold));
                    patientParagraph.Add(currentPatient.FirstName + " " + currentPatient.LastName);
                    document.Add(patientParagraph);
                    document.Add(new Paragraph());

                    Paragraph doctorParagraph = new Paragraph().Add(new Text("Lekarz: ").SetFont(fontArialBold));
                    doctorParagraph.Add(doctorData.FirstName + " " + doctorData.LastName);
                    document.Add(doctorParagraph);
                    document.Add(new Paragraph());

                    Paragraph dateParagraph = new Paragraph().Add(new Text("Data i godzina wizyty: ").SetFont(fontArialBold));
                    dateParagraph.Add(date);
                    document.Add(dateParagraph);
                    document.Add(new Paragraph());

                    Paragraph symptomsParagraph = new Paragraph().Add(new Text("Objawy: ").SetFont(fontArialBold));
                    symptomsParagraph.Add(examinationData.Symptoms);
                    document.Add(symptomsParagraph);
                    document.Add(new Paragraph());

                    Paragraph diagnosisParagraph = new Paragraph().Add(new Text("Diagnoza: ").SetFont(fontArialBold));
                    diagnosisParagraph.Add(examinationData.Diagnosis);
                    document.Add(diagnosisParagraph);
                    document.Add(new Paragraph());

                    Paragraph treatmentParagraph = new Paragraph().Add(new Text("Leczenie: ").SetFont(fontArialBold));
                    treatmentParagraph.Add(examinationData.Treatment);
                    document.Add(treatmentParagraph);
                    document.Add(new Paragraph());


                    document.Close();
                }
            }
        }
    }
}
