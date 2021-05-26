using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace Asamblor
{
    public partial class AsamblorForm : Form
    {
        public OperatorRepository operatorRepository;
        public RegisterRepository registerRepository;
        private List<String> asmElements = new List<String>();
        private List<(string, int)> Labels = new List<(string, int)>();
        private string fileName = "";

        public AsamblorForm()
        {
            operatorRepository = new OperatorRepository();
            registerRepository = new RegisterRepository();
            InitializeComponent();
            openFileBtn.Click += new EventHandler(OpenFile_Clicked);
            parseFileBtn.Click += new EventHandler(ParseFile_Clicked);
            generateBinaryFileBtn.Click += new EventHandler(ShowBinaryCode_Clicked);
            drawBtn.Click += new EventHandler(ShowInterface_Clicked);
        }

        public void DisplayResult(Label label)
        {
            var timer = new Timer();
            timer.Interval = 3000;
            label.ForeColor = Color.Red;
            timer.Tick += (s, e) =>
            {
                label.Hide();
                timer.Stop();
            };
            timer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void OpenFile_Clicked(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = false,
                InitialDirectory = Application.StartupPath,
                Filter = "Assembly Files (.asm)|*.asm"
            };
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                fileName = openFileDialog.FileName;
            }

            try
            {
                initialCodeTxtBox.Text = File.ReadAllText(openFileDialog.FileName);
            }
            catch
            {
                openErrorLbl.Text = "-error";
                DisplayResult(openErrorLbl);
            }
        }

        private void ParseFile_Clicked(object sender, EventArgs e)
        {
            int lineCounter = 0;
            try
            {
             
                TextFieldParser parser = new TextFieldParser(fileName);
                parsedCodeTxtBox.Text = "";
                String[] delimiters = { ",", " " };

                /* Specify that the elements in ASM file are delimited by some characters */
                parser.TextFieldType = FieldType.Delimited;
                /* Set-up the specified delimiters */
                parser.SetDelimiters(delimiters);
                /* Parse the entire ASM file based on previous specifications*/
                while (!parser.EndOfData)
                {
                    /* Read an entire line in ASM file
                       and split this line in many strings delimited by delimiters */
                    string[] asmFields = parser.ReadFields();
                    /* Store each string as a single element in the list
                       if this string is not empty */
                    foreach (string s in asmFields)
                    {
                        if (!s.Equals(""))
                        {
                            asmElements.Add(s);
                        }
                    }
                    /* Counting the number of lines stored in ASM file */
                    lineCounter++;
                }

                /* Close the parser */
                parser.Close();
                /* If the file is empty, trigger a new exception which
                   in turn will display an error message */
                if (lineCounter == 0)
                {
                    Exception ex = new Exception("The ASM file is empty!");
                    throw ex;
                }
                else
                {
                    /* Display every token in OutputTextBox */
                    foreach (String s in asmElements)
                    {
                        parsedCodeTxtBox.Text += s + Environment.NewLine;
                    }
                    /* Display an information about the process completion */
                    succededParseLbl.Text = "-completed";
                    DisplayResult(succededParseLbl);
                }
            }
            catch
            {
                succededParseLbl.Text = "-error";
                DisplayResult(succededParseLbl);
            }
        }

        private void FindLabels()
        {
            var lineCount = 0;
            int number = 0;
            for (int i = 0; i < asmElements.Count; i++)
            {
                var item = asmElements[i];
                if (Int32.TryParse(item, out number) || operatorRepository.Operators.ContainsKey(item) || item.Contains("+"))
                {
                    lineCount += 2;
                }
                if (item.Contains("et_") && item.Contains(":"))
                {
                    Labels.Add((item, lineCount));
                }
            }
        }

        private void GetInstructions()
        {
            var lineCount = 0;
            int number = 0;
            string offset = "";
            List<(string, string, string)> operands = new List<(string, string, string)>(); //mereu punem în operand, de ex, mov r4 r5
                                                                                            //R4 operands[0]
                                                                                            //R5 operans[1]
            string IR = "";

            //operands[0]- contine  registru + mad + daca e indexat
            //operands[1] - contine  registru + mas + daca e indexat
            for (int i = 0; i < asmElements.Count; i++)
            {
                var item = asmElements[i];

                //daca pe linie exista un string care  paote fi convertit in numar il adaug in operands - pt adresarea imediata
                if (Int32.TryParse(item, out number))
                {
                    operands.Add(("0000", "00", item));
                    lineCount += 2;
                }
                //verific daca am MOV r3, r4 si chestii care sunt mai mici de 3  // tot ce e adresare directa
                else if (item.Length <= 3)
                {
                    //daca gasim Registru (R5)
                    if (registerRepository.Registers.ContainsKey(item))
                    {
                        operands.Add((registerRepository.Registers[item], "01", "-"));
                    }
                    //daca gasim ca e oppcode
                    else if (operatorRepository.Operators.ContainsKey(item))
                    {
                        //eliberare instructiune daca o gasit oppcode
                        if (IR != "")
                        {
                            var instruction = "";

                            //sau e branch
                            if (offset != "")
                            {
                                instruction = IR;
                                foreach (var label in Labels)
                                {
                                    if (label.Item1.Contains(offset))
                                    {
                                        instruction += CreateBinaryValueForNumber(lineCount - label.Item2, 8);
                                    }
                                }
                                offset = "";
                            }
                            else if (operands.Count == 1)
                            {
                                // cu un operand
                                instruction = IR + operands[0].Item2 + operands[0].Item1;
                            }
                            else if (operands.Count == 2)
                            {
                                //Mov R5,R4 - oppcode + mas+ rs+mad+rd
                                // //operands[0]- contine  registru + mad + daca e indexat
                                //operands[1] - contine  registru + mas + daca e indexat
                                instruction = IR + operands[1].Item2 + operands[1].Item1 + operands[0].Item2 + operands[0].Item1;
                            }
                            else
                            {
                                instruction = IR;
                            }
                            PrintInstruction(instruction);
                            //daca e index sau imediata - adaugi 2 octeti adica codificarea pe biti a valorii
                            foreach (var operand in operands)
                            {
                                if (operand.Item3 != "-")
                                {
                                    var octet = CreateBinaryValueForNumber(Convert.ToInt32(operand.Item3), 16);
                                    PrintInstruction("operand octet:" + octet);
                                }
                            }

                            operands.Clear();
                        }

                        //daca am gasit o instructiunea noua
                        IR = operatorRepository.GetValue(item);
                        lineCount += 2;
                        if (item.Contains("b"))
                        {
                            offset = asmElements[i + 1];
                            //sa sara peste urmatorul rand daca o gasit offset
                            i++;
                        }
                    }
                }
                else
                { //instr > 3 PUSH PUSHF sau etichete
                    if (operatorRepository.Operators.ContainsKey(item))
                    {
                        //reset -
                        if (IR != "")
                        {
                            var instruction = "";
                            lineCount += 2;
                            if (operands.Count == 1)
                            {
                                // cu un operand
                                instruction = IR + operands[0];

                                ////sau e branch
                                if (offset != "")
                                {
                                    instruction = IR;
                                    foreach (var label in Labels)
                                    {
                                        if (label.Item1.Contains(offset))
                                        {
                                            instruction += CreateBinaryValueForNumber(lineCount - label.Item2, 8);
                                        }
                                    }

                                    offset = "";
                                }
                            }
                            else if (operands.Count == 2)
                            {
                                instruction = IR + operands[1].Item2 + operands[1].Item1 + operands[0].Item2 + operands[0].Item1;
                            }
                            else
                            {
                                instruction = IR;
                            }

                            PrintInstruction(instruction);
                            //daca e index sau imediata - adaugi 2 octeti adica codificarea pe biti a valorii
                            foreach (var operand in operands)
                            {
                                if (operand.Item3 != "-")
                                {
                                    var octet = CreateBinaryValueForNumber(Convert.ToInt32(operand.Item3), 16);
                                    PrintInstruction("operand octet: " + octet);
                                }
                            }
                            operands.Clear();
                        }

                        IR = operatorRepository.Operators[item];
                        lineCount += 2;
                        if (item.StartsWith("b"))
                        {
                            offset = asmElements[i + 1];
                            //sa sara peste urmatorul rand daca o gasit offset
                            i++;
                        }
                    }
                    else
                    {
                        if (item.Contains("(") && item.Contains(")"))
                        { //adresare indexata
                            if (item.Contains("+"))
                            {
                                //indexata // in stanga de ex R4 si in dreapta 5
                                var split = item.Split("+");
                                //am scos parantez
                                //MOV R4,[R4+5]
                                // opp + mas + rs + mad + rd
                                // //operands[0]- contine  registru + mad + daca e indexat
                                //operands[1] - contine  registru + mas + daca e indexat
                                operands.Add((registerRepository.Registers[(split[0].Substring(1, split[0].Length - 1))], "11", split[1].Substring(0, split[1].Length - 1)));
                                lineCount += 2;
                            }
                            else
                            {   //[R4]
                                //prima si ultima parannteza
                                operands.Add((registerRepository.Registers[item.Substring(1, item.Length - 2)], "10", "-"));
                            }
                        }
                    }
                }
            }

            if (IR != "")
            {
                var instruction = "";
                //sau e branch
                if (offset != "")
                {
                    instruction = IR;
                    foreach (var label in Labels)
                    {
                        if (label.Item1.Contains(offset))
                        {
                            instruction += CreateBinaryValueForNumber(lineCount - label.Item2, 8);
                        }
                    }
                    offset = "";
                }
                else if (operands.Count == 1)
                {
                    // cu un operand
                    instruction = IR + operands[0].Item2 + operands[0].Item1;
                }
                else if (operands.Count == 2)
                {
                    //Mov R5,R4 - oppcode + mas+ rs+mad+rd
                    // //operands[0]- contine  registru + mad + daca e indexat
                    //operands[1] - contine  registru + mas + daca e indexat
                    instruction = IR + operands[1].Item2 + operands[1].Item1 + operands[0].Item2 + operands[0].Item1;
                }
                else
                {
                    instruction = IR;
                }

                PrintInstruction(instruction);
                //daca e indexata sau imediata - adaugi 2 octeti adica codificarea pe biti a valorii
                foreach (var operand in operands)
                {
                    if (operand.Item3 != "-")
                    {
                        var octet = CreateBinaryValueForNumber(Convert.ToInt32(operand.Item3), 16);
                        PrintInstruction("operand octet:" + octet);
                    }
                }
                operands.Clear();
            }
        }


        //ADD (7) R1,R2 - NU  exista la noi asa cva

        private void PrintInstruction(string instruction)
        {
            binaryTxt.Text += instruction + Environment.NewLine;
        }

        private void ShowBinaryCode_Clicked(object sender, EventArgs e)
        {
            FindLabels();
            GetInstructions();
        }

        private string CreateBinaryValueForNumber(int number, int length)
        {
            return (length > 1 ? CreateBinaryValueForNumber(number >> 1, length - 1) : null) + "01"[number & 1];
        }

        private void ShowInterface_Clicked(object sender, EventArgs e)
        {
            SbusLbl.Visible = true;
            DbusLbl.Visible = true;
            MarLbl.Visible = true;
            AluLbl.Visible = true;
            RbusLbl.Visible = true;
            FlagLbl.Visible = true;
            SpLbl.Visible = true;
            TLbl.Visible = true;
            PcLbl.Visible = true;
            IvrLbl.Visible = true;
            AdrLbl.Visible = true;
            MdrLbl.Visible = true;
            IrLbl.Visible = true;

            IrValueLbl.Visible = true;
            FlagValueLbl.Visible = true;
            AluValueLbl.Visible = true;
            SpValueLbl.Visible = true;
            TValueLbl.Visible = true;
            PcValueLbl.Visible = true;
            IvrValueLbl.Visible = true;
            AdrValueLbl.Visible = true;
            MdrValueLbl.Visible = true;
            SbusValueLbl.Visible = true;
            DbusValueLbl.Visible = true;
            RbusValueLbl.Visible = true;
            MarValueLbl.Visible = true;

            System.Drawing.Pen myPen;
            System.Drawing.Graphics formGraphics = this.CreateGraphics();

            myPen = new System.Drawing.Pen(System.Drawing.Color.Black);



            //  Bloc Microprogramat
            formGraphics.DrawRectangle(myPen, 920, 25, 100, 470);
            //  IR
            formGraphics.DrawRectangle(myPen, 944, 438, 50, 15);


            myPen.Width = 3;
            //  SBUS
            formGraphics.DrawLine(myPen, 1080, 5, 1080, 500);
            //  DBUS
            formGraphics.DrawLine(myPen, 1120, 5, 1120, 500);

            myPen.Width = 1;
            //  ALU
            Point[] ALUpoints = new Point[7];

            ALUpoints[0].X = 1160;
            ALUpoints[0].Y = 10;
            ALUpoints[1].X = 1200;
            ALUpoints[1].Y = 30;
            ALUpoints[2].X = 1200;
            ALUpoints[2].Y = 70;
            ALUpoints[3].X = 1160;
            ALUpoints[3].Y = 90;
            ALUpoints[4].X = 1160;
            ALUpoints[4].Y = 60;
            ALUpoints[5].X = 1170;
            ALUpoints[5].Y = 50;
            ALUpoints[6].X = 1160;
            ALUpoints[6].Y = 40;
            formGraphics.DrawPolygon(myPen, ALUpoints);

            myPen.Width = 3;
            //  RBUS
            formGraphics.DrawLine(myPen, 1320, 5, 1320, 500);

            myPen.Width = 1;

            //  DBUS D(ALU)
            myPen.EndCap = LineCap.ArrowAnchor;
            formGraphics.DrawLine(myPen, 1120, 25, 1160, 25);

            //  SBUS S(ALU)
            formGraphics.DrawLine(myPen, 1080, 75, 1160, 75);

            //  R(ALU) RBUS
            formGraphics.DrawLine(myPen, 1200, 50, 1320, 50);

            myPen.EndCap = LineCap.NoAnchor;

            //  FLAG
            formGraphics.DrawRectangle(myPen, 1155, 110, 50, 15);

            Point[] MUXpoints = new Point[4];

            MUXpoints[0].X = 1235;
            MUXpoints[0].Y = 112;
            MUXpoints[1].X = 1235;
            MUXpoints[1].Y = 123;
            MUXpoints[2].X = 1260;
            MUXpoints[2].Y = 138;
            MUXpoints[3].X = 1260;
            MUXpoints[3].Y = 97;

            formGraphics.DrawPolygon(myPen, MUXpoints);

            // COND
            Point[] CONDpoints = new Point[5];

            CONDpoints[0].X = 1180;
            CONDpoints[0].Y = 80;
            CONDpoints[1].X = 1180;
            CONDpoints[1].Y = 92;
            CONDpoints[2].X = 1280;
            CONDpoints[2].Y = 92;
            CONDpoints[3].X = 1280;
            CONDpoints[3].Y = 110;
            CONDpoints[4].X = 1260;
            CONDpoints[4].Y = 110;

            myPen.EndCap = LineCap.ArrowAnchor;

            formGraphics.DrawLines(myPen, CONDpoints);
            formGraphics.DrawLine(myPen, 1320, 125, 1260, 125);
            formGraphics.DrawLine(myPen, 1235, 117, 1205, 117);

            // FLAG DBUS/SBUS
            formGraphics.DrawLine(myPen, 1155, 122, 1080, 122);
            formGraphics.DrawLine(myPen, 1155, 114, 1120, 114);

            //  REGISTER FILE
            formGraphics.DrawRectangle(myPen, 1155, 150, 50, 45);

            formGraphics.DrawLine(myPen, 1320, 174, 1205, 174);
            formGraphics.DrawLine(myPen, 1155, 160, 1120, 160);
            formGraphics.DrawLine(myPen, 1155, 185, 1080, 185);

            //  SP
            formGraphics.DrawRectangle(myPen, 1155, 215, 50, 15);           // din 45 in 45
            formGraphics.DrawLine(myPen, 1320, 222, 1205, 222);
            formGraphics.DrawLine(myPen, 1155, 219, 1120, 219);
            formGraphics.DrawLine(myPen, 1155, 226, 1080, 226);

            //  T
            formGraphics.DrawRectangle(myPen, 1155, 250, 50, 15);
            formGraphics.DrawLine(myPen, 1320, 257, 1205, 257);
            formGraphics.DrawLine(myPen, 1155, 254, 1120, 254);
            formGraphics.DrawLine(myPen, 1155, 261, 1080, 261);

            //  PC
            formGraphics.DrawRectangle(myPen, 1155, 295, 50, 15);
            formGraphics.DrawLine(myPen, 1320, 303, 1205, 303);
            formGraphics.DrawLine(myPen, 1155, 299, 1120, 299);
            formGraphics.DrawLine(myPen, 1155, 306, 1080, 306);

            //  IVR
            formGraphics.DrawRectangle(myPen, 1155, 340, 50, 15);
            formGraphics.DrawLine(myPen, 1320, 348, 1205, 348);
            formGraphics.DrawLine(myPen, 1155, 344, 1120, 344);
            formGraphics.DrawLine(myPen, 1155, 351, 1080, 351);

            //  ADR
            formGraphics.DrawRectangle(myPen, 1155, 385, 50, 15);
            formGraphics.DrawLine(myPen, 1320, 393, 1205, 393);
            formGraphics.DrawLine(myPen, 1155, 389, 1120, 389);
            formGraphics.DrawLine(myPen, 1155, 396, 1080, 396);

            //  MDR
            formGraphics.DrawRectangle(myPen, 1155, 430, 50, 15);
            formGraphics.DrawLine(myPen, 1155, 434, 1120, 434);
            formGraphics.DrawLine(myPen, 1155, 441, 1080, 441);


            formGraphics.DrawLine(myPen, 995, 441, 1080, 441);
            formGraphics.DrawLine(myPen, 995, 449, 1120, 449);

            Point[] MUXpoints2 = new Point[4];

            MUXpoints2[0].X = 1257;
            MUXpoints2[0].Y = 455;
            MUXpoints2[1].X = 1243;
            MUXpoints2[1].Y = 455;
            MUXpoints2[2].X = 1230;
            MUXpoints2[2].Y = 480;
            MUXpoints2[3].X = 1270;
            MUXpoints2[3].Y = 480;

            formGraphics.DrawPolygon(myPen, MUXpoints2);

            //  MUX MDR
            Point[] MUXMDR = new Point[3];
            MUXMDR[0].X = 1250;
            MUXMDR[0].Y = 455;
            MUXMDR[1].X = 1250;
            MUXMDR[1].Y = 438;
            MUXMDR[2].X = 1205;
            MUXMDR[2].Y = 438;
            formGraphics.DrawLines(myPen, MUXMDR);

            //  RBUS MUX
            Point[] RBUSMUX = new Point[3];
            RBUSMUX[0].X = 1320;
            RBUSMUX[0].Y = 495;
            RBUSMUX[1].X = 1260;
            RBUSMUX[1].Y = 495;
            RBUSMUX[2].X = 1260;
            RBUSMUX[2].Y = 480;

            formGraphics.DrawLines(myPen, RBUSMUX);

            Point[] MUX = new Point[3];
            MUX[0].X = 1180;
            MUX[0].Y = 580;
            MUX[1].X = 1240;
            MUX[1].Y = 580;
            MUX[2].X = 1240;
            MUX[2].Y = 480;
            formGraphics.DrawLines(myPen, MUX);

            Point[] IR = new Point[4];
            IR[0].X = 1180;
            IR[0].Y = 580;
            IR[1].X = 904;
            IR[1].Y = 580;
            IR[2].X = 904;
            IR[2].Y = 445;
            IR[3].X = 944;
            IR[3].Y = 445;
            formGraphics.DrawLines(myPen, IR);

            //  MEMORY
            formGraphics.DrawRectangle(myPen, 1145, 500, 70, 55);

            formGraphics.DrawLine(myPen, 1180, 555, 1180, 580);

            Point[] MDRMEM = new Point[4];
            MDRMEM[0].X = 1133;
            MDRMEM[0].Y = 434;
            MDRMEM[1].X = 1133;
            MDRMEM[1].Y = 490;
            MDRMEM[2].X = 1180;
            MDRMEM[2].Y = 490;
            MDRMEM[3].X = 1180;
            MDRMEM[3].Y = 500;
            formGraphics.DrawLines(myPen, MDRMEM);

            Point[] ADRMEM = new Point[3];
            ADRMEM[0].X = 1100;
            ADRMEM[0].Y = 396;
            ADRMEM[1].X = 1100;
            ADRMEM[1].Y = 540;
            ADRMEM[2].X = 1145;
            ADRMEM[2].Y = 540;
            formGraphics.DrawLines(myPen, ADRMEM);

            float[] dashValues = { 2, 2 };
            myPen.DashPattern = dashValues;

            // dashed lines

            Point[] BGCALU = new Point[5];
            BGCALU[0].X = 1020;
            BGCALU[0].Y = 40;
            BGCALU[1].X = 1030;
            BGCALU[1].Y = 40;
            BGCALU[2].X = 1030;
            BGCALU[2].Y = 3;
            BGCALU[3].X = 1180;
            BGCALU[3].Y = 3;
            BGCALU[4].X = 1180;
            BGCALU[4].Y = 20;
            formGraphics.DrawLines(myPen, BGCALU);

            Point[] BGCFLAG = new Point[3];
            BGCFLAG[0].X = 1020;
            BGCFLAG[0].Y = 100;
            BGCFLAG[1].X = 1175;
            BGCFLAG[1].Y = 100;
            BGCFLAG[2].X = 1175;
            BGCFLAG[2].Y = 110;
            formGraphics.DrawLines(myPen, BGCFLAG);

            Point[] BGCMUX = new Point[3];
            BGCMUX[0].X = 1175;
            BGCMUX[0].Y = 100;
            BGCMUX[1].X = 1243;
            BGCMUX[1].Y = 100;
            BGCMUX[2].X = 1243;
            BGCMUX[2].Y = 107;
            formGraphics.DrawLines(myPen, BGCMUX);

            Point[] BGCREGISTER = new Point[3];
            BGCREGISTER[0].X = 1020;
            BGCREGISTER[0].Y = 140;
            BGCREGISTER[1].X = 1175;
            BGCREGISTER[1].Y = 140;
            BGCREGISTER[2].X = 1175;
            BGCREGISTER[2].Y = 150;
            formGraphics.DrawLines(myPen, BGCREGISTER);

            Point[] BGCSP = new Point[3];
            BGCSP[0].X = 1020;
            BGCSP[0].Y = 205;
            BGCSP[1].X = 1175;
            BGCSP[1].Y = 205;
            BGCSP[2].X = 1175;
            BGCSP[2].Y = 215;
            formGraphics.DrawLines(myPen, BGCSP);                                                                 //45 in 45

            Point[] BGCT = new Point[3];
            BGCT[0].X = 1020;
            BGCT[0].Y = 240;
            BGCT[1].X = 1175;
            BGCT[1].Y = 240;
            BGCT[2].X = 1175;
            BGCT[2].Y = 250;
            formGraphics.DrawLines(myPen, BGCT);

            Point[] BGCPC = new Point[3];
            BGCPC[0].X = 1020;
            BGCPC[0].Y = 285;
            BGCPC[1].X = 1175;
            BGCPC[1].Y = 285;
            BGCPC[2].X = 1175;
            BGCPC[2].Y = 295;
            formGraphics.DrawLines(myPen, BGCPC);

            Point[] BGCIVR = new Point[3];
            BGCIVR[0].X = 1020;
            BGCIVR[0].Y = 330;
            BGCIVR[1].X = 1175;
            BGCIVR[1].Y = 330;
            BGCIVR[2].X = 1175;
            BGCIVR[2].Y = 340;
            formGraphics.DrawLines(myPen, BGCIVR);

            Point[] BGCADR = new Point[3];
            BGCADR[0].X = 1020;
            BGCADR[0].Y = 375;
            BGCADR[1].X = 1175;
            BGCADR[1].Y = 375;
            BGCADR[2].X = 1175;
            BGCADR[2].Y = 385;
            formGraphics.DrawLines(myPen, BGCADR);

            Point[] BGCMDR = new Point[3];
            BGCMDR[0].X = 1020;
            BGCMDR[0].Y = 420;
            BGCMDR[1].X = 1175;
            BGCMDR[1].Y = 420;
            BGCMDR[2].X = 1175;
            BGCMDR[2].Y = 430;
            formGraphics.DrawLines(myPen, BGCMDR);

            formGraphics.DrawLine(myPen, 1020, 465, 1238, 465);

            Point[] BGCMEMORY = new Point[4];
            BGCMEMORY[0].X = 1020;
            BGCMEMORY[0].Y = 480;
            BGCMEMORY[1].X = 1035;
            BGCMEMORY[1].Y = 480;
            BGCMEMORY[2].X = 1035;
            BGCMEMORY[2].Y = 515;
            BGCMEMORY[3].X = 1145;
            BGCMEMORY[3].Y = 515;
            formGraphics.DrawLines(myPen, BGCMEMORY);


            myPen.Dispose();
            formGraphics.Dispose();
        }

        private void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}