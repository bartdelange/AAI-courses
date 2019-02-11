using AIBehaviors;

namespace AIBehaviors
{
    partial class Form1
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
            this.splitBehaviourPanel = new System.Windows.Forms.SplitContainer();
            this.redBehaviourSelect = new BehaviourListBox();
            this.redBehaviourLabel = new System.Windows.Forms.Label();
            this.blueBehaviourSelect = new BehaviourListBox();
            this.blueBehaviourLabel = new System.Windows.Forms.Label();
            this.worldPanel = new DbPanel();
            ((System.ComponentModel.ISupportInitialize)(this.splitBehaviourPanel)).BeginInit();
            this.splitBehaviourPanel.Panel1.SuspendLayout();
            this.splitBehaviourPanel.Panel2.SuspendLayout();
            this.splitBehaviourPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitBehaviourPanel
            // 
            this.splitBehaviourPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitBehaviourPanel.Location = new System.Drawing.Point(1109, 0);
            this.splitBehaviourPanel.Name = "splitBehaviourPanel";
            // 
            // splitBehaviourPanel.Panel1
            // 
            this.splitBehaviourPanel.Panel1.Controls.Add(this.redBehaviourSelect);
            this.splitBehaviourPanel.Panel1.Controls.Add(this.redBehaviourLabel);
            // 
            // splitBehaviourPanel.Panel2
            // 
            this.splitBehaviourPanel.Panel2.Controls.Add(this.blueBehaviourSelect);
            this.splitBehaviourPanel.Panel2.Controls.Add(this.blueBehaviourLabel);
            this.splitBehaviourPanel.Size = new System.Drawing.Size(691, 1110);
            this.splitBehaviourPanel.SplitterDistance = 342;
            this.splitBehaviourPanel.TabIndex = 5;
            // 
            // redBehaviourSelect
            // 
            this.redBehaviourSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.redBehaviourSelect.BackColor = System.Drawing.SystemColors.Window;
            this.redBehaviourSelect.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.redBehaviourSelect.FormattingEnabled = true;
            this.redBehaviourSelect.ItemHeight = 22;
            this.redBehaviourSelect.Location = new System.Drawing.Point(0, 72);
            this.redBehaviourSelect.Name = "redBehaviourSelect";
            this.redBehaviourSelect.Size = new System.Drawing.Size(342, 1038);
            this.redBehaviourSelect.TabIndex = 3;
            this.redBehaviourSelect.SelectedIndexChanged += new System.EventHandler(this.ChangeBehaviour);
            // 
            // redBehaviourLabel
            // 
            this.redBehaviourLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.redBehaviourLabel.Location = new System.Drawing.Point(0, 0);
            this.redBehaviourLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.redBehaviourLabel.Name = "redBehaviourLabel";
            this.redBehaviourLabel.Size = new System.Drawing.Size(342, 69);
            this.redBehaviourLabel.TabIndex = 2;
            this.redBehaviourLabel.Text = "Red Behaviour";
            this.redBehaviourLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // blueBehaviourSelect
            // 
            this.blueBehaviourSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.blueBehaviourSelect.BackColor = System.Drawing.SystemColors.Window;
            this.blueBehaviourSelect.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.blueBehaviourSelect.FormattingEnabled = true;
            this.blueBehaviourSelect.ItemHeight = 22;
            this.blueBehaviourSelect.Location = new System.Drawing.Point(0, 72);
            this.blueBehaviourSelect.Name = "blueBehaviourSelect";
            this.blueBehaviourSelect.Size = new System.Drawing.Size(345, 1038);
            this.blueBehaviourSelect.TabIndex = 5;
            this.blueBehaviourSelect.SelectedIndexChanged += new System.EventHandler(this.ChangeBehaviour);
            // 
            // blueBehaviourLabel
            // 
            this.blueBehaviourLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.blueBehaviourLabel.Location = new System.Drawing.Point(0, 0);
            this.blueBehaviourLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.blueBehaviourLabel.Name = "blueBehaviourLabel";
            this.blueBehaviourLabel.Size = new System.Drawing.Size(345, 69);
            this.blueBehaviourLabel.TabIndex = 4;
            this.blueBehaviourLabel.Text = "Blue behaviour";
            this.blueBehaviourLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // worldPanel
            // 
            this.worldPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.worldPanel.BackColor = System.Drawing.Color.White;
            this.worldPanel.Location = new System.Drawing.Point(0, 0);
            this.worldPanel.Margin = new System.Windows.Forms.Padding(6);
            this.worldPanel.Name = "worldPanel";
            this.worldPanel.Size = new System.Drawing.Size(1100, 1110);
            this.worldPanel.TabIndex = 0;
            this.worldPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.dbPanel1_Paint);
            this.worldPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dbPanel1_MouseClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1801, 1116);
            this.Controls.Add(this.worldPanel);
            this.Controls.Add(this.splitBehaviourPanel);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.Text = "Steering";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.splitBehaviourPanel.Panel1.ResumeLayout(false);
            this.splitBehaviourPanel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitBehaviourPanel)).EndInit();
            this.splitBehaviourPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DbPanel worldPanel;
        private System.Windows.Forms.SplitContainer splitBehaviourPanel;
        private System.Windows.Forms.Label redBehaviourLabel;
        private System.Windows.Forms.Label blueBehaviourLabel;
        private BehaviourListBox redBehaviourSelect;
        private BehaviourListBox blueBehaviourSelect;
    }
}

