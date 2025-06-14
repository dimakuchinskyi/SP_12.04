using SharedLib;
using System;
using System.Collections.ObjectModel;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace SP_12._04_ex5
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<DataPoint> Points { get; set; } = new();

        public MainWindow()
        {
            InitializeComponent();
            dataGrid.ItemsSource = Points;
            // Add test data
            Points.Add(new DataPoint { Date = DateTime.Today, Value = 10 });
            Points.Add(new DataPoint { Date = DateTime.Today.AddDays(1), Value = 20 });
            Points.Add(new DataPoint { Date = DateTime.Today.AddDays(2), Value = 15 });
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            var data = new ChartData
            {
                Dates = Points.Select(p => p.Date).ToList(),
                Values = Points.Select(p => p.Value).ToList()
            };
            SendData(data);
        }

        private void SendData(ChartData data)
        {
            try
            {
                using (var pipe = new NamedPipeClientStream(".", "ChartPipe", PipeDirection.Out))
                {
                    pipe.Connect(2000); // 2 seconds timeout
                    var json = JsonSerializer.Serialize(data);
                    var bytes = Encoding.UTF8.GetBytes(json);
                    pipe.Write(bytes, 0, bytes.Length);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to send data: " + ex.Message, "Error");
            }
        }

        public class DataPoint
        {
            public DateTime Date { get; set; }
            public double Value { get; set; }
        }
    }
}