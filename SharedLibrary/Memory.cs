using System.Collections.ObjectModel;

namespace SharedLibrary
{
    public static class Memory
    {
        #region Fields

        public static int TotalAllocatedMemory = 56;

        #endregion

        #region Constructors

        static Memory()
        {
            Memory.BindableBase = new BindableBase();

            Memory.InitMainMemory();
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

            for (int i = 0; i <= Memory.TotalAllocatedMemory; i++)
            {
                Memory.Main.Add(0);
            }
        }

        #endregion
    }
}