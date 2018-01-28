using System.Windows;
using System.Windows.Controls;
using Othello.Ribbons;

namespace Othello.Models.Ribbons
{
    public class DataTemplateRibbonSelector : DataTemplateSelector
    {
        public DataTemplate RibbonDataTemplate { get; set; }
        public DataTemplate SplitTemplate { get; set; }

        public override DataTemplate SelectTemplate(object _item, DependencyObject _container)
        {
            if (_item is RibbonButtonItem)
            {
                return RibbonDataTemplate;
            }
            if (_item is RibbonSplitItem)
            {
                return SplitTemplate;
            }
            return RibbonDataTemplate;
        }
    }
}
