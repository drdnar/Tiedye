namespace TiedyeDesktop
{
    partial class MemoryEditorPanel
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
            this.components = new System.ComponentModel.Container();
            this.cursorTimer = new System.Windows.Forms.Timer(this.components);
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.SuspendLayout();
            // 
            // cursorTimer
            // 
            this.cursorTimer.Enabled = true;
            this.cursorTimer.Interval = 1000;
            this.cursorTimer.Tick += new System.EventHandler(this.cursorTimer_Tick);
            // 
            // vScrollBar
            // 
            this.vScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar.Location = new System.Drawing.Point(133, 0);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(17, 150);
            this.vScrollBar.TabIndex = 0;
            this.vScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
            // 
            // MemoryEditorPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.vScrollBar);
            this.Name = "MemoryEditorPanel";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MemoryEditorPanel_MouseClick);
            this.Resize += new System.EventHandler(this.MemoryEditorPanel_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer cursorTimer;
        private System.Windows.Forms.VScrollBar vScrollBar;
    }
}
