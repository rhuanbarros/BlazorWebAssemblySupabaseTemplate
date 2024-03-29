using Blazored.LocalStorage;
using Supabase.Gotrue;
using Supabase.Interfaces;

namespace BlazorWebAssemblySupabaseTemplate.Providers;

public class CustomSupabaseSessionHandler : ISupabaseSessionHandler
{
    private readonly ILocalStorageService localStorage;
    private readonly ILogger<CustomSupabaseSessionHandler> logger;
    private static string SESSION_KEY = "SUPABASE_SESSION";

    public CustomSupabaseSessionHandler(
        ILocalStorageService localStorage,
        ILogger<CustomSupabaseSessionHandler> logger
    )
    {
        logger.LogInformation("------------------- CONSTRUCTOR -------------------");
        this.localStorage = localStorage;
        this.logger = logger;
    }

    public async Task<bool> SessionDestroyer()
    {
        logger.LogInformation("------------------- SessionDestroyer -------------------");
        await localStorage.RemoveItemAsync(SESSION_KEY); 
        return true;
    }

    public async Task<bool> SessionPersistor<TSession>(TSession session) where TSession : Session
    {
        logger.LogInformation("------------------- SessionPersistor -------------------");
        await localStorage.SetItemAsync(SESSION_KEY, session);
        return true;
    }

    public async Task<TSession?> SessionRetriever<TSession>() where TSession : Session
    {
        logger.LogInformation("------------------- SessionRetriever -------------------");
        // return (TSession?) await localStorage.GetItemAsync<Session>(SESSION_KEY);
        TSession? session = (TSession?) await localStorage.GetItemAsync<Session>(SESSION_KEY);
        
        // Console.WriteLine("session");
        // Console.WriteLine(session);
        // Console.WriteLine("session.ExpiresIn");
        // Console.WriteLine(session.ExpiresIn);
        // Console.WriteLine("session.ExpiresAt()");
        // Console.WriteLine(session.ExpiresAt());
        return session;
    }
}