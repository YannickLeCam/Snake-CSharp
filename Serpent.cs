using System;
using System.Collections.Generic;

namespace Snake
{
    public class Serpent
    {
        private int _taille = 1;
        private List<Point> _coordonnee = new List<Point>();
        private int _grilleLargeur;
        private int _grilleLongueur;

        public Serpent(int grilleLargeur, int grilleLongueur)
        {
            _grilleLargeur = grilleLargeur;
            _grilleLongueur = grilleLongueur;
            _coordonnee.Add(new Point(grilleLargeur / 2, grilleLongueur / 2));
        }

        public bool SeDeplacer(char keyPress, List<List<int>> grille)
        {
            // Get the current head position (last element in the list)
            Point currentHead = _coordonnee[_coordonnee.Count - 1];
            Point newHead = new Point(currentHead.X, currentHead.Y);

            // Calculate the new head position based on the key pressed
            switch (keyPress)
            {
                case 'U': newHead.Y--; break;
                case 'D': newHead.Y++; break;
                case 'L': newHead.X--; break;
                case 'R': newHead.X++; break;
                default: return false; // Invalid key press
            }

            // Check for collisions with boundaries
            if (newHead.X < 0 || newHead.X >= _grilleLargeur || newHead.Y < 0 || newHead.Y >= _grilleLongueur)
            {
                return false; // Collision with boundary
            }

            // Check for collisions with itself
            if (_coordonnee.Contains(newHead))
            {
                return false; // Collision with itself
            }

            // Check if the new head position is on a fruit
            bool fruitEaten = grille[newHead.X][newHead.Y] == 2;

            // Update the grid
            _coordonnee.Add(newHead); // Add the new head position

            if (!fruitEaten)
            {
                Point tail = _coordonnee[0]; // Get the tail
                _coordonnee.RemoveAt(0); // Remove the tail
                grille[tail.X][tail.Y] = 0; // Clear the tail position
            }
            else
            {
                _taille++; // Increase the size of the snake
            }

            grille[newHead.X][newHead.Y] = 1; // Set the new head position

            return true;
        }

        public IEnumerable<Point> Positions => _coordonnee;
    }
}
