using System.ComponentModel;
using System.Net;
using Android.Content;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;

namespace TestPdfMauiApp
{
    public class PdfViewRenderer : WebViewRenderer
    {
        public PdfViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "Uri")
            {
                var pdfView = Element as PdfView;
                if (pdfView.Uri != null)
                {
                    Control.Settings.AllowFileAccess = true;
                    Control.Settings.AllowFileAccessFromFileURLs = true;
                    Control.Settings.AllowUniversalAccessFromFileURLs = true;
                    var filePath = string.Format("{0}/{1}/{2}", Android.App.Application.Context.CacheDir.AbsolutePath, "pdf", pdfView.Uri);
                    //Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file={0}", string.Format("file:///android_asset/Content/{0}", WebUtility.UrlEncode(customWebView.Uri))));
                    //Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file={0}", string.Format("file://{0}", WebUtility.UrlEncode(pdfView.Uri))));
                    Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file=file://{0}", filePath ));
                }
            }

        }
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var pdfView = Element as PdfView;
                if (pdfView.Uri != null)
                {
                    Control.Settings.AllowFileAccess = true;
                    Control.Settings.AllowFileAccessFromFileURLs = true;
                    Control.Settings.AllowUniversalAccessFromFileURLs = true;
                    //Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file={0}", string.Format("file:///android_asset/Content/{0}", WebUtility.UrlEncode(customWebView.Uri))));
                    Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file={0}", string.Format("file://{0}", WebUtility.UrlEncode(pdfView.Uri))));
                }
            }
        }
    }
}