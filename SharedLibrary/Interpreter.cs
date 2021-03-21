using System;
using System.Collections.Generic;

namespace SharedLibrary
{
    public class Interpreter
    {
        #region Properties

        public static IList<string> CommandStrings { get; set; }
        public static IList<Instruction> Program { get; set; }
        public static Registers Registers { get; set; } = new Registers();

        #endregion

        #region Enums

        public enum Move
        {
            End,
            Next,
            Previous,
            Restart
        }

        #endregion

        #region Methods

        public static void Run(Move move)
        {
            switch (move)
            {
                case Move.Restart:
                    Interpreter.Registers.ProgramCounter = Registers.ProgramCounterStart;

                    Interpreter.Registers.Init();

                    Memory.InitMainMemory();

                    return;
                case Move.End:
                    while (Interpreter.Program[Interpreter.Registers.ProgramCounter].Arg1 != "halt")
                    {
                        if (Interpreter.Registers.ProgramCounter < Interpreter.Program.Count)
                        {
                            Action instruction = Interpreter.Program[Interpreter.Registers.ProgramCounter].Action;

                            Interpreter.Registers.ProgramCounter++;

                            instruction.Invoke();
                        }
                    }

                    return;
                case Move.Next:
                    if (Interpreter.Program[Interpreter.Registers.ProgramCounter].Arg1 == "halt")
                    {
                        return;
                    }

                    if (Interpreter.Registers.ProgramCounter < Interpreter.Program.Count)
                    {
                        Action instruction = Interpreter.Program[Interpreter.Registers.ProgramCounter].Action;

                        Interpreter.Registers.ProgramCounter++;

                        instruction.Invoke();
                    }

                    return;
            }
        }

        #endregion
    }
}