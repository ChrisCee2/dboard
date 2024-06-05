using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls.Converters;
using mystery_app.ViewModels;

namespace mystery_app.Models;


public class NodeModel
{

    public NodeModel(string name = "", string desc = "", int x = 0, int y = 0)
    {
        Name = name;
        Description = desc;
        Coord = new Coordinate(x, y);
    }

    private string _name;
    private string _description;
    private Coordinate _coord;

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }
    public Coordinate Coord
    {
        get { return _coord; }
        set { _coord = value; }
    }
}
