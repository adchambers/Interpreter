using System;

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
    }
}