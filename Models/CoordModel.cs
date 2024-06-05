using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using mystery_app.ViewModels;

namespace mystery_app.Models;
public class Coordinate
{

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    private int _x;
    private int _y;

    public int X
    {
        get { return _x; }
        set { if (value > 0) _x = value; }
    }
    public int Y
    {
        get { return _y; }
        set { if (value > 0) _y = value; }
    }

    public List<int> GetAsList()
    {
        return new List<int> { X, Y };
    }
}
