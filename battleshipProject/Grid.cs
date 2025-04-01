using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleshipProject
{
    public class Grid
    {
        public List<Tile> Tiles;
        public int x, y, width, height, tileSize;

        public Grid()
        {

        }

        public Grid(int _x, int _y, int _width, int _height, int _tileSize)
        {
            //Get dimentions of grid
            x = _x;
            y = _y;
            Tiles = new List<Tile>();
            width = _width;
            height = _height;
            tileSize = _tileSize;

            //Create each tile in the grid
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    Tiles.Add(new Tile(x + i * tileSize,
                        y + j * tileSize, tileSize, i, j));
                }
            }
        }

        public int GetRemainingShips(int shipStartCount)
        {
            int remainingShips = shipStartCount;

            List<Tile> guessedTiles = Tiles.FindAll(t => t.wasGuessed == true && t.isShip == true);

            //Check if each of those tiles were a ship
            for (int i = 0; i < guessedTiles.Count; i++)
            {
                if (guessedTiles[i].shipType.shipParts.All(s => s.wasGuessed == true))
                {
                    if (guessedTiles[i].shipType.name == "twoByOne")
                    {
                        //Find other parts of two by one ships and remove them from guessedTiles so they are not both checked
                        Tile sp = guessedTiles[i].shipType.shipParts.Find(t => t.refX != guessedTiles[i].refX || t.refY != guessedTiles[i].refY);
                        int g = guessedTiles.FindIndex(t => t.refX == sp.refX || t.refY != sp.refY);
                        guessedTiles.RemoveAt(g);
                    }

                    //Lower remainingShips by one for each ship that has been guessed
                    remainingShips--;
                }
            }

            return remainingShips;
        }
    }
}
