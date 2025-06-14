using System.Collections.ObjectModel;
using System.Diagnostics;

namespace SP_12._04_ex4;

public class ProcessNode
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string Name { get; set; }
    public ObservableCollection<ProcessNode> Children { get; set; } = new();

    public ProcessNode(int id, int? parentId, string name)
    {
        Id = id;
        ParentId = parentId;
        Name = name;
    }

    public override string ToString() => $"{Name} (PID: {Id})";
}