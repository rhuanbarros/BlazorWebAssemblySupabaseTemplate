using BlazorWebAssemblySupabaseTemplate.Dtos;
using BlazorWebAssemblySupabaseTemplate.Services;
using MudBlazor;

namespace BlazorWebAssemblySupabaseTemplate.Pages.Crud;

public partial class CrudPage
{
    protected Supabase.Client SupabaseClient;
    protected override async Task OnInitializedAsync()
    {
        SupabaseClient = SupabaseService.instance;
        await GetTable();
    }

    private IReadOnlyList<Lista> _listaList { get; set; }
    private IReadOnlyList<Lista> _listaListFiltered { get; set; }
    private MudTable<Lista> table;
    protected async Task GetTable()
    {
        Postgrest.Responses.ModeledResponse<Lista> modeledResponse = await SupabaseClient.From<Lista>().Get();
        _listaList = modeledResponse.Models;
        _listaListFiltered = modeledResponse.Models;
        await InvokeAsync(StateHasChanged);
    }

    private async void OnSearch(string text)
    {
        _listaListFiltered = _listaList.Where(arg => arg.titulo.Contains(text)).ToList();
    }

}