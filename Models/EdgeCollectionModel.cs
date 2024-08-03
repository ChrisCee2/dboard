using System.Collections.ObjectModel;
using mystery_app.ViewModels;

namespace mystery_app.Models;


public class EdgeCollectionModel : ObservableCollection<EdgeViewModel>
{
    public bool ContainsEdge(NodeViewModelBase fromNode, NodeViewModelBase toNode)
    {
        foreach (EdgeViewModel edgeViewModel in this)
        {
            if (edgeViewModel.Edge.FromNode == fromNode && edgeViewModel.Edge.ToNode == toNode
                || edgeViewModel.Edge.FromNode == toNode && edgeViewModel.Edge.ToNode == fromNode)
            {
                return true;
            }
        }

        return false;
    }

    // Can be used in future if looking for one way edges
    public bool ContainsEdgeOneWay(NodeViewModelBase fromNode, NodeViewModelBase toNode)
    {
        foreach (EdgeViewModel edgeViewModel in this)
        {
            if (edgeViewModel.Edge.FromNode == fromNode && edgeViewModel.Edge.ToNode == toNode)
            {
                return true;
            }
        }

        return false;
    }
}
