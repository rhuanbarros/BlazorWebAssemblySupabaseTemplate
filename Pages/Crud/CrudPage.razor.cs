using BlazorWebAssemblySupabaseTemplate.Dtos;
using BlazorWebAssemblySupabaseTemplate.Services;

namespace BlazorWebAssemblySupabaseTemplate.Pages.Crud;

public partial class CrudPage
{
    protected Supabase.Client SupabaseClient;
    protected override async Task OnInitializedAsync()
    {
        SupabaseClient = SupabaseService.instance;
        await GetTable();
    }

    List<Lista> ListaList = null;
    protected async Task GetTable()
    {
        Postgrest.Responses.ModeledResponse<Lista> modeledResponse = await SupabaseClient.From<Lista>().Get();
        ListaList = modeledResponse.Models;
        await InvokeAsync(StateHasChanged);
    }
}