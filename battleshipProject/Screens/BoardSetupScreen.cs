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
        int maxLittleGuyCount = 5;
        int littleGuyCount = 0;

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

            if (selectedShip == "littleGuy" && littleGuyCount < maxLittleGuyCount && clickedTile != null)
            {
                if (clickedTile.isShip == false)
                {
                    clickedTile.isShip = true;
                    clickedTile.shipType = new LittleGuy();
                    littleGuyCount++;
                }
            }

            else if (clickedTile.isShip == true)
            {
                clickedTile.isShip = false;
                clickedTile.shipType = null;
                littleGuyCount--;
            }
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
            selectedShip = "remove";


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
