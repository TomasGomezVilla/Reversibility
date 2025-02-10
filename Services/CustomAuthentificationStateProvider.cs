using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IJSRuntime _jsRuntime;
    private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());
    private bool _isInitialized = false;

    private readonly Dictionary<string, string> _users = new()
    {
        { "TJONCOUR", "Joncour" },
        { "admin", "password" }
    };

    public CustomAuthenticationStateProvider(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (!_isInitialized)
        {
            await LoadUserFromStorage();  // Charger l'utilisateur après le premier rendu
        }

        return new AuthenticationState(_currentUser);
    }

    private async Task LoadUserFromStorage()
    {
        try
        {
            var username = await _jsRuntime.InvokeAsync<string>("blazorLocalStorage.getItem", "username");

            if (!string.IsNullOrEmpty(username) && _users.ContainsKey(username))
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }, "apiauth");

                _currentUser = new ClaimsPrincipal(identity);
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
            }
        }
        catch
        {
            // Ignore errors if localStorage is not available
        }

        _isInitialized = true;
    }

    public async Task<bool> Login(string username, string password)
    {
        if (_users.TryGetValue(username, out var storedPassword) && storedPassword == password)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            }, "apiauth");

            _currentUser = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));

            // Stocker l'utilisateur dans LocalStorage après le rendu
            await _jsRuntime.InvokeVoidAsync("blazorLocalStorage.setItem", "username", username);
            return true;
        }

        return false;
    }

    public async Task Logout()
    {
        _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));

        // Supprimer l'utilisateur du LocalStorage après le rendu
        await _jsRuntime.InvokeVoidAsync("blazorLocalStorage.removeItem", "username");
    }
}
