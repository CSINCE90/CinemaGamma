using CinemaFrontend.Data;
using CinemaFrontend.Services.Cinemas;
using CinemaFrontend.Services.Auditoriums;
using CinemaFrontend.Services.Movies;
using CinemaFrontend.Services.Reservations;
using CinemaFrontend.Services.Transactions;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Logging configuration
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Database configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Service registrations
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ICinemaService, CinemaService>();
builder.Services.AddScoped<IAuditoriumService, AuditoriumService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// HttpClient configuration
builder.Services.AddHttpClient("KeycloakClient")
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    });

// Authentication configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie("Cookies")
.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    options.Authority = "http://localhost:8080/realms/GammaExperience";
    options.RequireHttpsMetadata = false; // Should be true in production
    options.ClientId = "AppClient";
    options.ClientSecret = "3hMyaQdf5KFE16fwq4c7ezMjYC4gREWR";
    options.GetClaimsFromUserInfoEndpoint = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "http://localhost:8080/realms/GammaExperience",
        ValidateAudience = true,
        ValidAudience = "AppClient",
        ValidateLifetime = true,
        RoleClaimType = "roles" // Aggiunto per mappare correttamente i ruoli
    };
});

// Authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    //app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Aggiunto per assicurare che l'autenticazione venga eseguita prima dell'autorizzazione
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "movies",
    pattern: "Movies/{action=Index}/{id?}",
    defaults: new { controller = "Movies" });

app.Run();
