using AIBehaviours;
using AIBehaviours.Controls;

namespace AIBehaviours
{
    partial class PathFollowingDemo
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
            this.worldPanel = new AIBehaviours.Controls.DoubleBufferedPanel();
            this.SuspendLayout();
            // 
            // worldPanel
            // 
            this.worldPanel.BackColor = System.Drawing.Color.White;
            this.worldPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.worldPanel.Location = new System.Drawing.Point(0, 0);
            this.worldPanel.Margin = new System.Windows.Forms.Padding(6);
            this.worldPanel.Name = "worldPanel";
            this.worldPanel.Size = new System.Drawing.Size(2259, 1422);
            this.worldPanel.TabIndex = 0;
            this.worldPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.WorldPanel_Paint);
            this.worldPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.WorldPanel_MouseClick);
            // 
            // PathFollowing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2259, 1422);
            this.Controls.Add(this.worldPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PathFollowing";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Steering";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PathFollowingDemo_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion
        private DoubleBufferedPanel worldPanel;
    }
}

