using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleshipProject
{
    public class Ship
    {
        public string name;
        public int xSize, ySize;
        public SolidBrush shipBrush;
        List<Tile> shipParts;
        public int orientation = 0;

        public void ChangeOrientation(int modifier)
        {
            orientation += modifier;

            if (orientation > 3)
            {
                orientation = 0;
            }

            else if (orientation < 0)
            {
                orientation = 3;
            }
        }

        //public Ship(string _name, int _xSize, int _ySize)
        //{
        //    name = _name;
        //    xSize = _xSize;
        //    ySize = _ySize;
        //}
    }
}

