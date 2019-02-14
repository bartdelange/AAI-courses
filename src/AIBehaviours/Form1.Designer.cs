using AIBehaviours;
using AIBehaviours.Controls;

namespace AIBehaviours
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
            this.entityOverviewPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.addVehicleButton = new System.Windows.Forms.Button();
            this.entityList = new System.Windows.Forms.ListBox();
            this.removeVehicleButton = new System.Windows.Forms.Button();
            this.worldPanel = new DoubleBufferedPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.entityOverviewPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // entityOverviewPanel
            // 
            this.entityOverviewPanel.AutoScroll = true;
            this.entityOverviewPanel.Controls.Add(this.groupBox1);
            this.entityOverviewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.entityOverviewPanel.Location = new System.Drawing.Point(3, 3);
            this.entityOverviewPanel.Name = "entityOverviewPanel";
            this.entityOverviewPanel.Size = new System.Drawing.Size(274, 764);
            this.entityOverviewPanel.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.addVehicleButton);
            this.groupBox1.Controls.Add(this.entityList);
            this.groupBox1.Controls.Add(this.removeVehicleButton);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(249, 161);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Entities";
            // 
            // addVehicleButton
            // 
            this.addVehicleButton.Location = new System.Drawing.Point(3, 18);
            this.addVehicleButton.Name = "addVehicleButton";
            this.addVehicleButton.Size = new System.Drawing.Size(86, 23);
            this.addVehicleButton.TabIndex = 2;
            this.addVehicleButton.Text = "Add vehicle";
            this.addVehicleButton.UseVisualStyleBackColor = true;
            this.addVehicleButton.Click += new System.EventHandler(this.AddVehicleButton_Click);
            // 
            // entityList
            // 
            this.entityList.FormattingEnabled = true;
            this.entityList.Location = new System.Drawing.Point(6, 47);
            this.entityList.Name = "entityList";
            this.entityList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.entityList.Size = new System.Drawing.Size(237, 95);
            this.entityList.TabIndex = 3;
            // 
            // removeVehicleButton
            // 
            this.removeVehicleButton.Location = new System.Drawing.Point(95, 18);
            this.removeVehicleButton.Name = "removeVehicleButton";
            this.removeVehicleButton.Size = new System.Drawing.Size(108, 23);
            this.removeVehicleButton.TabIndex = 1;
            this.removeVehicleButton.Text = "Remove vehicle";
            this.removeVehicleButton.UseVisualStyleBackColor = true;
            this.removeVehicleButton.Click += new System.EventHandler(this.RemoveVehicleButton_Click);
            // 
            // worldPanel
            // 
            this.worldPanel.BackColor = System.Drawing.Color.White;
            this.worldPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.worldPanel.Location = new System.Drawing.Point(283, 3);
            this.worldPanel.Name = "worldPanel";
            this.worldPanel.Size = new System.Drawing.Size(946, 764);
            this.worldPanel.TabIndex = 0;
            this.worldPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.WorldPanel_Paint);
            this.worldPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.WorldPanel_MouseClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 280F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.worldPanel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.entityOverviewPanel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1232, 770);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 770);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Steering";
            this.entityOverviewPanel.ResumeLayout(false);
            this.entityOverviewPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel entityOverviewPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button addVehicleButton;
        private System.Windows.Forms.Button removeVehicleButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox entityList;
        private DoubleBufferedPanel worldPanel;
    }
}

