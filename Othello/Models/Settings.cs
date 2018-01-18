using System.Collections.ObjectModel;

namespace Othello.Models
{
    public class Settings : IModel
    {
        public void Initialize()
        {

        }

        public void Dispose()
        {
            
        }

        public ObservableCollection<RibbonItem> RibbonItems { get; set; }
    }
}
