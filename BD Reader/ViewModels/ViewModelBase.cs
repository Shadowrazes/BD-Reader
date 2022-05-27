using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BD_Reader.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        public virtual object? GetTable() { return null; }  // Возвращает массив строк таблицы
        public List<object>? RemovableItems { get; set; }   // Список удаляемых элементов
        public bool RemoveInProgress { get; set; } = false; // Происходит ли сейчас удаление строк в таблице
    }
}
