using Afiliacion.Models.BD;
using Afiliacion.Models.Page;
using Afiliacion.Models.Sesion;
using Afiliacion.Services.AppServices;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Afiliacion.Services.Prospecto;

public class MainService {
    private readonly IHttpClientFactory clientFactory;
    public BDModel RegistroActual { get; private set; }

    public MainService(IHttpClientFactory _clientFactory) {
        clientFactory = _clientFactory;
    }

    public async Task GuardarRegistro(BDModel JsonFromDB) {
        if (!string.IsNullOrEmpty(JsonFromDB.ci_cedu)) {
            RegistroActual = JsonFromDB;
        }
    }
    public async Task SetRegistroActual (BDModel JsonFromDB) {
        //bdModel = JsonConvert.DeserializeObject<BDModel>(JsonFromDB);
        RegistroActual = new();
        if (!string.IsNullOrEmpty(JsonFromDB.ci_cedu)) {
            RegistroActual = JsonFromDB;
        }
    }
    public async Task BuscaRegistro (string co_empr, string cedula) {
        string url = $"Main/GetRegistro?co_empr={co_empr}&ci_cedula={cedula}";
        using var httpClient = clientFactory.CreateClient("API");
        
        BDModel DataFromDB = new();
        try {
            HttpResponseMessage response = new();            
            response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode) {
                string Resp = await response.Content.ReadAsStringAsync();
                DataFromDB  = JsonConvert.DeserializeObject<BDModel>(Resp);
            }
            SetRegistroActual(DataFromDB);
        } catch (Exception ex) {
            Console.WriteLine($"Entro al CATCH Error: {ex.Message}");
        }
    }//Fin BuscaRegistro
    public async Task SetRegistroBlank() {
        try {
            RegistroActual = new();

        } catch (Exception ex) {
            Console.WriteLine($"Entro al CATCH Error: {ex.Message}");
        }
    }//Fin SetRegistroBlank
}
