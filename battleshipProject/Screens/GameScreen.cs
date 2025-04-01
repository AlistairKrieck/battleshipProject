using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace battleshipProject
{
    public partial class GameScreen : UserControl
    {
        //Stores position of missles when fired
        PointF[] missleData = new PointF[4];

        //Create both game boards
        Grid enemyBoard = new Grid();
        Grid yourBoard;

        Stopwatch timer = new Stopwatch();

        //Create soundplayers for sfx
        SoundPlayer missSoundPlayer = new SoundPlayer(Properties.Resources.missSound);
        SoundPlayer explosionSoundPlayer = new SoundPlayer(Properties.Resources.explosionSound);

        //Get board dimentions from Form1 for convenience
        int boardWidth = Form1.boardWidth;
        int boardHeight = Form1.boardHeight;
        int tileSize = Form1.tileSize;

        //Set number of each ship type to be placed
        int littleGuyCount = 3;
        int twoByOneCount = 3;


        Random rand = new Random();

        //Allow turn to be read from other screens
        public static string turn = "player";

        public GameScreen()
        {
            InitializeComponent();
            GameInit();
        }

        //Places ships into enemyBoard at random positions
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

        //Gets ship placement from boardSetUpScreen
        public void MakePlayerBoard()
        {
            int x = (this.Width * 3 / 4) - (boardWidth * tileSize / 2);
            int y = (this.Height / 2) - (boardHeight * tileSize / 2);

            yourBoard = new Grid(x, y, boardWidth, boardHeight, tileSize);

            for (int i = 0; i < yourBoard.Tiles.Count; i++)
            {
                //Find ship placements from BoardSetupScreen
                if (yourBoard.Tiles[i].refX == BoardSetupScreen.playerBoard.Tiles[i].refX
                    && yourBoard.Tiles[i].refY == BoardSetupScreen.playerBoard.Tiles[i].refY
                    && BoardSetupScreen.playerBoard.Tiles[i].isShip == true)
                {
                    //Copy data to yourBoard
                    yourBoard.Tiles[i].isShip = BoardSetupScreen.playerBoard.Tiles[i].isShip;
                    yourBoard.Tiles[i].isShipPart = BoardSetupScreen.playerBoard.Tiles[i].isShipPart;
                    yourBoard.Tiles[i].shipType = BoardSetupScreen.playerBoard.Tiles[i].shipType;
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
                //Find mouse position
                int mouseX = e.X;
                int mouseY = e.Y;

                int tileSize = enemyBoard.tileSize;
                Tile clickedTile;

                //Find the tile that was clicked
                clickedTile = enemyBoard.Tiles.Find(t => mouseX >= t.x && mouseX <= t.x + t.size
                            && mouseY >= t.y && mouseY <= t.y + t.size);

                //Check if the tile exists and has not been guessed yet
                if (clickedTile != null && clickedTile.wasGuessed == false)
                {
                    //Fire missle at clicked tile
                    FirePlayerMissileAnimation(clickedTile);

                    //Tell clickedTile it was guessed
                    clickedTile.wasGuessed = true;

                    //Check if guessed tile is a ship
                    if (clickedTile.isShip == true)
                    {
                        //Play hit sound
                        explosionSoundPlayer.Play();
                    }
                    else
                    {
                        //Play miss sound
                        missSoundPlayer.Play();
                    }

                    turn = "bot";
                }
            }
        }

        //Chooses the tile the computer will guess
        private void RunCPU()
        {
            //Select a random x and y position on the created grid
            int j = rand.Next(0, boardWidth);
            int k = rand.Next(0, boardHeight);

            //Find tile with those x and y positions
            Tile tile = yourBoard.Tiles.Find(t => t.refX == j && t.refY == k);

            //If chosen tile has already been guessed, rerandomize and check again
            while (tile.wasGuessed == true)
            {
                j = rand.Next(0, boardWidth);
                k = rand.Next(0, boardHeight);

                tile = yourBoard.Tiles.Find(t => t.refX == j && t.refY == k);
            }

            if (tile.isShip == true && tile.wasGuessed == false)
            {
                //Tell chosen tile it was guessed
                tile.wasGuessed = true;

                //Fire missle at chosen tile
                FireBotMissileAnimation(tile);

                //Find tile inside of shipParts list and set it to be guessed as well
                Tile t = tile.shipType.shipParts.Find(s => s.refX == tile.refX && s.refY == tile.refY);
                t.wasGuessed = true;

                //Play hit sound
                explosionSoundPlayer.Play();
            }
            else if (tile.isShip == false && tile.wasGuessed == false)
            {
                //Tell chosen tile it was guessed
                tile.wasGuessed = true;

                //Fire missle at chosen tile
                FireBotMissileAnimation(tile);

                //Play miss sound
                missSoundPlayer.Play();
            }

            turn = "player";
        }

        //Updates the missleData list to move a missle from a start point to a chosen tile on the player's board
        private void FirePlayerMissileAnimation(Tile tile)
        {
            //Define how many frames it will take for the missle to reach its target
            float missleSpeed = 100;

            //Find starting point of missle
            float startX = this.Width / 2;
            float startY = this.Height;

            //Set initial x and y values to their start points
            float x = startX;
            float y = startY;

            //Find end x and y values
            int endX = tile.x + (tile.size / 2);
            int endY = tile.y + (tile.size / 2);

            //Find how far the missle should travel per tick
            float yStep = (endY - startY) / missleSpeed;
            float xStep = (endX - startX) / missleSpeed;

            while (x > endX)
            {
                //Update x and y values of the missle until they hit their target
                y += yStep;
                x += xStep;

                //Update missle data
                GetMissile(x, y, 10);

                //Refresh page to draw missle
                Refresh();
            }

            //Set the missle aside for later
            GetMissile(0, 0, 0);

            //Hide missle
            Refresh();
        }

        //Updates the missleData list to move a missle from a start point to a chosen tile on the bot's board
        private void FireBotMissileAnimation(Tile tile)
        {
            float missleSpeed = 100;

            //Find starting point of missle
            float startX = this.Width / 2;
            float startY = this.Height;

            //Set initial x and y values to their start points
            float x = startX;
            float y = startY;

            //Find end x and y values
            int endX = tile.x + (tile.size / 2);
            int endY = tile.y + (tile.size / 2);

            //Find how far the missle should travel per tick
            float yStep = (endY - startY) / missleSpeed;
            float xStep = (endX - startX) / missleSpeed;

            while (x < endX)
            {
                //Update x and y values of the missle until they hit their target
                y += yStep;
                x += xStep;

                //Update missle data
                GetMissile(x, y, 10);

                //Refresh page to draw missle
                Refresh();
            }

            //Set the missle aside for later
            GetMissile(0, 0, 0);

            //Hide missle
            Refresh();
        }

        //Updates missleData list to have the coordinates of each corner
        private void GetMissile(float x, float y, float size)
        {
            missleData[0] = new PointF(x, y);
            missleData[1] = new PointF(x + size, y);
            missleData[2] = new PointF(x + size, y + size);
            missleData[3] = new PointF(x, y + size);
        }

        //Finds the number of ships that both sides have sunk
        private int CheckRemainingShips(bool checkEnemy)
        {
            //Starting number of ships
            int remainingShips = littleGuyCount + twoByOneCount;

            //For player board
            if (checkEnemy == false)
            {
                //Find all guessed tiles on the player's board
                remainingShips = yourBoard.GetRemainingShips(remainingShips);
            }
            //For bot's board
            else
            {
                //Find all guessed tiles on the bot's board
                remainingShips = enemyBoard.GetRemainingShips(remainingShips);
            }

            //Return how many ships are yet to be guessed
            return remainingShips;
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //Find how many ships remain for player and bot
            int enemyShips = CheckRemainingShips(true);
            int playerShips = CheckRemainingShips(false);

            //Write that data to labels
            enemyShipLabel.Text = $"Remaining: {enemyShips}";
            playerShipLabel.Text = $"Remaining: {playerShips}";

            if (turn == "bot")
            {
                turnLabel.Text = "Bot Thinking...";

                Refresh();

                Wait(1500); //Create illusion of thought

                //Have bot guess a tile
                RunCPU();
            }
            else if (turn == "player")
            {
                turnLabel.Text = "Your Move!";
            }
            else
            {
                //If turn is neither player or bot, stop the timer and go to GameOverScreen
                gameTimer.Stop();
                Form1.ChangeScreen(this, new GameOverScreen());
            }

            //If either side has 0 ships left, set turn to the opposite winner
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

        //Waits for a specified time using timer
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
            //Paint each tile from the bot's board to screen
            foreach (var t in enemyBoard.Tiles)
            {
                DrawTile(e, t);
            }

            //Paint each tile from the player's board to screen
            foreach (var t in yourBoard.Tiles)
            {
                DrawTile(e, t);

                //Show player where their own ships are
                if (t.wasGuessed == false && t.isShip == true)
                {
                    e.Graphics.FillRectangle(Form1.shipBrush, t.x, t.y, t.size, t.size);
                }
            }

            //Paint the missle on screen
            e.Graphics.FillPolygon(new SolidBrush(Color.Brown), missleData);
        }

        private static void DrawTile(PaintEventArgs e, Tile t)
        {
            if (t.wasGuessed == true && t.isShip == false)
            {
                e.Graphics.FillRectangle(Form1.missBrush, t.x, t.y, t.size, t.size);
            }

            else if (t.wasGuessed == true && t.isShip == true)
            {
                e.Graphics.FillRectangle(t.shipType.hitBrush, t.x, t.y, t.size, t.size);
            }


            e.Graphics.DrawRectangle(Form1.tilePen, t.x, t.y, t.size, t.size);
        }
    }
}
