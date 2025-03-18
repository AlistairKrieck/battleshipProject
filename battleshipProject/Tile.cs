using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleshipProject
{
    public class Tile
    {
        public int x, y, size, refX, refY;
        public bool isShip, wasGuessed, isShipPart;
        public Ship shipType;

        public Tile(int _x, int _y, int _size, int _refX, int _refY)
        {
            x = _x;
            y = _y;
            size = _size;
            refX = _refX;
            refY = _refY;
        }
    }
}
