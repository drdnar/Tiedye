namespace TiedyeDesktop
{
    partial class TimersDebug
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
            this.timer1NextTick = new System.Windows.Forms.Label();
            this.timer1Period = new System.Windows.Forms.Label();
            this.timer1Speed = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.timer1InterruptEnableCheckBox = new System.Windows.Forms.CheckBox();
            this.timer1InterruptCheckBox = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.timer2NextTick = new System.Windows.Forms.Label();
            this.timer2Period = new System.Windows.Forms.Label();
            this.timer2Speed = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.timer2InterruptEnableCheckBox = new System.Windows.Forms.CheckBox();
            this.timer2InterruptCheckBox = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.timer1NextTick, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.timer1Period, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.timer1Speed, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(194, 96);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // timer1NextTick
            // 
            this.timer1NextTick.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.timer1NextTick.AutoSize = true;
            this.timer1NextTick.Location = new System.Drawing.Point(68, 48);
            this.timer1NextTick.Name = "timer1NextTick";
            this.timer1NextTick.Size = new System.Drawing.Size(13, 13);
            this.timer1NextTick.TabIndex = 6;
            this.timer1NextTick.Text = "0";
            // 
            // timer1Period
            // 
            this.timer1Period.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.timer1Period.AutoSize = true;
            this.timer1Period.Location = new System.Drawing.Point(68, 26);
            this.timer1Period.Name = "timer1Period";
            this.timer1Period.Size = new System.Drawing.Size(13, 13);
            this.timer1Period.TabIndex = 5;
            this.timer1Period.Text = "0";
            // 
            // timer1Speed
            // 
            this.timer1Speed.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.timer1Speed.AutoSize = true;
            this.timer1Speed.Location = new System.Drawing.Point(68, 4);
            this.timer1Speed.Name = "timer1Speed";
            this.timer1Speed.Size = new System.Drawing.Size(13, 13);
            this.timer1Speed.TabIndex = 4;
            this.timer1Speed.Text = "0";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Speed:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Period:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Next Tick:";
            // 
            // flowLayoutPanel2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel2, 2);
            this.flowLayoutPanel2.Controls.Add(this.timer1InterruptEnableCheckBox);
            this.flowLayoutPanel2.Controls.Add(this.timer1InterruptCheckBox);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 69);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(188, 24);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // timer1InterruptEnableCheckBox
            // 
            this.timer1InterruptEnableCheckBox.AutoSize = true;
            this.timer1InterruptEnableCheckBox.Location = new System.Drawing.Point(3, 3);
            this.timer1InterruptEnableCheckBox.Name = "timer1InterruptEnableCheckBox";
            this.timer1InterruptEnableCheckBox.Size = new System.Drawing.Size(101, 17);
            this.timer1InterruptEnableCheckBox.TabIndex = 0;
            this.timer1InterruptEnableCheckBox.Text = "Interrupt Enable";
            this.timer1InterruptEnableCheckBox.UseVisualStyleBackColor = true;
            this.timer1InterruptEnableCheckBox.CheckedChanged += new System.EventHandler(this.timer1InterruptEnableCheckBox_CheckedChanged);
            // 
            // timer1InterruptCheckBox
            // 
            this.timer1InterruptCheckBox.AutoSize = true;
            this.timer1InterruptCheckBox.Location = new System.Drawing.Point(110, 3);
            this.timer1InterruptCheckBox.Name = "timer1InterruptCheckBox";
            this.timer1InterruptCheckBox.Size = new System.Drawing.Size(65, 17);
            this.timer1InterruptCheckBox.TabIndex = 1;
            this.timer1InterruptCheckBox.Text = "Interrupt";
            this.timer1InterruptCheckBox.UseVisualStyleBackColor = true;
            this.timer1InterruptCheckBox.CheckedChanged += new System.EventHandler(this.timer1InterruptCheckBox_CheckedChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(419, 399);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 115);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Timer 1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Location = new System.Drawing.Point(209, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 115);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Timer 2";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.timer2NextTick, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.timer2Period, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.timer2Speed, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel3, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(194, 96);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // timer2NextTick
            // 
            this.timer2NextTick.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.timer2NextTick.AutoSize = true;
            this.timer2NextTick.Location = new System.Drawing.Point(68, 48);
            this.timer2NextTick.Name = "timer2NextTick";
            this.timer2NextTick.Size = new System.Drawing.Size(13, 13);
            this.timer2NextTick.TabIndex = 6;
            this.timer2NextTick.Text = "0";
            // 
            // timer2Period
            // 
            this.timer2Period.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.timer2Period.AutoSize = true;
            this.timer2Period.Location = new System.Drawing.Point(68, 26);
            this.timer2Period.Name = "timer2Period";
            this.timer2Period.Size = new System.Drawing.Size(13, 13);
            this.timer2Period.TabIndex = 5;
            this.timer2Period.Text = "0";
            // 
            // timer2Speed
            // 
            this.timer2Speed.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.timer2Speed.AutoSize = true;
            this.timer2Speed.Location = new System.Drawing.Point(68, 4);
            this.timer2Speed.Name = "timer2Speed";
            this.timer2Speed.Size = new System.Drawing.Size(13, 13);
            this.timer2Speed.TabIndex = 4;
            this.timer2Speed.Text = "0";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Speed:";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Period:";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Next Tick:";
            // 
            // flowLayoutPanel3
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.flowLayoutPanel3, 2);
            this.flowLayoutPanel3.Controls.Add(this.timer2InterruptEnableCheckBox);
            this.flowLayoutPanel3.Controls.Add(this.timer2InterruptCheckBox);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 69);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(188, 24);
            this.flowLayoutPanel3.TabIndex = 3;
            // 
            // timer2InterruptEnableCheckBox
            // 
            this.timer2InterruptEnableCheckBox.AutoSize = true;
            this.timer2InterruptEnableCheckBox.Location = new System.Drawing.Point(3, 3);
            this.timer2InterruptEnableCheckBox.Name = "timer2InterruptEnableCheckBox";
            this.timer2InterruptEnableCheckBox.Size = new System.Drawing.Size(101, 17);
            this.timer2InterruptEnableCheckBox.TabIndex = 0;
            this.timer2InterruptEnableCheckBox.Text = "Interrupt Enable";
            this.timer2InterruptEnableCheckBox.UseVisualStyleBackColor = true;
            this.timer2InterruptEnableCheckBox.CheckedChanged += new System.EventHandler(this.timer2InterruptEnableCheckBox_CheckedChanged);
            // 
            // timer2InterruptCheckBox
            // 
            this.timer2InterruptCheckBox.AutoSize = true;
            this.timer2InterruptCheckBox.Location = new System.Drawing.Point(110, 3);
            this.timer2InterruptCheckBox.Name = "timer2InterruptCheckBox";
            this.timer2InterruptCheckBox.Size = new System.Drawing.Size(65, 17);
            this.timer2InterruptCheckBox.TabIndex = 1;
            this.timer2InterruptCheckBox.Text = "Interrupt";
            this.timer2InterruptCheckBox.UseVisualStyleBackColor = true;
            this.timer2InterruptCheckBox.CheckedChanged += new System.EventHandler(this.timer2InterruptCheckBox_CheckedChanged);
            // 
            // TimersDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 399);
            this.Controls.Add(this.flowLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TimersDebug";
            this.ShowIcon = false;
            this.Text = "Timers Debug";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label timer1NextTick;
        private System.Windows.Forms.Label timer1Period;
        private System.Windows.Forms.Label timer1Speed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox timer1InterruptEnableCheckBox;
        private System.Windows.Forms.CheckBox timer1InterruptCheckBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label timer2NextTick;
        private System.Windows.Forms.Label timer2Period;
        private System.Windows.Forms.Label timer2Speed;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.CheckBox timer2InterruptEnableCheckBox;
        private System.Windows.Forms.CheckBox timer2InterruptCheckBox;
    }
}