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

            Pen myPen;
            Graphics formGraphics = this.CreateGraphics();
            myPen = new Pen(Color.Black);

            //Bloc Microprogramat
            formGraphics.DrawRectangle(myPen, 920, 50, 200, 550);

            //IR
            formGraphics.DrawRectangle(myPen, 960, 540, 50, 15);

            myPen.Width = 3;
            //  SBUS
            formGraphics.DrawLine(myPen, 1200, 50, 1200, 610);
            //  DBUS
            formGraphics.DrawLine(myPen, 1260, 50, 1260, 610);
            //  RBUS
            formGraphics.DrawLine(myPen, 1500, 50, 1500, 610);

            myPen.Width = 1;
            //  ALU
            Point[] ALUpoints = new Point[7];

            ALUpoints[0].X = 1330;
            ALUpoints[0].Y = 60;
            ALUpoints[1].X = 1370;
            ALUpoints[1].Y = 80;
            ALUpoints[2].X = 1370;
            ALUpoints[2].Y = 120;
            ALUpoints[3].X = 1330;
            ALUpoints[3].Y = 140;
            ALUpoints[4].X = 1330;
            ALUpoints[4].Y = 110;
            ALUpoints[5].X = 1340;
            ALUpoints[5].Y = 100;
            ALUpoints[6].X = 1330;
            ALUpoints[6].Y = 90;
            formGraphics.DrawPolygon(myPen, ALUpoints);

            myPen.Width = 1;

            //DBUS to ALU
            myPen.EndCap = LineCap.ArrowAnchor;
            formGraphics.DrawLine(myPen, 1260, 75, 1330, 75);

            //SBUS to ALU
            formGraphics.DrawLine(myPen, 1200, 125, 1330, 125);

            //  FLAG
            formGraphics.DrawRectangle(myPen, 1300, 150, 50, 15);

            Point[] MUXpoints = new Point[4];

            MUXpoints[0].X = 1500; //1235
            MUXpoints[0].Y = 150; //112
            MUXpoints[1].X = 1500;
            MUXpoints[1].Y = 160; //123
            MUXpoints[2].X = 1320; //1260
            MUXpoints[2].Y = 170; //138
            MUXpoints[3].X = 1320; //1260
            MUXpoints[3].Y = 105; //97

            //formGraphics.DrawPolygon(myPen, MUXpoints);

            //  REGISTER FILE
            formGraphics.DrawRectangle(myPen, 1300, 200, 50, 45);

            //formGraphics.DrawLine(myPen, 1400, 174, 1300, 174);
            //formGraphics.DrawLine(myPen, 1300, 180, 1300, 180);
            //formGraphics.DrawLine(myPen, 1300, 200, 1080, 200);

            //  SP
            formGraphics.DrawRectangle(myPen, 1300, 280, 50, 15);    //mypen, sus, jos, latime, inaltime     
            //formGraphics.DrawLine(myPen, 1320, 222, 1205, 222);
            //formGraphics.DrawLine(myPen, 1155, 219, 1120, 219);
            //formGraphics.DrawLine(myPen, 1155, 226, 1080, 226);

            //  T
            formGraphics.DrawRectangle(myPen, 1300, 320, 50, 15);
            //formGraphics.DrawLine(myPen, 1320, 257, 1205, 257);
            //formGraphics.DrawLine(myPen, 1155, 254, 1120, 254);
            //formGraphics.DrawLine(myPen, 1155, 261, 1080, 261);

            //  PC
            formGraphics.DrawRectangle(myPen, 1300, 360, 50, 15);
            //formGraphics.DrawLine(myPen, 1320, 303, 1205, 303);
            //formGraphics.DrawLine(myPen, 1155, 299, 1120, 299);
            //formGraphics.DrawLine(myPen, 1155, 306, 1080, 306);

            //  IVR
            formGraphics.DrawRectangle(myPen, 1300, 420, 50, 15);
            //formGraphics.DrawLine(myPen, 1320, 348, 1205, 348);
            //formGraphics.DrawLine(myPen, 1155, 344, 1120, 344);
            //formGraphics.DrawLine(myPen, 1155, 351, 1080, 351);

            //  ADR
            formGraphics.DrawRectangle(myPen, 1300, 460, 50, 15);
            //formGraphics.DrawLine(myPen, 1320, 393, 1205, 393);
            //formGraphics.DrawLine(myPen, 1155, 389, 1120, 389);
            //formGraphics.DrawLine(myPen, 1155, 396, 1080, 396);

            //  MDR
            formGraphics.DrawRectangle(myPen, 1300, 500, 50, 15);
            //formGraphics.DrawLine(myPen, 1155, 434, 1120, 434);
            //formGraphics.DrawLine(myPen, 1155, 441, 1080, 441);



            //formGraphics.DrawLine(myPen, 995, 441, 1080, 441);
            //formGraphics.DrawLine(myPen, 995, 449, 1120, 449);

            //  MEMORY
            formGraphics.DrawRectangle(myPen, 1300, 600, 95, 55);

        }

        private void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void IrLbl_Click(object sender, EventArgs e)
        {

        }

        private void TValueLbl_Click(object sender, EventArgs e)
        {

        }

        private void PcValueLbl_Click(object sender, EventArgs e)
        {

        }
    }
}