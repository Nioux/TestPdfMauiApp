using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using iTextSharp.text.pdf;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;

namespace TestPdfMauiApp
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private void OnButtonClicked(object sender, EventArgs e)
		{
            // https://www.formulaires.service-public.fr/gf/cerfa_13406_07.do
            var inputFilename = "TestPdfMauiApp.Resources.cerfa_13406-07.pdf";
            var outputFilename = Path.Combine(FileSystem.CacheDirectory, "pdf\\modified.pdf");

            using (var inputStream = GetResourceStream(inputFilename))
            {
                using (var outputStream = File.Create(outputFilename))
                {
                    ListFieldsInPdf(inputStream, outputStream);
                }
            }
            /*Launcher.OpenAsync(new OpenFileRequest
            {
                File = new ReadOnlyFile(outputFilename)
            });*/

            PdfView.Uri = "modified.pdf"; //outputFilename;
            //PdfView.Source = new Uri(string.Format("ms-appdata:///localcache/pdf/pdfjs/web/viewer.html?file=../../{0}",
            //            "modified.pdf"));
            //PdfView.Source = new Uri("ms-appdata:///localcache/pdf/pdfjs/web/viewer.html");
            //Debug.WriteLine(PdfView.Width);
        }

        public static Stream GetResourceStream(string resourceName)
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            return assembly.GetManifestResourceStream(resourceName);
        }

        public void StampPdf(Stream inputStream, Stream outputStream, Action<PdfStamper> stamp)
        {
            PdfReader reader = null;
            try
            {
                reader = new PdfReader(inputStream);
                PdfStamper stamper = null;
                try
                {
                    stamper = new PdfStamper(reader, outputStream);
                    stamp(stamper);
                }
                finally
                {
                    stamper?.Close();
                }
            }
            finally
            {
                reader?.Close();
            }
        }
        public void ListFieldsInPdf(Stream inputStream, Stream outputStream)
        {
            StampPdf(inputStream, outputStream, stamper =>
            {
                var form = stamper.AcroFields;
                var fields = form.Fields;
#if DEBUG
                foreach (DictionaryEntry field in fields)
                {
                    var item = field.Value as AcroFields.Item;
                    Debug.WriteLine(field.Key);
                    form.SetField(field.Key.ToString(), field.Key.ToString());
                }
#endif // DEBUG
            });
        }

        public void CommentFieldsInPdf(Stream inputStream, Stream outputStream)
        {
            StampPdf(inputStream, outputStream, stamper =>
            {
                var form = stamper.AcroFields;
                var fields = form.Fields;

                /*form.SetField("Nom", SelectedPlayerCharacter?.Name ?? string.Empty);
                form.SetField("Niveau", "1");
                form.SetField("Race", SelectedPlayerCharacter?.Race?.Name ?? string.Empty);
                form.SetField("Classe", SelectedPlayerCharacter?.Class?.Name ?? string.Empty);
                form.SetField("Alignement", SelectedPlayerCharacter?.Alignment?.Name ?? string.Empty);
                form.SetField("Historique", SelectedPlayerCharacter?.Background?.Background?.Name ?? string.Empty);
                form.SetField("Trait de personnalité",
                    (SelectedPlayerCharacter.Background.PersonalityTrait ?? string.Empty) + "\n\n" +
                    (SelectedPlayerCharacter.Background.PersonalityIdeal ?? string.Empty) + "\n\n" +
                    (SelectedPlayerCharacter.Background.PersonalityLink ?? string.Empty) + "\n\n" +
                    (SelectedPlayerCharacter.Background.PersonalityDefect ?? string.Empty)
                    );
                form.SetField("For Valeur", SelectedPlayerCharacter?.Abilities?.Strength?.Value?.ToString());
                form.SetField("For MOD", SelectedPlayerCharacter?.Abilities?.Strength?.ModString);
                form.SetField("Dex Valeur", SelectedPlayerCharacter?.Abilities?.Dexterity?.Value?.ToString());
                form.SetField("Dex MOD", SelectedPlayerCharacter?.Abilities?.Dexterity?.ModString);
                form.SetField("Con Valeur", SelectedPlayerCharacter?.Abilities?.Constitution?.Value?.ToString());
                form.SetField("Con MOD", SelectedPlayerCharacter?.Abilities?.Constitution?.ModString);
                form.SetField("Int Valeur", SelectedPlayerCharacter?.Abilities?.Intelligence?.Value?.ToString());
                form.SetField("Int MOD", SelectedPlayerCharacter?.Abilities?.Intelligence?.ModString);
                form.SetField("Sag Valeur", SelectedPlayerCharacter?.Abilities?.Wisdom?.Value?.ToString());
                form.SetField("Sag MOD", SelectedPlayerCharacter?.Abilities?.Wisdom?.ModString);
                form.SetField("Cha Valeur", SelectedPlayerCharacter?.Abilities?.Charisma?.Value?.ToString());
                form.SetField("Cha MOD", SelectedPlayerCharacter?.Abilities?.Charisma?.ModString);*/
            });
        }

    }
}
