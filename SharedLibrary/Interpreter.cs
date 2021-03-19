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
            Interpreter.Program = Parser.ParseInstructions();

            //Interpreter.Registers.Init();

            //switch (move)
            //{
            //    case Move.Next:
            //        Interpreter.Registers.ProgramCounter++;
            //        break;
            //    case Move.End:
            //        Interpreter.Registers.ProgramCounter = Interpreter.Program.Count - 1;
            //        break;
            //    case Move.Previous:
            //        Interpreter.Registers.ProgramCounter--;
            //        break;
            //    case Move.Restart:
            //        Interpreter.Registers.ProgramCounter = Registers.ProgramCounterStart;

            //        Interpreter.Registers.Init();

            //        Memory.InitMainMemory();

            //        return;
            //}

            Interpreter.Program[Interpreter.Registers.ProgramCounter].Action.Invoke();

            Interpreter.Registers.ProgramCounter++;
        }

        #endregion
    }
}