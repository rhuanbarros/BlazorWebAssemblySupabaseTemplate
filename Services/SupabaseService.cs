using Blazored.LocalStorage;
using BlazorWebAssemblySupabaseTemplate.Dtos;
using Microsoft.AspNetCore.Components.Authorization;
using Supabase.Gotrue;
using Supabase.Realtime;

namespace BlazorWebAssemblySupabaseTemplate.Services;

public class SupabaseService
{
    public readonly Supabase.Client instance;
    private readonly AuthenticationStateProvider customAuthStateProvider;
    private readonly ILocalStorageService localStorage;
    private readonly ILogger<SupabaseService> logger;

    public SupabaseService(
        string url, 
        string key, 
        AuthenticationStateProvider CustomAuthStateProvider, 
        ILocalStorageService localStorage,
        ILogger<SupabaseService> logger) : base()
    {
        customAuthStateProvider = CustomAuthStateProvider;
        this.localStorage = localStorage;
        this.logger = logger;
        logger.LogInformation("CONSTRUCTOR: SupabaseService");

        Supabase.Client.InitializeAsync(
                url,
                key,
                new Supabase.SupabaseOptions { 
                        AutoConnectRealtime = true, 
                        ShouldInitializeRealtime = true
                    }
            );

        instance = Supabase.Client.Instance;

        // Login("user", "password");
        // readDatabaseTest();
    }

    public async Task Login(string email, string password)
    {
        logger.LogInformation("METHOD: Login");
        
        Session session = await instance.Auth.SignIn(email, password);

        logger.LogInformation("------------------- User logged in -------------------");
        logger.LogInformation($"instance.Auth.CurrentUser.Id {instance.Auth.CurrentUser.Id}");
        logger.LogInformation($"instance.Auth.CurrentUser.Email {instance.Auth.CurrentUser.Email}");
        
        await localStorage.SetItemAsStringAsync("token", session.AccessToken);
        await customAuthStateProvider.GetAuthenticationStateAsync();
    }
    
    public async Task Logout()
    {
        await localStorage.RemoveItemAsync("token");
        await customAuthStateProvider.GetAuthenticationStateAsync();
    }

    private async void readDatabaseTest()
    {
        try
        {
            Console.WriteLine("Get listas");
            var listas = await instance.From<Lista>().Get();
            foreach (var item in listas.Models)
            {
                Console.WriteLine(item.Titulo);
            }

        }
        catch (Postgrest.RequestException e)
        {
            Console.WriteLine($"RequestException? e.Error {e.Error}");
            Console.WriteLine($"RequestException? e.Data {e.Data}");
            Console.WriteLine($"RequestException? e.Message {e.Message}");
            Console.WriteLine($"RequestException? e.Response {e.Response}");
        }
        catch (Exception e)
        {
            Console.WriteLine("Erro geral");
        }
    }

    // private readonly ILocalStorageService _localStorage;

    // public SupabaseService(ILocalStorageService localStorage)
    // {
    //     _localStorage = localStorage;
    // }

    public async void Initialise()
    {


        // var result = await instance.Auth.SignUp("rhuanbarros@gmail.com", "senhasdadasdaasd");
        // var result = await instance.Auth.SignUp("cliente1@gmail.com", "senhasdadasdaasd");
        // var result = await instance.Auth.SignIn("rhuanbarros@gmail.com", "senhasdadasdaasd");
        Session result = await instance.Auth.SignIn("cliente1@gmail.com", "senhasdadasdaasd");

        Console.WriteLine(instance.Auth.CurrentUser.Id);
        Console.WriteLine(instance.Auth.CurrentUser.Email);




        // await instance.From<Lista>().On(ChannelEventType.Insert, (sender,  args) =>
        //     {
        //         Console.WriteLine("REALTIME");
        //         // Console.WriteLine(args.Response.Model<Lista>());
        //         Lista lista = args.Response.Model<Lista>();
        //         Console.WriteLine(lista.titulo);

        //         // Console.WriteLine("-----");
        //         // Console.WriteLine(args.Response.Payload.Record);
        //         // Console.WriteLine("-----");

        //         // Lista lista = (Lista) args.Response.Payload.Record;
        //         // Console.WriteLine(lista);


        //         // Lista? lista2 = JsonSerializer.Deserialize<Lista>(args.Response.Payload.Record.ToString());
        //         // Console.WriteLine(lista2);

        //         // Console.WriteLine("-----");


        //     });


        try
        {
            Console.WriteLine("Get listas");
            var listas = await instance.From<Lista>().Get();
            foreach (var item in listas.Models)
            {
                Console.WriteLine(item.Titulo);
            }


            // Console.WriteLine("Criacao de uma lista");
            // Lista novaLista = new()
            // {
            //     titulo = "teste5",
            //     user_id = instance.Auth.CurrentUser.Id
            // };
            // Postgrest.Responses.ModeledResponse<Lista> modeledResponse = await instance.From<Lista>().Insert(novaLista);

            // Console.WriteLine("modeledResponse");
            // Console.WriteLine(modeledResponse);

        }
        catch (Postgrest.RequestException e)
        {
            Console.WriteLine($"RequestException? e.Error {e.Error}");
            Console.WriteLine($"RequestException? e.Data {e.Data}");
            Console.WriteLine($"RequestException? e.Message {e.Message}");
            Console.WriteLine($"RequestException? e.Response {e.Response}");
        }
        catch (Exception e)
        {
            Console.WriteLine("Erro geral");
        }


    }

    private void OnSubscriptionEvent(object sender, SocketResponseEventArgs args)
    {
        var lista = args.Response.Model<Lista>();
        Console.WriteLine("REALTIME");
        Console.WriteLine(lista);

    }
}
