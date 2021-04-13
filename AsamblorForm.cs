using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Asamblor
{
    public partial class AsamblorForm : Form
    {
        public OperatorRepository operatorRepository;
        public RegisterRepository registerRepository;
        /* List which will store each token (element) read from ASM file */
        private List<String> asmElements = new List<String>();
        private List<String> binaryElements = new List<String>();
        private string fileName = "";

        public AsamblorForm()
        {
            operatorRepository = new OperatorRepository();
            registerRepository = new RegisterRepository();
            InitializeComponent();
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

        private void Menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            openFileBtn.Click += new EventHandler(OpenFile_Clicked);
            parseFileBtn.Click += new EventHandler(ParseFile_Clicked);
            generateBinaryFileBtn.Click += new EventHandler(ShowBinaryCode_Clicked);
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
                /* Create a parser object used for ASM file
                    REMEMBER: this parser can be used for all kind of text files!!!
                 */
                TextFieldParser parser = new TextFieldParser(fileName);
                /* Reinitialize the Text property of OutputTextBox */
                parsedCodeTxtBox.Text = "";
                /* Define delimiters in ASM file */
                String[] delimiters = { ":", ",", " " };

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

        private void GetInstructions()
        {
            int number = 0;
            string offset = "";
            int lineCount = 0;
            List<(string, int)> Labels = new List<(string, int)>();
            List<(string, string, string)> operands = new List<(string, string, string)>();
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
                //verific daca am MOV, r4.. chestii care sunt mai mici de 3  // tot ce e adresare directa
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
                        if (IR != "")
                        {
                            var instruction = "";
                            lineCount += 2;

                            //sau e branch
                            if (offset != "")
                            {

                                instruction = IR;
                                foreach (var label in Labels)
                                {
                                    if (label.Item1.Equals(offset))
                                    {
                                        instruction += CreateOffsetForLabel(lineCount - label.Item2);

                                    }
                                }
                            }

                            if (operands.Count == 1)
                            {
                                // cu un operand
                                instruction = IR + operands[0];

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
                            operands.Clear();
                            PrintInstruction(instruction);
                        }

                        //daca am gasit o instructiunea noua
                        IR = operatorRepository.GetValue(item);
                        if (IR.Contains("b"))
                        {
                            offset = asmElements[i + 1];
                            //sa sara peste urmatorul rand daca o gasit offset
                            i++;
                        }
                    }
                }
                else
                { //instr > 3 PUSH PUSHF sau etichete
                    //i = lineCount
                    if (item.Contains("et_"))
                    {
                        Labels.Add((item, i));
                    }

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
                                    instruction = IR + offset;
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
                            operands.Clear();
                            PrintInstruction(instruction);
                        }

                        IR = operatorRepository.Operators[item];

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
                                operands.Add((split[0].Substring(1, split[0].Length - 1), "11", split[1].Substring(0, split[1].Length - 1)));
                                lineCount += 2;
                            }
                            else
                            {   //[R4]
                                //prima si ultima parannteza
                                operands.Add((item.Substring(1, item.Length - 2), "10", "-"));
                            }
                        }
                    }
                }
            }
        }

        //adresare indexata (R4+1)
        // ADREASRE INDIRECTA(R6)

        //ADD (7) R1,R2 - NU  exista la noi asa cva

        private void PrintInstruction(string instruction)
        {
            binaryTxt.Text += instruction + Environment.NewLine;
        }

        private void ShowBinaryCode_Clicked(object sender, EventArgs e)
        {
            GetInstructions();
        }

        private string CreateOffsetForLabel(int n)
        {
            int k = 8;
            bool neg = false;
            int a = 0;
            if (n < 0)
            {
                neg = true;
                n *= -1;
                n--;
            }
            string num = null;
            string x = null;
            for (int i = 0; i < k; i++)
            {
                if (neg)
                {
                    a = n % 2;
                    if (a == 1) x = string.Concat(Convert.ToString(0), num);
                    else x = string.Concat(Convert.ToString(1), num);
                }
                else
                    x = string.Concat(Convert.ToString(n % 2), num);
                num = x;
                n /= 2;
            }

            return x;
        }

    }
}