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


![Exception Handling image](./images/exceptionHandling.png)
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
#### Example try-finally
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