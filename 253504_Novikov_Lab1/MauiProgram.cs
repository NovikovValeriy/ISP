using _253504_Novikov_Lab1.Services;
using Microsoft.Extensions.Logging;

namespace _253504_Novikov_Lab1
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<IDbService, SQLiteService>();
            builder.Services.AddSingleton<SQLiteDemo>();
            builder.Services.AddTransient<IRateService, RateService>();
            builder.Services.AddHttpClient<IRateService, RateService>(opt => opt.BaseAddress = new Uri("https://api.nbrb.by/exrates/rates/"));
            builder.Services.AddSingleton<CurrencyConverter>();
            return builder.Build();
        }
    }
}
