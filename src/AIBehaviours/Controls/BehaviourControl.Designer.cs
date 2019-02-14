namespace AIBehaviours
{
    partial class BehaviourControl
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
            this.behaviourNameBox = new System.Windows.Forms.GroupBox();
            this.forceLabel = new System.Windows.Forms.Label();
            this.weightInput = new System.Windows.Forms.NumericUpDown();
            this.behaviourNameBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weightInput)).BeginInit();
            this.SuspendLayout();
            // 
            // behaviourNameBox
            // 
            this.behaviourNameBox.Controls.Add(this.forceLabel);
            this.behaviourNameBox.Controls.Add(this.weightInput);
            this.behaviourNameBox.Location = new System.Drawing.Point(3, 3);
            this.behaviourNameBox.Name = "behaviourNameBox";
            this.behaviourNameBox.Size = new System.Drawing.Size(244, 53);
            this.behaviourNameBox.TabIndex = 0;
            this.behaviourNameBox.TabStop = false;
            this.behaviourNameBox.Text = "behaviourName";
            // 
            // forceLabel
            // 
            this.forceLabel.AutoSize = true;
            this.forceLabel.Location = new System.Drawing.Point(7, 21);
            this.forceLabel.Name = "forceLabel";
            this.forceLabel.Size = new System.Drawing.Size(34, 13);
            this.forceLabel.TabIndex = 10;
            this.forceLabel.Text = "Force";
            // 
            // weightInput
            // 
            this.weightInput.Location = new System.Drawing.Point(81, 19);
            this.weightInput.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.weightInput.Name = "weightInput";
            this.weightInput.Size = new System.Drawing.Size(150, 20);
            this.weightInput.TabIndex = 9;
            this.weightInput.ValueChanged += new System.EventHandler(this.WeightInput_ValueChanged);
            // 
            // BehaviourControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.behaviourNameBox);
            this.Name = "BehaviourControl";
            this.Size = new System.Drawing.Size(267, 72);
            this.behaviourNameBox.ResumeLayout(false);
            this.behaviourNameBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weightInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox behaviourNameBox;
        private System.Windows.Forms.Label forceLabel;
        private System.Windows.Forms.NumericUpDown weightInput;
    }
}
