
namespace Asamblor
{
    partial class AsamblorForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menu = new System.Windows.Forms.MenuStrip();
            this.openFileBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.parseFileBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.initialCodeTxtBox = new System.Windows.Forms.RichTextBox();
            this.parsedASMCodeLbl = new System.Windows.Forms.Label();
            this.parsedCodeTxtBox = new System.Windows.Forms.RichTextBox();
            this.completedParseLbl = new System.Windows.Forms.Label();
            this.succededParseLbl = new System.Windows.Forms.Label();
            this.initialASMCodeLbl = new System.Windows.Forms.Label();
            this.openFileErrorLbl = new System.Windows.Forms.Label();
            this.openErrorLbl = new System.Windows.Forms.Label();
            this.binaryTxt = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileBtn,
            this.parseFileBtn});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(749, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "menu";
            this.menu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.Menu_ItemClicked);
            // 
            // openFileBtn
            // 
            this.openFileBtn.CheckOnClick = true;
            this.openFileBtn.Name = "openFileBtn";
            this.openFileBtn.Size = new System.Drawing.Size(69, 20);
            this.openFileBtn.Text = "Open File";
            // 
            // parseFileBtn
            // 
            this.parseFileBtn.Name = "parseFileBtn";
            this.parseFileBtn.Size = new System.Drawing.Size(68, 20);
            this.parseFileBtn.Text = "Parse File";
            // 
            // initialCodeTxtBox
            // 
            this.initialCodeTxtBox.Location = new System.Drawing.Point(12, 64);
            this.initialCodeTxtBox.Name = "initialCodeTxtBox";
            this.initialCodeTxtBox.Size = new System.Drawing.Size(149, 179);
            this.initialCodeTxtBox.TabIndex = 1;
            this.initialCodeTxtBox.Text = "";
            // 
            // parsedASMCodeLbl
            // 
            this.parsedASMCodeLbl.AutoSize = true;
            this.parsedASMCodeLbl.Location = new System.Drawing.Point(199, 37);
            this.parsedASMCodeLbl.Name = "parsedASMCodeLbl";
            this.parsedASMCodeLbl.Size = new System.Drawing.Size(101, 15);
            this.parsedASMCodeLbl.TabIndex = 3;
            this.parsedASMCodeLbl.Text = "Parsed ASM Code";
            // 
            // parsedCodeTxtBox
            // 
            this.parsedCodeTxtBox.Location = new System.Drawing.Point(199, 64);
            this.parsedCodeTxtBox.Name = "parsedCodeTxtBox";
            this.parsedCodeTxtBox.Size = new System.Drawing.Size(149, 179);
            this.parsedCodeTxtBox.TabIndex = 4;
            this.parsedCodeTxtBox.Text = "";
            // 
            // completedParseLbl
            // 
            this.completedParseLbl.AutoSize = true;
            this.completedParseLbl.Location = new System.Drawing.Point(2193, -106);
            this.completedParseLbl.Name = "completedParseLbl";
            this.completedParseLbl.Size = new System.Drawing.Size(0, 15);
            this.completedParseLbl.TabIndex = 5;
            // 
            // succededParseLbl
            // 
            this.succededParseLbl.AutoSize = true;
            this.succededParseLbl.ForeColor = System.Drawing.Color.Red;
            this.succededParseLbl.Location = new System.Drawing.Point(298, 37);
            this.succededParseLbl.Name = "succededParseLbl";
            this.succededParseLbl.Size = new System.Drawing.Size(0, 15);
            this.succededParseLbl.TabIndex = 6;
            // 
            // initialASMCodeLbl
            // 
            this.initialASMCodeLbl.AutoSize = true;
            this.initialASMCodeLbl.Location = new System.Drawing.Point(12, 37);
            this.initialASMCodeLbl.Name = "initialASMCodeLbl";
            this.initialASMCodeLbl.Size = new System.Drawing.Size(95, 15);
            this.initialASMCodeLbl.TabIndex = 2;
            this.initialASMCodeLbl.Text = "Initial ASM Code";
            // 
            // openFileErrorLbl
            // 
            this.openFileErrorLbl.AutoSize = true;
            this.openFileErrorLbl.ForeColor = System.Drawing.Color.Red;
            this.openFileErrorLbl.Location = new System.Drawing.Point(1994, -102);
            this.openFileErrorLbl.Name = "openFileErrorLbl";
            this.openFileErrorLbl.Size = new System.Drawing.Size(0, 15);
            this.openFileErrorLbl.TabIndex = 7;
            // 
            // openErrorLbl
            // 
            this.openErrorLbl.AutoSize = true;
            this.openErrorLbl.ForeColor = System.Drawing.Color.Red;
            this.openErrorLbl.Location = new System.Drawing.Point(104, 37);
            this.openErrorLbl.Name = "openErrorLbl";
            this.openErrorLbl.Size = new System.Drawing.Size(0, 15);
            this.openErrorLbl.TabIndex = 8;
            // 
            // binaryTxt
            // 
            this.binaryTxt.Location = new System.Drawing.Point(389, 64);
            this.binaryTxt.Name = "binaryTxt";
            this.binaryTxt.Size = new System.Drawing.Size(195, 179);
            this.binaryTxt.TabIndex = 9;
            this.binaryTxt.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(389, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Binary Code";
            // 
            // AsamblorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(749, 346);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.binaryTxt);
            this.Controls.Add(this.openErrorLbl);
            this.Controls.Add(this.openFileErrorLbl);
            this.Controls.Add(this.succededParseLbl);
            this.Controls.Add(this.completedParseLbl);
            this.Controls.Add(this.parsedCodeTxtBox);
            this.Controls.Add(this.parsedASMCodeLbl);
            this.Controls.Add(this.initialASMCodeLbl);
            this.Controls.Add(this.initialCodeTxtBox);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Name = "AsamblorForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem openFileBtn;
        private System.Windows.Forms.RichTextBox initialCodeTxtBox;
        private System.Windows.Forms.Label parsedASMCodeLbl;
        private System.Windows.Forms.ToolStripMenuItem parseFileBtn;
        private System.Windows.Forms.RichTextBox parsedCodeTxtBox;
        private System.Windows.Forms.Label completedParseLbl;
        private System.Windows.Forms.Label succededParseLbl;
        private System.Windows.Forms.Label initialASMCodeLbl;
        private System.Windows.Forms.Label openFileErrorLbl;
        private System.Windows.Forms.Label openErrorLbl;
        private System.Windows.Forms.RichTextBox binaryTxt;
        private System.Windows.Forms.Label label1;
    }
}

