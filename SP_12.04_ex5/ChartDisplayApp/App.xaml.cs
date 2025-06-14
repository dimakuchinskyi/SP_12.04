using System.IO;
using SharedLib;
using System.IO.Pipes;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace ChartDisplayApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = (MainWindow)Current.MainWindow;
            Task.Run(() =>
            {
                while (true)
                {
                    using (var pipe = new NamedPipeServerStream("ChartPipe", PipeDirection.In))
                    {
                        pipe.WaitForConnection();
                        using (var ms = new MemoryStream())
                        {
                            pipe.CopyTo(ms);
                            var json = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                            var data = JsonSerializer.Deserialize<ChartData>(json);
                            mainWindow.Dispatcher.Invoke(() => mainWindow.ShowChartData(data));
                        }
                    }
                }
            });
        }
    }
}