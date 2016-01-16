using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TypingOfTheDead_DictionaryCreator
{
    public partial class Form1 : Form
    {
        private string InputFileName, OutputFileName;
        private int minLetters = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            numericUpDown1.InterceptArrowKeys = true;
            numericUpDown1.DecimalPlaces = 0;
            Input.KeyPress += Input_KeyPress;
            this.Text = "Typing of the Dead - Dictionary Creator";
            this.Icon = new Icon("TypingOfTheDead.ico");
        }

        private void Input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') {
                convert(InputFileName);
            }
        }

        private void convert(string fileName)
        {
            string[] tokens = "".Split();
            try
            {
                StreamReader sr = new StreamReader(fileName);
                string inputString = sr.ReadToEnd();
                for (int i = 0; i < inputString.Length; i++) {
                    char c = inputString[i];
                    if (c == '{' || c == '}' || c == '(' || c == ')' ||
                        c == '[' || c == ']' || c == '/' || c == '\\' ||
                        c == '-' || c == '_' || c == ',' || c == '.' ||
                        c == '<' || c == '>' || c == ';' || c == ':' ||
                        c == '|' || c == '%' || c == '&' || c == '¤' ||
                        c == '#' || c == '"' || c == '!' || c == '?' ||
                        c == '´' || c == '`' || c == '*' || c == '\'' ||
                        c == '+' || c == '@' || c == '£' || c == '$' ||
                        c == '1' || c == '2' || c == '3' || c == '4' ||
                        c == '5' || c == '6' || c == '7' || c == '8' ||
                        c == '9' || c == '0' || c == '§' || c == '½' ||
                        c == '^' || c == '¨' || c == '~' || c == '=' ||
                        c == '\n' || c == '\r')
                    {
                        inputString = inputString.Replace(inputString[i], ' ');
                    }
                }
                tokens = inputString.Split();
                string[] outputTokens = new string[tokens.Length];
                for (int i = 0, a = 0; i < tokens.Length; i++) {
                    if (tokens[i] != "" && tokens[i].Length > minLetters) {
                        //tokens = removeValue(tokens, inputString[i].ToString());
                        bool unique = true;
                        for (int j = 0; j < outputTokens.Length; j++)
                        {
                            if (outputTokens[j] == tokens[i].ToLower()) {
                                unique = false;
                            }
                        }
                        if (unique) {
                            outputTokens[a] = tokens[i].ToLower();
                            a++;
                        }
                    }
                }
                Array.Sort(outputTokens, (x, y) => String.Compare(x, y));
                sr.Close();
                StreamWriter sw = new StreamWriter(OutputFileName);
                foreach (string s in outputTokens)
                {
                    if (s != null)
                        sw.WriteLine(s);
                }
                sw.Close();


                MessageBox.Show("Dictionary successfully created!");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public string[] removeValue(string[] input, string value) {
            string[] output = new string[input.Length - 1];
            for (int i = 0, a = 0; i < input.Length - 1; i++) {
                if (input[i] == value)
                    continue;
                output[a] = input[i];
                a++;
            }
            return output;
        }

        private void Output_TextChanged(object sender, EventArgs e)
        {

        }

        private void Input_TextChanged(object sender, EventArgs e)
        {
            InputFileName = Input.Text;
            if (InputFileName.Length > 4 && InputFileName.Substring(InputFileName.Length - 4) == ".txt")
            {
                OutputFileName = InputFileName.Substring(0, InputFileName.Length - 4) + "_output.txt";
                Output.Text = OutputFileName;
            }
            else
            {
                Output.Text = "Invalid input. Are you missing '.txt'?";
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            minLetters = (int)numericUpDown1.Value;
        }

        private void Converte_Click(object sender, EventArgs e)
        {
            convert(InputFileName);
        }
    }
}
