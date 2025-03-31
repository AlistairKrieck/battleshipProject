using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleshipProject
{
    public partial class LittleGuy : Ship
    {

        public LittleGuy()
        {
            //Define dimentions and name of "Little Guy" ships
            name = "littleGuy";
            int xSize = 1;
            int ySize = 1;

            //Define colour of "Little Guy" ships when painted
            shipBrush = new SolidBrush(Color.Tan);
            hitBrush = new SolidBrush(Color.Red);
        }
    }
}
