using System.Windows;

namespace Othello.Views
{
    /// <summary>
    /// Logique d'interaction pour PlayerView.xaml
    /// </summary>
    public partial class PlayerView
    {
        public PlayerView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty PlayerProperty = DependencyProperty.Register("Player", typeof(Player), typeof(PlayerView), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Player Player
        {
            get => (Player)GetValue(PlayerProperty);
            set => SetValue(PlayerProperty, value);
        }
    }
}
