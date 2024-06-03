using Afiliacion.Models.Sesion;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Afiliacion.Services.AppServices;
public class AuthenticationStateProvider_ : AuthenticationStateProvider {
    private readonly AuthenticationService _authenticationService;
    private readonly TokenizerService _tokenizerService;
    private readonly SessionStorageService _sessionStorageService;
    private UserModel UsuarioAutenticado { get; set; }
    public AuthenticationStateProvider_(AuthenticationService authenticationService, SessionStorageService sessionStorageService, TokenizerService tokenizerService) {
        _authenticationService = authenticationService;
        _tokenizerService = tokenizerService;
        _sessionStorageService = sessionStorageService;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
        var isAuthenticated = await _authenticationService.IsAuthenticated();
        ClaimsIdentity identity;
		if (isAuthenticated) {
            var token = await _sessionStorageService.GetItem("authToken");
            UsuarioAutenticado = _tokenizerService.DecodeToken(token);
            var claims = new[] { new Claim(ClaimTypes.Name, UsuarioAutenticado.Name) };
			identity = new ClaimsIdentity(claims, "apiauth_type");
		} else {
			identity = new ClaimsIdentity();
		}
		var user = new ClaimsPrincipal(identity);
		return new AuthenticationState(user);
	}
    public void NotifyUserAuthentication(string userId) {
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userId) }, "apiauth_type"));
        var authState = new AuthenticationState(authenticatedUser);
        NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }
    public void NotifyUserLogout() {
        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = new AuthenticationState(anonymousUser);
        NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }
}
