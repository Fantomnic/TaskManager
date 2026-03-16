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
        }

        public static NewSectionCommand NewSectionCommand { get; set; }

        public static DeleteSectionCommand DeleteSectionCommand { get; set; }

        public static ShowSectionPropertyCommand ShowSectionPropertyCommand { get; set; }

        public static NewTaskCommand NewTaskCommand { get; set; }

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

        public Section? SelectedSection { get; set; }
    }
}
