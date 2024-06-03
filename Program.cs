using Afiliacion.Data;
using Afiliacion.Services.AppServices;
using Afiliacion;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Afiliacion.Services.Page;
using Afiliacion.Services.Prospecto;

namespace Afiliacion;
public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddSingleton<WeatherForecastService>();

        // Servicios de la aplicacion
        builder.Services.AddSingleton<IHttpClientFactory_, HttpClientFactory>();
        builder.Services.AddHttpClient("API", client => {
            client.BaseAddress = new Uri("https://localhost:7080/");
        });
        builder.Services.AddScoped<SessionStorageService>();
        builder.Services.AddScoped<TokenizerService>();
        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<PageElementsService>();
        builder.Services.AddScoped<AuthenticationService>();
        builder.Services.AddScoped<AuthenticationStateProvider_>();
        builder.Services.AddScoped<MainService>();

        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();

        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment()) {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");
        app.Run();
    }
}
