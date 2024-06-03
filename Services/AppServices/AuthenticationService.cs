using System.Net.Http.Headers;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Net.Http.Json;
using System.Net.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Components;
using System.Text;
using Afiliacion.Models.Sesion;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Components.Authorization;

namespace Afiliacion.Services.AppServices;
public class AuthenticationService {
    private readonly IHttpClientFactory clientFactory;
	private readonly SessionStorageService _sessionStorageService;
    private readonly TokenizerService _tokenizerService;
    private readonly UserService _userService;
    public AuthenticationService(IHttpClientFactory _clientFactory, SessionStorageService sessionStorageService, TokenizerService tokenizerService, UserService userService) {
		clientFactory = _clientFactory;
        _sessionStorageService = sessionStorageService;
        _tokenizerService = tokenizerService;
        _userService = userService;

        //_pageElementsService = pageElementsService;
    }
    
    public async Task<bool> Authenticate(string usuario, string password) {
        string url = "Auth";
        using var httpClient = clientFactory.CreateClient("API");
        try {
            HttpResponseMessage response = new();
            var loginData = new LoginModel { Usuario = usuario, Password = password };
            response = await httpClient.PostAsJsonAsync(url, loginData);
            if (response.IsSuccessStatusCode) {
                string token = await response.Content.ReadAsStringAsync();
                // Decodificar el token para obtener la información del usuario
                var decodedUser = _tokenizerService.DecodeToken(token);
                if (decodedUser != null) {
                    // Almacenar el token solo si es válido
                    await _sessionStorageService.SetItem("authToken", token);
                    _userService.SetUser(decodedUser);
                    //_pageElementsService.GetPageElements();
                    // Configurar el encabezado de autorización para futuras solicitudes
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    return true;
                } else {
                    // El token no es válido, manejar el error apropiadamente
                    return false;
                }
            } else {
                // Manejar errores de solicitud no exitosa
                string errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error al autenticar: {errorMessage}");
                return false;
            }
        } catch (Exception ex) {
            Console.WriteLine($"Entro al CATCH Error: {ex.Message}");
            return false;
        }
    }
	public async Task<bool> IsAuthenticated() {
        var token = await _sessionStorageService.GetItem("authToken");
        return !string.IsNullOrEmpty(token);
    }
    public async void LogOut() {
        UserModel newUser = new UserModel();
        _userService.SetUser(newUser);
        await _sessionStorageService.RemoveItem("authToken");
    }
}
