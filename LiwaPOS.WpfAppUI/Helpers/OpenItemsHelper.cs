using System.Windows;
using System.Windows.Controls;

public static class OpenItemsHelper
{
    // Tüm açık olan pencereleri alır
    public static IEnumerable<Window> GetOpenWindows()
    {
        return System.Windows.Application.Current.Windows.Cast<Window>();
    }

    // Tüm açık olan sayfaları alır
    public static IEnumerable<Page> GetOpenPages()
    {
        var pages = new List<Page>();
        foreach (var window in GetOpenWindows())
        {
            if (window.Content is Page page)
            {
                pages.Add(page);
            }
            else if (window.Content is Frame frame)
            {
                var pageContent = frame.Content as Page;
                if (pageContent != null)
                {
                    pages.Add(pageContent);
                }
            }
        }
        return pages;
    }

    // Tüm açık olan UserControl'leri alır
    public static IEnumerable<System.Windows.Controls.UserControl> GetOpenUserControls()
    {
        var userControls = new List<System.Windows.Controls.UserControl>();
        foreach (var window in GetOpenWindows())
        {
            if (window.Content is System.Windows.Controls.UserControl userControl)
            {
                userControls.Add(userControl);
            }
            else if (window.Content is Frame frame)
            {
                var userControlContent = frame.Content as System.Windows.Controls.UserControl;
                if (userControlContent != null)
                {
                    userControls.Add(userControlContent);
                }
            }
        }
        return userControls;
    }

    // Belirli bir adla açık olan UserControl'ü bulur
    public static System.Windows.Controls.UserControl GetOpenUserControlByName(string name)
    {
        return GetOpenUserControls().FirstOrDefault(uc => uc.Name == name);
    }

    // Belirli bir adla açık olan Page'i bulur
    public static Page GetOpenPageByName(string name)
    {
        return GetOpenPages().FirstOrDefault(p => p.Name == name);
    }

    // Belirli bir adla açık olan Window'u bulur
    public static Window GetOpenWindowByName(string name)
    {
        return GetOpenWindows().FirstOrDefault(w => w.Name == name);
    }

    // Tüm açık olan sayfaları kapatır
    public static void CloseAllPages()
    {
        foreach (var window in GetOpenWindows())
        {
            if (window.Content is Page || window.Content is Frame frame && frame.Content is Page)
            {
                window.Close();
            }
        }
    }

    // Tüm açık olan UserControl'leri kapatır
    public static void CloseAllUserControls()
    {
        foreach (var window in GetOpenWindows())
        {
            if (window.Content is System.Windows.Controls.UserControl || window.Content is Frame frame && frame.Content is System.Windows.Controls.UserControl)
            {
                window.Close();
            }
        }
    }

    // Belirli bir adla açık olan UserControl'ü kapatır
    public static void CloseUserControlByName(string name)
    {
        var userControl = GetOpenUserControlByName(name);
        if (userControl != null)
        {
            var window = Window.GetWindow(userControl);
            window?.Close();
        }
    }

    // Belirli bir adla açık olan Page'i kapatır
    public static void ClosePageByName(string name)
    {
        var page = GetOpenPageByName(name);
        if (page != null)
        {
            var window = Window.GetWindow(page);
            window?.Close();
        }
    }

    // Belirli bir adla açık olan Window'u kapatır
    public static void CloseWindowByName(string name)
    {
        var window = GetOpenWindowByName(name);
        window?.Close();
    }
}
