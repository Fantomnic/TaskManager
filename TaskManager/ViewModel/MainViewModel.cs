using System.Collections.ObjectModel;
using TaskManager.Commands;
using TaskManager.Model;

namespace TaskManager.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        // TODO: Вообще, ObservableCollection тут не нужно, т.к. не выводится в интерфейс
        /// <summary>Список всех разделов трекера</summary>
        public ObservableCollection<Section> Sections { get; set; } = [];

        // Не null, т.к. заполняется при инициализации главного окна
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Section SelectedSection { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        internal Section BaseSection => Sections.First(s => s.IsBaseSection);
    }
}
