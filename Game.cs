using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Snake
{
    public class Cell : INotifyPropertyChanged
    {
        private Brush _color;
        public Brush Color
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged("Color");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Game : INotifyPropertyChanged
    {
        private ObservableCollection<Cell> _grille;
        private int _largeurGrille;
        private int _longueurGrille;
        private int _nbTour = 0;
        private Serpent _serpent;
        private char _keyPressed;
        private Point _fruitPosition;
        private Random _random = new Random();

        public ObservableCollection<Cell> Grille
        {
            get { return _grille; }
            set
            {
                _grille = value;
                OnPropertyChanged("Grille");
            }
        }

        public Game(int largeurGrille, int longueurGrille)
        {
            _longueurGrille = longueurGrille;
            _largeurGrille = largeurGrille;
            _grille = new ObservableCollection<Cell>();
            _serpent = new Serpent(largeurGrille, longueurGrille);

            // Initialize the grid with empty cells
            for (int i = 0; i < largeurGrille * longueurGrille; i++)
            {
                _grille.Add(new Cell { Color = Brushes.White });
            }

            // Set up the initial snake position
            _grille[largeurGrille / 2 + (longueurGrille / 2) * largeurGrille].Color = Brushes.Green;

            // Set up a fruit
            _fruitPosition = GenererNouveauFruit();
            _grille[_fruitPosition.X + _fruitPosition.Y * largeurGrille].Color = Brushes.Red;
        }

        public void AffiGrille()
        {
            // Clear the grid
            foreach (var cell in _grille)
            {
                cell.Color = Brushes.White;
            }

            // Update the snake position
            foreach (var position in _serpent.Positions)
            {
                _grille[position.X + position.Y * _largeurGrille].Color = Brushes.Green;
            }

            // Update the fruit position
            _grille[_fruitPosition.X + _fruitPosition.Y * _largeurGrille].Color = Brushes.Red;
        }

        public void UpdateGameState(char keyPress)
        {
            if (keyPress == '♥')
            {
                return;
            }
            List<List<int>> grille = new List<List<int>>();
            for (int i = 0; i < _largeurGrille; i++)
            {
                grille.Add(new List<int>());
                for (int j = 0; j < _longueurGrille; j++)
                {
                    grille[i].Add(0);
                }
            }

            // Update the grid based on the current game state
            foreach (var position in _serpent.Positions)
            {
                grille[position.X][position.Y] = 1;
            }

            // Update the fruit position
            grille[_fruitPosition.X][_fruitPosition.Y] = 2;

            // Move the snake
            bool gameOver = !_serpent.SeDeplacer(keyPress, grille, ref _fruitPosition);
            if (gameOver)
            {
                // Handle game over
                MessageBox.Show("Game Over!");
                return;
            }

            AffiGrille();
        }

        private Point GenererNouveauFruit()
        {
            Point newFruitPosition;
            do
            {
                newFruitPosition = new Point(
                    _random.Next(0, _largeurGrille),
                    _random.Next(0, _longueurGrille)
                );
            } while (_serpent.Positions.Any(p => p.X == newFruitPosition.X && p.Y == newFruitPosition.Y));

            return newFruitPosition;
        }


        public Point GetFruitPosition()
        {
            Point newFruitPosition;
            do
            {
                newFruitPosition = new Point(_random.Next(0, _largeurGrille), _random.Next(0, _longueurGrille));
            } while (_serpent.Positions.Contains(newFruitPosition));

            return newFruitPosition;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
