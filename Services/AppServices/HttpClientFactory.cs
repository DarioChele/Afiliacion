namespace Afiliacion.Services.AppServices;
public class HttpClientFactory : IHttpClientFactory_ {
    private readonly Dictionary<string, HttpClient> _clients = new Dictionary<string, HttpClient>();

    public HttpClient CreateClient(string name) {
        if (!_clients.ContainsKey(name)) {
            var client = new HttpClient();
            _clients.Add(name, client);
        }

        return _clients[name];
    }
}