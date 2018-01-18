using System.Windows;
using System.Windows.Controls;
using Othello.ViewsModels;

namespace Othello.Views
{
    /// <summary>
    /// Logique d'interaction pour RibbonBarView.xaml
    /// </summary>
    public partial class RibbonBarView : UserControl
    {
        public RibbonBarView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register("ViewModel", typeof(IViewModel), typeof(RibbonBarView), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public IViewModel ViewModel
        {
            get => (IViewModel) GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
