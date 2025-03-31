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
    }
}
