using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Hosting;

namespace TestPdfMauiApp
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				});
			builder.ConfigureMauiHandlers(handlers =>
			{
				handlers.AddCompatibilityRenderer(typeof(PdfView), typeof(PdfViewRenderer));
			});

			return builder.Build();
		}
	}
}