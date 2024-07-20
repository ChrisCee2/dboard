using System.Collections.ObjectModel;
using mystery_app.ViewModels;

namespace mystery_app.Models;


public class EdgeCollection : ObservableCollection<Edge>
{
    public bool ContainsEdge(NodeViewModelBase fromNode, NodeViewModelBase toNode)
    {
        foreach (Edge edge in this)
        {
            if (edge.FromNode == fromNode && edge.ToNode == toNode)
            {
                return true;
            }
        }

        return false;
    }
}
