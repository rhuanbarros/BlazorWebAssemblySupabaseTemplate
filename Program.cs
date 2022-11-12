using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWebAssemblySupabaseTemplate;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorWebAssemblySupabaseTemplate.Providers;
using BlazorWebAssemblySupabaseTemplate.Services;
using Blazored.LocalStorage;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// builder.Services.AddHttpClient("BaseHttpClient", httpClient =>
//     {
//         httpClient.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
//     }
// );

builder.Services.AddMudServices();

builder.Services.AddBlazoredLocalStorage();

// Auth
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();

var url = "https://pylnesfgmytjegzzculn.supabase.co";
var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InB5bG5lc2ZnbXl0amVnenpjdWxuIiwicm9sZSI6ImFub24iLCJpYXQiOjE2NjgyOTMwMzcsImV4cCI6MTk4Mzg2OTAzN30.kI29Q_qYWDH5SD6oi5NTwHG6Pxy1e1AUfR8s_ga45lE";

builder.Services.AddScoped<SupabaseService>(args => new SupabaseService(url, key, args.GetRequiredService<AuthenticationStateProvider>(), args.GetRequiredService<ILocalStorageService>(), args.GetRequiredService<ILogger<SupabaseService>>() ));

var host = builder.Build();

// TODO: I dont know if this line is correct?
var supabaseService = host.Services.GetRequiredService<SupabaseService>();
// supabaseService.Initialise();

await host.RunAsync();
// await builder.Build().RunAsync();