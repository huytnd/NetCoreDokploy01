using BlazorApp01.Components;
using Infisical.Sdk;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var infisicalSettings = new ClientSettings
{
    SiteUrl = Environment.GetEnvironmentVariable("INFISICAL_SITE_URL") ?? string.Empty,
    
    Auth = new AuthenticationOptions
    {
        UniversalAuth = new UniversalAuthMethod
        {
            ClientId = Environment.GetEnvironmentVariable("INFISICAL_CLIENT_ID") ?? string.Empty,
            ClientSecret = Environment.GetEnvironmentVariable("INFISICAL_CLIENT_SECRET") ?? string.Empty
        }
    }
};

builder.Services.AddSingleton(new InfisicalClient(infisicalSettings));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorApp01.Client._Imports).Assembly);

app.Run();