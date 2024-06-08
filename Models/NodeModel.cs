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

    public NodeModel(string name = "", string desc = "")
    {
        Name = name;
        Description = desc;
    }

    private string _name;
    private string _description;

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
}
