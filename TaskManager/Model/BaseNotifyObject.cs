using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TaskManager.Model
{
    public abstract class BaseNotifyObject : BaseObject, INotifyPropertyChanged
    {
        public override string Name
        {
            get => base.Name;
            set
            {
                base.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public override string ToString() => Name;
    }
}
