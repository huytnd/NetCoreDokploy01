using BlazorApp01.Client.Pages;
using BlazorApp01.Components;
using Infisical.Sdk;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var infisicalSettings = new ClientSettings
{
    SiteUrl = builder.Configuration["Infisical:SiteUrl"],
    Auth = new AuthenticationOptions
    {
        UniversalAuth = new UniversalAuthMethod
        {
            ClientId = Environment.GetEnvironmentVariable("INFISICAL_CLIENT_ID"),
            ClientSecret = Environment.GetEnvironmentVariable("INFISICAL_CLIENT_SECRET")
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