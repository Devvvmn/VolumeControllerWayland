using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace VolumeController;

public partial class App : Application
{
    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            int x = 0;
            int y = 0;

            if (desktop.Args.Length >= 2 &&
                int.TryParse(desktop.Args[0], out var parsedX) &&
                int.TryParse(desktop.Args[1], out var parsedY))
            {
                x = parsedX;
                y = parsedY;
            }

            desktop.MainWindow = new MainWindow(x, y);
        }

        base.OnFrameworkInitializationCompleted();
    }
}