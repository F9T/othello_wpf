using System;
using System.Collections.ObjectModel;
using Othello.Models.Ribbons;
using Othello.Ribbons;

namespace Othello
{
    public interface IModel : IDisposable
    {
        ObservableCollection<AbstractRibbonItem> RibbonItems { get; set; }

        void Initialize();
    }
}
