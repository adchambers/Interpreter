﻿using System.Collections.ObjectModel;

namespace SharedLibrary
{
    public static class Memory
    {
        #region Fields

        public static int HeapSize = 48;

        #endregion

        #region Constructors

        static Memory()
        {
            Memory.BindableBase = new BindableBase();

            Memory.InitMainMemory();

            //Memory.BindableBase.Text = "enter 2\n"
            //                           + "loadc 42\n"
            //                           + "alloc 1\n"
            //                           + "enter 5\n"
            //                           + "alloc 1\n"
            //                           + "mark\n"
            //                           + "loadc _main\n"
            //                           + "call 1\n"
            //                           + "halt\n"
            //                           + "_main enter 10\n"
            //                           + "loadc 30\n"
            //                           + "loadc 40\n"
            //                           + "alloc 1\n"
            //                           + "alloc 1\n"
            //                           + "mark\n"
            //                           + "loadr 1\n"
            //                           + "loadr 2\n"
            //                           + "loadc _add\n"
            //                           + "call 3\n"
            //                           + "storer 3\n"
            //                           + "loadr 3\n"
            //                           + "return 0\n"
            //                           + "_add enter 3\n"
            //                           + "loadc 20\n"
            //                           + "loadr 1\n"
            //                           + "loadr 2\n"
            //                           + "add\n"
            //                           + "add\n"
            //                           + "storer -3\n"
            //                           + "return 0";
            Memory.BindableBase.Text =
                "enter 5\n"
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
        }

        #endregion

        #region Properties

        public static BindableBase BindableBase { get; set; }

        public static ObservableCollection<int> Main { get; set; }

        #endregion

        #region Methods

        public static void InitMainMemory()
        {
            Memory.Main = new ObservableCollection<int>();

            for (int i = 0; i <= Memory.HeapSize; i++)
            {
                Memory.Main.Add(0);
            }
        }

        #endregion
    }
}