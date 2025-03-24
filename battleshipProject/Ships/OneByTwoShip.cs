using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleshipProject
{
    public partial class OneByTwoShip : Ship
    {
        public OneByTwoShip()
        {
            name = "twoByOne";
            xSize = 2;
            ySize = 1;
            shipBrush = new SolidBrush(Color.Red);
            hitBrush = new SolidBrush(Color.DarkRed);
        }
    }
}
