namespace TiedyeDesktop
{
    partial class Breakpoints
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.execBpUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.readBpUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.writeBpUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.retBpCheckBox = new System.Windows.Forms.CheckBox();
            this.intBpCheckBox = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.inBpUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.outBpUpDown = new System.Windows.Forms.NumericUpDown();
            this.anyIoCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.execBpUpDown)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.readBpUpDown)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.writeBpUpDown)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inBpUpDown)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outBpUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 67);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Memory";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.flowLayoutPanel3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 76);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(258, 67);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "I/O";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.execBpUpDown);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(75, 40);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Execution";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox6, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(264, 221);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.groupBox3);
            this.flowLayoutPanel1.Controls.Add(this.groupBox4);
            this.flowLayoutPanel1.Controls.Add(this.groupBox5);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(252, 48);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // execBpUpDown
            // 
            this.execBpUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.execBpUpDown.Hexadecimal = true;
            this.execBpUpDown.Location = new System.Drawing.Point(3, 16);
            this.execBpUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.execBpUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.execBpUpDown.Name = "execBpUpDown";
            this.execBpUpDown.Size = new System.Drawing.Size(69, 20);
            this.execBpUpDown.TabIndex = 3;
            this.execBpUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.execBpUpDown.ValueChanged += new System.EventHandler(this.execBpUpDown_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.readBpUpDown);
            this.groupBox4.Location = new System.Drawing.Point(84, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(75, 40);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Read";
            // 
            // readBpUpDown
            // 
            this.readBpUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.readBpUpDown.Hexadecimal = true;
            this.readBpUpDown.Location = new System.Drawing.Point(3, 16);
            this.readBpUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.readBpUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.readBpUpDown.Name = "readBpUpDown";
            this.readBpUpDown.Size = new System.Drawing.Size(69, 20);
            this.readBpUpDown.TabIndex = 3;
            this.readBpUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.readBpUpDown.ValueChanged += new System.EventHandler(this.readBpUpDown_ValueChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.writeBpUpDown);
            this.groupBox5.Location = new System.Drawing.Point(165, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(75, 40);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Write";
            // 
            // writeBpUpDown
            // 
            this.writeBpUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.writeBpUpDown.Hexadecimal = true;
            this.writeBpUpDown.Location = new System.Drawing.Point(3, 16);
            this.writeBpUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.writeBpUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.writeBpUpDown.Name = "writeBpUpDown";
            this.writeBpUpDown.Size = new System.Drawing.Size(69, 20);
            this.writeBpUpDown.TabIndex = 3;
            this.writeBpUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.writeBpUpDown.ValueChanged += new System.EventHandler(this.writeBpUpDown_ValueChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.flowLayoutPanel2);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(3, 149);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(258, 69);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Other";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.retBpCheckBox);
            this.flowLayoutPanel2.Controls.Add(this.intBpCheckBox);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(252, 50);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // retBpCheckBox
            // 
            this.retBpCheckBox.AutoSize = true;
            this.retBpCheckBox.Location = new System.Drawing.Point(3, 3);
            this.retBpCheckBox.Name = "retBpCheckBox";
            this.retBpCheckBox.Size = new System.Drawing.Size(48, 17);
            this.retBpCheckBox.TabIndex = 0;
            this.retBpCheckBox.Text = "RET";
            this.retBpCheckBox.UseVisualStyleBackColor = true;
            this.retBpCheckBox.CheckedChanged += new System.EventHandler(this.retBpCheckBox_CheckedChanged);
            // 
            // intBpCheckBox
            // 
            this.intBpCheckBox.AutoSize = true;
            this.intBpCheckBox.Location = new System.Drawing.Point(57, 3);
            this.intBpCheckBox.Name = "intBpCheckBox";
            this.intBpCheckBox.Size = new System.Drawing.Size(65, 17);
            this.intBpCheckBox.TabIndex = 1;
            this.intBpCheckBox.Text = "Interrupt";
            this.intBpCheckBox.UseVisualStyleBackColor = true;
            this.intBpCheckBox.CheckedChanged += new System.EventHandler(this.intBpCheckBox_CheckedChanged);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.groupBox7);
            this.flowLayoutPanel3.Controls.Add(this.groupBox8);
            this.flowLayoutPanel3.Controls.Add(this.anyIoCheckBox);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(252, 48);
            this.flowLayoutPanel3.TabIndex = 0;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.inBpUpDown);
            this.groupBox7.Location = new System.Drawing.Point(3, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(75, 40);
            this.groupBox7.TabIndex = 3;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "IN";
            // 
            // inBpUpDown
            // 
            this.inBpUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inBpUpDown.Hexadecimal = true;
            this.inBpUpDown.Location = new System.Drawing.Point(3, 16);
            this.inBpUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.inBpUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.inBpUpDown.Name = "inBpUpDown";
            this.inBpUpDown.Size = new System.Drawing.Size(69, 20);
            this.inBpUpDown.TabIndex = 3;
            this.inBpUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.inBpUpDown.ValueChanged += new System.EventHandler(this.inBpUpDown_ValueChanged);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.outBpUpDown);
            this.groupBox8.Location = new System.Drawing.Point(84, 3);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(75, 40);
            this.groupBox8.TabIndex = 4;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "OUT";
            // 
            // outBpUpDown
            // 
            this.outBpUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outBpUpDown.Hexadecimal = true;
            this.outBpUpDown.Location = new System.Drawing.Point(3, 16);
            this.outBpUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.outBpUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.outBpUpDown.Name = "outBpUpDown";
            this.outBpUpDown.Size = new System.Drawing.Size(69, 20);
            this.outBpUpDown.TabIndex = 3;
            this.outBpUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.outBpUpDown.ValueChanged += new System.EventHandler(this.outBpUpDown_ValueChanged);
            // 
            // anyIoCheckBox
            // 
            this.anyIoCheckBox.AutoSize = true;
            this.anyIoCheckBox.Location = new System.Drawing.Point(165, 3);
            this.anyIoCheckBox.Name = "anyIoCheckBox";
            this.anyIoCheckBox.Size = new System.Drawing.Size(63, 17);
            this.anyIoCheckBox.TabIndex = 5;
            this.anyIoCheckBox.Text = "Any I/O";
            this.anyIoCheckBox.UseVisualStyleBackColor = true;
            this.anyIoCheckBox.CheckedChanged += new System.EventHandler(this.anyIoCheckBox_CheckedChanged);
            // 
            // Breakpoints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 221);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(280, 260);
            this.MinimumSize = new System.Drawing.Size(280, 260);
            this.Name = "Breakpoints";
            this.Text = "Breakpoints";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Breakpoints_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.execBpUpDown)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.readBpUpDown)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.writeBpUpDown)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inBpUpDown)).EndInit();
            this.groupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.outBpUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown execBpUpDown;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown readBpUpDown;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown writeBpUpDown;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox retBpCheckBox;
        private System.Windows.Forms.CheckBox intBpCheckBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.NumericUpDown inBpUpDown;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.NumericUpDown outBpUpDown;
        private System.Windows.Forms.CheckBox anyIoCheckBox;
    }
}