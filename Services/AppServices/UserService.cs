using Afiliacion.Models.Sesion;

namespace Afiliacion.Services.AppServices;
public class UserService {
    public UserModel CurrentUser { get; private set; }
    public async Task SetUser(UserModel user) {
        CurrentUser = user;
    }
}
