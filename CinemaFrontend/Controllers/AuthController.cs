using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

public class AuthController : Controller
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("Login")]
    public IActionResult Login(string returnUrl = "/")
    {
        if (User.Identity.IsAuthenticated)
        {
            return Redirect(returnUrl);
        }

        var properties = new AuthenticationProperties { RedirectUri = returnUrl };
        return Challenge(properties, OpenIdConnectDefaults.AuthenticationScheme);
    }

    [HttpPost("Logout")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        // Logout from the application
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);

        // Construct the Keycloak logout URL
        var keycloakAuthority = _configuration["Keycloak:Authority"];
        var clientId = _configuration["Keycloak:ClientId"];
        var keycloakLogoutUrl = $"{keycloakAuthority}/protocol/openid-connect/logout";
        var redirectUri = Url.Action("Index", "Home", null, Request.Scheme, Request.Host.ToString());

        // Redirect to Keycloak logout
        return Redirect($"{keycloakLogoutUrl}?client_id={clientId}&redirect_uri={Uri.EscapeDataString(redirectUri)}");
    }
}

