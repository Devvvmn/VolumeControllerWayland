using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;

namespace VolumeController
{
    public partial class MainWindow : Window
    {
        public MainWindow(int x, int y)
        {
            InitializeComponent();
            // Получаем ссылку на слайдер
            var slider = this.FindControl<Slider>("Slider");

            // Подписываемся на событие изменения слайдера
            slider.ValueChanged += VolumeChanged;
            
            Position = new PixelPoint(x, y);
            
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        // Обработчик события для изменения значения слайдера
        private void VolumeChanged(object? sender, RangeBaseValueChangedEventArgs e)
        {
            var slider = (Slider)sender!;
            var volume = slider.Value;

            // Убедимся, что значение громкости в пределах допустимого диапазона
            if (volume < 0) volume = 0;
            if (volume > 100) volume = 100;

            SetVolume(volume);
        }

        private void SetVolume(double volume)
        {
            var volumeCommand = $"pactl set-sink-volume @DEFAULT_SINK@ {volume}%";

            var processStartInfo = new ProcessStartInfo("zsh", $"-c \"{volumeCommand}\"")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            // Запускаем процесс
            var process = Process.Start(processStartInfo);
            process?.WaitForExit();
        }
        
    }
}