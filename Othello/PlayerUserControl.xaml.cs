using System;
using System.Windows;
using System.Windows.Input;

namespace Othello
{
    /// <summary>
    /// Logique d'interaction pour PlayerUserControl.xaml
    /// </summary>
    public partial class PlayerUserControl
    {
        public PlayerUserControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty PlayerProperty = DependencyProperty.Register("Player", typeof(Player), typeof(PlayerUserControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Player Player
        {
            get => (Player)GetValue(PlayerProperty);
            set => SetValue(PlayerProperty, value);
        }
    }
}
