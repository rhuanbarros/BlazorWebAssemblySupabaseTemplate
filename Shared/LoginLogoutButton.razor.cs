
namespace BlazorWebAssemblySupabaseTemplate.Shared;
public partial class LoginLogoutButton
{
    void Login()
    {
        NavigationManager.NavigateTo("login");
    }

    async Task Logout()
    {
        // await LocalStorage.RemoveItemAsync("token");
        await AuthStateProvider.GetAuthenticationStateAsync();
    }
}