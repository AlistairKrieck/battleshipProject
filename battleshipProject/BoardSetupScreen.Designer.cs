namespace battleshipProject
{
    partial class BoardSetupScreen
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
            this.confirmButton = new System.Windows.Forms.Button();
            this.shipList = new System.Windows.Forms.Label();
            this.littleGuyButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gameTimer
            // 
            this.gameTimer.Enabled = true;
            this.gameTimer.Interval = 20;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // confirmButton
            // 
            this.confirmButton.Location = new System.Drawing.Point(381, 450);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(197, 50);
            this.confirmButton.TabIndex = 0;
            this.confirmButton.Text = "Confirm Positions?";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // shipList
            // 
            this.shipList.BackColor = System.Drawing.Color.White;
            this.shipList.Location = new System.Drawing.Point(16, 16);
            this.shipList.Name = "shipList";
            this.shipList.Size = new System.Drawing.Size(140, 424);
            this.shipList.TabIndex = 1;
            // 
            // littleGuyButton
            // 
            this.littleGuyButton.Location = new System.Drawing.Point(35, 39);
            this.littleGuyButton.Name = "littleGuyButton";
            this.littleGuyButton.Size = new System.Drawing.Size(94, 59);
            this.littleGuyButton.TabIndex = 2;
            this.littleGuyButton.Text = "Little Guy";
            this.littleGuyButton.UseVisualStyleBackColor = true;
            this.littleGuyButton.Click += new System.EventHandler(this.littleGuyButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(35, 366);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(94, 59);
            this.removeButton.TabIndex = 3;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // BoardSetupScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.littleGuyButton);
            this.Controls.Add(this.shipList);
            this.Controls.Add(this.confirmButton);
            this.DoubleBuffered = true;
            this.Name = "BoardSetupScreen";
            this.Size = new System.Drawing.Size(943, 503);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameScreen_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BoardSetupScreen_MouseClick);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Label shipList;
        private System.Windows.Forms.Button littleGuyButton;
        private System.Windows.Forms.Button removeButton;
    }
}
