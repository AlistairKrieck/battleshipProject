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

        public void RotateShip(Tile checkTile, Grid board)
        {
            if (GetOrientation(checkTile, 1, board).isShip == false)
            {
                RemoveShipParts(checkTile, board);

                checkTile.shipType.ChangeOrientation(1);

                SetShipParts(checkTile, board);
            }
        }

        public static Tile GetOrientation(Tile checkTile, int futureCheck, Grid board)
        {
            Tile shipPart = new Tile();

            int ori;

            try
            {
                ori = checkTile.shipType.orientation + futureCheck;
            }

            catch
            {
                ori = futureCheck;
            }



            if (ori == -1)
            {
                ori = 3;
            }

            else if (ori > 3)
            {
                ori -= 4;
            }

            if (ori == 0)
            {
                shipPart = board.Tiles.Find(t => t.refX == checkTile.refX + 1 && t.refY == checkTile.refY);
            }

            else if (ori == 1)
            {
                shipPart = board.Tiles.Find(t => t.refX == checkTile.refX && t.refY + 1 == checkTile.refY);
            }

            else if (ori == 2)
            {
                shipPart = board.Tiles.Find(t => t.refX == checkTile.refX - 1 && t.refY == checkTile.refY);
            }

            else if (ori == 3)
            {
                shipPart = board.Tiles.Find(t => t.refX == checkTile.refX && t.refY - 1 == checkTile.refY);
            }

            return shipPart;
        }

        public void SetShipParts(Tile checkTile, Grid board)
        {
            GetOrientation(checkTile, 0, board).isShipPart = true;
            GetOrientation(checkTile, 0, board).isShip = true;
            GetOrientation(checkTile, 0, board).shipType = checkTile.shipType;
        }

        public void RemoveShipParts(Tile checkTile, Grid board)
        {
            Tile shipPart = GetOrientation(checkTile, 0, board);

            shipPart.isShipPart = shipPart.isShip = false;
            shipPart.shipType = null;
        }
    }
}

