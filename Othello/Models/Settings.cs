using System.Collections.ObjectModel;
using Othello.Models.Ribbons;
using Othello.Ribbons;

namespace Othello
{
    public class Settings : IModel
    {
        public void Initialize()
        {

        }

        public void Dispose()
        {
            
        }

        public ObservableCollection<AbstractRibbonItem> RibbonItems { get; set; }
    }
}
