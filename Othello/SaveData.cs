using System;
using System.Collections.ObjectModel;
using Othello.Pawns;

namespace Othello
{
    [Serializable]
    public class SaveData
    {
        public TimeSpan BlackTime { get; set; }
        public TimeSpan WhiteTime { get; set; }
        public ObservableCollection<PawnColor> Pawns { get; set; }
        public PawnColor CurrentPlayer { get; set; }
    }
}
