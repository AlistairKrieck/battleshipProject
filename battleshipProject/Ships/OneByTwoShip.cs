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
        int orientaion = 0;

        public OneByTwoShip()
        {
            name = "twoByOne";
            xSize = 2;
            ySize = 1;

            List<int[]> data = new List<int[]>();
            data.Add(new int[] { 0, 1, 0 });
            data.Add(new int[] { 1, 1, 1 });
            data.Add(new int[] { 0, 1, 0 });

            Matrix orientaions = new Matrix(3, 3);

            orientaions.Fill(data);

            shipBrush = new SolidBrush(Color.Red);
        }

        public void Rotate()
        {
            if (orientaion < 4)
            {
                orientaion++;
            }
            else
            {
                orientaion = 0;
            }
        }
    }
}
