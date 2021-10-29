using Microsoft.Maui.Controls;

namespace TestPdfMauiApp
{
    public class PdfView : WebView
    {
        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }
        public static readonly BindableProperty UriProperty = BindableProperty.Create(
            nameof(Uri),
            typeof(string),
            typeof(PdfView),
            defaultValue: default(string),
            propertyChanged: OnUriChanged);

        static void OnUriChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as PdfView;
        }
    }
}
