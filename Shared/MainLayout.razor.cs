namespace BlazorWebAssemblySupabaseTemplate.Shared;

public partial class MainLayout
{
    bool _drawerOpen = true;

    void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }

    private async Task OnClickLogout()
    {
        await SupabaseService.Logout();
        Snackbar.Add("Logout successfull");
        NavigationManager.NavigateTo($"/");
    }
}