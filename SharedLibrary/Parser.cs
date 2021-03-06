using System;
using System.Collections.Generic;

namespace SharedLibrary
{
    public static class Parser
    {
        #region Methods

        public static IList<Instruction> ParseInstructions()
        {
            VirtualMachine.Init();

            if (string.IsNullOrWhiteSpace(DataStructure.BindableBase.Text) == false)
            {
                string[] input = DataStructure.BindableBase.Text.Split("\n");

                IList<Instruction> instructions = new List<Instruction>();

                int address = 0;

                foreach (string instruction in input)
                {
                    string[] valueWithArgs = instruction.Split(" ");

                    switch (valueWithArgs[0])
                    {
                        case "add":
                            if (valueWithArgs.Length == 1)
                            {
                                instructions.Add(new Instruction(address, valueWithArgs, () => Instruction.Add()));
                            }
                            else
                            {
                                throw new Exception("Add takes 0 arguments.");
                            }

                            break;
                        case "alloc":
                            if (valueWithArgs.Length == 2)
                            {
                                instructions.Add(new Instruction(address,
                                    valueWithArgs,
                                    () => Instruction.Alloc(int.Parse(valueWithArgs[1]))));
                            }
                            else
                            {
                                throw new Exception("Alloc takes 1 argument.");
                            }

                            break;
                        case "call":
                            if (valueWithArgs.Length == 2)
                            {
                                instructions.Add(new Instruction(address,
                                    valueWithArgs,
                                    () => Instruction.Call(int.Parse(valueWithArgs[1]))));
                            }
                            else
                            {
                                throw new Exception("Call takes 1 arguments.");
                            }

                            break;
                        case "dup":
                            if (valueWithArgs.Length == 1)
                            {
                                instructions.Add(new Instruction(address, valueWithArgs, () => Instruction.Dup()));
                            }
                            else
                            {
                                throw new Exception("Dup takes 0 arguments.");
                            }

                            break;
                        case "geq":
                            if (valueWithArgs.Length == 1)
                            {
                                instructions.Add(new Instruction(address, valueWithArgs, () => Instruction.Geq()));
                            }
                            else
                            {
                                throw new Exception("Geq takes 0 arguments.");
                            }

                            break;

                        case "halt":
                            if (valueWithArgs.Length == 1)
                            {
                                instructions.Add(new Instruction(address, valueWithArgs, () => Instruction.Halt()));
                            }
                            else
                            {
                                throw new Exception("Halt takes 0 arguments.");
                            }

                            break;
                        case "jumpi":
                            if (valueWithArgs.Length == 2)
                            {
                                instructions.Add(new Instruction(address,
                                    valueWithArgs,
                                    () => Instruction.Jumpi(int.Parse(valueWithArgs[1]))));
                            }
                            else
                            {
                                throw new Exception("Jumpi takes 1 argument.");
                            }

                            break;
                        case "jumpz":
                            if (valueWithArgs.Length == 2)
                            {
                                instructions.Add(new Instruction(address,
                                    valueWithArgs,
                                    () => Instruction.Jumpz(valueWithArgs[1])));
                            }
                            else
                            {
                                throw new Exception("Jumpz takes 1 argument.");
                            }

                            break;
                        case "le":
                            if (valueWithArgs.Length == 1)
                            {
                                instructions.Add(new Instruction(address, valueWithArgs, () => Instruction.Le()));
                            }
                            else
                            {
                                throw new Exception("Le takes 0 arguments.");
                            }

                            break;
                        case "leq":
                            if (valueWithArgs.Length == 1)
                            {
                                instructions.Add(new Instruction(address, valueWithArgs, () => Instruction.Leq()));
                            }
                            else
                            {
                                throw new Exception("Leq takes 0 arguments.");
                            }

                            break;
                        case "load":
                            if (valueWithArgs.Length == 1)
                            {
                                instructions.Add(new Instruction(address, valueWithArgs, () => Instruction.Load()));
                            }
                            else if (valueWithArgs.Length == 2)
                            {
                                instructions.Add(new Instruction(address,
                                    valueWithArgs,
                                    () => Instruction.Load(int.Parse(valueWithArgs[1]))));
                            }
                            else
                            {
                                throw new Exception("Load takes 0 to 1 argument.");
                            }

                            break;
                        case "loada":
                            if (valueWithArgs.Length == 2)
                            {
                                instructions.Add(new Instruction(address,
                                    valueWithArgs,
                                    () => Instruction.Loada(int.Parse(valueWithArgs[1]))));
                            }
                            else if (valueWithArgs.Length == 3)
                            {
                                instructions.Add(new Instruction(address,
                                    valueWithArgs,
                                    () =>
                                        Instruction.Loada(int.Parse(valueWithArgs[1]), int.Parse(valueWithArgs[2]))));
                            }
                            else
                            {
                                throw new Exception("Loada takes 1 to 2 arguments.");
                            }

                            break;
                        case "loadc":
                            if (valueWithArgs.Length == 2)
                            {
                                instructions.Add(new Instruction(address,
                                    valueWithArgs,
                                    () => Instruction.Loadc(valueWithArgs[1])));
                            }
                            else
                            {
                                throw new Exception("Loadc takes 1 argument.");
                            }

                            break;
                        case "loadr":
                            if (valueWithArgs.Length == 2)
                            {
                                instructions.Add(new Instruction(address,
                                    valueWithArgs,
                                    () => Instruction.Loadr(int.Parse(valueWithArgs[1]))));
                            }
                            else if (valueWithArgs.Length == 3)
                            {
                                instructions.Add(new Instruction(address,
                                    valueWithArgs,
                                    () =>
                                        Instruction.Loadr(int.Parse(valueWithArgs[1]), int.Parse(valueWithArgs[2]))));
                            }
                            else
                            {
                                throw new Exception("Loadr takes 1 to 2 arguments.");
                            }

                            break;
                        case "loadrc":
                            if (valueWithArgs.Length == 2)
                            {
                                instructions.Add(new Instruction(address,
                                    valueWithArgs,
                                    () => Instruction.Loadrc(int.Parse(valueWithArgs[1]))));
                            }
                            else
                            {
                                throw new Exception("Loadrc takes 1 argument.");
                            }

                            break;
                        case "mark":
                            if (valueWithArgs.Length == 1)
                            {
                                instructions.Add(new Instruction(address, valueWithArgs, () => Instruction.Mark()));
                            }
                            else
                            {
                                throw new Exception("Mark takes 0 arguments.");
                            }

                            break;
                        case "mul":
                            if (valueWithArgs.Length == 1)
                            {
                                instructions.Add(new Instruction(address, valueWithArgs, () => Instruction.Mul()));
                            }
                            else
                            {
                                throw new Exception("Mul takes 0 arguments.");
                            }

                            break;
                        case "new":
                            if (valueWithArgs.Length == 1)
                            {
                                instructions.Add(new Instruction(address, valueWithArgs, () => Instruction.New()));
                            }
                            else
                            {
                                throw new Exception("New takes 0 arguments.");
                            }

                            break;
                        case "pop":
                            if (valueWithArgs.Length == 1)
                            {
                                instructions.Add(new Instruction(address, valueWithArgs, () => Instruction.Pop()));
                            }
                            else
                            {
                                throw new Exception("Pop takes 0 arguments.");
                            }

                            break;
                        case "print":
                            if (valueWithArgs.Length == 1)
                            {
                                instructions.Add(new Instruction(address, valueWithArgs, () => Instruction.Print()));
                            }
                            else
                            {
                                throw new Exception("Print takes 0 arguments.");
                            }

                            break;
                        case "return":
                            if (valueWithArgs.Length == 2)
                            {
                                instructions.Add(new Instruction(address,
                                    valueWithArgs,
                                    () => Instruction.Return(int.Parse(valueWithArgs[1]))));
                            }
                            else
                            {
                                throw new Exception("Return takes 1 argument.");
                            }

                            break;
                        case "slide":
                            if (valueWithArgs.Length == 3)
                            {
                                instructions.Add(new Instruction(address,
                                    valueWithArgs,
                                    () =>
                                        Instruction.Slide(int.Parse(valueWithArgs[1]), int.Parse(valueWithArgs[2]))));
                            }
                            else
                            {
                                throw new Exception("Slide takes 2 argument.");
                            }

                            break;

                        case "sub":
                            if (valueWithArgs.Length == 1)
                            {
                                instructions.Add(new Instruction(address, valueWithArgs, () => Instruction.Sub()));
                            }
                            else
                            {
                                throw new Exception("Sub takes 0 arguments.");
                            }

                            break;
                        case "store":
                            if (valueWithArgs.Length == 1)
                            {
                                instructions.Add(new Instruction(address,
                                    valueWithArgs,
                                    () => Instruction.Store(int.Parse(valueWithArgs[1]))));
                            }
                            else if (valueWithArgs.Length == 2)
                            {
                                instructions.Add(new Instruction(address,
                                    valueWithArgs,
                                    () =>
                                        Instruction.Store(int.Parse(valueWithArgs[1]))));
                            }
                            else
                            {
                                throw new Exception("Store takes 0 to 1 argument.");
                            }

                            break;
                        case "storea":
                            if (valueWithArgs.Length == 2)
                            {
                                instructions.Add(new Instruction(address,
                                    valueWithArgs,
                                    () => Instruction.Storea(int.Parse(valueWithArgs[1]))));
                            }
                            else if (valueWithArgs.Length == 3)
                            {
                                instructions.Add(new Instruction(address,
                                    valueWithArgs,
                                    () =>
                                        Instruction.Storea(int.Parse(valueWithArgs[1]), int.Parse(valueWithArgs[2]))));
                            }
                            else
                            {
                                throw new Exception("Storea takes 1 to 2 arguments.");
                            }

                            break;
                        case "storer":
                            if (valueWithArgs.Length == 2)
                            {
                                instructions.Add(new Instruction(address,
                                    valueWithArgs,
                                    () => Instruction.Storer(int.Parse(valueWithArgs[1]))));
                            }
                            else if (valueWithArgs.Length == 3)
                            {
                                instructions.Add(new Instruction(address,
                                    valueWithArgs,
                                    () =>
                                        Instruction.Storer(int.Parse(valueWithArgs[1]), int.Parse(valueWithArgs[2]))));
                            }
                            else
                            {
                                throw new Exception("Storer takes 1 to 2 arguments.");
                            }

                            break;
                        default:
                            if (valueWithArgs[0] == "enter")
                            {
                                if (valueWithArgs.Length == 2)
                                {
                                    instructions.Add(new Instruction(address,
                                        valueWithArgs,
                                        () => Instruction.Enter(int.Parse(valueWithArgs[1]))));
                                }
                                else
                                {
                                    throw new Exception("Enter takes 1 arguments.");
                                }
                            }
                            else if (valueWithArgs[1] == "enter")
                            {
                                if (valueWithArgs.Length == 3)
                                {
                                    instructions.Add(new Instruction(address,
                                        valueWithArgs,
                                        () => Instruction.Enter(int.Parse(valueWithArgs[2])),
                                        valueWithArgs[0]));
                                }
                                else
                                {
                                    throw new Exception("Enter takes 2 arguments.");
                                }
                            }
                            else
                            {
                                throw new Exception("Unrecognized command");
                            }

                            break;
                    }

                    address++;
                }

                return instructions;
            }

            return new List<Instruction>();
        }

        #endregion
    }
}