namespace TiedyeDesktop
{
    partial class MainWindow
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
            this.masterMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newCalculatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cPUDebugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executionHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memoryDebugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.breakPointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hWSchedulerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lCDDebugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keyboardDebugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timersDebugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.masterMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // masterMenuStrip
            // 
            this.masterMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.calculatorToolStripMenuItem,
            this.playToolStripMenuItem});
            this.masterMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.masterMenuStrip.Name = "masterMenuStrip";
            this.masterMenuStrip.Size = new System.Drawing.Size(1264, 24);
            this.masterMenuStrip.TabIndex = 1;
            this.masterMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newCalculatorToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newCalculatorToolStripMenuItem
            // 
            this.newCalculatorToolStripMenuItem.Name = "newCalculatorToolStripMenuItem";
            this.newCalculatorToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.newCalculatorToolStripMenuItem.Text = "&New Calculator";
            this.newCalculatorToolStripMenuItem.Click += new System.EventHandler(this.newCalculatorToolStripMenuItem_Click);
            // 
            // calculatorToolStripMenuItem
            // 
            this.calculatorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cPUDebugToolStripMenuItem,
            this.executionHistoryToolStripMenuItem,
            this.memoryDebugToolStripMenuItem,
            this.breakPointsToolStripMenuItem,
            this.hWSchedulerToolStripMenuItem,
            this.lCDDebugToolStripMenuItem,
            this.keyboardDebugToolStripMenuItem,
            this.timersDebugToolStripMenuItem});
            this.calculatorToolStripMenuItem.Name = "calculatorToolStripMenuItem";
            this.calculatorToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.calculatorToolStripMenuItem.Text = "&Calculator";
            // 
            // cPUDebugToolStripMenuItem
            // 
            this.cPUDebugToolStripMenuItem.Name = "cPUDebugToolStripMenuItem";
            this.cPUDebugToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.cPUDebugToolStripMenuItem.Text = "&CPU Debug";
            this.cPUDebugToolStripMenuItem.Click += new System.EventHandler(this.cPUDebugToolStripMenuItem_Click);
            // 
            // executionHistoryToolStripMenuItem
            // 
            this.executionHistoryToolStripMenuItem.Name = "executionHistoryToolStripMenuItem";
            this.executionHistoryToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.executionHistoryToolStripMenuItem.Text = "E&xecution History";
            this.executionHistoryToolStripMenuItem.Click += new System.EventHandler(this.executionHistoryToolStripMenuItem_Click);
            // 
            // memoryDebugToolStripMenuItem
            // 
            this.memoryDebugToolStripMenuItem.Name = "memoryDebugToolStripMenuItem";
            this.memoryDebugToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.memoryDebugToolStripMenuItem.Text = "&Memory Debug";
            this.memoryDebugToolStripMenuItem.Click += new System.EventHandler(this.memoryDebugToolStripMenuItem_Click);
            // 
            // breakPointsToolStripMenuItem
            // 
            this.breakPointsToolStripMenuItem.Name = "breakPointsToolStripMenuItem";
            this.breakPointsToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.breakPointsToolStripMenuItem.Text = "&Break Points";
            this.breakPointsToolStripMenuItem.Click += new System.EventHandler(this.breakPointsToolStripMenuItem_Click);
            // 
            // hWSchedulerToolStripMenuItem
            // 
            this.hWSchedulerToolStripMenuItem.Name = "hWSchedulerToolStripMenuItem";
            this.hWSchedulerToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.hWSchedulerToolStripMenuItem.Text = "HW &Scheduler";
            this.hWSchedulerToolStripMenuItem.Click += new System.EventHandler(this.hWSchedulerToolStripMenuItem_Click);
            // 
            // lCDDebugToolStripMenuItem
            // 
            this.lCDDebugToolStripMenuItem.Name = "lCDDebugToolStripMenuItem";
            this.lCDDebugToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.lCDDebugToolStripMenuItem.Text = "&LCD Debug";
            this.lCDDebugToolStripMenuItem.Click += new System.EventHandler(this.lCDDebugToolStripMenuItem_Click);
            // 
            // keyboardDebugToolStripMenuItem
            // 
            this.keyboardDebugToolStripMenuItem.Name = "keyboardDebugToolStripMenuItem";
            this.keyboardDebugToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.keyboardDebugToolStripMenuItem.Text = "&Keypad Debug";
            this.keyboardDebugToolStripMenuItem.Click += new System.EventHandler(this.keyboardDebugToolStripMenuItem_Click);
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.playToolStripMenuItem.Text = "&Play";
            this.playToolStripMenuItem.Click += new System.EventHandler(this.playToolStripMenuItem_Click);
            // 
            // timersDebugToolStripMenuItem
            // 
            this.timersDebugToolStripMenuItem.Name = "timersDebugToolStripMenuItem";
            this.timersDebugToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.timersDebugToolStripMenuItem.Text = "&Timers Debug";
            this.timersDebugToolStripMenuItem.Click += new System.EventHandler(this.timersDebugToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 729);
            this.Controls.Add(this.masterMenuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.masterMenuStrip;
            this.Name = "MainWindow";
            this.Text = "Tiedye";
            this.masterMenuStrip.ResumeLayout(false);
            this.masterMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip masterMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newCalculatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calculatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cPUDebugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem memoryDebugToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem breakPointsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hWSchedulerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lCDDebugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keyboardDebugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem executionHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem timersDebugToolStripMenuItem;
    }
}

