namespace BlazorWebAssemblySupabaseTemplate.Shared;

public partial class MainLayout
{
    bool _drawerOpen = true;

    void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }
}