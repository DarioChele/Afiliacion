using Afiliacion.Models.Page;
using Afiliacion.Models.Sesion;
using Afiliacion.Services.AppServices;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Afiliacion.Services.Page;
public class PageElementsService
{
    private readonly IHttpClientFactory clientFactory;
    public PageElementsModel PageElements { get; private set; }
    public PageElementsService(IHttpClientFactory _clientFactory)
    {
        clientFactory = _clientFactory;
    }
    public async Task GetPageElements()
    {
        string url = "Common/GetPageElements";
        using var httpClient = clientFactory.CreateClient("API");
        try
        {
            HttpResponseMessage response = new();
            response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string Resp = await response.Content.ReadAsStringAsync();
                // Deserializar el JSON para obtener la información de los elementos de la página
                PageElements = JsonConvert.DeserializeObject<PageElementsModel>(Resp);
            }
            else
            {
                // Manejar errores de solicitud no exitosa
                string errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error al autenticar: {errorMessage}");

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Entro al CATCH Error: {ex.Message}");

        }
    }
}
