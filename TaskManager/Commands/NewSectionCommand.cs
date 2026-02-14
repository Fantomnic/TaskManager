using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using TaskManager.View;
using TaskManager.ViewModel;

namespace TaskManager.Commands
{
    internal class NewSectionCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly MainWindow _mainWindow;

        public NewSectionCommand() => _mainWindow = (MainWindow)Application.Current.MainWindow;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            AddSection();
        }

        internal void AddSection()
        {
            var windowProperty = new SectionPropertyWindow
            {
                Owner = _mainWindow,
            };

            if (windowProperty.ShowDialog() != true)
                return;

            string name = windowProperty.sectionName.Text;

            var sectionViewModel = new SectionViewModel(name);

            var textBlock = new TextBlock()
            {
                DataContext = sectionViewModel,
                //Text = "New",
                ContextMenu = CreateSectionContextMenu(),
            };

            var binding = new Binding
            {
                Path = new PropertyPath(nameof(sectionViewModel.Name))
            };

            textBlock.SetBinding(TextBlock.TextProperty, binding);

            var newItem = new TabItem()
            {
                DataContext = sectionViewModel,
                Header = textBlock,
                Content = new SectionView(),
            };

            FillSectionCommandsBindings(newItem);

            _mainWindow.sections.Items.Add(newItem);
            newItem.Focus();
        }

        private ContextMenu CreateSectionContextMenu()
        {
            var menu = new ContextMenu();
            menu.Items.Add(new MenuItem() { Command = Commands0.DeleteSectionCommand });
            menu.Items.Add(new MenuItem() { Header = "Свойства" });
            return menu;
        }

        private void FillSectionCommandsBindings(TabItem section)
        {
            var deleteBinding = new CommandBinding()
            {
                Command = Commands0.DeleteSectionCommand,
            };

            deleteBinding.Executed += CommandBinding_Executed;
            deleteBinding.CanExecute += CommandBinding_CanExecute;
            section.CommandBindings.Add(deleteBinding);
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var tabItem = (TabItem)sender;

            _mainWindow.sections.Items.Remove(tabItem);
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
