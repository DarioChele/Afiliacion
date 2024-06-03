using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Afiliacion.Services.AppServices;
public class SessionStorageService {
    private IJSRuntime _jsRuntime;

    public SessionStorageService(IJSRuntime jsRuntime) {
        _jsRuntime = jsRuntime;
    }

    public async Task<string> GetItem(string key) {
        return await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", key);
    }

    public async Task SetItem(string key, string value) {
        try {
            await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", key, value);
        } catch (Exception ex) {
            Console.WriteLine(ex);
            throw;
        }        
    }
    public async Task RemoveItem(string key) {
        await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", key);
    }
}