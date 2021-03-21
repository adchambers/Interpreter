using System.Collections.ObjectModel;

namespace SharedLibrary
{
    public static partial class DataStructure
    {
        #region Fields

        private const string problem19 = "enter 5\n" //*******
                                         + "alloc 1\n" // alloc for return value of _main
                                         + "mark\n" // create stack frame for EP, FP, and PC
                                         + "loadc _main\n" // put the address of _main on top of the stack
                                         + "call 0\n" // call _main without formal parameters
                                         + "halt\n"
                                         + "_main enter 7\n" //*******
                                         + "alloc 1\n" // allocate space for return value of _main
                                         + "mark\n" // create stack frame for EP, FP, and PC
                                         + "loadc 3\n" // load the formal parameter of _fac
                                         + "loadc _fac\n" // put the address of _fac on top of the stack
                                         + "call 1\n" // call _fac with one formal parameter
                                         //+ "storer 1\n"     
                                         //+ "loadr 1\n"
                                         + "print\n"
                                         + "return 0\n"
                                         + "_fac enter 7\n" //********
                                         + "loadr 1\n"
                                         + "loadc 1\n"
                                         + "leq\n"
                                         + "jumpz A\n"
                                         + "loadc 1\n"
                                         + "storer -3\n"
                                         + "return 0\n"
                                         + "A enter 0\n"
                                         + "loadr 1\n"
                                         + "loadc 1\n"
                                         + "sub\n"
                                         + "alloc 1\n"
                                         + "mark\n"
                                         + "loadr 2\n"
                                         + "loadc _fac\n"
                                         + "call 1\n"
                                         + "loadr 1\n"
                                         + "mul\n"
                                         + "storer -3\n"
                                         + "return 0";

        private const string recursiveFunction = "enter 5\n"
                                                 + "alloc 1\n"
                                                 + "mark\n"
                                                 + "loadc _main\n"
                                                 + "call 0\n"
                                                 + "halt\n"
                                                 + "_main enter 7\n"
                                                 + "alloc 1\n"
                                                 + "alloc 1\n"
                                                 + "mark\n"
                                                 + "loadc 3\n"
                                                 + "loadc _fac\n"
                                                 + "call 1\n"
                                                 + "storer 1\n"
                                                 + "loadr 1\n"
                                                 + "print\n"
                                                 + "return 0\n"
                                                 + "_fac enter 7\n"
                                                 + "loadr 1\n"
                                                 + "loadc 1\n"
                                                 + "leq\n"
                                                 + "jumpz A\n"
                                                 + "loadc 1\n"
                                                 + "storer -3\n"
                                                 + "return 0\n"
                                                 + "A enter 0\n"
                                                 + "loadr 1\n"
                                                 + "loadc 1\n"
                                                 + "sub\n"
                                                 + "alloc 1\n"
                                                 + "mark\n"
                                                 + "loadr 2\n"
                                                 + "loadc _fac\n"
                                                 + "call 1\n"
                                                 + "loadr 1\n"
                                                 + "mul\n"
                                                 + "storer -3\n"
                                                 + "return 0";

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