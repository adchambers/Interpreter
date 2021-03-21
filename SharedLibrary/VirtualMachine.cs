using System;

namespace SharedLibrary
{
    public class VirtualMachine
    {
        #region Properties

        public static Registers Registers { get; set; } = new Registers();

        #endregion

        #region Enums

        public enum Move
        {
            Run,
            Next,
            Restart
        }

        #endregion

        #region Methods

        public static void Init()
        {
            VirtualMachine.Registers.ProgramCounter = Registers.ProgramCounterStart;

            VirtualMachine.Registers.Init();

            DataStructure.InitMainMemory();
        }

        public static void Interpret(Move move)
        {
            switch (move)
            {
                case Move.Restart:

                    VirtualMachine.Init();

                    return;

                case Move.Run:

                    while (DataStructure.ProgramStoreC[VirtualMachine.Registers.ProgramCounter].Arg1 != "halt")
                    {
                        if (VirtualMachine.Registers.ProgramCounter < DataStructure.ProgramStoreC.Count)
                        {
                            Action instruction = DataStructure.ProgramStoreC[VirtualMachine.Registers.ProgramCounter]
                                .Action;

                            VirtualMachine.Registers.ProgramCounter++;

                            instruction.Invoke();
                        }
                    }

                    return;

                case Move.Next:

                    if (DataStructure.ProgramStoreC[VirtualMachine.Registers.ProgramCounter].Arg1 == "halt")
                    {
                        return;
                    }

                    if (VirtualMachine.Registers.ProgramCounter < DataStructure.ProgramStoreC.Count)
                    {
                        Action instruction =
                            DataStructure.ProgramStoreC[VirtualMachine.Registers.ProgramCounter].Action;

                        VirtualMachine.Registers.ProgramCounter++;

                        instruction.Invoke();
                    }

                    return;
            }
        }

        #endregion
    }
}