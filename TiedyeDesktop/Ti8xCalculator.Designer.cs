namespace TiedyeDesktop
{
    partial class Ti8xCalculator
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
            this.components = new System.ComponentModel.Container();
            this.screen = new System.Windows.Forms.PictureBox();
            this.cpuTimer = new System.Windows.Forms.Timer(this.components);
            this.screenTimer = new System.Windows.Forms.Timer(this.components);
            this.keypad = new TiedyeDesktop.Keypad();
            this.keyLogTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.screen)).BeginInit();
            this.SuspendLayout();
            // 
            // screen
            // 
            this.screen.Location = new System.Drawing.Point(12, 12);
            this.screen.Name = "screen";
            this.screen.Size = new System.Drawing.Size(320, 240);
            this.screen.TabIndex = 0;
            this.screen.TabStop = false;
            // 
            // cpuTimer
            // 
            this.cpuTimer.Tick += new System.EventHandler(this.cpuTimer_Tick);
            // 
            // screenTimer
            // 
            this.screenTimer.Enabled = true;
            this.screenTimer.Interval = 50;
            this.screenTimer.Tick += new System.EventHandler(this.screenTimer_Tick);
            // 
            // keypad
            // 
            this.keypad.Location = new System.Drawing.Point(12, 259);
            this.keypad.Name = "keypad";
            this.keypad.Size = new System.Drawing.Size(320, 380);
            this.keypad.TabIndex = 1;
            this.keypad.KeyPressed += new System.EventHandler<TiedyeDesktop.Keypad.KeyLocation>(this.keypad_KeyPressed);
            this.keypad.KeyReleased += new System.EventHandler<TiedyeDesktop.Keypad.KeyLocation>(this.keypad_KeyReleased);
            // 
            // keyLogTextBox
            // 
            this.keyLogTextBox.AcceptsReturn = true;
            this.keyLogTextBox.Location = new System.Drawing.Point(351, 12);
            this.keyLogTextBox.Multiline = true;
            this.keyLogTextBox.Name = "keyLogTextBox";
            this.keyLogTextBox.Size = new System.Drawing.Size(241, 627);
            this.keyLogTextBox.TabIndex = 2;
            // 
            // Ti8xCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 651);
            this.Controls.Add(this.keyLogTextBox);
            this.Controls.Add(this.keypad);
            this.Controls.Add(this.screen);
            this.Name = "Ti8xCalculator";
            this.Text = "Calculator";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ti8xCalculator_FormClosed);
            this.Enter += new System.EventHandler(this.Ti8xCalculator_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.screen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox screen;
        private Keypad keypad;
        private System.Windows.Forms.Timer cpuTimer;
        private System.Windows.Forms.Timer screenTimer;
        private System.Windows.Forms.TextBox keyLogTextBox;
    }
}