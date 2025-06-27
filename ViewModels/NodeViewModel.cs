using Avalonia.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using dboard.Models;
using DynamicData;
using static System.Net.Mime.MediaTypeNames;

namespace dboard.ViewModels;

public partial class NodeViewModel : NodeViewModelBase
{
    [ObservableProperty]
    private TextEditViewModel _name = new TextEditViewModel("");
    [ObservableProperty]
    private TextEditViewModel _notes = new TextEditViewModel("");

    public NodeViewModel()
    {
        Node = new NodeModel();
        Name.IsWordWrap = false;
        Notes.IsWordWrap = true;
        Name.Text = Node.Name;
        Notes.Text = Node.Notes;
    }

    public NodeViewModel(NodeModel node)
    {
        Node = node;
        Name.IsWordWrap = false;
        Notes.IsWordWrap = true;
        Name.Text = Node.Name;
        Notes.Text = Node.Notes;
    }

    [ObservableProperty]
    private NodeModel _node;
    public override NodeModelBase NodeBase
    {
        get { return _node; }
        set { _node = (NodeModel)value; }
    }

    public override NodeViewModelBase Clone()
    {
        return new NodeViewModel(Node.Clone());
    }

    public override NodeViewModelBase Clone(int zIndex)
    {
        NodeModel node = Node.Clone();
        node.ZIndex = zIndex;
        return new NodeViewModel(node);
    }

    public override void Copy(NodeViewModelBase nodeToCopy)
    {
        NodeViewModel nodeViewModel = (NodeViewModel)nodeToCopy;
        Node.Copy(nodeViewModel.Node);
    }

    public void UpdateNodeModel()
    {
        Node.Name = Name.Text;
        Node.Notes = Notes.Text;
    }
}