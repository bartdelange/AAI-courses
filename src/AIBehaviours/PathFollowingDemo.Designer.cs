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
            this.worldPanel = new DoubleBufferedPanel();
            this.SuspendLayout();
            // 
            // worldPanel
            // 
            this.worldPanel.BackColor = System.Drawing.Color.White;
            this.worldPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.worldPanel.Location = new System.Drawing.Point(283, 3);
            this.worldPanel.Name = "worldPanel";
            this.worldPanel.Size = new System.Drawing.Size(946, 764);
            this.worldPanel.TabIndex = 0;
            this.worldPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.worldPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.WorldPanel_Paint);
            this.worldPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.WorldPanel_MouseClick);

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 770);
            this.Controls.Add(this.worldPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Steering";
            this.ResumeLayout(false);

        }

        #endregion
        private DoubleBufferedPanel worldPanel;
    }
}

