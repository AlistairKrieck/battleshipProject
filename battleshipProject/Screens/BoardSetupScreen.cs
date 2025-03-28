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
        //Defines game board
        public static Grid playerBoard;

        //Hold ship type that player selects in a string
        string selectedShip = "null";

        //Set maximum count to place for each type of ship
        int maxLittleGuyCount = 3;
        int maxTwoByOneCount = 3;

        //Hold current count placed for each type of ship
        int littleGuyCount = 0;
        int twoByOneCount = 0;

        //Stores buttons to be able to efficiently update bg colours
        List<Button> buttonList = new List<Button>();

        public BoardSetupScreen()
        {
            InitializeComponent();
            InitGame();
        }

        public void InitGame()
        {
            //Set start point of the game board
            int x = (this.Width / 2) - (Form1.boardWidth * Form1.tileSize / 2);
            int y = (this.Height / 2) - (Form1.boardHeight * Form1.tileSize / 2);

            //Denines playerBoard as a new grid starting at the defined start point with board dimentions
            playerBoard = new Grid(x, y, Form1.boardWidth, Form1.boardHeight, Form1.tileSize);

            //Adds each button to the list of buttons
            buttonList.Add(littleGuyButton);
            buttonList.Add(removeButton);
            buttonList.Add(twoByOneButton);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
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
            //Find mouse position
            int mouseX = e.X;
            int mouseY = e.Y;

            int tileSize = playerBoard.tileSize;
            Tile clickedTile;

            //Find tile that was clicked
            clickedTile = playerBoard.Tiles.Find(t => mouseX >= t.x && mouseX <= t.x + t.size
                                        && mouseY >= t.y && mouseY <= t.y + t.size);

            //Check if tile exists
            if (e.Button == MouseButtons.Left && clickedTile != null)
            {
                if (selectedShip == "littleGuy" && littleGuyCount < maxLittleGuyCount)
                {
                    if (clickedTile.isShip == false)
                    {
                        //Check if clicked tile isn't already a ship and place a "LittleGuy"
                        clickedTile.isShip = true;
                        clickedTile.shipType = new LittleGuy();
                        clickedTile.shipType.shipParts.Add(clickedTile);

                        //Increase the current number of placed little guys
                        littleGuyCount++;
                    }
                }

                else if (selectedShip == "twoByOne" && twoByOneCount < maxTwoByOneCount)
                {
                    if (clickedTile.isShip == false && Ship.GetOrientation(clickedTile, 0, playerBoard) != null && Ship.GetOrientation(clickedTile, 0, playerBoard).isShip == false)
                    {
                        //Check if clicked tile isn't already a ship and place a "OneByTwo"
                        clickedTile.isShip = true;
                        clickedTile.shipType = new OneByTwoShip();
                        clickedTile.shipType.SetShipParts(clickedTile, playerBoard);

                        //Add both parts of the new ship to shipParts
                        clickedTile.shipType.shipParts.Add(clickedTile);
                        clickedTile.shipType.shipParts.Add(Ship.GetOrientation(clickedTile, 0, playerBoard));

                        //Increase the current number of placed two by ones
                        twoByOneCount++;
                    }
                }

                //Make sure clicked tile exists
                else if (selectedShip == "remove" && clickedTile.isShip == true)
                {
                    //Remove placed ship and lower relevant count
                    if (clickedTile.shipType.name == "littleGuy")
                    {
                        littleGuyCount--;
                    }

                    else if (clickedTile.shipType.name == "twoByOne" && clickedTile.isShipPart == false)
                    {
                        twoByOneCount--;

                        //Remove ships parts
                        clickedTile.shipType.RemoveShipParts(clickedTile, playerBoard);
                    }

                    else if (clickedTile.shipType.name == "twoByOne" && clickedTile.isShipPart == true)
                    {
                        twoByOneCount--;

                        //Remove base tile of the ship
                        Ship.GetOrientation(clickedTile, 2, playerBoard).shipType = null;
                        Ship.GetOrientation(clickedTile, 2, playerBoard).isShipPart = false;
                        Ship.GetOrientation(clickedTile, 2, playerBoard).isShip = false;
                    }

                    //Tell clicked tile that it is no longer a ship
                    clickedTile.isShip = false;
                    clickedTile.isShipPart = false;
                    clickedTile.shipType = null;
                }
            }

            //Make sure clicked tile exists
            else if (e.Button == MouseButtons.Right && clickedTile != null && clickedTile.shipType != null)
            {
                //See if clicked tile is the main body of a ship
                if (clickedTile.isShipPart == false && clickedTile.shipType.name == "twoByOne")
                {
                    //If possible, rotates the ship around its base
                    try
                    {
                        clickedTile.shipType.RotateShip(clickedTile, playerBoard);
                    }
                    catch { }
                }
            }
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            //Check if all ships have been placed
            if (littleGuyCount == maxLittleGuyCount && twoByOneCount == maxTwoByOneCount)
            {
                Form1.ChangeScreen(this, new GameScreen());
            }

            else
            {
                confirmButton.Text = "Not all ships placed!";
            }
        }

        //Select relevent ship
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
