using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Linq;
using TaskManager.Helpers;
using TaskManager.View;
using TaskManager.ViewModel;

namespace TaskManager.Commands
{
    internal class NewSectionCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            AddSection();
        }

        internal void AddSection(bool baseSection = false)
        {
            SectionViewModel sectionViewModel;

            if (baseSection)
            {
                sectionViewModel = new SectionViewModel("Все");
            }
            else
            {
                sectionViewModel = new SectionViewModel("Новый раздел");

                var windowProperty = new SectionPropertyWindow(sectionViewModel);

                if (windowProperty.ShowDialog() != true)
                    return;
            }

            var newItem = CreateTabItem(sectionViewModel, baseSection);
            Helper.MainWindow.sections.Items.Add(newItem);
            newItem.Focus();
        }

        private TabItem CreateTabItem(SectionViewModel sectionViewModel, bool baseSection = false)
        {
            var textBlock = CreateSectionHeader(sectionViewModel);

            var section = new TabItem()
            {
                Header = textBlock,
                Content = new SectionView(sectionViewModel),
            };

            textBlock.ContextMenu = CreateSectionHeaderContextMenu();

            return section;

            ContextMenu CreateSectionHeaderContextMenu()
            {
                var menuItems = baseSection
                    ? CreateBaseSectionHeaderContextMenuItemsList(sectionViewModel)
                    : CreateNewSectionHeaderContextMenuItemsList(sectionViewModel, section);

                menuItems = [.. menuItems.OrderBy(x => x.Header)];

                var menu = new ContextMenu();

                foreach (var menuItem in menuItems)
                    menu.Items.Add(menuItem);

                return menu;
            }
        }

        private static TextBlock CreateSectionHeader(SectionViewModel sectionViewModel)
        {
            var textBlock = new TextBlock()
            {
                DataContext = sectionViewModel,
            };

            var headerNameBinding = new Binding
            {
                Path = new PropertyPath(nameof(sectionViewModel.Name))
            };

            textBlock.SetBinding(TextBlock.TextProperty, headerNameBinding);

            return textBlock;
        }

        private List<MenuItem> CreateNewSectionHeaderContextMenuItemsList(SectionViewModel sectionViewModel, TabItem section)
        {
            var menuItems = CreateBaseSectionHeaderContextMenuItemsList(sectionViewModel);

            menuItems.AddRange(
                [
                    new() { Header = "Удалить раздел", Command = MainViewModel.DeleteSectionCommand, CommandParameter = section },
                ]);

            return menuItems;
        }

        private List<MenuItem> CreateBaseSectionHeaderContextMenuItemsList(SectionViewModel sectionViewModel) => 
            [
                new() { Header = "Свойства", Command = MainViewModel.ShowSectionPropertyCommand, CommandParameter = sectionViewModel },
            ];
    }
}
