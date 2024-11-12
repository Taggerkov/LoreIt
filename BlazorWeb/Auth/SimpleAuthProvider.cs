using System.Security.Claims;
using System.Text.Json;
using APILibrary;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace BlazorWeb.Auth;

/// <summary>
/// Provides simple authentication functionality for a Blazor application.
/// </summary>
public class SimpleAuthProvider(HttpClient httpClient, IJSRuntime jsRuntime) : AuthenticationStateProvider {

    /// <summary>
    /// Authenticates a user asynchronously.
    /// </summary>
    /// <param name="userName">The username of the user trying to log in.</param>
    /// <param name="password">The password of the user trying to log in.</param>
    /// <returns>A task that represents the asynchronous operation, which includes updating the authentication state.</returns>
    /// <exception cref="Exception">Thrown when the login attempt fails.</exception>
    public async Task LoginASync(string? userName, string? password) {
        var response = await httpClient.PostAsJsonAsync("auth/login", new LoginRequest(userName!, password!));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) {
            throw new Exception(content);
        }
        var userDto = JsonSerializer.Deserialize<UserDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        var serialisedData = JsonSerializer.Serialize(userDto);
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
        var claims = new List<Claim>() { new(ClaimTypes.Name, userDto.Username!), new(ClaimTypes.NameIdentifier, userDto.Id.ToString()) };
        var identity = new ClaimsIdentity(claims, "apiauth");
        var claimsPrincipal = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    /// <summary>
    /// Retrieves the current authentication state by checking session storage for stored user information.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing the <see cref="AuthenticationState"/> based on the stored user information.</returns>
    public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
        var userAsJson = "";
        try {
            userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
        }
        catch (InvalidOperationException) {
            return new AuthenticationState(new());
        }
        if (string.IsNullOrEmpty(userAsJson)) {
            return new AuthenticationState(new());
        }
        var userDto = JsonSerializer.Deserialize<UserDto>(userAsJson)!;
        var claims = new List<Claim>() { new(ClaimTypes.Name, userDto.Username!), new(ClaimTypes.NameIdentifier, userDto.Id.ToString()) };
        var identity = new ClaimsIdentity(claims, "apiauth");
        var claimsPrincipal = new ClaimsPrincipal(identity);
        return new AuthenticationState(claimsPrincipal);
    }

    /// <summary>
    /// Logs out the current user by clearing the authentication state and removing the stored user information from session storage.
    /// </summary>
    /// <returns>A task representing the asynchronous logout operation.</returns>
    public async Task Logout() {
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new())));
    }
}