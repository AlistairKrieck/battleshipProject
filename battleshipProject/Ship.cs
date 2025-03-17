using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleshipProject
{
    public class Ship
    {
        public Tile tile;
        public string name;
        public int xSize, ySize;

        public Ship(Tile _tile, string _name, int _xSize, int _ySize)
        {
            tile = _tile;
            name = _name;
            xSize = _xSize;
            ySize = _ySize;
        }

        public Ship(Tile _tile)
        {
            tile = _tile;
        }
    }
}

