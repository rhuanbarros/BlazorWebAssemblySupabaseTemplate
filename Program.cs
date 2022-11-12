using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWebAssemblySupabaseTemplate;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorWebAssemblySupabaseTemplate.Providers;
using BlazorWebAssemblySupabaseTemplate.Services;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// builder.Services.AddHttpClient("BaseHttpClient", httpClient =>
//     {
//         httpClient.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
//     }
// );

builder.Services.AddBlazoredLocalStorage();

// Auth
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();

var url = "https://ybqilfcwesgbkvxmgxpm.supabase.co";
var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InlicWlsZmN3ZXNnYmt2eG1neHBtIiwicm9sZSI6ImFub24iLCJpYXQiOjE2NTcyMjAyNDgsImV4cCI6MTk3Mjc5NjI0OH0.PofQ_rSqpTt6EFt7BTgXwEjYNhanUYWLpExBdFq6t2s";

builder.Services.AddScoped<SupabaseService>(args => new SupabaseService(url, key, args.GetRequiredService<AuthenticationStateProvider>(), args.GetRequiredService<ILocalStorageService>(), args.GetRequiredService<ILogger<SupabaseService>>() ));

var host = builder.Build();

// TODO: I dont know if this line is correct?
var supabaseService = host.Services.GetRequiredService<SupabaseService>();
// supabaseService.Initialise();

await host.RunAsync();
// await builder.Build().RunAsync();