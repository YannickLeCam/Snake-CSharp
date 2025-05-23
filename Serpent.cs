using System;
using System.Collections.Generic;

namespace Snake
{
    public class Serpent
    {
        private int _taille = 1;
        private LinkedList<Point> _coordonnee = new LinkedList<Point>();
        private int _grilleLargeur;
        private int _grilleLongueur;

        public Serpent(int grilleLargeur, int grilleLongueur)
        {
            _grilleLargeur = grilleLargeur;
            _grilleLongueur = grilleLongueur;
            _coordonnee.AddLast(new Point(grilleLargeur / 2, grilleLongueur / 2));
        }

        public bool SeDeplacer(char keyPress, List<List<int>> grille, ref Point fruitPosition)
        {
            // Get the current head position
            Point currentHead = _coordonnee.Last.Value;
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

            bool biteItself = grille[newHead.X][newHead.Y] == 1;
            if (biteItself) 
            {
                return false;
            }
            // Check if the new head position is on a fruit
            bool fruitEaten = grille[newHead.X][newHead.Y] == 2;

            // Update the grid
            _coordonnee.AddLast(newHead); // Add the new head position

            if (!fruitEaten)
            {
                Point tail = _coordonnee.First.Value; // Get the tail
                _coordonnee.RemoveFirst(); // Remove the tail
                grille[tail.X][tail.Y] = 0; // Clear the tail position
            }
            else
            {
                _taille++; // Increase the size of the snake
                           // Generate new fruit position
                fruitPosition = GenererNouveauFruit(grille);
            }

            grille[newHead.X][newHead.Y] = 1; // Set the new head position

            return true;
        }

        private Point GenererNouveauFruit(List<List<int>> grille)
        {
            Random random = new Random();
            Point newFruitPosition;
            do
            {
                newFruitPosition = new Point(
                    random.Next(0, _grilleLargeur),
                    random.Next(0, _grilleLongueur)
                );
            } while (grille[newFruitPosition.X][newFruitPosition.Y] != 0); // Continue until empty cell is found

            return newFruitPosition;
        }

        public IEnumerable<Point> Positions => _coordonnee;
        public int Taille => _taille;
    }
}
