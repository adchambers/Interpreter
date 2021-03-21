using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SharedLibrary
{
    public static partial class DataStructure
    {
        #region Fields

        public static int HeapSize = 64;

        #endregion

        #region Properties

        public static ObservableCollection<int> MainMemoryS { get; set; }
        public static IList<Instruction> ProgramStoreC { get; set; }

        #endregion
    }
}