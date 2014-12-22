namespace TiedyeDesktop
{
    partial class IoLogDebug
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.traceEnableCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.traceCountUpDown = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.traceCountUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.logTextBox, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.traceEnableCheckBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.traceCountUpDown, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(284, 261);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // logTextBox
            // 
            this.logTextBox.AcceptsReturn = true;
            this.logTextBox.AcceptsTab = true;
            this.tableLayoutPanel1.SetColumnSpan(this.logTextBox, 2);
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextBox.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.logTextBox.Location = new System.Drawing.Point(3, 58);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTextBox.Size = new System.Drawing.Size(278, 200);
            this.logTextBox.TabIndex = 0;
            // 
            // traceEnableCheckBox
            // 
            this.traceEnableCheckBox.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.traceEnableCheckBox, 2);
            this.traceEnableCheckBox.Location = new System.Drawing.Point(3, 3);
            this.traceEnableCheckBox.Name = "traceEnableCheckBox";
            this.traceEnableCheckBox.Size = new System.Drawing.Size(63, 17);
            this.traceEnableCheckBox.TabIndex = 1;
            this.traceEnableCheckBox.Text = "Log I/O";
            this.traceEnableCheckBox.UseVisualStyleBackColor = true;
            this.traceEnableCheckBox.CheckedChanged += new System.EventHandler(this.traceEnableCheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Display Count:";
            // 
            // traceCountUpDown
            // 
            this.traceCountUpDown.Location = new System.Drawing.Point(103, 28);
            this.traceCountUpDown.Name = "traceCountUpDown";
            this.traceCountUpDown.Size = new System.Drawing.Size(86, 20);
            this.traceCountUpDown.TabIndex = 3;
            this.traceCountUpDown.ValueChanged += new System.EventHandler(this.traceCountUpDown_ValueChanged);
            // 
            // IoLogDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IoLogDebug";
            this.ShowIcon = false;
            this.Text = "I/O Log Debug";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.traceCountUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.CheckBox traceEnableCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown traceCountUpDown;
    }
}