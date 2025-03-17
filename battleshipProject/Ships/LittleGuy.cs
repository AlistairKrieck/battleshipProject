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
            name = "littleGuy";
            int xSize = 1;
            int ySize = 1;
            shipBrush = new SolidBrush(Color.Tan);
        }
    }
}
