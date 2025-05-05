using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Serpent
    {
        private int _taille = 1;
        private Queue<Point> _coordonnee = new Queue<Point>();

        public Serpent(int grilleLargeur , int grillerLongueur) 
        {
            this._coordonnee.Enqueue(new Point((int)grilleLargeur/2, (int)grillerLongueur/2));
        }

        public bool seDeplacer(char keyPress , List<List<int>> Grille)
        {

        }

    }
}
