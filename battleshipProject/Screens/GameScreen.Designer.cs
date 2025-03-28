namespace battleshipProject
{
    partial class GameScreen
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.enemyBoardLabel = new System.Windows.Forms.Label();
            this.playerBoard = new System.Windows.Forms.Label();
            this.turnLabel = new System.Windows.Forms.Label();
            this.enemyShipLabel = new System.Windows.Forms.Label();
            this.playerShipLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // gameTimer
            // 
            this.gameTimer.Enabled = true;
            this.gameTimer.Interval = 50;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // enemyBoardLabel
            // 
            this.enemyBoardLabel.AutoSize = true;
            this.enemyBoardLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enemyBoardLabel.ForeColor = System.Drawing.Color.White;
            this.enemyBoardLabel.Location = new System.Drawing.Point(156, 46);
            this.enemyBoardLabel.Name = "enemyBoardLabel";
            this.enemyBoardLabel.Size = new System.Drawing.Size(185, 32);
            this.enemyBoardLabel.TabIndex = 0;
            this.enemyBoardLabel.Text = "Enemy Board";
            // 
            // playerBoard
            // 
            this.playerBoard.AutoSize = true;
            this.playerBoard.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerBoard.ForeColor = System.Drawing.Color.White;
            this.playerBoard.Location = new System.Drawing.Point(917, 46);
            this.playerBoard.Name = "playerBoard";
            this.playerBoard.Size = new System.Drawing.Size(157, 32);
            this.playerBoard.TabIndex = 1;
            this.playerBoard.Text = "Your Board";
            // 
            // turnLabel
            // 
            this.turnLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.turnLabel.ForeColor = System.Drawing.Color.White;
            this.turnLabel.Location = new System.Drawing.Point(519, 46);
            this.turnLabel.Name = "turnLabel";
            this.turnLabel.Size = new System.Drawing.Size(224, 32);
            this.turnLabel.TabIndex = 2;
            this.turnLabel.Text = "...";
            this.turnLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // enemyShipLabel
            // 
            this.enemyShipLabel.AutoSize = true;
            this.enemyShipLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enemyShipLabel.ForeColor = System.Drawing.Color.White;
            this.enemyShipLabel.Location = new System.Drawing.Point(156, 477);
            this.enemyShipLabel.Name = "enemyShipLabel";
            this.enemyShipLabel.Size = new System.Drawing.Size(230, 32);
            this.enemyShipLabel.TabIndex = 3;
            this.enemyShipLabel.Text = "Remaining Ships";
            // 
            // playerShipLabel
            // 
            this.playerShipLabel.AutoSize = true;
            this.playerShipLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerShipLabel.ForeColor = System.Drawing.Color.White;
            this.playerShipLabel.Location = new System.Drawing.Point(886, 477);
            this.playerShipLabel.Name = "playerShipLabel";
            this.playerShipLabel.Size = new System.Drawing.Size(230, 32);
            this.playerShipLabel.TabIndex = 4;
            this.playerShipLabel.Text = "Remaining Ships";
            // 
            // GameScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.Controls.Add(this.playerShipLabel);
            this.Controls.Add(this.enemyShipLabel);
            this.Controls.Add(this.turnLabel);
            this.Controls.Add(this.playerBoard);
            this.Controls.Add(this.enemyBoardLabel);
            this.DoubleBuffered = true;
            this.Name = "GameScreen";
            this.Size = new System.Drawing.Size(1311, 536);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameScreen_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GameScreen_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label enemyBoardLabel;
        private System.Windows.Forms.Label playerBoard;
        private System.Windows.Forms.Label turnLabel;
        private System.Windows.Forms.Label enemyShipLabel;
        private System.Windows.Forms.Label playerShipLabel;
    }
}
