using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Foundation;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Essentials;
using UIKit;

namespace TestPdfMauiApp
{
    public class PdfViewRenderer : ViewRenderer<PdfView, UIWebView>
    {
        protected override async void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if(e.PropertyName == "Uri")
            {
                LoadPdfJS();
                var pdfView = Element as PdfView;

                if (pdfView?.Uri != null)
                {
                    await Task.Delay(1000);
                    LoadFile(pdfView.Uri);
                }
            }
        }
        protected override async void OnElementChanged(ElementChangedEventArgs<PdfView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                // Cleanup
            }
            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    SetNativeControl(new UIWebView());
                }
                LoadPdfJS();
                var pdfView = Element as PdfView;

                if (pdfView?.Uri != null)
                {
                    await Task.Delay(1000);
                    LoadFile(pdfView.Uri);
                }
            }
        }

        void LoadFile(string fileName)
        {
            string filePath = Path.Combine(FileSystem.CacheDirectory, string.Format("pdf/{0}", WebUtility.UrlEncode(fileName)));
            //Control.LoadRequest(new NSUrlRequest(new NSUrl(filePath, false)));
            //Control.ScalesPageToFit = true;

            var bytes = File.ReadAllBytes(filePath);
            
            var bytesString = string.Join(",", bytes);
            Control.EvaluateJavascript($"PDFViewerApplication.open(new Uint8Array([{bytesString}]))");
        }

        void LoadPdfJS()
        {
            string filePath = NSBundle.MainBundle.ResourceUrl.Append("pdfjs/web/viewer.html", false).Path;
            //string filePath = Path.Combine(Xamarin.Essentials.FileSystem.CacheDirectory, string.Format("pdf/{0}", WebUtility.UrlEncode(fileName)));
            Control.LoadRequest(new NSUrlRequest(new NSUrl(filePath, false)));
            Control.ScalesPageToFit = true;
        }
    }
}