using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BD_Reader.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        public bool RemoveInProgress { get; set; } = false;                     // Происходит ли сейчас удаление строк в таблице
        public List<object>? RemovableItems { get; set; }                       // Список удаляемых элементов

        // Возвращает массив строк таблицы
        public virtual object? GetTable() { return null; }

        // Возвращает список строк таблицы, приведенный к списку словарей
        public virtual List<Dictionary<string, object?>> GetRows()
        {
            return new List<Dictionary<string, object?>>();
        }
    }
}
