namespace TiedyeDesktop
{
    partial class Keypad
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
            this.keyIdsPicture = new System.Windows.Forms.PictureBox();
            this.keypadPicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.keyIdsPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.keypadPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // keyIdsPicture
            // 
            this.keyIdsPicture.Image = global::TiedyeDesktop.Properties.Resources.layout_myriad_trans_keys;
            this.keyIdsPicture.Location = new System.Drawing.Point(3, 396);
            this.keyIdsPicture.Name = "keyIdsPicture";
            this.keyIdsPicture.Size = new System.Drawing.Size(323, 388);
            this.keyIdsPicture.TabIndex = 1;
            this.keyIdsPicture.TabStop = false;
            // 
            // keypadPicture
            // 
            this.keypadPicture.Image = global::TiedyeDesktop.Properties.Resources.layout_myriad_trans;
            this.keypadPicture.Location = new System.Drawing.Point(0, 0);
            this.keypadPicture.Margin = new System.Windows.Forms.Padding(0);
            this.keypadPicture.Name = "keypadPicture";
            this.keypadPicture.Size = new System.Drawing.Size(326, 387);
            this.keypadPicture.TabIndex = 0;
            this.keypadPicture.TabStop = false;
            this.keypadPicture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Keypad_MouseDown);
            this.keypadPicture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Keypad_MouseUp);
            // 
            // Keypad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.keyIdsPicture);
            this.Controls.Add(this.keypadPicture);
            this.Name = "Keypad";
            this.Size = new System.Drawing.Size(320, 380);
            ((System.ComponentModel.ISupportInitialize)(this.keyIdsPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.keypadPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
	
	private System.Windows.Forms.PictureBox keypadPicture;
        private System.Windows.Forms.PictureBox keyIdsPicture;
    }
}
