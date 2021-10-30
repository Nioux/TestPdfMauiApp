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
            var inputFilename = "TestPdfMauiApp.Resources.feuille_de_personnage_editable.pdf";
            
            var outputFilename = Path.Combine(FileSystem.CacheDirectory, "feuille_de_personnage_modifiee.pdf");

            using (var inputStream = GetResourceStream(inputFilename))
            {
                using (var outputStream = File.Create(outputFilename))
                {
                    //ListFieldsInPdf(inputStream, outputStream);
                    ModifyFieldsInPdf(inputStream, outputStream);
                }
            }
            Launcher.OpenAsync(new OpenFileRequest
            {
                File = new ReadOnlyFile(outputFilename)
            });

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

        public void ModifyFieldsInPdf(Stream inputStream, Stream outputStream)
        {
            StampPdf(inputStream, outputStream, stamper =>
            {
                var form = stamper.AcroFields;
                var fields = form.Fields;

                form.SetField("Nom", "Legolas");
                form.SetField("Niveau", "5");
                form.SetField("Race", "Elfe");
                form.SetField("Classe", "Ranger");
                form.SetField("Alignement", "Chaotique Bon");
                form.SetField("Historique", "");
                form.SetField("Trait de personnalité",
                    "Fils de Thranduil\n\n" +
                    "Compagnon de l'Anneau\n\n" +
                    "Ami des Nains"
                    );
                form.SetField("For Valeur", "17");
                form.SetField("For MOD", "+3");
                form.SetField("Dex Valeur", "20");
                form.SetField("Dex MOD", "+5");
                form.SetField("Con Valeur", "16");
                form.SetField("Con MOD", "+3");
                form.SetField("Int Valeur", "8");
                form.SetField("Int MOD", "-1");
                form.SetField("Sag Valeur", "13");
                form.SetField("Sag MOD", "+1");
                form.SetField("Cha Valeur", "12");
                form.SetField("Cha MOD", "+1");
            });
        }

    }
}
