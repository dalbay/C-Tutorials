using System;
using System.Collections;
using System.Windows.Forms;

namespace ExceptionHandling
{
    public partial class Form1 : Form
    {
        static ArrayList scoreCard;
        public Form1()
        {
            InitializeComponent();
            // initialize the scoreCard ArrayList
            scoreCard = new ArrayList();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                lblResult.Text = "";
                var score = txtScore.Text;
                if (score == string.Empty)   // checking input not empty
                {
                    errorProviderEmpty.SetError(txtScore, "Please provide a Score");
                    lblResult.Text = "No score added!";
                    throw new NullReferenceException("Null Reference Exception! \nInput Score Value Expected.");
                }
                else if (Convert.ToInt32(score) > 100) // checking max input value
                {
                    errorProviderEmpty.SetError(txtScore, "Maximum Score is 100");
                    lblResult.Text = "Add a value between 0 - 100 \nNo score added!";
                }
                else
                {
                    errorProviderEmpty.SetError(txtScore, "");
                    scoreCard.Add((Convert.ToUInt32(txtScore.Text))); // checking positive number
                    lblResult.Text = txtScore.Text + " was succesfully added to your scorecard.";
                }

            }
            catch (NotImplementedException e_notImplemented)
            {
                lblResult.Text = e_notImplemented.ToString();
                errorProviderEmpty.SetError(txtScore, "Internal Error!");
            }
            catch (FormatException e_format)
            {
                lblResult.Text = e_format.Message.ToString() + "\nAdd numeric score!";
                errorProviderEmpty.SetError(txtScore, "Enter a numeric value");
            }
            catch (OverflowException e_overflow)
            {
                lblResult.Text = e_overflow.Message.ToString() + "\nAdd a value between 0 - 100 \nNo score added!";
                errorProviderEmpty.SetError(txtScore, "Minimum Score value is 0");
            }
            catch (NullReferenceException e_null)
            {
                lblResult.Text = e_null.Message.ToString();
            }
        }
        public static string CalculateRubic()
        {
            int score = 0;
            foreach (var item in scoreCard)
            {
                score += Convert.ToInt32(item);
            }
            var value = Convert.ToInt32(score / scoreCard.Count);
            var result = "";
            switch (value)
            {
                case int a when a >= 90:
                    result = "A";
                    break;
                case int b when b >= 80:
                    result = "B";
                    break;
                case int c when c >= 70:
                    result = "C";
                    break;
                case int d when d >= 60:
                    result = "D";
                    break;
                default: result = "F"; break;
            }
            return "Final Grade: " + value + " - " + result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lblFinalResult.Text = CalculateRubic();
        }
    }
}
