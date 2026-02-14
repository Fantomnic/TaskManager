using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TaskManager.ViewModel
{
    /// <summary>Базовый класс для всех моделей представления приложения</summary>
    internal abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
            => PropertyChanged?.Invoke(this, new(prop));
    }
}
