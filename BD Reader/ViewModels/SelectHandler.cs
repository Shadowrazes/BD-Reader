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
    public class SelectHandler: Handler
    {
        public SelectHandler(QueryManagerViewModel _QueryManager)
        {
            QM = _QueryManager;
        }

        public override void Try()
        {

        }
    }
}
