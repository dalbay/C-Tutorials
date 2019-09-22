# C#-Tutorials

## C# Exception Handling

### SystemException Class
| Properties      | Description         
| ------------- |:-------------:|  
|   Data    | Gets a collection of key/value pairs that provide additional user-defined information about the exception. |
| HelpLink | Gets or sets a link to the help file associated with this exception. |
| HResult | Gets or sets HRESULT, a coded numerical value that is assigned to a specific exception. |
| InnerException | Gets the Exception instance that caused the current exception.  |
| Message | Gets a message that describes the current exception.|
|Source|Gets or sets the name of the application or the object that causes the error.|
|StackTrace|Gets a string representation of the immediate frames on the call stack.|
|TargetSite|Gets the method that throws the current exception.

---
### Example 1 try-catch-finally
```C#
try
{
    // code that may raise exceptions
}
catch(Exception ex)
{
    // handle exception
}
finally
{
    // final cleanup code
}
```
---
## using Statements - try/finally block

* When the lifetime of an IDisposable object is limited to a single method, you should declare and instantiate it in the using statement. 
* The using statement ensures that Dispose is called even if an exception occurs within the using block.
* Within the using block, the object is read-only and cannot be modified or reassigned.
* Unmanaged resources and class library types must implement the IDisposable interface.
* it's generally better to instantiate the object in the using statement and limit its scope to the using block.
```C#
using (var font1 = new Font("Arial", 10.0f)) 
{
    byte charset = font1.GdiCharSet;
}
``` 
* You can achieve the same result by putting the object inside a try block and then calling Dispose in a finally block;
```C#
{
  var font1 = new Font("Arial", 10.0f);
  try
  {
    byte charset = font1.GdiCharSet;
  }
  finally
  {
    if (font1 != null)
      ((IDisposable)font1).Dispose();
  }
}
```
---
### Example 2 try-catch-finally
*docs.microsoft.com example* - an exception from the TryCast method is caught in a method farther up the call stack.

```C#
public class ThrowTestB
{
    static void Main()
    {
        try
        {
            // TryCast produces an unhandled exception.
            TryCast();
        }
        catch (Exception ex)
        {
            // Catch the exception that is unhandled in TryCast.
            Console.WriteLine
                ("Catching the {0} exception triggers the finally block.",
                ex.GetType());

            // Restore the original unhandled exception. You might not
            // know what exception to expect, or how to handle it, so pass 
            // it on.
            throw;
        }
    }

    public static void TryCast()
    {
        int i = 123;
        string s = "Some string";
        object obj = s;

        try
        {
            // Invalid conversion; obj contains a string, not a numeric type.
            i = (int)obj;

            // The following statement is not run.
            Console.WriteLine("WriteLine at the end of the try block.");
        }
        finally
        {
            // Report that the finally block is run, and show that the value of
            // i has not been changed.
            Console.WriteLine("\nIn the finally block in TryCast, i = {0}.\n", i);
        }
    }
    // Output:
    // In the finally block in TryCast, i = 123.

    // Catching the System.InvalidCastException exception triggers the finally block.

    // Unhandled Exception: System.InvalidCastException: Specified cast is not valid.
}
```
---
### Example 3 try-catch
![Exception Handling image](./images/exceptionHandling.png)
```C#
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
                else if(Convert.ToInt32(score) > 100) // checking max input value
                {
                    errorProviderEmpty.SetError(txtScore, "Maximum Score is 100");
                    lblResult.Text = "Add a value between 0 - 100 \nNo score added!";
                }
                else
                {
                    errorProviderEmpty.SetError(txtScore, "");
                    scoreCard.Add((Convert.ToUInt32(txtScore.Text))); // checking positive number
                    lblResult.Text = txtScore.Text + " was succesfully added to your scorecard." ;
                }
                
            }
            catch (NotImplementedException e_notImplemented)
            {
                lblResult.Text = e_notImplemented.ToString();
                errorProviderEmpty.SetError(txtScore, "Internal Error!");
            }
            catch(FormatException e_format)
            {
                lblResult.Text = e_format.Message.ToString() + "\nAdd numeric score!";
                errorProviderEmpty.SetError(txtScore, "Enter a numeric value");
            }
            catch(OverflowException e_overflow)
            {
                lblResult.Text = e_overflow.Message.ToString() + "\nAdd a value between 0 - 100 \nNo score added!";
                errorProviderEmpty.SetError(txtScore, "Minimum Score value is 0");
            }
            catch(NullReferenceException e_null)
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
                default:result = "F";break;
            }
            return "Final Grade: " + value + " - " + result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lblFinalResult.Text = CalculateRubic();
        }
    }
```