using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SP_12._04_ex4;

public partial class MainWindow : Window
{
    public ObservableCollection<ProcessNode> RootNodes { get; set; } = new();

    public MainWindow()
    {
        InitializeComponent();
        ProcessTreeView.ItemsSource = RootNodes;
        LoadProcessTreeAsync();
    }

    private async void LoadProcessTreeAsync()
    {
        var nodes = await Task.Run(BuildProcessTree);
        Application.Current.Dispatcher.Invoke(() =>
        {
            RootNodes.Clear();
            foreach (var node in nodes)
                RootNodes.Add(node);
        });
    }

    private List<ProcessNode> BuildProcessTree()
    {
        var processList = new List<(int Id, int? ParentId, string Name)>();
        var searcher = new ManagementObjectSearcher("SELECT ProcessId, ParentProcessId, Name FROM Win32_Process");
        foreach (ManagementObject obj in searcher.Get())
        {
            int id = Convert.ToInt32(obj["ProcessId"]);
            int? parentId = obj["ParentProcessId"] != null ? Convert.ToInt32(obj["ParentProcessId"]) : (int?)null;
            string name = obj["Name"]?.ToString() ?? "Unknown";
            processList.Add((id, parentId, name));
        }

        var nodeDict = processList.ToDictionary(
            p => p.Id,
            p => new ProcessNode(p.Id, p.ParentId, p.Name)
        );

        List<ProcessNode> roots = new();
        foreach (var node in nodeDict.Values)
        {
            if (node.ParentId.HasValue && nodeDict.ContainsKey(node.ParentId.Value) && node.ParentId != node.Id)
                nodeDict[node.ParentId.Value].Children.Add(node);
            else
                roots.Add(node);
        }
        return roots;
    }

    private void ProcessTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (ProcessTreeView.SelectedItem is ProcessNode node)
        {
            try
            {
                var proc = Process.GetProcessById(node.Id);
                ProcessInfoTextBlock.Text =
                    $"Name: {proc.ProcessName}\n" +
                    $"PID: {proc.Id}\n" +
                    $"Start Time: {TryGetStartTime(proc)}\n" +
                    $"Threads: {proc.Threads.Count}\n" +
                    $"Memory: {proc.WorkingSet64 / 1024} KB";
            }
            catch (Exception ex)
            {
                ProcessInfoTextBlock.Text = $"Process info unavailable: {ex.Message}";
            }
        }
        else
        {
            ProcessInfoTextBlock.Text = "";
        }
    }

    private string TryGetStartTime(Process proc)
    {
        try { return proc.StartTime.ToString(); }
        catch { return "N/A"; }
    }
}