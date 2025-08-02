using Supabase;
using Supabase.Gotrue;
public class AuthService
{
    private readonly Supabase.Client _client;

    public AuthService(Supabase.Client client)
    {
        _client = client;
    }

    public async Task<(bool Success, string? Error)> SignUpAsync(string email, string password)
    {
        try
        {
            var session = await _client.Auth.SignUp(email, password);
            return session is not null
                ? (true, null)
                : (false, "Signup failed: session was null.");
        }
        catch (Exception ex)
        {
            return (false, $"Signup failed: {ex.Message}");
        }
    }

    public async Task<(bool Success, string? Error)> SignInAsync(string email, string password)
    {
        try
        {
            var session = await _client.Auth.SignIn(email, password);
            return session is not null
                ? (true, null)
                : (false, "Login failed: session was null.");
        }
        catch (Exception ex)
        {
            return (false, $"Login failed: {ex.Message}");
        }
    }

    public Task SignOutAsync() => _client.Auth.SignOut();
}
