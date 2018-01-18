using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Othello.Models;

namespace Othello.ViewsModels
{
    public interface IViewModel : INotifyPropertyChanged, IDisposable
    {
        ObservableCollection<RibbonItem> RibbonItems { get; set; }
    }
}
