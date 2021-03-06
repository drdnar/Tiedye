﻿namespace TiedyeDesktop
{
    partial class ExecutionHistory
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
            this.traceEnableCheckBox = new System.Windows.Forms.CheckBox();
            this.disassemblyTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.instrCountUpDown = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.bcallLogCheckBox = new System.Windows.Forms.CheckBox();
            this.showExecRadioButton = new System.Windows.Forms.RadioButton();
            this.showBcallRadioButton = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.instrCountUpDown)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.disassemblyTextBox, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.instrCountUpDown, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(734, 411);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // traceEnableCheckBox
            // 
            this.traceEnableCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.traceEnableCheckBox.AutoSize = true;
            this.traceEnableCheckBox.Location = new System.Drawing.Point(3, 3);
            this.traceEnableCheckBox.Name = "traceEnableCheckBox";
            this.traceEnableCheckBox.Size = new System.Drawing.Size(104, 17);
            this.traceEnableCheckBox.TabIndex = 2;
            this.traceEnableCheckBox.Text = "Trace Execution";
            this.traceEnableCheckBox.UseVisualStyleBackColor = true;
            this.traceEnableCheckBox.CheckedChanged += new System.EventHandler(this.traceEnableCheckBox_CheckedChanged);
            // 
            // disassemblyTextBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.disassemblyTextBox, 2);
            this.disassemblyTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.disassemblyTextBox.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.disassemblyTextBox.Location = new System.Drawing.Point(3, 58);
            this.disassemblyTextBox.Multiline = true;
            this.disassemblyTextBox.Name = "disassemblyTextBox";
            this.disassemblyTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.disassemblyTextBox.Size = new System.Drawing.Size(728, 350);
            this.disassemblyTextBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Display Count:";
            // 
            // instrCountUpDown
            // 
            this.instrCountUpDown.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.instrCountUpDown.Location = new System.Drawing.Point(103, 30);
            this.instrCountUpDown.Name = "instrCountUpDown";
            this.instrCountUpDown.Size = new System.Drawing.Size(86, 20);
            this.instrCountUpDown.TabIndex = 6;
            this.instrCountUpDown.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.instrCountUpDown.ValueChanged += new System.EventHandler(this.instrCountUpDown_ValueChanged);
            // 
            // flowLayoutPanel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.traceEnableCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.bcallLogCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.showExecRadioButton);
            this.flowLayoutPanel1.Controls.Add(this.showBcallRadioButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(734, 25);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // bcallLogCheckBox
            // 
            this.bcallLogCheckBox.AutoSize = true;
            this.bcallLogCheckBox.Location = new System.Drawing.Point(113, 3);
            this.bcallLogCheckBox.Name = "bcallLogCheckBox";
            this.bcallLogCheckBox.Size = new System.Drawing.Size(97, 17);
            this.bcallLogCheckBox.TabIndex = 3;
            this.bcallLogCheckBox.Text = "Trace BCALLS";
            this.bcallLogCheckBox.UseVisualStyleBackColor = true;
            this.bcallLogCheckBox.CheckedChanged += new System.EventHandler(this.bcallLogCheckBox_CheckedChanged);
            // 
            // showExecRadioButton
            // 
            this.showExecRadioButton.AutoSize = true;
            this.showExecRadioButton.Checked = true;
            this.showExecRadioButton.Location = new System.Drawing.Point(216, 3);
            this.showExecRadioButton.Name = "showExecRadioButton";
            this.showExecRadioButton.Size = new System.Drawing.Size(134, 17);
            this.showExecRadioButton.TabIndex = 4;
            this.showExecRadioButton.TabStop = true;
            this.showExecRadioButton.Text = "Show execution history";
            this.showExecRadioButton.UseVisualStyleBackColor = true;
            this.showExecRadioButton.CheckedChanged += new System.EventHandler(this.showExecRadioButton_CheckedChanged);
            // 
            // showBcallRadioButton
            // 
            this.showBcallRadioButton.AutoSize = true;
            this.showBcallRadioButton.Location = new System.Drawing.Point(356, 3);
            this.showBcallRadioButton.Name = "showBcallRadioButton";
            this.showBcallRadioButton.Size = new System.Drawing.Size(121, 17);
            this.showBcallRadioButton.TabIndex = 5;
            this.showBcallRadioButton.Text = "Show BCALL history";
            this.showBcallRadioButton.UseVisualStyleBackColor = true;
            // 
            // ExecutionHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 411);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExecutionHistory";
            this.ShowIcon = false;
            this.Text = "Execution History";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.instrCountUpDown)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox traceEnableCheckBox;
        private System.Windows.Forms.TextBox disassemblyTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown instrCountUpDown;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox bcallLogCheckBox;
        private System.Windows.Forms.RadioButton showExecRadioButton;
        private System.Windows.Forms.RadioButton showBcallRadioButton;
    }
}