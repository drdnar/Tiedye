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
            this.keypad = new TiedyeDesktop.Keypad();
            this.cpuTimer = new System.Windows.Forms.Timer(this.components);
            this.screenTimer = new System.Windows.Forms.Timer(this.components);
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
            // keypad
            // 
            this.keypad.Location = new System.Drawing.Point(12, 259);
            this.keypad.Name = "keypad";
            this.keypad.Size = new System.Drawing.Size(320, 380);
            this.keypad.TabIndex = 1;
            this.keypad.KeyPressed += new System.EventHandler<TiedyeDesktop.Keypad.KeyLocation>(this.keypad_KeyPressed);
            this.keypad.KeyReleased += new System.EventHandler<TiedyeDesktop.Keypad.KeyLocation>(this.keypad_KeyReleased);
            // 
            // cpuTimer
            // 
            this.cpuTimer.Interval = 25;
            this.cpuTimer.Tick += new System.EventHandler(this.cpuTimer_Tick);
            // 
            // screenTimer
            // 
            this.screenTimer.Enabled = true;
            this.screenTimer.Interval = 1000;
            this.screenTimer.Tick += new System.EventHandler(this.screenTimer_Tick);
            // 
            // Ti8xCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 651);
            this.Controls.Add(this.keypad);
            this.Controls.Add(this.screen);
            this.Name = "Ti8xCalculator";
            this.Text = "Calculator";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ti8xCalculator_FormClosed);
            this.Enter += new System.EventHandler(this.Ti8xCalculator_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.screen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox screen;
        private Keypad keypad;
        private System.Windows.Forms.Timer cpuTimer;
        private System.Windows.Forms.Timer screenTimer;
    }
}