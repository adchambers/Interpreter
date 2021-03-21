namespace SharedLibrary
{
    public class Registers : BindableBase
    {
        #region Fields

        public const int ProgramCounterStart = 0;

        public int _extremePointer;
        public int _framePointer;
        public int _heapPointer;
        public int _programCounter;
        public int _stackPointer;

        #endregion

        #region Constructors

        public Registers()
        {
            this.Init();

            this.ProgramCounter = Registers.ProgramCounterStart;
        }

        #endregion

        #region Properties

        public int StackPointer
        {
            get => this._stackPointer;
            set => this.SetProperty(ref this._stackPointer, value);
        }

        public int FramePointer
        {
            get => this._framePointer;
            set => this.SetProperty(ref this._framePointer, value);
        }

        public int ProgramCounter
        {
            get => this._programCounter;
            set => this.SetProperty(ref this._programCounter, value);
        }

        public int HeapPointer
        {
            get => this._heapPointer;
            set => this.SetProperty(ref this._heapPointer, value);
        }

        public int ExtremePointer
        {
            get => this._extremePointer;
            set => this.SetProperty(ref this._extremePointer, value);
        }

        #endregion

        #region Methods

        public void Init()
        {
            this.StackPointer = -1;

            this.FramePointer = 0;

            this.HeapPointer = DataStructure.HeapSize;

            this.ExtremePointer = -1;
        }

        #endregion
    }
}