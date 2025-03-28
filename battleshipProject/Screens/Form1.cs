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
    public partial class Form1 : Form
    {
        //Commonly used brushes/pens
        public static Pen tilePen = new Pen(Color.White);
        public static SolidBrush shipBrush = new SolidBrush(Color.Black);
        public static SolidBrush missBrush = new SolidBrush(Color.Gray);

        //Dimentions of game boards
        public static int boardWidth = 10;
        public static int boardHeight = 10;
        public static int tileSize = 30;

        public Form1()
        {
            InitializeComponent();

            ChangeScreen(this, new MenuScreen());
        }

        public static void ChangeScreen(object sender, UserControl next)
        {
            Form f;

            if (sender is Form)
            {
                f = (Form)sender;
            }
            else
            {
                UserControl current = (UserControl)sender;
                f = current.FindForm();
                f.Controls.Remove(current);
            }

            next.Location = new Point((f.ClientSize.Width - next.Width) / 2,
                (f.ClientSize.Height - next.Height) / 2);
            f.Controls.Add(next);
            next.Focus();
        }
    }
}
