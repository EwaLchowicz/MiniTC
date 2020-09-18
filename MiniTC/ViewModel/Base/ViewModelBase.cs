using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MiniTC.ViewModel.Base
{
    class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void onPropertyChanged(params string[] namesOfProperties)
        {
            if (PropertyChanged != null)
            {
                foreach (var prop in namesOfProperties)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
                }
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            this.onPropertyChanged(new[] { name });
        }
    }
}
