// MainWindowViewModel
// Главное окно приложения, которое сменяет окна просмотрщика таблиц и менеджера запросов

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ReactiveUI;
using System.Reactive;
using BD_Reader.Models;
using Microsoft.Data.Sqlite;
using System.IO;
using System;
using System.Reactive.Linq;

namespace BD_Reader.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase page;                 // Текущая страница
        private DBViewerViewModel dbViewer;         // Страница просмотра таблиц
        private QueryManagerViewModel queryManager; // Страница менеджера запросов

        public ViewModelBase Page
        {
            set => this.RaiseAndSetIfChanged(ref page, value);
            get => page;
        }

        public MainWindowViewModel()
        {
            dbViewer = new DBViewerViewModel();
            queryManager = new QueryManagerViewModel(dbViewer, this);
            Page = dbViewer;
        }

        // Открываем менеджер запросов
        public void OpenQueryManager()
        {
            Page = queryManager;

            // И удаляем из списка запросов нужные таблицы, если такие есть
            queryManager.DeleteRequests();
        }

        // Открываем окно просмотра таблиц
        public void OpenDBViewer()
        {
            Page = dbViewer;
        }
    }
}
