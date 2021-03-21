using System.Collections.ObjectModel;

namespace SharedLibrary
{
    public static partial class DataStructure
    {
        #region Fields

        //private const string problem19 = "enter 5\n" //*******
        //                                 + "alloc 1\n" // alloc for return value of _main
        //                                 + "mark\n" // create stack frame for EP, FP, and PC
        //                                 + "loadc _main\n" // put the address of _main on top of the stack
        //                                 + "call 0\n" // call _main without formal parameters
        //                                 + "halt\n"
        //                                 + "_main enter 7\n" //*******
        //                                 + "alloc 1\n" // allocate space for return value of _main
        //                                 + "mark\n" // create stack frame for EP, FP, and PC
        //                                 + "loadc 3\n" // load the formal parameter of _fac
        //                                 + "loadc _fac\n" // put the address of _fac on top of the stack
        //                                 + "call 1\n" // call _fac with one formal parameter
        //                                 + "print\n"
        //                                 + "return 0\n"
        //                                 + "_fac enter 7\n" //********
        //                                 + "loadr 1\n"
        //                                 + "loadc 1\n"
        //                                 + "leq\n"
        //                                 + "jumpz A\n"
        //                                 + "loadc 1\n"
        //                                 + "storer -3\n"
        //                                 + "return 0\n"
        //                                 + "A enter 0\n"
        //                                 + "loadr 1\n"
        //                                 + "loadc 1\n"
        //                                 + "sub\n"
        //                                 + "alloc 1\n"
        //                                 + "mark\n"
        //                                 + "loadr 2\n"
        //                                 + "loadc _fac\n"
        //                                 + "call 1\n"
        //                                 + "loadr 1\n"
        //                                 + "mul\n"
        //                                 + "storer -3\n"
        //                                 + "return 0";

        private const string problem19 = "enter 5\n" // Push the EP up for variables, parameters and function calls
                                         + "alloc 1\n" // Allocate for the return value of _main
                                         + "mark\n" // Create a stack frame for the EP, FP, and PC
                                         + "loadc _main\n" // Put the address of _main on top of the stack
                                         + "call 0\n" // Call _main (no formal parameters)
                                         + "halt\n" // Return execution to the OS
                                         + "_main enter 7\n" // Push the EP up for variables, parameters and function calls
                                         + "alloc 1\n" // Allocate space for the return value of _main
                                         + "mark\n" // Create a stack frame for EP, FP, and PC
                                         + "loadc 3\n" // Load the formal parameter, n, of _fac
                                         + "loadc _fac\n" // Put the address of _fac on top of the stack
                                         + "call 1\n" // Call _fac with one formal parameter
                                         + "print\n" // Print the result
                                         + "return 3\n" // Remove 3 cells from above the return value
                                         + "_fac enter 7\n" // Push the EP up for variables, parameters and function calls
                                         + "loadr 1\n" // Put n on top of the stack
                                         + "loadc 1\n" // Put constant 0 on top of the stack
                                         + "leq\n" // Compare n <= 0
                                         + "jumpz A\n" // If n <= 0 is false, jump to A
                                         + "loadc 1\n" // Else, if n <= 0 is true, put constant 1 on top of the stack
                                         + "storer -3\n" // Store the return value of _fac
                                         + "return 3\n" // Remove 3 cells from above the return value
                                         + "A enter 0\n" // Push EP up for variables, parameters and function calls
                                         + "loadr 1\n" // Put the variable n on top of the stack
                                         + "loadc 1\n" // Put the const 1 on top of the stack
                                         + "sub\n" // Compute the difference of n - 1 for the formal parameter of _fac
                                         + "alloc 1\n" // Allocate space for the return value of _fac
                                         + "mark\n" // Create a stack frame for EP, FP, and PC
                                         + "loadr 2\n" // Put the formal parameter of _fac on top of the stack
                                         + "loadc _fac\n" // Put the address of _fac on top of the stack
                                         + "call 1\n" // Call _fac
                                         + "loadr 1\n" // Put the variable n on top of the stack
                                         + "mul\n" // Multiply (n * the return value of fac(n - 1)
                                         + "storer -3\n" // Store the return value
                                         + "return 3"; // Remove 3 cells from above the return value

