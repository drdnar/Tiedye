namespace TiedyeDesktop
{
    partial class SystemStatusDebug
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
            this.masterTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.execTimeLabel = new System.Windows.Forms.Label();
            this.frequencyLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.intsLabel = new System.Windows.Forms.Label();
            this.speedNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.busyTimeLabel = new System.Windows.Forms.Label();
            this.resetBusyTimeButton = new System.Windows.Forms.Button();
            this.masterTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speedNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // masterTableLayoutPanel
            // 
            this.masterTableLayoutPanel.ColumnCount = 3;
            this.masterTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.masterTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.masterTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.masterTableLayoutPanel.Controls.Add(this.label5, 0, 4);
            this.masterTableLayoutPanel.Controls.Add(this.execTimeLabel, 1, 1);
            this.masterTableLayoutPanel.Controls.Add(this.frequencyLabel, 1, 0);
            this.masterTableLayoutPanel.Controls.Add(this.label1, 0, 0);
            this.masterTableLayoutPanel.Controls.Add(this.label2, 0, 1);
            this.masterTableLayoutPanel.Controls.Add(this.label3, 0, 3);
            this.masterTableLayoutPanel.Controls.Add(this.label4, 0, 2);
            this.masterTableLayoutPanel.Controls.Add(this.intsLabel, 1, 3);
            this.masterTableLayoutPanel.Controls.Add(this.speedNumericUpDown, 1, 2);
            this.masterTableLayoutPanel.Controls.Add(this.busyTimeLabel, 1, 4);
            this.masterTableLayoutPanel.Controls.Add(this.resetBusyTimeButton, 2, 4);
            this.masterTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.masterTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.masterTableLayoutPanel.Name = "masterTableLayoutPanel";
            this.masterTableLayoutPanel.RowCount = 6;
            this.masterTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.masterTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.masterTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.masterTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.masterTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.masterTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.masterTableLayoutPanel.Size = new System.Drawing.Size(284, 261);
            this.masterTableLayoutPanel.TabIndex = 0;
            // 
            // execTimeLabel
            // 
            this.execTimeLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.execTimeLabel.AutoSize = true;
            this.execTimeLabel.Location = new System.Drawing.Point(83, 19);
            this.execTimeLabel.Name = "execTimeLabel";
            this.execTimeLabel.Size = new System.Drawing.Size(10, 13);
            this.execTimeLabel.TabIndex = 5;
            this.execTimeLabel.Text = "-";
            // 
            // frequencyLabel
            // 
            this.frequencyLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.frequencyLabel.AutoSize = true;
            this.frequencyLabel.Location = new System.Drawing.Point(83, 2);
            this.frequencyLabel.Name = "frequencyLabel";
            this.frequencyLabel.Size = new System.Drawing.Size(10, 13);
            this.frequencyLabel.TabIndex = 4;
            this.frequencyLabel.Text = "-";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Frequency:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Exec overrun:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Interrupts:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Time scale:";
            // 
            // intsLabel
            // 
            this.intsLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.intsLabel.AutoSize = true;
            this.intsLabel.Location = new System.Drawing.Point(83, 61);
            this.intsLabel.Name = "intsLabel";
            this.intsLabel.Size = new System.Drawing.Size(10, 13);
            this.intsLabel.TabIndex = 6;
            this.intsLabel.Text = "-";
            // 
            // speedNumericUpDown
            // 
            this.speedNumericUpDown.DecimalPlaces = 1;
            this.speedNumericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.speedNumericUpDown.Location = new System.Drawing.Point(83, 37);
            this.speedNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.speedNumericUpDown.Name = "speedNumericUpDown";
            this.speedNumericUpDown.Size = new System.Drawing.Size(66, 20);
            this.speedNumericUpDown.TabIndex = 7;
            this.speedNumericUpDown.ValueChanged += new System.EventHandler(this.speedNumericUpDown_ValueChanged);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Busy time:";
            // 
            // busyTimeLabel
            // 
            this.busyTimeLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.busyTimeLabel.AutoSize = true;
            this.busyTimeLabel.Location = new System.Drawing.Point(83, 83);
            this.busyTimeLabel.Name = "busyTimeLabel";
            this.busyTimeLabel.Size = new System.Drawing.Size(10, 13);
            this.busyTimeLabel.TabIndex = 11;
            this.busyTimeLabel.Text = "-";
            // 
            // resetBusyTimeButton
            // 
            this.resetBusyTimeButton.Location = new System.Drawing.Point(207, 79);
            this.resetBusyTimeButton.Name = "resetBusyTimeButton";
            this.resetBusyTimeButton.Size = new System.Drawing.Size(74, 22);
            this.resetBusyTimeButton.TabIndex = 12;
            this.resetBusyTimeButton.Text = "Reset";
            this.resetBusyTimeButton.UseVisualStyleBackColor = true;
            this.resetBusyTimeButton.Click += new System.EventHandler(this.resetBusyTimeButton_Click);
            // 
            // SystemStatusDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.masterTableLayoutPanel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SystemStatusDebug";
            this.ShowIcon = false;
            this.Text = "System Status";
            this.masterTableLayoutPanel.ResumeLayout(false);
            this.masterTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speedNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel masterTableLayoutPanel;
        private System.Windows.Forms.Label execTimeLabel;
        private System.Windows.Forms.Label frequencyLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label intsLabel;
        private System.Windows.Forms.NumericUpDown speedNumericUpDown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label busyTimeLabel;
        private System.Windows.Forms.Button resetBusyTimeButton;
    }
}