using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SharedLibrary
{
    public class BindableBase : INotifyPropertyChanged
    {
        #region Fields

        private string _text = string.Empty;

        #endregion

        #region Properties

        public string Text
        {
            get => this._text;
            set => this.SetProperty(ref this._text, value);
        }

        #endregion

        #region Methods

        public event PropertyChangedEventHandler PropertyChanged;

        internal void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
            {
                return;
            }

            backingField = value;

            this.OnPropertyChanged(propertyName);
        }

        #endregion
    }
}