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
    public partial class GameOverScreen : UserControl
    {
        public GameOverScreen()
        {
            InitializeComponent();

            if (GameScreen.turn == "playerWon")
            {
                winLabel.Text = "Player Won!";
            }

            else if (GameScreen.turn == "enemyWon")
            {
                winLabel.Text = "Enemy Won!";
            }
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            Form1.ChangeScreen(this, new MenuScreen());
        }
    }
}
