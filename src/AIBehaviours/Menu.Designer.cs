namespace AIBehaviours
{
    partial class Menu
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.header = new System.Windows.Forms.Label();
            this.playGround = new System.Windows.Forms.Button();
            this.pathFinding = new System.Windows.Forms.Button();
            this.menuPanel = new System.Windows.Forms.Panel();
            this.menuPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // header
            // 
            this.header.Dock = System.Windows.Forms.DockStyle.Top;
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(317, 60);
            this.header.TabIndex = 0;
            this.header.Text = "Chose a simulation";
            this.header.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // playGround
            // 
            this.playGround.Dock = System.Windows.Forms.DockStyle.Top;
            this.playGround.Location = new System.Drawing.Point(0, 120);
            this.playGround.Name = "playGround";
            this.playGround.Size = new System.Drawing.Size(317, 60);
            this.playGround.TabIndex = 1;
            this.playGround.Text = "Play ground";
            this.playGround.UseVisualStyleBackColor = true;
            this.playGround.Click += new System.EventHandler(this.playGround_Click);
            // 
            // pathFinding
            // 
            this.pathFinding.Dock = System.Windows.Forms.DockStyle.Top;
            this.pathFinding.Location = new System.Drawing.Point(0, 60);
            this.pathFinding.Name = "pathFinding";
            this.pathFinding.Size = new System.Drawing.Size(317, 60);
            this.pathFinding.TabIndex = 2;
            this.pathFinding.Text = "Path finding";
            this.pathFinding.UseVisualStyleBackColor = true;
            this.pathFinding.Click += new System.EventHandler(this.pathFinding_Click);
            // 
            // menuPanel
            // 
            this.menuPanel.Controls.Add(this.playGround);
            this.menuPanel.Controls.Add(this.pathFinding);
            this.menuPanel.Controls.Add(this.header);
            this.menuPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuPanel.Location = new System.Drawing.Point(0, 0);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Size = new System.Drawing.Size(317, 269);
            this.menuPanel.TabIndex = 3;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 269);
            this.Controls.Add(this.menuPanel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Menu";
            this.Text = "Menu";
            this.menuPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label header;
        private System.Windows.Forms.Button playGround;
        private System.Windows.Forms.Button pathFinding;
        private System.Windows.Forms.Panel menuPanel;
    }
}