using Microsoft.EntityFrameworkCore;
using SistemaFIMETaller.Client.Pages;
using SistemaFIMETaller.Components;
using SistemaFIMETaller.Data;
using SistemaFIMETaller.Services;
using SistemaFIMETaller.Services.EspaciosServices;
using MudBlazor.Services;
using SistemaFIMETaller.Services.CookieAccessToken;
using SistemaFIMETaller.Security;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// CAMBIO: AddDbContextFactory en lugar de AddDbContext
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAlumnoService, AlumnoService>();
builder.Services.AddScoped<IMaterialService, MaterialService>();
builder.Services.AddScoped<IPrestamoService, PrestamoService>();
builder.Services.AddScoped<IPrestamoMaterialService, PrestamoMaterialService>();
builder.Services.AddScoped<ExcelService>();
builder.Services.AddScoped<IRegistroAccesoService, RegistroAccesoService>();
builder.Services.AddScoped<IEspacioService, EspacioService>();
builder.Services.AddScoped<IReservaEspacioService, ReservaEspacioService>();
builder.Services.AddScoped<IProfesorService, ProfesorService>();
builder.Services.AddScoped<CookieService>();
builder.Services.AddScoped<AccessTokenService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ResourceService>();
builder.Services.AddScoped<APIService>();
builder.Services.AddScoped<SistemaFIMETaller.Services.CookieAccessToken.RefreshTokenService>();
builder.Services.AddScoped<SistemaFIMETaller.Services.CookieAccessToken.UserClientService>();
builder.Services.AddHttpClient("ApiClient", opt =>
{
    opt.BaseAddress = new Uri("https://localhost:7054/api/");
});

builder.Services.AddAuthorization();

builder.Services.AddAuthentication()
    .AddScheme<CustomOption, JWTAuthenticationHandler>(
    "JWTAuth", options => { }
    );

builder.Services.AddScoped<JWTAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, JWTAuthenticationStateProvider>();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddMudServices();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(SistemaFIMETaller.Client._Imports).Assembly);

app.Run();
