using BlazorWebAssemblySupabaseTemplate.Dtos;
using Microsoft.AspNetCore.Components;
using Supabase.Gotrue;
using Supabase.Realtime;

namespace BlazorWebAssemblySupabaseTemplate.Services;

public class SupabaseService
{
    public readonly Supabase.Client instance;
    private readonly ILogger<SupabaseService> logger;

    // protected SupabaseService(ILogger<SupabaseService> logger)
    // {
    //     this.logger = logger;
    // }

    public SupabaseService(string url, string key, ILogger<SupabaseService> logger) : base()
    {
        this.logger = logger;
        logger.LogTrace("CONSTRUCTOR: SupabaseService");

        Supabase.Client.InitializeAsync(
                url,
                key,
                new Supabase.SupabaseOptions { 
                        AutoConnectRealtime = true, 
                        ShouldInitializeRealtime = true
                    }
            );

        instance = Supabase.Client.Instance;

        readDatabaseTest();
    }

    private async void readDatabaseTest()
    {
        try
        {
            Console.WriteLine("Get listas");
            var listas = await instance.From<Lista>().Get();
            foreach (var item in listas.Models)
            {
                Console.WriteLine(item.titulo);
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
                Console.WriteLine(item.titulo);
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
