using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace battleshipProject
{
    public partial class GameScreen : UserControl
    {
        Grid enemyBoard = new Grid();
        Grid yourBoard;

        Stopwatch timer = new Stopwatch();

        int boardWidth = 10;
        int boardHeight = 10;
        int tileSize = 30;

        int littleGuyCount = 3;
        int twoByOneCount = 3;


        Random rand = new Random();

        public static string turn = "player";

        public GameScreen()
        {
            InitializeComponent();
            GameInit();
        }

        public void MakeEnemyBoard()
        {
            Random rand = new Random();
            int x = (this.Width / 4) - (boardWidth * tileSize / 2);
            int y = (this.Height / 2) - (boardHeight * tileSize / 2);

            enemyBoard = new Grid(x, y, boardWidth, boardHeight, tileSize);

            foreach (var t in enemyBoard.Tiles)
            {
                t.isShip = false;
                t.isShipPart = false;
                t.wasGuessed = false;
            }

            for (int i = 0; i < twoByOneCount; i++)
            {
                int j = rand.Next(0, boardWidth);
                int k = rand.Next(0, boardHeight);

                int ori = rand.Next(0, 4);


                Tile tile = enemyBoard.Tiles.Find(t => t.refX == j && t.refY == k);

                if (Ship.GetOrientation(tile, ori, enemyBoard) == null || tile.isShip == true || Ship.GetOrientation(tile, ori, enemyBoard).isShip == true)
                {
                    i--;
                }

                else
                {
                    tile.shipType = new OneByTwoShip();
                    tile.shipType.ChangeOrientation(ori);
                    tile.shipType.SetShipParts(tile, enemyBoard);

                    tile.shipType.shipParts.Add(tile);
                    tile.shipType.shipParts.Add(Ship.GetOrientation(tile, 0, enemyBoard));

                    foreach (var t in tile.shipType.shipParts)
                    {
                        t.isShip = true;
                    }
                }
            }

            for (int i = 0; i < littleGuyCount; i++)
            {
                int j = rand.Next(0, boardWidth);
                int k = rand.Next(0, boardHeight);

                Tile tile = enemyBoard.Tiles.Find(t => t.refX == j && t.refY == k);

                if (tile.isShip == true)
                {
                    i--;
                }
                else
                {
                    tile.isShip = true;
                    tile.shipType = new LittleGuy();
                    tile.shipType.shipParts.Add(tile);
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

        public void GameInit()
        {
            turn = "player";

            MakeEnemyBoard();
            MakePlayerBoard();
        }

        private void GameScreen_MouseClick(object sender, MouseEventArgs e)
        {
            if (turn == "player")
            {
                int mouseX = e.X;
                int mouseY = e.Y;
                int tileSize = enemyBoard.tileSize;
                Tile clickedTile;


                clickedTile = enemyBoard.Tiles.Find(t => mouseX >= t.x && mouseX <= t.x + t.size
                            && mouseY >= t.y && mouseY <= t.y + t.size);

                if (clickedTile != null && clickedTile.wasGuessed == false)
                {
                    clickedTile.wasGuessed = true;
                    turn = "bot";
                }
            }
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
            int remainingShips = littleGuyCount + twoByOneCount;

            if (checkEnemy == false)
            {
                List<Tile> guessedTiles = yourBoard.Tiles.FindAll(t => t.wasGuessed == true && t.isShip == true);

                for (int i = 0; i < guessedTiles.Count; i++)
                {
                    if (guessedTiles[i].shipType.shipParts.All(s => s.wasGuessed == true))
                    {
                        if (guessedTiles[i].shipType.name == "twoByOne")
                        {
                            guessedTiles.Remove(guessedTiles[i].shipType.shipParts[guessedTiles[i].shipType.shipParts.Count - 1]);
                        }

                        remainingShips--;
                    }
                }
            }

            else
            {
                List<Tile> guessedTiles = enemyBoard.Tiles.FindAll(t => t.wasGuessed == true && t.isShip == true);

                for (int i = 0; i < guessedTiles.Count; i++)
                {
                    if (guessedTiles[i].shipType.shipParts.All(s => s.wasGuessed == true))
                    {
                        if (guessedTiles[i].shipType.name == "twoByOne")
                        {
                            guessedTiles.Remove(guessedTiles[i].shipType.shipParts[guessedTiles[i].shipType.shipParts.Count - 1]);
                        }

                        remainingShips--;
                    }
                }
            }

            return remainingShips;
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            int enemyShips = CheckRemainingShips(true);
            int playerShips = CheckRemainingShips(false);

            enemyShipLabel.Text = $"Remaining: {enemyShips}";
            playerShipLabel.Text = $"Remaining: {playerShips}";

            if (turn == "bot")
            {
                botLabel.Text = "Bot Thinking...";

                Refresh();

                Wait(1000); //Create illusion of thought

                RunCPU();
            }
            else if (turn == "player")
            {
                botLabel.Text = "Your Move!";
            }
            else
            {
                gameTimer.Stop();
                Form1.ChangeScreen(this, new GameOverScreen());
            }

            if (enemyShips == 0)
            {
                turn = "playerWon";
            }

            else if (playerShips == 0)
            {
                turn = "enemyWon";
            }


            Refresh();
        }

        public void Wait(int waitTime)
        {
            timer.Start();

            while (timer.ElapsedMilliseconds < waitTime)
            {
                //wait
            }

            timer.Reset();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            foreach (var t in enemyBoard.Tiles)
            {
                if (t.wasGuessed == true && t.isShip == false)
                {
                    e.Graphics.FillRectangle(Form1.missBrush, t.x, t.y, t.size, t.size);
                }
                else if (t.wasGuessed == false && t.isShip == true)
                {
                    e.Graphics.FillRectangle(Form1.hitBrush, t.x, t.y, t.size, t.size);
                }

                else
                {
                    e.Graphics.DrawRectangle(Form1.tilePen, t.x, t.y, t.size, t.size);
                }
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
