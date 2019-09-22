## IEnumerable / ICollection / IList / List
### IEnumerable
* Base interface for any collection
* read only - GetEnumerator()

### ICollection
* extends IEnumerable
* non-index based operations - (add, remove, check,..)

### IList vs List
* supports index based collection operations
* IList - easier to make changes compared to List when providing DLL (class library)to expose functionality.
* List - concrete class 
```C#
    public interface Investment
    {
        void Invest();
    }
    public class Savings : Investment
    {
        public void Invest()
        {
            Console.WriteLine("Investing in something");
        }
        public void Accumulate()   
        {
            // adding a new behaviour.
        }
    }
    static void Main(string[] args)
    {
        Investment interfaceInvestment = new Savings();
        interfaceInvestment.Invest();     // Calling function outside the world.  
        interfaceInvestment.Accumulate(); // Error! to be able to call it add it to the Interface. 
    }

```


