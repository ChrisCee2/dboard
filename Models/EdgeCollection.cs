using System.Collections.ObjectModel;
using mystery_app.ViewModels;

namespace mystery_app.Models;


public class EdgeCollection : ObservableCollection<Edge>
{
    public bool ContainsEdge(NodeViewModel fromNode, NodeViewModel toNode)
    {
        var containsEdge = false;
        foreach (Edge edge in this)
        {
            if (edge.FromNode == fromNode && edge.ToNode == toNode)
            {
                containsEdge = true;
            }
        }

        return containsEdge;
    }
}
