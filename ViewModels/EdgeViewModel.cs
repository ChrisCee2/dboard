﻿using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using mystery_app.Constants;
using mystery_app.Messages;
using mystery_app.Models;

namespace mystery_app.ViewModels;

public partial class EdgeViewModel : ObservableObject
{
    public EdgeViewModel(EdgeModel edgeModel)
    {
        Edge = edgeModel;
    }

    public EdgeViewModel(
        NodeModelBase fromNode,
        NodeModelBase toNode,
        Color color,
        string description = "",
        double thickness = EdgeConstants.THICKNESS)
    {
        Edge = new EdgeModel(fromNode, toNode, description, thickness, color.A, color.R, color.G, color.B);
    }

    [ObservableProperty]
    private EdgeModel _edge;
    [ObservableProperty]
    private bool _isSelected;

    [RelayCommand]
    private void Delete()
    {
        WeakReferenceMessenger.Default.Send(new DeleteMessage(""));
    }

    public EdgeViewModel Clone()
    {
        return new EdgeViewModel(new EdgeModel(
            Edge.FromNode,
            Edge.ToNode,
            Edge.Description,
            Edge.Thickness,
            Edge.A,
            Edge.R,
            Edge.G,
            Edge.B));
    }

    public EdgeViewModel CloneWithNewNodes(NodeModelBase fromNode, NodeModelBase toNode)
    {
        return new EdgeViewModel(new EdgeModel(
            fromNode,
            toNode,
            Edge.Description,
            Edge.Thickness,
            Edge.A,
            Edge.R,
            Edge.G,
            Edge.B));
    }
}