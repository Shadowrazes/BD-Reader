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

namespace BD_Reader.ViewModels
{
    public class JoinHandler : Handler
    {
        public JoinHandler(QueryManagerViewModel _QueryManager)
        {
            QM = _QueryManager;
        }

        private bool TryJoin(string key1, List<Dictionary<string, object?>> table2, string key2)
        {
            try
            {
                QM.JoinedTable = QM.JoinedTable.Join(
                    table2,
                    firstItem => firstItem[key1],
                    secondItem => secondItem[key2],
                    (firstItem, secondItem) =>
                    {
                        Dictionary<string, object?> resultItem = new Dictionary<string, object?>();
                        foreach (var item in firstItem)
                        {
                            resultItem.TryAdd(item.Key, item.Value);
                        }
                        foreach (var item in secondItem)
                        {
                            if (item.Key != key2)
                                resultItem.TryAdd(item.Key, item.Value);
                        }
                        return resultItem;
                    }
                    ).ToList();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public override void Try()
        {
            if (QM.SelectedTables.Count > 0)
            {
                var check = QM.SelectedTables.Where(tab => tab.Name == "Events");
                if (check.Count() != 0)
                {
                    Table tmp = check.Last();
                    QM.SelectedTables.Remove(check.Last());
                    QM.SelectedTables.Add(tmp);
                }
                QM.JoinedTable = new List<Dictionary<string, object?>>(QM.SelectedTables[0].Rows);
                if (QM.SelectedTables.Count > 1)
                {
                    List<Dictionary<string, object?>> joiningTable;
                    bool success = false;
                    for (int i = 1; i < QM.SelectedTables.Count; i++)
                    {
                        joiningTable = QM.SelectedTables[i].Rows;
                        foreach (var keysPair in QM.Keys)
                        {
                            success = TryJoin(keysPair.Key, joiningTable, keysPair.Value);
                            if (success)
                                break;
                            else
                            {
                                success = TryJoin(keysPair.Value, joiningTable, keysPair.Key);
                                if (success)
                                    break;
                            }
                        }
                        if (!success)
                        {
                            QM.JoinedTable.Clear();
                            QM.UpdateColumnList();
                            return;
                        }
                    }
                }
                QM.UpdateColumnList();
            }
            else
            {
                QM.JoinedTable.Clear();
                QM.ColumnList.Clear();
            }
        }
    }
}
