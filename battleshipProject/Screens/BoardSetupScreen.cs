using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace battleshipProject
{
    public partial class BoardSetupScreen : UserControl
    {
        public static Grid playerBoard;

        string selectedShip = "null";
        int maxLittleGuyCount = 3;
        int maxTwoByOneCount = 3;
        int littleGuyCount = 0;
        int twoByOneCount = 0;

        List<Button> buttonList = new List<Button>();

        public BoardSetupScreen()
        {
            InitializeComponent();
            InitGame();
        }

        public void InitGame()
        {
            Form1.boardWidth = 10;
            Form1.boardHeight = 10;
            Form1.tileSize = 30;
            int x = (this.Width / 2) - (Form1.boardWidth * Form1.tileSize / 2);
            int y = (this.Height / 2) - (Form1.boardHeight * Form1.tileSize / 2);
            playerBoard = new Grid(x, y, Form1.boardWidth, Form1.boardHeight, Form1.tileSize);

            buttonList.Add(littleGuyButton);
            buttonList.Add(removeButton);
            buttonList.Add(twoByOneButton);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (selectedShip == "littleGuy" && littleGuyCount >= maxLittleGuyCount)
            {
                selectedShip = "null";
                littleGuyButton.BackColor = Color.White;
            }

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            foreach (var t in playerBoard.Tiles)
            {
                if (t.isShip == true)
                {
                    e.Graphics.FillRectangle(t.shipType.shipBrush, t.x, t.y, t.size, t.size);
                }

                e.Graphics.DrawRectangle(Form1.tilePen, t.x, t.y, t.size, t.size);
            }
        }

        private void BoardSetupScreen_MouseClick(object sender, MouseEventArgs e)
        {
            int mouseX = e.X;
            int mouseY = e.Y;
            int tileSize = playerBoard.tileSize;
            Tile clickedTile;

            clickedTile = playerBoard.Tiles.Find(t => mouseX >= t.x && mouseX <= t.x + t.size
                                        && mouseY >= t.y && mouseY <= t.y + t.size);

            if (e.Button == MouseButtons.Left && clickedTile != null)
            {
                if (selectedShip == "littleGuy" && littleGuyCount < maxLittleGuyCount)
                {
                    if (clickedTile.isShip == false)
                    {
                        clickedTile.isShip = true;
                        clickedTile.isMainShip = true;
                        clickedTile.shipType = new LittleGuy();
                        littleGuyCount++;
                    }
                }

                else if (selectedShip == "twoByOne" && twoByOneCount < maxTwoByOneCount)
                {
                    if (clickedTile.isShip == false && GetShipParts(clickedTile).isShip == false)
                    {
                        clickedTile.isShip = true;
                        clickedTile.isMainShip = true;
                        clickedTile.shipType = new OneByTwoShip();
                        SetShipParts(clickedTile);

                        twoByOneCount++;
                    }
                }

                else if (selectedShip == "remove" && clickedTile.isShip == true)
                {
                    if (clickedTile.shipType.name == "littleGuy")
                    {
                        littleGuyCount--;
                    }

                    else if (clickedTile.shipType.name == "twoByOne" && clickedTile.isShipPart == false)
                    {
                        twoByOneCount--;

                        RemoveShipParts(clickedTile);
                    }

                    else if (clickedTile.shipType.name == "twoByOne" && clickedTile.isShipPart == true)
                    {
                        twoByOneCount--;

                        playerBoard.Tiles.Find(t => t == GetOrientation(clickedTile, 2)).shipType = null;
                        playerBoard.Tiles.Find(t => t == GetOrientation(clickedTile, 2)).isShipPart = false;
                        playerBoard.Tiles.Find(t => t == GetOrientation(clickedTile, 2)).isShip = false;
                    }

                    clickedTile.isShip = false;
                    clickedTile.shipType = null;
                }
            }

            else if (e.Button == MouseButtons.Right && clickedTile != null)
            {
                if (clickedTile.isMainShip == true && clickedTile.shipType.name == "twoByOne")
                {
                    RotateShip(clickedTile);
                }
            }
        }

        private void RotateShip(Tile clickedTile)
        {
            clickedTile.shipType.ChangeOrientation(1);

            if (GetOrientation(clickedTile, 0).isShip == false)
            {
                //clickedTile.shipType.ChangeOrientation(-1);

                RemoveShipParts(clickedTile);

                //clickedTile.shipType.ChangeOrientation(1);

                SetShipParts(clickedTile);
            }

            else
            {
                clickedTile.shipType.ChangeOrientation(-1);
            }
        }

        private Tile GetOrientation(Tile clickedTile, int futureCheck)
        {
            Tile shipPart = new Tile();
            int ori = clickedTile.shipType.orientation + futureCheck;

            if (ori == -1)
            {
                ori = 3;
            }

            else if (ori > 3)
            {
                ori = 4 - ori;
            }

            if (ori == 0)
            {
                shipPart = playerBoard.Tiles.Find(t => t.refX == clickedTile.refX + 1 && t.refY == clickedTile.refY);
            }

            else if (ori == 1)
            {
                shipPart = playerBoard.Tiles.Find(t => t.refX == clickedTile.refX && t.refY + 1 == clickedTile.refY);
            }

            else if (ori == 2)
            {
                shipPart = playerBoard.Tiles.Find(t => t.refX == clickedTile.refX - 1 && t.refY == clickedTile.refY);
            }

            else if (ori == 3)
            {
                shipPart = playerBoard.Tiles.Find(t => t.refX == clickedTile.refX && t.refY - 1 == clickedTile.refY);
            }

            return shipPart;
        }

        private void SetShipParts(Tile clickedTile)
        {
            Tile shipPart = GetOrientation(clickedTile, 0);

            shipPart.isShipPart = shipPart.isShip = true;
            shipPart.shipType = clickedTile.shipType;
        }

        private void RemoveShipParts(Tile clickedTile)
        {
            Tile shipPart = GetOrientation(clickedTile, -1);

            shipPart.isShipPart = shipPart.isShip = false;
            shipPart.shipType = null;
        }

        private Tile GetShipParts(Tile clickedTile)
        {
            Tile shipPart = playerBoard.Tiles.Find(t => t.refX == clickedTile.refX + 1 && t.refY == clickedTile.refY);

            return shipPart;
        }

        private Tile GetMainShip(Tile clickedTile)
        {
            Tile shipPart = playerBoard.Tiles.Find(t => t.refX == clickedTile.refX - 1 && t.refY == clickedTile.refY);

            return shipPart;
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            if (littleGuyCount == maxLittleGuyCount)
            {
                Form1.ChangeScreen(this, new GameScreen());
            }

            else
            {
                confirmButton.Text = "Not all ships placed!";
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            selectedShip = "remove";

            Button unclickedButton = buttonList.Find(t => t.BackColor == Color.SkyBlue);

            if (unclickedButton != null)
            {
                unclickedButton.BackColor = Color.White;
            }
            removeButton.BackColor = Color.SkyBlue;
        }

        private void twoByOneButton_Click(object sender, EventArgs e)
        {
            selectedShip = "twoByOne";


            Button unclickedButton = buttonList.Find(t => t.BackColor == Color.SkyBlue);
            if (unclickedButton != null)
            {
                unclickedButton.BackColor = Color.White;
            }

            twoByOneButton.BackColor = Color.SkyBlue;
        }

        private void littleGuyButton_Click(object sender, EventArgs e)
        {
            selectedShip = "littleGuy";

            Button unclickedButton = buttonList.Find(t => t.BackColor == Color.SkyBlue);

            if (unclickedButton != null)
            {
                unclickedButton.BackColor = Color.White;
            }

            littleGuyButton.BackColor = Color.SkyBlue;
        }
    }
}
