using Authentication.Components;
using MudBlazor.Services;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorComponents()                // ✅ Add this first
    .AddInteractiveServerComponents();               // ✅ Required for Blazor Server interactivity

builder.Services.AddMudServices();
builder.Services.AddScoped<AuthService>();
builder.Services.AddSingleton(provider =>
{
    var options = new SupabaseOptions();
    var client = new Supabase.Client(
        builder.Configuration["Supabase:Url"],
        builder.Configuration["Supabase:AnonKey"],
        options
    );
    client.InitializeAsync().Wait();
    return client;
});

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Correct Mapping
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode(); // ✅ required for Blazor Server

app.Run();
