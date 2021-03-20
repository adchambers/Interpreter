using System;
using System.Linq;

namespace SharedLibrary
{
    public static class Command
    {
        #region Methods

        public static void Add()
        {
            Memory.Main[Interpreter.Registers.StackPointer - 1] =
                Memory.Main[Interpreter.Registers.StackPointer - 1] + Memory.Main[Interpreter.Registers.StackPointer];

            Interpreter.Registers.StackPointer--;
        }

        public static void Alloc(int m)
        {
            Interpreter.Registers.StackPointer = Interpreter.Registers.StackPointer + m;
        }

        public static void Call(int q)
        {
            Interpreter.Registers.FramePointer = Interpreter.Registers.StackPointer;

            int tmp = Interpreter.Registers.ProgramCounter;

            // Modified form of call is required; otherwise, the ProgramCounter is incremented +1 beyond the called function's address
            Interpreter.Registers.ProgramCounter = Memory.Main[Interpreter.Registers.StackPointer] - q;

            Memory.Main[Interpreter.Registers.StackPointer] = tmp;
        }

        public static void Dup()
        {
            Memory.Main[Interpreter.Registers.StackPointer + 1] = Memory.Main[Interpreter.Registers.StackPointer];
        }

        public static void Enter(int m)
        {
            Interpreter.Registers.ExtremePointer = Interpreter.Registers.StackPointer + m;
        }

        public static void Jumpi(int b)
        {
            Interpreter.Registers.ProgramCounter = b + Memory.Main[Interpreter.Registers.StackPointer];

            Interpreter.Registers.StackPointer--;
        }

        public static void Halt()
        {
            //TODO: implement
        }

        public static void Load()
        {
            Memory.Main[Interpreter.Registers.StackPointer] =
                Memory.Main[Memory.Main[Interpreter.Registers.StackPointer]];
        }

        public static void Load(int m)
        {
            for (int i = m - 1; i >= 0; i--)
            {
                Memory.Main[Interpreter.Registers.StackPointer + i] =
                    Memory.Main[Memory.Main[Interpreter.Registers.StackPointer] + 1];
            }

            Interpreter.Registers.StackPointer = Interpreter.Registers.StackPointer + m - 1;
        }

        public static void Loada(int q)
        {
            Command.Loadc(q);

            Command.Load();
        }

        public static void Loada(int q, int m)
        {
            Command.Loadc(q);

            Command.Load(m);
        }

        public static void Loadc(object q)
        {
            bool isInt = int.TryParse(q.ToString(), out int qq);

            //Load const q on top of stack
            if (isInt)
            {
                Interpreter.Registers.StackPointer = Interpreter.Registers.StackPointer + 1;

                Memory.Main[Interpreter.Registers.StackPointer] = qq;
            }
            //Load a function address on top of the stack
            else
            {
                Interpreter.Registers.StackPointer = Interpreter.Registers.StackPointer + 1;

                Memory.Main[Interpreter.Registers.StackPointer] = Interpreter.Program
                    .First(instruction => instruction.Name == q.ToString())
                    .Address;
            }
        }

        public static void Loadr(int j)
        {
            Command.Loadr(j, 1);
        }

        public static void Loadr(int j, int m)
        {
            Command.Loadrc(j);

            Command.Load(m);
        }

        public static void Loadrc(int j)
        {
            Interpreter.Registers.StackPointer++;

            Memory.Main[Interpreter.Registers.StackPointer] = Interpreter.Registers.FramePointer + j;
        }

        public static void Mark()
        {
            Memory.Main[Interpreter.Registers.StackPointer + 1] = Interpreter.Registers.ExtremePointer;

            Memory.Main[Interpreter.Registers.StackPointer + 2] = Interpreter.Registers.FramePointer;

            Interpreter.Registers.StackPointer = Interpreter.Registers.StackPointer + 2;
        }

        public static void New()
        {
            if (Interpreter.Registers.HeapPointer - Memory.Main[Interpreter.Registers.StackPointer]
                > Interpreter.Registers.ExtremePointer)
            {
                Interpreter.Registers.HeapPointer =
                    Interpreter.Registers.HeapPointer - Memory.Main[Interpreter.Registers.StackPointer];

                Memory.Main[Interpreter.Registers.StackPointer] = Interpreter.Registers.HeapPointer;
            }
            else
            {
                Memory.Main[Interpreter.Registers.StackPointer] = 0;
            }
        }

        public static void Pop()
        {
            Interpreter.Registers.StackPointer--;
        }

        public static void Return(int q)
        {
            Interpreter.Registers.ProgramCounter = Memory.Main[Interpreter.Registers.FramePointer];

            Interpreter.Registers.ExtremePointer = Memory.Main[Interpreter.Registers.FramePointer - 2];

            if (Interpreter.Registers.ExtremePointer >= Interpreter.Registers.HeapPointer)
            {
                throw new Exception("Stack Overflow");
            }

            Interpreter.Registers.StackPointer = Interpreter.Registers.FramePointer - q;

            Interpreter.Registers.FramePointer = Memory.Main[Interpreter.Registers.FramePointer - 1];
        }

        public static void Slide(int q, int m)
        {
            if (q > 0)
            {
                if (m == 0)
                {
                    Interpreter.Registers.StackPointer = Interpreter.Registers.StackPointer - q;
                }
                else
                {
                    Interpreter.Registers.StackPointer = Interpreter.Registers.StackPointer - q - m;

                    for (int i = 0; i < m; i++)
                    {
                        Interpreter.Registers.StackPointer++;

                        Memory.Main[Interpreter.Registers.StackPointer] =
                            Memory.Main[Interpreter.Registers.StackPointer + q];
                    }
                }
            }
        }

        public static void Store()
        {
            Memory.Main[Memory.Main[Interpreter.Registers.StackPointer]] =
                Memory.Main[Interpreter.Registers.StackPointer - 1];

            Interpreter.Registers.StackPointer--;
        }

        public static void Store(int m)
        {
            for (int i = 0; i < m; i++)
            {
                Memory.Main[Memory.Main[Interpreter.Registers.StackPointer] + 1] =
                    Memory.Main[Interpreter.Registers.StackPointer - m + i];
            }

            Interpreter.Registers.StackPointer = Interpreter.Registers.StackPointer + m - 1;
        }

        public static void Storea(int q)
        {
            Command.Loadc(q);

            Command.Store();
        }

        public static void Storea(int q, int m)
        {
            Command.Loada(q);

            Command.Store(m);
        }

        public static void Storer(int j)
        {
            Command.Storer(j, 1);
        }

        public static void Storer(int j, int m)
        {
            Command.Loadrc(j);

            Command.Store(m);
        }

        #endregion
    }
}