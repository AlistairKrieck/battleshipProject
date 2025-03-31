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
            //Define dimentions and name of "Two by One" ships
            name = "twoByOne";
            xSize = 2;
            ySize = 1;

            //Define colour of "Two by One" ships when painted
            shipBrush = new SolidBrush(Color.Red);
            hitBrush = new SolidBrush(Color.DarkRed);
        }
    }
}
