using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMP123_S2016_Lesson9
{
    public partial class Calculator : Form
    {
        public string ActiveOperator { get; set; }

        public string FirstOperand { get; set; }

        public string SecondOperand { get; set; }

        public bool ActiveError { get; set; }

        public bool ActiveDecimal { get; set; }

        public Calculator()
        {
            InitializeComponent();
        }

        private void CalculatorButton_Click(object sender, EventArgs e)
        {
            if (!this.ActiveError)
            {
                // create a reference to the sender object
                // and tell csharp that it is a button
                Button buttonClicked = (Button)sender;

                // check to see if ResultTextBox has a 0 in it
                if (String.Equals(ResultTextBox.Text, "0"))
                {
                    ResultTextBox.Clear();
                }

                // read the string in the TextBox
                string currentResult = ResultTextBox.Text;

                // add the text of the button clicked to the currentResult
                currentResult += buttonClicked.Text;

                // update the ResultTextBox
                ResultTextBox.Text = currentResult;
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            ResultTextBox.Text = "0";
            this.FirstOperand = String.Empty;
            this.SecondOperand = String.Empty;
            this.ActiveOperator = String.Empty;
            this.ActiveError = false;
            this.ActiveDecimal = false;
        }

        private void Operator_Click(object sender, EventArgs e)
        {
            if (!this.ActiveError)
            {
                Button operatorClicked = (Button)sender;

                this.ActiveOperator = operatorClicked.Text;

                this.FirstOperand = ResultTextBox.Text;

                this.ActiveDecimal = false;

                ResultTextBox.Clear();
            }
        }

        private void EqualsButton_Click(object sender, EventArgs e)
        {
            if (!this.ActiveError)
            {
                double firstNumber = 0;
                double secondNumber = 0;
                double result = 0;
                bool divisionByZero = false;

                this.SecondOperand = ResultTextBox.Text;

                if (!String.IsNullOrEmpty(this.FirstOperand))
                {
                    firstNumber = Convert.ToDouble(this.FirstOperand);
                }

                if (!String.IsNullOrEmpty(this.SecondOperand))
                {
                    secondNumber = Convert.ToDouble(this.SecondOperand);
                }

                switch (ActiveOperator)
                {
                    case "+":
                        result = firstNumber + secondNumber;
                        break;
                    case "-":
                        result = firstNumber - secondNumber;
                        break;
                    case "X":
                        result = firstNumber * secondNumber;
                        break;
                    case "/":
                        if (secondNumber == 0)
                        {
                            divisionByZero = true;
                        }
                        else
                        {
                            result = firstNumber / secondNumber;
                        }
                        break;
                }

                if (divisionByZero)
                {
                    ResultTextBox.Text = "DIV by Zero";
                    this.ActiveError = true;
                }
                else
                {
                    this.ActiveDecimal = false;
                    ResultTextBox.Text = result.ToString();
                    if (ResultTextBox.Text.Length > 9)
                    {
                        ResultTextBox.Font = new Font(ResultTextBox.Font.FontFamily, 20);
                        ResultTextBox.Size = new Size(277, 65);
                        ResultTextBox.Multiline = true;
                    }
                }
            }
        }

        private void Calculator_Load(object sender, EventArgs e)
        {
            this.ClearButton_Click(sender, e);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            if (ResultTextBox.Text.Length == 1)
            {
                ResultTextBox.Text = "0";
            }
            else if (ResultTextBox.Text.Length > 1)
            {
                if (ResultTextBox.Text[ResultTextBox.Text.Length - 1] == '.')
                {
                    this.ActiveDecimal = false;
                }

                //ResultTextBox.Text = ResultTextBox.Text.Substring(0, ResultTextBox.Text.Length - 1);
                ResultTextBox.Text = ResultTextBox.Text.Remove(ResultTextBox.Text.Length - 1);
            }
        }

        private void DecimalButton_Click(object sender, EventArgs e)
        {
            if (!this.ActiveError)
            {
                string currentString = String.Empty;

                if (!this.ActiveDecimal)
                {
                    this.ActiveDecimal = true;
                    currentString = ResultTextBox.Text;
                    currentString += ".";
                    ResultTextBox.Clear();
                    ResultTextBox.Text = currentString;
                }
            }
        }
    }
}
