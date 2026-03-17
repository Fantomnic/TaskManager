using System.Collections.ObjectModel;
using TaskManager.Commands;
using TaskManager.Model;

namespace TaskManager.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        //private SectionView _currentSectionView;

        static MainViewModel()
        {
            DeleteSectionCommand = new DeleteSectionCommand();
            NewSectionCommand = new NewSectionCommand();
            ShowSectionPropertyCommand = new ShowSectionPropertyCommand();
            NewTaskCommand = new NewTaskCommand();
            DeleteTaskCommand = new DeleteTaskCommand();
        }

        public static NewSectionCommand NewSectionCommand { get; set; }

        public static DeleteSectionCommand DeleteSectionCommand { get; set; }

        public static ShowSectionPropertyCommand ShowSectionPropertyCommand { get; set; }

        public static NewTaskCommand NewTaskCommand { get; set; }

        public static DeleteTaskCommand DeleteTaskCommand { get; set; }

        //public SectionView CurrentSectionView
        //{
        //    get => _currentSectionView;
        //    set
        //    {
        //        _currentSectionView = value;
        //        OnPropertyChanged(nameof(CurrentSectionView));
        //    }
        //}

        // TODO: Вообще, ObservableCollection тут не нужно, т.к. не выводится в интерфейс
        /// <summary>Список разделов</summary>
        public ObservableCollection<Section> Sections { get; set; } = [];

        // Не null, т.к. заполняется при инициализации главного окна
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Section SelectedSection { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    }
}
