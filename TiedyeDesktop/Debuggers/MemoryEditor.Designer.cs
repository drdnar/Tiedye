namespace TiedyeDesktop
{
    partial class MemoryEditor
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.currentAddressBox = new System.Windows.Forms.TextBox();
            this.pagedAddressBox = new System.Windows.Forms.CheckBox();
            this.exportDialogOpenButton = new System.Windows.Forms.Button();
            this.dataPanel = new TiedyeDesktop.Debuggers.HexEditorPanel();
            this.exportDialogGroupBox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.fileNameTextBox = new System.Windows.Forms.TextBox();
            this.toTextBox = new System.Windows.Forms.TextBox();
            this.fromTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.closeExportDialog = new System.Windows.Forms.Button();
            this.memoryEditorPanel = new TiedyeDesktop.MemoryEditorPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.dataPanel.SuspendLayout();
            this.exportDialogGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dataPanel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(314, 300);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.currentAddressBox);
            this.flowLayoutPanel1.Controls.Add(this.pagedAddressBox);
            this.flowLayoutPanel1.Controls.Add(this.exportDialogOpenButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 268);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(308, 29);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // currentAddressBox
            // 
            this.currentAddressBox.Location = new System.Drawing.Point(3, 3);
            this.currentAddressBox.Name = "currentAddressBox";
            this.currentAddressBox.Size = new System.Drawing.Size(75, 20);
            this.currentAddressBox.TabIndex = 2;
            this.currentAddressBox.Text = "XXX:XXXX";
            // 
            // pagedAddressBox
            // 
            this.pagedAddressBox.AutoSize = true;
            this.pagedAddressBox.Checked = true;
            this.pagedAddressBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.pagedAddressBox.Location = new System.Drawing.Point(84, 3);
            this.pagedAddressBox.Name = "pagedAddressBox";
            this.pagedAddressBox.Size = new System.Drawing.Size(135, 17);
            this.pagedAddressBox.TabIndex = 0;
            this.pagedAddressBox.Text = "Paged Address Display";
            this.pagedAddressBox.UseVisualStyleBackColor = true;
            this.pagedAddressBox.CheckedChanged += new System.EventHandler(this.pagedAddressBox_CheckedChanged);
            // 
            // exportDialogOpenButton
            // 
            this.exportDialogOpenButton.Location = new System.Drawing.Point(225, 3);
            this.exportDialogOpenButton.Name = "exportDialogOpenButton";
            this.exportDialogOpenButton.Size = new System.Drawing.Size(75, 23);
            this.exportDialogOpenButton.TabIndex = 1;
            this.exportDialogOpenButton.Text = "Export";
            this.exportDialogOpenButton.UseVisualStyleBackColor = true;
            this.exportDialogOpenButton.Visible = false;
            // 
            // dataPanel
            // 
            this.dataPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataPanel.Controls.Add(this.exportDialogGroupBox);
            this.dataPanel.Controls.Add(this.memoryEditorPanel);
            this.dataPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataPanel.Location = new System.Drawing.Point(3, 3);
            this.dataPanel.Name = "dataPanel";
            this.dataPanel.Size = new System.Drawing.Size(308, 259);
            this.dataPanel.TabIndex = 1;
            // 
            // exportDialogGroupBox
            // 
            this.exportDialogGroupBox.Controls.Add(this.label3);
            this.exportDialogGroupBox.Controls.Add(this.fileNameTextBox);
            this.exportDialogGroupBox.Controls.Add(this.toTextBox);
            this.exportDialogGroupBox.Controls.Add(this.fromTextBox);
            this.exportDialogGroupBox.Controls.Add(this.button1);
            this.exportDialogGroupBox.Controls.Add(this.label2);
            this.exportDialogGroupBox.Controls.Add(this.label1);
            this.exportDialogGroupBox.Controls.Add(this.closeExportDialog);
            this.exportDialogGroupBox.Location = new System.Drawing.Point(60, 62);
            this.exportDialogGroupBox.Name = "exportDialogGroupBox";
            this.exportDialogGroupBox.Size = new System.Drawing.Size(168, 124);
            this.exportDialogGroupBox.TabIndex = 2;
            this.exportDialogGroupBox.TabStop = false;
            this.exportDialogGroupBox.Text = "Export";
            this.exportDialogGroupBox.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "File Path";
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.Location = new System.Drawing.Point(62, 71);
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.fileNameTextBox.TabIndex = 6;
            // 
            // toTextBox
            // 
            this.toTextBox.Location = new System.Drawing.Point(62, 45);
            this.toTextBox.Name = "toTextBox";
            this.toTextBox.Size = new System.Drawing.Size(100, 20);
            this.toTextBox.TabIndex = 5;
            // 
            // fromTextBox
            // 
            this.fromTextBox.Location = new System.Drawing.Point(62, 19);
            this.fromTextBox.Name = "fromTextBox";
            this.fromTextBox.Size = new System.Drawing.Size(100, 20);
            this.fromTextBox.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 97);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 21);
            this.button1.TabIndex = 4;
            this.button1.Text = "Export";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "To";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "From";
            // 
            // closeExportDialog
            // 
            this.closeExportDialog.Location = new System.Drawing.Point(87, 97);
            this.closeExportDialog.Name = "closeExportDialog";
            this.closeExportDialog.Size = new System.Drawing.Size(75, 21);
            this.closeExportDialog.TabIndex = 0;
            this.closeExportDialog.Text = "Close";
            this.closeExportDialog.UseVisualStyleBackColor = true;
            // 
            // memoryEditorPanel
            // 
            this.memoryEditorPanel.DataBase = 0;
            this.memoryEditorPanel.DataDisplayedBase = 0;
            this.memoryEditorPanel.DataLength = 0;
            this.memoryEditorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoryEditorPanel.Location = new System.Drawing.Point(0, 0);
            this.memoryEditorPanel.Name = "memoryEditorPanel";
            this.memoryEditorPanel.PagedAddress = false;
            this.memoryEditorPanel.PageDisplayOffset = 0;
            this.memoryEditorPanel.PageSize = 16384;
            this.memoryEditorPanel.SixteenBitAddresses = false;
            this.memoryEditorPanel.Size = new System.Drawing.Size(304, 255);
            this.memoryEditorPanel.TabIndex = 3;
            this.memoryEditorPanel.TopLine = 0;
            this.memoryEditorPanel.CursorMove += new System.EventHandler(this.memoryEditorPanel_CursorMove);
            // 
            // MemoryEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "MemoryEditor";
            this.Size = new System.Drawing.Size(314, 300);
            this.Resize += new System.EventHandler(this.MemoryEditor_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.dataPanel.ResumeLayout(false);
            this.exportDialogGroupBox.ResumeLayout(false);
            this.exportDialogGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox pagedAddressBox;
        private System.Windows.Forms.Button exportDialogOpenButton;
        private System.Windows.Forms.GroupBox exportDialogGroupBox;
        private System.Windows.Forms.TextBox toTextBox;
        private System.Windows.Forms.TextBox fromTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button closeExportDialog;
        private Debuggers.HexEditorPanel dataPanel;
        private System.Windows.Forms.TextBox currentAddressBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox fileNameTextBox;
        private MemoryEditorPanel memoryEditorPanel;
    }
}
