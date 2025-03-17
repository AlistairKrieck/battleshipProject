using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleshipProject
{
    public partial class LittleGuy
    {
        Ship newShip;
        string n = "littleGuy";
        int xS = 2;
        int yS = 1;

        public LittleGuy(Tile t)
        {
            newShip.tile = t;
            newShip.name = n;
            newShip.xSize = xS;
            newShip.ySize = yS;
        }
    }
}
