using System;
using System.Collections.ObjectModel;

namespace Othello.Models
{
    public interface IModel : IDisposable
    {
        ObservableCollection<RibbonItem> RibbonItems { get; set; }

        void Initialize();
    }
}
