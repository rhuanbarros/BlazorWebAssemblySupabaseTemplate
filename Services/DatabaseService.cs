using Blazored.LocalStorage;
using BlazorWebAssemblySupabaseTemplate.Dtos;
using Microsoft.AspNetCore.Components.Authorization;
using Postgrest.Models;
using Supabase.Gotrue;
using Supabase.Interfaces;
using Supabase.Realtime;
using Supabase.Storage;
using static Postgrest.Constants;

namespace BlazorWebAssemblySupabaseTemplate.Services;

public class DatabaseService
{
	private readonly Supabase.Client client;
	private readonly AuthenticationStateProvider customAuthStateProvider;
	private readonly ILocalStorageService localStorage;
	private readonly ILogger<DatabaseService> logger;

	public DatabaseService(
		Supabase.Client client,
		AuthenticationStateProvider CustomAuthStateProvider,
		ILocalStorageService localStorage,
		ILogger<DatabaseService> logger
	) : base()
	{
		logger.LogInformation("------------------- CONSTRUCTOR -------------------");

		this.client = client;
		customAuthStateProvider = CustomAuthStateProvider;
		this.localStorage = localStorage;
		this.logger = logger;
	}

	public async Task<IReadOnlyList<TModel>> From<TModel>() where TModel : BaseModel, new()
	{
		Postgrest.Responses.ModeledResponse<TModel> modeledResponse = await client.From<TModel>().Get();
		return modeledResponse.Models;
	}

	public async Task<List<TModel>> Delete<TModel>(TModel item) where TModel : BaseModel, new()
	{
		Postgrest.Responses.ModeledResponse<TModel> modeledResponse = await client.From<TModel>().Delete(item);
		return modeledResponse.Models;
	}

	public async Task<List<TModel>> Insert<TModel>(TModel item) where TModel : BaseModel, new()
	{
		Postgrest.Responses.ModeledResponse<TModel> modeledResponse = await client.From<TModel>().Insert(item);
		return modeledResponse.Models;
	}

	public async Task<List<Todo>> SoftDelete(Todo item)
    {
		Console.WriteLine("item.Id");
		Console.WriteLine(item.Id);
		
        Postgrest.Responses.ModeledResponse<Todo> modeledResponse = await client.Postgrest
			.Table<Todo>()
            .Set(x => new KeyValuePair<object,object>(x.SoftDelete, "true"))
            // .Set(x => new KeyValuePair<object,object>(x.SoftDeletedAt, DateTime.Now))
            .Where(x => x.Id == item.Id)
            // .Filter(x => x.Id, Operator.Equals, item.Id)
            .Update();
        return modeledResponse.Models;
    }

}
