using System;
using System.Linq;

namespace SharedLibrary
{
    public class Instruction
    {
        #region Constructors

        public Instruction(int address,
            string[] valueWithArgs,
            Action action,
            string Name = null)
        {
            this.ValueWithArgs = valueWithArgs;

            this.Address = address;

            this.Action = action;

            this.Name = Name;
        }

        #endregion

        #region Properties

        public string[] ValueWithArgs { get; }
        public int Address { get; }
        public Action Action { get; }
        public string Name { get; }

        public string Arg1 => this.ValueWithArgs[0];
        public string Arg2 => this.ValueWithArgs[1];
        public string Arg3 => this.ValueWithArgs[2];

        #endregion

        #region Methods

        public static void Add()
        {
            DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer - 1] =
                DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer - 1]
                + DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer];

            VirtualMachine.Registers.StackPointer--;
        }

        public static void Sub()
        {
            DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer - 1] =
                DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer - 1]
                - DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer];

            VirtualMachine.Registers.StackPointer--;
        }

        public static void Alloc(int m)
        {
            VirtualMachine.Registers.StackPointer = VirtualMachine.Registers.StackPointer + m;
        }

        public static void Call(int q)
        {
            VirtualMachine.Registers.FramePointer = VirtualMachine.Registers.StackPointer - q - 1;

            DataStructure.MainMemoryS[VirtualMachine.Registers.FramePointer] = VirtualMachine.Registers.ProgramCounter;

            VirtualMachine.Registers.ProgramCounter = DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer];

            VirtualMachine.Registers.StackPointer--;
        }

        public static void Dup()
        {
            DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer + 1] =
                DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer];
        }

        public static void Enter(int m)
        {
            VirtualMachine.Registers.ExtremePointer = VirtualMachine.Registers.StackPointer + m;

            if (VirtualMachine.Registers.HeapPointer < VirtualMachine.Registers.ExtremePointer)
            {
                throw new Exception("Stack overflow");
            }
        }

        public static void Jumpi(int b)
        {
            VirtualMachine.Registers.ProgramCounter =
                b + DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer];

            VirtualMachine.Registers.StackPointer--;
        }

        public static void Jumpz(object q)
        {
            if (DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer] == 0)
            {
                bool isInt = int.TryParse(q.ToString(), out int qq);

                //Jump to address
                if (isInt)
                {
                    VirtualMachine.Registers.ProgramCounter = qq;
                }
                //Jump to address assigned to name (of function)
                else
                {
                    VirtualMachine.Registers.ProgramCounter = DataStructure.ProgramStoreC
                        .First(instruction => instruction.Name == q.ToString())
                        .Address;
                }
            }

            VirtualMachine.Registers.StackPointer--;
        }

        public static void Halt()
        {
            //TODO: implement
        }

        public static void Leq()
        {
            if (DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer - 1]
                <= DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer])
            {
                DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer - 1] = 1;
            }
            else
            {
                DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer - 1] = 0;
            }

            VirtualMachine.Registers.StackPointer--;
        }

        public static void Geq()
        {
            if (DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer - 1]
                >= DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer])
            {
                DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer - 1] = 1;
            }
            else
            {
                DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer - 1] = 0;
            }

            VirtualMachine.Registers.StackPointer--;
        }

        public static void Le()
        {
            if (DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer - 1]
                < DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer])
            {
                DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer - 1] = 1;
            }
            else
            {
                DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer - 1] = 0;
            }

            VirtualMachine.Registers.StackPointer--;
        }

        public static void Load()
        {
            DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer] =
                DataStructure.MainMemoryS[DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer]];
        }

        public static void Load(int m)
        {
            for (int i = m - 1; i >= 0; i--)
            {
                DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer + i] =
                    DataStructure.MainMemoryS[DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer] + 1];
            }

            VirtualMachine.Registers.StackPointer = VirtualMachine.Registers.StackPointer + m - 1;
        }

        public static void Loada(int q)
        {
            Instruction.Loadc(q);

            Instruction.Load();
        }

        public static void Loada(int q, int m)
        {
            Instruction.Loadc(q);

            Instruction.Load(m);
        }

        public static void Loadc(object q)
        {
            bool isInt = int.TryParse(q.ToString(), out int qq);

            //Load const q on top of stack
            if (isInt)
            {
                VirtualMachine.Registers.StackPointer = VirtualMachine.Registers.StackPointer + 1;

                DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer] = qq;
            }
            //Load a function address on top of the stack
            else
            {
                VirtualMachine.Registers.StackPointer = VirtualMachine.Registers.StackPointer + 1;

                DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer] = DataStructure.ProgramStoreC
                    .First(instruction => instruction.Name == q.ToString())
                    .Address;
            }
        }

        public static void Loadr(int j)
        {
            VirtualMachine.Registers.StackPointer = VirtualMachine.Registers.StackPointer + 1;

            DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer] =
                DataStructure.MainMemoryS[VirtualMachine.Registers.FramePointer + j];
        }

        public static void Loadr(int j, int m)
        {
            Instruction.Loadrc(j);

            Instruction.Load(m);
        }

        public static void Loadrc(int j)
        {
            VirtualMachine.Registers.StackPointer++;

            DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer] =
                VirtualMachine.Registers.FramePointer + j;
        }

        public static void Mark()
        {
            DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer + 1] =
                VirtualMachine.Registers.ExtremePointer;

            DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer + 2] =
                VirtualMachine.Registers.FramePointer;

            VirtualMachine.Registers.StackPointer = VirtualMachine.Registers.StackPointer + 3;
        }

        public static void Mul()
        {
            DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer - 1] =
                DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer - 1]
                * DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer];

            VirtualMachine.Registers.StackPointer--;
        }

        public static void New()
        {
            if (VirtualMachine.Registers.HeapPointer - DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer]
                > VirtualMachine.Registers.ExtremePointer)
            {
                VirtualMachine.Registers.HeapPointer =
                    VirtualMachine.Registers.HeapPointer
                    - DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer];

                DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer] = VirtualMachine.Registers.HeapPointer;
            }
            else
            {
                DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer] = 0;
            }
        }

        public static void Pop()
        {
            VirtualMachine.Registers.StackPointer--;
        }

        public static void Print()
        {
            Console.WriteLine(DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer]);
        }

        public static void Return(int q)
        {
            VirtualMachine.Registers.ProgramCounter = DataStructure.MainMemoryS[VirtualMachine.Registers.FramePointer];

            VirtualMachine.Registers.ExtremePointer =
                DataStructure.MainMemoryS[VirtualMachine.Registers.FramePointer - 2];

            if (VirtualMachine.Registers.ExtremePointer >= VirtualMachine.Registers.HeapPointer)
            {
                throw new Exception("Stack Overflow");
            }

            VirtualMachine.Registers.StackPointer = VirtualMachine.Registers.FramePointer - q;

            VirtualMachine.Registers.FramePointer =
                DataStructure.MainMemoryS[VirtualMachine.Registers.FramePointer - 1];
        }

        public static void Slide(int q, int m)
        {
            if (q > 0)
            {
                if (m == 0)
                {
                    VirtualMachine.Registers.StackPointer = VirtualMachine.Registers.StackPointer - q;
                }
                else
                {
                    VirtualMachine.Registers.StackPointer = VirtualMachine.Registers.StackPointer - q - m;

                    for (int i = 0; i < m; i++)
                    {
                        VirtualMachine.Registers.StackPointer++;

                        DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer] =
                            DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer + q];
                    }
                }
            }
        }

        public static void Store()
        {
            DataStructure.MainMemoryS[DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer]] =
                DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer - 1];

            VirtualMachine.Registers.StackPointer--;
        }

        public static void Store(int m)
        {
            for (int i = 0; i < m; i++)
            {
                DataStructure.MainMemoryS[DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer] + 1] =
                    DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer - m + i];
            }

            VirtualMachine.Registers.StackPointer = VirtualMachine.Registers.StackPointer + m - 1;
        }

        public static void Storea(int q)
        {
            Instruction.Loadc(q);

            Instruction.Store();
        }

        public static void Storea(int q, int m)
        {
            Instruction.Loada(q);

            Instruction.Store(m);
        }

        public static void Storer(int j)
        {
            DataStructure.MainMemoryS[VirtualMachine.Registers.FramePointer + j] =
                DataStructure.MainMemoryS[VirtualMachine.Registers.StackPointer];

            VirtualMachine.Registers.StackPointer = VirtualMachine.Registers.StackPointer - 1;
        }

        public static void Storer(int j, int m)
        {
            Instruction.Loadrc(j);

            Instruction.Store(m);
        }

        #endregion
    }
}