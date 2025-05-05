using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Game
    {
        //If 0 = vide if 1 = snake if 2 = fruit
        private int[,] _grille;
        private int _largeurGrille;
        private int _longueurGrille;
        private int _nbTour = 0;
        private Serpent _serpent;
        private char _keyPressed;

        public Game(int largeurGrille , int longueurGrille) 
        {
            _longueurGrille = longueurGrille;
            _largeurGrille = largeurGrille;
            _grille = new int[largeurGrille,longueurGrille];
            _serpent = new Serpent(largeurGrille,longueurGrille);
            //Set up du serpent de base
            _grille[largeurGrille/2,longueurGrille/2] = 1;
            Random rdm = new Random();
            //On set up un fruit
            _grille[rdm.Next(0, largeurGrille), rdm.Next(0, longueurGrille)] = 2;
        }

        public void affiGrille()
        {

        }

    }
}
