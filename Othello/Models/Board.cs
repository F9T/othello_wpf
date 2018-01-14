﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows.Input;
using System.Windows.Media;
using Othello.Models;
using Othello.Properties;

namespace Othello
{
    public class Board : IModel, INotifyPropertyChanged, IDisposable
    {
        private readonly Timer timer;

        public Board()
        {
            RibbonItems = new ObservableCollection<RibbonItem>
            {
                new RibbonItem("../Images/play.png", new RelayCommand(_param => StartGame())),
                new RibbonItem("../Images/pause.png", new RelayCommand(_param => StopGame())),
                new RibbonItem("../Images/save.png", new RelayCommand(_param => Save())),
                new RibbonItem("../Images/load.png", new RelayCommand(_param => Open()))
            };
            timer = new Timer(1000);
            timer.Elapsed += TimerTick;
            Initialize();
            InitializeGame();
        }

        public ObservableCollection<RibbonItem> RibbonItems { get; set; }

        public void Initialize()
        {
            Pawns = new ObservableCollection<Pawn>();
            BackgroundColor = new SolidColorBrush(Colors.MediumSeaGreen);
            UseBackgroundImage = false;
            BackgroundImage = "/Images/background.png";
            BlackPlayer = new Player(PawnColor.Black, "Black");
            WhitePlayer = new Player(PawnColor.White, "White");
            SquareSize = 8;
        }

        public void InitializeGame()
        {
            IsStarted = false;
            Reset(BlackPlayer, WhitePlayer);
            /**
             * 
             * remove after
             */
            StartGame();
        }

        private void TimerTick(object _sender, ElapsedEventArgs _elapsedEventArgs)
        {
            if (IsStarted)
            {
                CurrentPlayer.Time -= new TimeSpan(0, 0, 0, 1);
            }
        }

        public void StartGame()
        {
            BlackPlayer.Reset();
            WhitePlayer.Reset();
            CurrentPlayer = BlackPlayer;
            GetLegalMove(CurrentPlayer);
            UpdateScore();
            IsStarted = true;
            timer.Start();
        }

        public void ResumeGame()
        {
            IsStarted = true;
            timer.Start();
        }

        public void StopGame()
        {
            IsStarted = false;
            timer.Stop();
        }

        public void EndGame()
        {
            timer.Elapsed -= TimerTick;
            timer?.Dispose();
        }

        public void Save()
        {
            
        }

        public void Open()
        {
            
        }

        public void ChangePlayer()
        {
            CurrentPlayer = CurrentPlayer == WhitePlayer ? BlackPlayer : WhitePlayer;
            if (GetLegalMove(CurrentPlayer).Count == 0)
            {
                CurrentPlayer = CurrentPlayer == WhitePlayer ? BlackPlayer : WhitePlayer;
            }
        }

        private void UpdateScore()
        {
            UpdateScore(BlackPlayer);
            UpdateScore(WhitePlayer);
        }

        public bool UseBackgroundImage { get; set; }

        public int SquareSize { get; set; }

        public ObservableCollection<Pawn> Pawns { get; set; }

        public string BackgroundImage { get; set; }

        public bool IsStarted { get; set; }

        public Brush BackgroundColor { get; set; }

        public Player CurrentPlayer { get; set; }

        public Player BlackPlayer { get; set; }

        public Player WhitePlayer { get; set; }

        public void PlayMove(int _index)
        {
            bool isPlayable = IsPlayable(_index, CurrentPlayer, true);
            if (isPlayable)
            {
                AddPawn(_index, CurrentPlayer);
                ChangePlayer();
                UpdateScore();
            }
        }

        public void Reset(Player _blackPlayer, Player _whitePlayer)
        {
            Pawns.Clear();
            int size = SquareSize * SquareSize;
            int indexMiddle = (size - 1 - SquareSize) / 2;
            for (int i = 0; i < size; i++)
            {
                Player player = null;
                if (i == indexMiddle || i == indexMiddle + (SquareSize + 1))
                {
                    player = _blackPlayer;
                }
                else if (i == indexMiddle + 1 || i == indexMiddle + SquareSize)
                {
                    player = _whitePlayer;
                }
                Pawns.Add(new Pawn(player, i));
            }
        }

        public List<int> GetLegalMove(Player _player)
        {
            var listIndex = new List<int>();
            for (int i = 0; i < Pawns.Count; i++)
            {
                var pawn = Pawns.ElementAt(i);
                if (IsPlayable(i, _player))
                {
                    pawn.IsPlayable = true;
                    listIndex.Add(i);
                }
                else
                {
                    pawn.IsPlayable = false;
                }
            }
            return listIndex;
        }

        public void UpdateScore(Player _player)
        {
            int score = 0;
            foreach (var pawn in Pawns)
            {
                if (pawn.GetColor() == _player.Color)
                {
                    score++;
                }
            }
            _player.ActualScore = score;
        }

        public bool IsPlayable(int _index, Player _player, bool _turn = false)
        {
            //Start with diagonal on top left
            var direction = -SquareSize - 1;
            if (Pawns.ElementAt(_index).GetColor() != PawnColor.Empty)
            {
                return false;
            }
            bool globalIsPlayable = false;
            for (int numberDirection = 0; numberDirection < 8; numberDirection++)
            {
                if (CheckDirection(_index, direction, _player, _turn))
                {
                    globalIsPlayable = true;
                    if (!_turn)
                    {
                        return true;
                    }
                }
                direction++;
                if (numberDirection == 2)
                {
                    direction = -1;
                }
                else if (numberDirection == 3)
                {
                    direction = 1;
                }
                else if (numberDirection == 4)
                {
                    direction = SquareSize - 1;
                }
            }
            return globalIsPlayable;
        }

        private bool CheckDirection(int _index, int _direction, Player _player, bool _turn)
        {
            bool findOtherColor = false;
            int newIndex = _index + _direction;
            bool isPlayable = false;
            var turnPawn = new List<int>();
            var color = _player.Color;
            while (true)
            {
                if (newIndex < 0)
                {
                    break;
                }
                if (newIndex > (SquareSize * SquareSize) - 1)
                {
                    break;
                }
                var findPawnColor = Pawns.ElementAt(newIndex).GetColor();
                if (findPawnColor == color && !findOtherColor)
                {
                    break;
                }
                if (findPawnColor == color && findOtherColor)
                {
                    if (_turn)
                    {
                        isPlayable = true;
                        break;
                    }
                    //One direction is possible, useless check all direction (if not in turn mode)
                    return true;
                }
                if (findPawnColor == PawnColor.Empty)
                {
                    break;
                }
                if (findPawnColor != color)
                {
                    findOtherColor = true;
                    if (_turn)
                    {
                        turnPawn.Add(newIndex);
                    }
                }
                //Check if edges
                if (newIndex % SquareSize == 0)
                {
                    if (((newIndex - _direction) + 1) % SquareSize == 0)
                    {
                        break;
                    }
                }
                if ((newIndex + 1) % SquareSize == 0)
                {
                    if ((newIndex - _direction) % SquareSize == 0)
                    {
                        break;
                    }
                }
                newIndex += _direction;
            }
            if (isPlayable)
            {
                foreach (int i in turnPawn)
                {
                    var pawn = Pawns.ElementAt(i);
                    pawn.Owner = _player;
                    pawn.IsPlayable = false;
                }
            }
            return isPlayable;
        }

        public void AddPawn(int _index, Player _player)
        {
            Pawns.ElementAt(_index).Owner = _player;
        }

        public void Dispose()
        {
            if (IsStarted)
            {
                EndGame();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}