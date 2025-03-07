using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace battleshipProject
{
    public partial class GameScreen : UserControl
    {
        Grid enemyBoard;
        Grid yourBoard;

        int boardWidth = 10;
        int boardHeight = 10;
        int tileSize = 30;

        Random rand = new Random();

        string turn = "player";

        public GameScreen()
        {
            InitializeComponent();
            MakeEnemyBoard();
            MakePlayerBoard();
        }

        public void MakeEnemyBoard()
        {
            int x = (this.Width / 4) - (boardWidth * tileSize / 2);
            int y = (this.Height / 2) - (boardHeight * tileSize / 2);
            enemyBoard = new Grid(x, y, boardWidth, boardHeight, tileSize);

            int j, k;

            for (int i = 0; i < 5; i++)
            {
                j = rand.Next(0, boardWidth);
                k = rand.Next(0, boardHeight);

                Tile tile = enemyBoard.Tiles.Find(t => t.refX == j && t.refY == k);

                if (tile.isShip == true)
                {
                    i--;
                }
                else
                {
                    tile.isShip = true;
                }
            }
        }

        public void MakePlayerBoard()
        {
            int x = (this.Width * 3 / 4) - (boardWidth * tileSize / 2);
            int y = (this.Height / 2) - (boardHeight * tileSize / 2);

            yourBoard = new Grid(x, y, boardWidth, boardHeight, tileSize);

            List<Tile> tiles = BoardSetupScreen.playerBoard.Tiles.FindAll(t => t.isShip == true);

            for (int i = 0; i < yourBoard.Tiles.Count; i++)
            {
                if (yourBoard.Tiles[i].refX == BoardSetupScreen.playerBoard.Tiles[i].refX
                    && yourBoard.Tiles[i].refY == BoardSetupScreen.playerBoard.Tiles[i].refY
                    && BoardSetupScreen.playerBoard.Tiles[i].isShip == true)
                {
                    yourBoard.Tiles[i].isShip = BoardSetupScreen.playerBoard.Tiles[i].isShip;
                }
            }
        }

        private void GameScreen_MouseClick(object sender, MouseEventArgs e)
        {
            int mouseX = e.X;
            int mouseY = e.Y;
            int tileSize = enemyBoard.tileSize;
            Tile clickedTile;


            clickedTile = enemyBoard.Tiles.Find(t => mouseX >= t.x && mouseX <= t.x + t.size
                        && mouseY >= t.y && mouseY <= t.y + t.size);

            if (clickedTile != null)
            {
                clickedTile.wasGuessed = true;
            }

            turn = "bot";
        }

        private void RunCPU()
        {
        p:

            int j = rand.Next(0, boardWidth);
            int k = rand.Next(0, boardHeight);

            Tile tile = yourBoard.Tiles.Find(t => t.refX == j && t.refY == k);

            if (tile.wasGuessed == true)
            {
                goto p;
            }
            else
            {
                tile.wasGuessed = true;
            }

            turn = "player";
        }

        private int CheckRemainingShips(bool checkEnemy)
        {
            int remainingShips = 5;

            if (checkEnemy == false)
            {
                foreach (var t in yourBoard.Tiles)
                {
                    if (t.isShip == true && t.wasGuessed == true)
                    {
                        remainingShips--;
                    }
                }
            }

            else
            {
                foreach (var t in enemyBoard.Tiles)
                {
                    if (t.isShip == true && t.wasGuessed == true)
                    {
                        remainingShips--;
                    }
                }
            }

            return remainingShips;
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (turn == "bot")
            {
                botLabel.Text = "Bot Thinking...";

                Refresh();
                Thread.Sleep(1000);

                RunCPU();
            }
            else
            {
                botLabel.Text = "Your Move!";
            }

            enemyShipLabel.Text = $"Remaining: {CheckRemainingShips(true)}";
            playerShipLabel.Text = $"Remaining: {CheckRemainingShips(false)}";

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            foreach (var t in enemyBoard.Tiles)
            {
                if (t.wasGuessed == true && t.isShip == false)
                {
                    e.Graphics.FillRectangle(Form1.missBrush, t.x, t.y, t.size, t.size);
                }
                else if (t.wasGuessed == true && t.isShip == true)
                {
                    e.Graphics.FillRectangle(Form1.hitBrush, t.x, t.y, t.size, t.size);
                }

                e.Graphics.DrawRectangle(Form1.tilePen, t.x, t.y, t.size, t.size);
            }


            foreach (var t in yourBoard.Tiles)
            {
                if (t.wasGuessed == true && t.isShip == false)
                {
                    e.Graphics.FillRectangle(Form1.missBrush, t.x, t.y, t.size, t.size);
                }

                else if (t.wasGuessed == true && t.isShip == true)
                {
                    e.Graphics.FillRectangle(Form1.hitBrush, t.x, t.y, t.size, t.size);
                }

                else if (t.wasGuessed == false && t.isShip == true)
                {
                    e.Graphics.FillRectangle(Form1.shipBrush, t.x, t.y, t.size, t.size);
                }

                e.Graphics.DrawRectangle(Form1.tilePen, t.x, t.y, t.size, t.size);
            }
        }
    }
}
