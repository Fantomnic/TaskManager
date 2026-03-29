using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TaskManager.Model
{
    public abstract class BaseObject : INotifyPropertyChanged
    {
        private Guid _guid;
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public override string ToString() => Name;
    }
}
