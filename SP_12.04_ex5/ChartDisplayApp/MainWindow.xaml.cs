using SharedLib;
using System.Windows;

namespace ChartDisplayApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void ShowChartData(ChartData data)
    {
        // Наприклад, показати MessageBox з кількістю точок
        MessageBox.Show($"Received {data.Dates.Count} points.", "Chart Data");
        // Тут можна додати код для побудови графіка
    }
}