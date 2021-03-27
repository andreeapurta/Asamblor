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
            parseFileBtn.Click += new EventHandler(ShowBinaryCode_Clicked);
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
            string label;
            string index = "";
            string indirect = "";
            string opr = "";
            string register = "";
            int mightBeIndex = 0;
            List<string> registers = new List<string>();
            registers[0] = "0";
            registers[1] = "0";
            string IR;
            foreach (var item in asmElements)
            {
                label = "";
                IR = "";

                //check if the repositories contain the item, if they don't the variables = ""
                opr = operatorRepository.GetValue(item);
                register = registerRepository.GetValue(item);

                if (operatorRepository.OtherOperators.ContainsKey(item))
                {
                    IR = opr;
                    TryToGenerateBinaryCodeFor1OperandInstructions(IR);
                }
                //for operators
                if (opr != "")
                {
                    IR = opr;
                }

                //a list of 2 elemnts that stores the registers
                //if the array exceeds 2 => list.Clear

                if (register != "")
                {
                    registers.Add(register);
                }

                //checking for labels (the labels start with lbl_)
                if (item.Contains("lbl_"))
                {
                    label = item;
                }
                //pt adresare indexata
                if (item.Contains("(") && item.Contains(")"))
                {
                    if (registerRepository.Contains(item.Split('(', ')')[1]))
                    {
                        indirect = registerRepository.GetValue(item.Split('(', ')')[1]);
                    }
                    else
                    {
                        index = item.Split('(', ')')[1];
                    }

                    mightBeIndex = 1;
                }
                if (opr == "" && register == "" && !item.Contains("lbl_") && index == "")
                {
                    number = Convert.ToInt32(item); //4
                }

                //Mov R4,4 - refers to number
                //Mov R4, R4(4) - refers to adresare indexata
                TryToGenerateBinaryCodeFor2OperandsInstructions(IR, registers[0], registers[1], number, index, indirect, label);
                if (registers.Count == 2 || (registers.Count == 1 && number != 0) || (registers.Count == 1 && index != "") || (registers.Count == 1 && indirect != ""))
                {
                    number = 0;
                    index = "";
                    registers.Clear();
                }
            }
        }

        //Mov R4,5
        //Mov R4,(R5)
        //Eticheta: ADD R1,R2 (7) /// R1<-[R2+7] ?!?!?!
        //ADD R1, R2

        //ADD (7) R1,R2 - NU  exista la noi asa cva

        private void TryToGenerateBinaryCodeFor1OperandInstructions(string operandOpcode)
        {
            binaryElements.Add(operandOpcode);
        }

        private void TryToGenerateBinaryCodeFor2OperandsInstructions(string operandOpcode, string sourceRegister, string destinationRegister, int number, string index, string indirect, string label)
        {
            string binaryNumber = "";
            string mad = "";
            string mas = "";

            //directa ->ADD R4,R5 - mad=mas=01
            if (sourceRegister != "" && destinationRegister != "")
            {
                mad = "01";
                mas = "01";
                binaryElements.Add(operandOpcode + mas + sourceRegister + mad + destinationRegister);
            }

            //imediata -> MOV R4,5 mad=01 (direct) mas =00 (imediat)
            else if (destinationRegister != "" && number != 0)
            {
                binaryNumber = ConvertIntoBinary(number);
                mad = "01";
                mas = "00";
                binaryElements.Add(operandOpcode + mas + binaryNumber + mad + destinationRegister);
            }
            //indirecta -> Mov R4,(R5) mad = 01 (direct) mas=10 (indirect)
            else if (destinationRegister != "" && indirect != "")
            {
                mad = "01";
                mas = "10";
                binaryElements.Add(operandOpcode + mas + indirect + mad + destinationRegister);
            }
            //indirecta -> Mov (R4),R5 mad = 11 (indirect) mas =01 (direct)
            else if (sourceRegister != "" && indirect != "")
            {
                mad = "11";
                mas = "01";
                binaryElements.Add(operandOpcode + mas + indirect + mad + sourceRegister);
            }
            //indexata -> ADD R1,R2 (7) -> mad = 01 (direct) mas=11(indexat) ????????????????????
            else if (sourceRegister != "" && destinationRegister != "" && index != "")
            {
                mad = "01";
                mas = "11";
                binaryElements.Add(operandOpcode + mas + sourceRegister + mad + destinationRegister);
            }
        }

        private string ConvertIntoBinary(int number)
        {
            int k = 16;
            bool neg = false;
            int a = 0;
            if (number < 0)
            {
                neg = true;
                number *= -1;
                number--;
            }
            string num = null;
            string x = null;
            for (int i = 0; i < k; i++)
            {
                if (neg)
                {
                    a = number % 2;
                    if (a == 1) x = string.Concat(Convert.ToString(0), num);
                    else x = string.Concat(Convert.ToString(1), num);
                }
                else
                    x = string.Concat(Convert.ToString(number % 2), num);
                num = x;
                number /= 2;
            }

            return x;
        }

        private void ShowBinaryCode_Clicked(object sender, EventArgs e)
        {
            GetInstructions();
            foreach (String binaryCode in binaryElements)
            {
                binaryTxt.Text += binaryCode + Environment.NewLine;
            }
            /* Display an information about the process completion */
        }
    }
}