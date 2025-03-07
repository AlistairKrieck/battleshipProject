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



        public BoardSetupScreen()
        {
            InitializeComponent();
            InitGame();
        }

        public void InitGame()
        {
            int boardWidth = 10;
            int boardHeight = 10;
            int tileSize = 30;
            int x = (this.Width / 2) - (boardWidth * tileSize / 2);
            int y = (this.Height / 2) - (boardHeight * tileSize / 2);
            playerBoard = new Grid(x, y, boardWidth, boardHeight, tileSize);
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
                    e.Graphics.FillRectangle(Form1.shipBrush, t.x, t.y, t.size, t.size);
                }

                e.Graphics.DrawRectangle(Form1.tilePen, t.x, t.y, t.size, t.size);
            }
        }

        private void littleGuyButton_Click(object sender, EventArgs e)
        {
            selectedShip = "littleGuy";
            littleGuyButton.BackColor = Color.SkyBlue;
            removeButton.BackColor = Color.White;
        }

        private void BoardSetupScreen_MouseClick(object sender, MouseEventArgs e)
        {
            int mouseX = e.X;
            int mouseY = e.Y;
            int tileSize = playerBoard.tileSize;
            Tile clickedTile;

            clickedTile = playerBoard.Tiles.Find(t => mouseX >= t.x && mouseX <= t.x + t.size
                                        && mouseY >= t.y && mouseY <= t.y + t.size);

            if (selectedShip == "littleGuy" && littleGuyCount < maxLittleGuyCount)
            {
                if (clickedTile != null && clickedTile.isShip == false)
                {
                    clickedTile.isShip = true;
                    littleGuyCount++;
                }
            }

            else if (clickedTile.isShip == true)
            {
                if (clickedTile != null && clickedTile.isShip == true)
                {
                    clickedTile.isShip = false;
                    littleGuyCount--;
                }
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
            removeButton.BackColor = Color.SkyBlue;
            littleGuyButton.BackColor = Color.White;
        }
    }
}
