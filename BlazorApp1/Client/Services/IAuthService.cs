using System.Net.Http.Json;

public interface IAuthService
{
    Task Login(LoginRequest loginRequest);
    Task Register(RegisterRequest1 registerRequest);
    Task Logout();
    Task<CurrentUser> CurrentUserInfo();
}

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<CurrentUser> CurrentUserInfo()
    {
        var result = await _httpClient.GetFromJsonAsync<CurrentUser>("api/auth/currentuserinfo");
        return result;
    }
    public async Task Login(LoginRequest loginRequest)
    {
        var result = await _httpClient.PostAsJsonAsync("api/auth/login", loginRequest);
        if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
        result.EnsureSuccessStatusCode();
    }
    public async Task Logout()
    {
        var result = await _httpClient.PostAsync("api/auth/logout", null);
        result.EnsureSuccessStatusCode();
    }
    public async Task Register(RegisterRequest1 registerRequest)
    {
        var result = await _httpClient.PostAsJsonAsync("api/auth/register", registerRequest);
        if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
        result.EnsureSuccessStatusCode();
    }
}