        //private const string recursiveFunction = "enter 5\n"
        //                                         + "alloc 1\n"
        //                                         + "mark\n"
        //                                         + "loadc _main\n"
        //                                         + "call 0\n"
        //                                         + "halt\n"
        //                                         + "_main enter 7\n"
        //                                         + "alloc 1\n"
        //                                         + "alloc 1\n"
        //                                         + "mark\n"
        //                                         + "loadc 3\n"
        //                                         + "loadc _fac\n"
        //                                         + "call 1\n"
        //                                         + "storer 1\n"
        //                                         + "loadr 1\n"
        //                                         + "print\n"
        //                                         + "return 0\n"
        //                                         + "_fac enter 7\n"
        //                                         + "loadr 1\n"
        //                                         + "loadc 1\n"
        //                                         + "leq\n"
        //                                         + "jumpz A\n"
        //                                         + "loadc 1\n"
        //                                         + "storer -3\n"
        //                                         + "return 0\n"
        //                                         + "A enter 0\n"
        //                                         + "loadr 1\n"
        //                                         + "loadc 1\n"
        //                                         + "sub\n"
        //                                         + "alloc 1\n"
        //                                         + "mark\n"
        //                                         + "loadr 2\n"
        //                                         + "loadc _fac\n"
        //                                         + "call 1\n"
        //                                         + "loadr 1\n"
        //                                         + "mul\n"
        //                                         + "storer -3\n"
        //                                         + "return 0";

        private const string problem18 =
            "enter 3\n" // Push the EP up for variables, parameters and function calls
            + "alloc 1\n" // Allocate for global variable result
            + "enter 6\n" // Push the EP up for variables, parameters and function calls
            + "alloc 1\n" // Allocate for local variable n
            + "loadc 5\n" // Put constant 5 on top of the stack
            + "storer 1\n" // Assign constant value 5 to local variable n
            + "mark\n" // Create a stack frame for EP, FP, and PC
            + "loadr 1\n" // Put the formal parameter, n, of _fibo on top of the stack
            + "loadc _fibo\n" // Put the address of _fibo on top of the stack
            + "call 1\n" // Call _fibo with 1 formal parameter
            + "storer 1\n" // Store the return value of _fibo
            + "halt\n" // Return execution to the OS
            + "_fibo enter 3\n" // Push the EP up for variables, parameters and function calls
            + "alloc 1\n" // Allocate for local variable result
            + "loadr 1\n" // Put varible n on top of the stack
            + "loadc 0\n" // Put constant 0 on top of the stack
            + "le\n" // Compare n < 0
            + "jumpz A\n" // If n < 0 is false, jump to A
            + "loadr 1\n" // Else, if n < 0 is true, put variable n on top of the stack for the switch statement
            + "dup\n" // Duplicate n on top of the stack for comparison
            + "loadc 0\n" // Add const 0 (i.e., case 0) to the top of the stack
            + "geq\n" // Compare n > 0 (i.e., case 0)
            + "jumpz D\n" // If n > 0 is false, jump to D (the default case)
            + "dup\n" // Duplicate n on top of the stack for comparison
            + "loadc 1\n" // Put constant 1 (i.e., case k - 1) on top of the stack
            + "leq\n" // Compare n < 1 (i.e., case k - 1)
            + "jumpz D\n" // If n < 1 is false, jump to D
            + "jumpi B\n" // Else if n < 1 is true, jump to the corresponding index (i.e., case)
            + "A enter 1\n" // Push the EP up for variables, parameters and function calls
            + "loadc -1\n" // Put constant -1 on top of the stack
            + "storer 2\n" // Store the return value
            + "return 0\n" // Remove 0 cells from above the return value
            + "D enter 3\n" // Push the EP up for variables, parameters and function calls
            + "loadr 1\n" // Put n on top of the stack
            + "loadc 1\n" // Put constant 1 on top of the stack
            + "sub\n" // Subtract n - 1 for the formal parameter of _fibo
            + "mark\n" // Create a stack frame for EP, FP, and PC
            + "loadr 1\n" // Put the difference on top of the stack
            + "loadc _fibo\n" // Load the address of _fib on top of the stack
            + "call 1\n" // Call _fibo with 1 formal parameter
            + "storer 1\n" // Store the return value of _fibo
            + "loadr 1\n" // Put n on top of the stack
            + "loadc 2\n" // Put constant 2 on top of the stack
            + "sub\n" // Subtract n - 1 for the formal parameter of _fibo
            + "mark\n" // Create a stack frame for EP, FP, and PC
            + "loadr 1\n" // Put the difference on top of the stack
            + "loadc _fibo\n" // Load the address of _fib on top of the stack
            + "call 1\n" // Call _fibo with 1 formal parameter
            + "storer 1\n" // Store the return value of _fibo
            + "add\n" // Add the returned results of _fibo (respectively)
            + "storer 1\n" // Store the returned values
            + "return 3"; // Remove 3 cells from above the return value

        #endregion

        #region Constructors

        static DataStructure()
        {
            DataStructure.BindableBase = new BindableBase();

            DataStructure.InitMainMemory();

            DataStructure.BindableBase.Text = DataStructure.problem19;
        }

        #endregion

        #region Properties

        public static BindableBase BindableBase { get; set; }

        #endregion

        #region Methods

        public static void InitMainMemory()
        {
            DataStructure.MainMemoryS = new ObservableCollection<int>();

            for (int i = 0; i <= DataStructure.HeapSize; i++)
            {
                DataStructure.MainMemoryS.Add(0);
            }
        }

        #endregion
    }
}