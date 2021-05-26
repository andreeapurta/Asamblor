
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
            this.generateBinaryFileBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.drawBtn = new System.Windows.Forms.ToolStripMenuItem();
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
            this.MarLbl = new System.Windows.Forms.Label();
            this.MarValueLbl = new System.Windows.Forms.Label();
            this.SbusLbl = new System.Windows.Forms.Label();
            this.SbusValueLbl = new System.Windows.Forms.Label();
            this.DbusLbl = new System.Windows.Forms.Label();
            this.DbusValueLbl = new System.Windows.Forms.Label();
            this.AluLbl = new System.Windows.Forms.Label();
            this.AluValueLbl = new System.Windows.Forms.Label();
            this.RbusLbl = new System.Windows.Forms.Label();
            this.RbusValueLbl = new System.Windows.Forms.Label();
            this.FlagLbl = new System.Windows.Forms.Label();
            this.FlagValueLbl = new System.Windows.Forms.Label();
            this.RegFileLbl = new System.Windows.Forms.Label();
            this.SpLbl = new System.Windows.Forms.Label();
            this.SpValueLbl = new System.Windows.Forms.Label();
            this.TLbl = new System.Windows.Forms.Label();
            this.TValueLbl = new System.Windows.Forms.Label();
            this.PcLbl = new System.Windows.Forms.Label();
            this.PcValueLbl = new System.Windows.Forms.Label();
            this.IvrLbl = new System.Windows.Forms.Label();
            this.IvrValueLbl = new System.Windows.Forms.Label();
            this.AdrLbl = new System.Windows.Forms.Label();
            this.AdrValueLbl = new System.Windows.Forms.Label();
            this.MdrLbl = new System.Windows.Forms.Label();
            this.MdrValueLbl = new System.Windows.Forms.Label();
            this.IrLbl = new System.Windows.Forms.Label();
            this.IrValueLbl = new System.Windows.Forms.Label();
            this.BgcLbl = new System.Windows.Forms.Label();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileBtn,
            this.parseFileBtn,
            this.generateBinaryFileBtn,
            this.drawBtn});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.menu.Size = new System.Drawing.Size(1467, 30);
            this.menu.TabIndex = 0;
            this.menu.Text = "menu";
            this.menu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menu_ItemClicked);
            // 
            // openFileBtn
            // 
            this.openFileBtn.CheckOnClick = true;
            this.openFileBtn.Name = "openFileBtn";
            this.openFileBtn.Size = new System.Drawing.Size(86, 24);
            this.openFileBtn.Text = "Open File";
            // 
            // parseFileBtn
            // 
            this.parseFileBtn.Name = "parseFileBtn";
            this.parseFileBtn.Size = new System.Drawing.Size(84, 24);
            this.parseFileBtn.Text = "Parse File";
            // 
            // generateBinaryFileBtn
            // 
            this.generateBinaryFileBtn.Name = "generateBinaryFileBtn";
            this.generateBinaryFileBtn.Size = new System.Drawing.Size(118, 24);
            this.generateBinaryFileBtn.Text = "Get Binary File";
            // 
            // drawBtn
            // 
            this.drawBtn.Name = "drawBtn";
            this.drawBtn.Size = new System.Drawing.Size(121, 24);
            this.drawBtn.Text = "Show interface";
            // 
            // initialCodeTxtBox
            // 
            this.initialCodeTxtBox.Location = new System.Drawing.Point(14, 85);
            this.initialCodeTxtBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.initialCodeTxtBox.Name = "initialCodeTxtBox";
            this.initialCodeTxtBox.Size = new System.Drawing.Size(158, 237);
            this.initialCodeTxtBox.TabIndex = 1;
            this.initialCodeTxtBox.Text = "";
            // 
            // parsedASMCodeLbl
            // 
            this.parsedASMCodeLbl.AutoSize = true;
            this.parsedASMCodeLbl.Location = new System.Drawing.Point(194, 49);
            this.parsedASMCodeLbl.Name = "parsedASMCodeLbl";
            this.parsedASMCodeLbl.Size = new System.Drawing.Size(126, 20);
            this.parsedASMCodeLbl.TabIndex = 3;
            this.parsedASMCodeLbl.Text = "Parsed ASM Code";
            // 
            // parsedCodeTxtBox
            // 
            this.parsedCodeTxtBox.Location = new System.Drawing.Point(194, 85);
            this.parsedCodeTxtBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.parsedCodeTxtBox.Name = "parsedCodeTxtBox";
            this.parsedCodeTxtBox.Size = new System.Drawing.Size(182, 237);
            this.parsedCodeTxtBox.TabIndex = 4;
            this.parsedCodeTxtBox.Text = "";
            // 
            // completedParseLbl
            // 
            this.completedParseLbl.AutoSize = true;
            this.completedParseLbl.Location = new System.Drawing.Point(2506, -141);
            this.completedParseLbl.Name = "completedParseLbl";
            this.completedParseLbl.Size = new System.Drawing.Size(0, 20);
            this.completedParseLbl.TabIndex = 5;
            // 
            // succededParseLbl
            // 
            this.succededParseLbl.AutoSize = true;
            this.succededParseLbl.ForeColor = System.Drawing.Color.Red;
            this.succededParseLbl.Location = new System.Drawing.Point(307, 49);
            this.succededParseLbl.Name = "succededParseLbl";
            this.succededParseLbl.Size = new System.Drawing.Size(0, 20);
            this.succededParseLbl.TabIndex = 6;
            // 
            // initialASMCodeLbl
            // 
            this.initialASMCodeLbl.AutoSize = true;
            this.initialASMCodeLbl.Location = new System.Drawing.Point(14, 49);
            this.initialASMCodeLbl.Name = "initialASMCodeLbl";
            this.initialASMCodeLbl.Size = new System.Drawing.Size(120, 20);
            this.initialASMCodeLbl.TabIndex = 2;
            this.initialASMCodeLbl.Text = "Initial ASM Code";
            // 
            // openFileErrorLbl
            // 
            this.openFileErrorLbl.AutoSize = true;
            this.openFileErrorLbl.ForeColor = System.Drawing.Color.Red;
            this.openFileErrorLbl.Location = new System.Drawing.Point(2279, -136);
            this.openFileErrorLbl.Name = "openFileErrorLbl";
            this.openFileErrorLbl.Size = new System.Drawing.Size(0, 20);
            this.openFileErrorLbl.TabIndex = 7;
            // 
            // openErrorLbl
            // 
            this.openErrorLbl.AutoSize = true;
            this.openErrorLbl.ForeColor = System.Drawing.Color.Red;
            this.openErrorLbl.Location = new System.Drawing.Point(119, 49);
            this.openErrorLbl.Name = "openErrorLbl";
            this.openErrorLbl.Size = new System.Drawing.Size(0, 20);
            this.openErrorLbl.TabIndex = 8;
            // 
            // binaryTxt
            // 
            this.binaryTxt.Location = new System.Drawing.Point(398, 84);
            this.binaryTxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.binaryTxt.Name = "binaryTxt";
            this.binaryTxt.Size = new System.Drawing.Size(373, 237);
            this.binaryTxt.TabIndex = 9;
            this.binaryTxt.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(398, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Binary Code";
            // 
            // MarLbl
            // 
            this.MarLbl.AutoSize = true;
            this.MarLbl.Location = new System.Drawing.Point(407, 369);
            this.MarLbl.Name = "MarLbl";
            this.MarLbl.Size = new System.Drawing.Size(41, 20);
            this.MarLbl.TabIndex = 11;
            this.MarLbl.Text = "MAR";
            // 
            // MarValueLbl
            // 
            this.MarValueLbl.AutoSize = true;
            this.MarValueLbl.Location = new System.Drawing.Point(468, 369);
            this.MarValueLbl.Name = "MarValueLbl";
            this.MarValueLbl.Size = new System.Drawing.Size(17, 20);
            this.MarValueLbl.TabIndex = 12;
            this.MarValueLbl.Text = "0";
            // 
            // SbusLbl
            // 
            this.SbusLbl.AutoSize = true;
            this.SbusLbl.Location = new System.Drawing.Point(1081, 13);
            this.SbusLbl.Name = "SbusLbl";
            this.SbusLbl.Size = new System.Drawing.Size(44, 20);
            this.SbusLbl.TabIndex = 13;
            this.SbusLbl.Text = "SBUS";
            // 
            // SbusValueLbl
            // 
            this.SbusValueLbl.AutoSize = true;
            this.SbusValueLbl.Location = new System.Drawing.Point(1094, 37);
            this.SbusValueLbl.Name = "SbusValueLbl";
            this.SbusValueLbl.Size = new System.Drawing.Size(17, 20);
            this.SbusValueLbl.TabIndex = 14;
            this.SbusValueLbl.Text = "0";
            // 
            // DbusLbl
            // 
            this.DbusLbl.AutoSize = true;
            this.DbusLbl.Location = new System.Drawing.Point(1164, 13);
            this.DbusLbl.Name = "DbusLbl";
            this.DbusLbl.Size = new System.Drawing.Size(47, 20);
            this.DbusLbl.TabIndex = 15;
            this.DbusLbl.Text = "DBUS";
            // 
            // DbusValueLbl
            // 
            this.DbusValueLbl.AutoSize = true;
            this.DbusValueLbl.Location = new System.Drawing.Point(1178, 37);
            this.DbusValueLbl.Name = "DbusValueLbl";
            this.DbusValueLbl.Size = new System.Drawing.Size(17, 20);
            this.DbusValueLbl.TabIndex = 16;
            this.DbusValueLbl.Text = "0";
            // 
            // AluLbl
            // 
            this.AluLbl.AutoSize = true;
            this.AluLbl.Location = new System.Drawing.Point(1266, 49);
            this.AluLbl.Name = "AluLbl";
            this.AluLbl.Size = new System.Drawing.Size(46, 20);
            this.AluLbl.TabIndex = 17;
            this.AluLbl.Text = "ALU=";
            // 
            // AluValueLbl
            // 
            this.AluValueLbl.AutoSize = true;
            this.AluValueLbl.Location = new System.Drawing.Point(1308, 49);
            this.AluValueLbl.Name = "AluValueLbl";
            this.AluValueLbl.Size = new System.Drawing.Size(17, 20);
            this.AluValueLbl.TabIndex = 18;
            this.AluValueLbl.Text = "0";
            // 
            // RbusLbl
            // 
            this.RbusLbl.AutoSize = true;
            this.RbusLbl.Location = new System.Drawing.Point(1383, 13);
            this.RbusLbl.Name = "RbusLbl";
            this.RbusLbl.Size = new System.Drawing.Size(45, 20);
            this.RbusLbl.TabIndex = 19;
            this.RbusLbl.Text = "RBUS";
            // 
            // RbusValueLbl
            // 
            this.RbusValueLbl.AutoSize = true;
            this.RbusValueLbl.Location = new System.Drawing.Point(1397, 37);
            this.RbusValueLbl.Name = "RbusValueLbl";
            this.RbusValueLbl.Size = new System.Drawing.Size(17, 20);
            this.RbusValueLbl.TabIndex = 20;
            this.RbusValueLbl.Text = "0";
            // 
            // FlagLbl
            // 
            this.FlagLbl.AutoSize = true;
            this.FlagLbl.Location = new System.Drawing.Point(1266, 114);
            this.FlagLbl.Name = "FlagLbl";
            this.FlagLbl.Size = new System.Drawing.Size(53, 20);
            this.FlagLbl.TabIndex = 21;
            this.FlagLbl.Text = "FLAG=";
            // 
            // FlagValueLbl
            // 
            this.FlagValueLbl.AutoSize = true;
            this.FlagValueLbl.Location = new System.Drawing.Point(1317, 114);
            this.FlagValueLbl.Name = "FlagValueLbl";
            this.FlagValueLbl.Size = new System.Drawing.Size(17, 20);
            this.FlagValueLbl.TabIndex = 22;
            this.FlagValueLbl.Text = "0";
            // 
            // RegFileLbl
            // 
            this.RegFileLbl.AutoSize = true;
            this.RegFileLbl.Location = new System.Drawing.Point(1275, 143);
            this.RegFileLbl.Name = "RegFileLbl";
            this.RegFileLbl.Size = new System.Drawing.Size(86, 20);
            this.RegFileLbl.TabIndex = 23;
            this.RegFileLbl.Text = "RegisterFile";
            // 
            // SpLbl
            // 
            this.SpLbl.AutoSize = true;
            this.SpLbl.Location = new System.Drawing.Point(1275, 192);
            this.SpLbl.Name = "SpLbl";
            this.SpLbl.Size = new System.Drawing.Size(35, 20);
            this.SpLbl.TabIndex = 24;
            this.SpLbl.Text = "SP=";
            // 
            // SpValueLbl
            // 
            this.SpValueLbl.AutoSize = true;
            this.SpValueLbl.Location = new System.Drawing.Point(1308, 192);
            this.SpValueLbl.Name = "SpValueLbl";
            this.SpValueLbl.Size = new System.Drawing.Size(17, 20);
            this.SpValueLbl.TabIndex = 25;
            this.SpValueLbl.Text = "0";
            // 
            // TLbl
            // 
            this.TLbl.AutoSize = true;
            this.TLbl.Location = new System.Drawing.Point(1275, 234);
            this.TLbl.Name = "TLbl";
            this.TLbl.Size = new System.Drawing.Size(27, 20);
            this.TLbl.TabIndex = 26;
            this.TLbl.Text = "T=";
            // 
            // TValueLbl
            // 
            this.TValueLbl.AutoSize = true;
            this.TValueLbl.Location = new System.Drawing.Point(1299, 234);
            this.TValueLbl.Name = "TValueLbl";
            this.TValueLbl.Size = new System.Drawing.Size(17, 20);
            this.TValueLbl.TabIndex = 27;
            this.TValueLbl.Text = "0";
            // 
            // PcLbl
            // 
            this.PcLbl.AutoSize = true;
            this.PcLbl.Location = new System.Drawing.Point(1275, 273);
            this.PcLbl.Name = "PcLbl";
            this.PcLbl.Size = new System.Drawing.Size(36, 20);
            this.PcLbl.TabIndex = 28;
            this.PcLbl.Text = "PC=";
            // 
            // PcValueLbl
            // 
            this.PcValueLbl.AutoSize = true;
            this.PcValueLbl.Location = new System.Drawing.Point(1308, 273);
            this.PcValueLbl.Name = "PcValueLbl";
            this.PcValueLbl.Size = new System.Drawing.Size(17, 20);
            this.PcValueLbl.TabIndex = 29;
            this.PcValueLbl.Text = "0";
            // 
            // IvrLbl
            // 
            this.IvrLbl.AutoSize = true;
            this.IvrLbl.Location = new System.Drawing.Point(1275, 317);
            this.IvrLbl.Name = "IvrLbl";
            this.IvrLbl.Size = new System.Drawing.Size(41, 20);
            this.IvrLbl.TabIndex = 30;
            this.IvrLbl.Text = "IVR=";
            // 
            // IvrValueLbl
            // 
            this.IvrValueLbl.AutoSize = true;
            this.IvrValueLbl.Location = new System.Drawing.Point(1317, 317);
            this.IvrValueLbl.Name = "IvrValueLbl";
            this.IvrValueLbl.Size = new System.Drawing.Size(17, 20);
            this.IvrValueLbl.TabIndex = 31;
            this.IvrValueLbl.Text = "0";
            // 
            // AdrLbl
            // 
            this.AdrLbl.AutoSize = true;
            this.AdrLbl.Location = new System.Drawing.Point(1275, 358);
            this.AdrLbl.Name = "AdrLbl";
            this.AdrLbl.Size = new System.Drawing.Size(49, 20);
            this.AdrLbl.TabIndex = 32;
            this.AdrLbl.Text = "ADR=";
            // 
            // AdrValueLbl
            // 
            this.AdrValueLbl.AutoSize = true;
            this.AdrValueLbl.Location = new System.Drawing.Point(1330, 358);
            this.AdrValueLbl.Name = "AdrValueLbl";
            this.AdrValueLbl.Size = new System.Drawing.Size(17, 20);
            this.AdrValueLbl.TabIndex = 33;
            this.AdrValueLbl.Text = "0";
            // 
            // MdrLbl
            // 
            this.MdrLbl.AutoSize = true;
            this.MdrLbl.Location = new System.Drawing.Point(1275, 395);
            this.MdrLbl.Name = "MdrLbl";
            this.MdrLbl.Size = new System.Drawing.Size(52, 20);
            this.MdrLbl.TabIndex = 34;
            this.MdrLbl.Text = "MDR=";
            // 
            // MdrValueLbl
            // 
            this.MdrValueLbl.AutoSize = true;
            this.MdrValueLbl.Location = new System.Drawing.Point(1330, 395);
            this.MdrValueLbl.Name = "MdrValueLbl";
            this.MdrValueLbl.Size = new System.Drawing.Size(17, 20);
            this.MdrValueLbl.TabIndex = 35;
            this.MdrValueLbl.Text = "0";
            // 
            // IrLbl
            // 
            this.IrLbl.AutoSize = true;
            this.IrLbl.Location = new System.Drawing.Point(938, 395);
            this.IrLbl.Name = "IrLbl";
            this.IrLbl.Size = new System.Drawing.Size(32, 20);
            this.IrLbl.TabIndex = 36;
            this.IrLbl.Text = "IR=";
            // 
            // IrValueLbl
            // 
            this.IrValueLbl.AutoSize = true;
            this.IrValueLbl.Location = new System.Drawing.Point(976, 395);
            this.IrValueLbl.Name = "IrValueLbl";
            this.IrValueLbl.Size = new System.Drawing.Size(17, 20);
            this.IrValueLbl.TabIndex = 37;
            this.IrValueLbl.Text = "0";
            // 
            // BgcLbl
            // 
            this.BgcLbl.AutoSize = true;
            this.BgcLbl.Location = new System.Drawing.Point(828, 202);
            this.BgcLbl.Name = "BgcLbl";
            this.BgcLbl.Size = new System.Drawing.Size(111, 20);
            this.BgcLbl.TabIndex = 38;
            this.BgcLbl.Text = "BGC Programat";
            // 
            // AsamblorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1467, 529);
            this.Controls.Add(this.BgcLbl);
            this.Controls.Add(this.IrValueLbl);
            this.Controls.Add(this.IrLbl);
            this.Controls.Add(this.MdrValueLbl);
            this.Controls.Add(this.MdrLbl);
            this.Controls.Add(this.AdrValueLbl);
            this.Controls.Add(this.AdrLbl);
            this.Controls.Add(this.IvrValueLbl);
            this.Controls.Add(this.IvrLbl);
            this.Controls.Add(this.PcValueLbl);
            this.Controls.Add(this.PcLbl);
            this.Controls.Add(this.TValueLbl);
            this.Controls.Add(this.TLbl);
            this.Controls.Add(this.SpValueLbl);
            this.Controls.Add(this.SpLbl);
            this.Controls.Add(this.RegFileLbl);
            this.Controls.Add(this.FlagValueLbl);
            this.Controls.Add(this.FlagLbl);
            this.Controls.Add(this.RbusValueLbl);
            this.Controls.Add(this.RbusLbl);
            this.Controls.Add(this.AluValueLbl);
            this.Controls.Add(this.AluLbl);
            this.Controls.Add(this.DbusValueLbl);
            this.Controls.Add(this.DbusLbl);
            this.Controls.Add(this.SbusValueLbl);
            this.Controls.Add(this.SbusLbl);
            this.Controls.Add(this.MarValueLbl);
            this.Controls.Add(this.MarLbl);
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
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
        private System.Windows.Forms.ToolStripMenuItem generateBinaryFileBtn;
        private System.Windows.Forms.ToolStripMenuItem drawBtn;
        private System.Windows.Forms.Label MarLbl;
        private System.Windows.Forms.Label MarValueLbl;
        private System.Windows.Forms.Label SbusLbl;
        private System.Windows.Forms.Label SbusValueLbl;
        private System.Windows.Forms.Label DbusLbl;
        private System.Windows.Forms.Label DbusValueLbl;
        private System.Windows.Forms.Label AluLbl;
        private System.Windows.Forms.Label AluValueLbl;
        private System.Windows.Forms.Label RbusLbl;
        private System.Windows.Forms.Label RbusValueLbl;
        private System.Windows.Forms.Label FlagLbl;
        private System.Windows.Forms.Label FlagValueLbl;
        private System.Windows.Forms.Label RegFileLbl;
        private System.Windows.Forms.Label SpLbl;
        private System.Windows.Forms.Label SpValueLbl;
        private System.Windows.Forms.Label TLbl;
        private System.Windows.Forms.Label TValueLbl;
        private System.Windows.Forms.Label PcLbl;
        private System.Windows.Forms.Label PcValueLbl;
        private System.Windows.Forms.Label IvrLbl;
        private System.Windows.Forms.Label IvrValueLbl;
        private System.Windows.Forms.Label AdrLbl;
        private System.Windows.Forms.Label AdrValueLbl;
        private System.Windows.Forms.Label MdrLbl;
        private System.Windows.Forms.Label MdrValueLbl;
        private System.Windows.Forms.Label IrLbl;
        private System.Windows.Forms.Label IrValueLbl;
        private System.Windows.Forms.Label BgcLbl;
    }
}

