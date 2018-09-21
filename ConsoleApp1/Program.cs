using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo playAgain;

            do
            {
                Jeu();

                Console.WriteLine("Voulez-vous jouer à nouveau: O/N");

                playAgain = Console.ReadKey(true);

            } while (playAgain.Key == ConsoleKey.O);
            

            Console.WriteLine("Merci d'avoir joué. A bientôt. Appuyez sur une touche pour fermer.");
            Console.ReadKey(true);
            
        }

        public class Joueur
        {
            
            public int PointsDeVie {get; private set;}
            private De de;

            public bool EstVivant {
                get
                {
                    if (PointsDeVie > 0)
                    {
                        return true;
                    } else
                    {
                        return false;
                    }
                }
            }

            public Joueur(int ptsDeVie)
            {
                PointsDeVie = ptsDeVie;
                de = new De();
            }

            public void Attaque( MonstreFacile monstre)
            {
                int deJoueur = de.LanceLeDe();
                int deMonstre = de.LanceLeDe();

                if (deJoueur >= deMonstre) {
                    monstre.EstVaincu();
                }
                else
                {
                    monstre.Attaque(this);
                }
                
            }

            public void SubitDesDegats(int degats)
            {
                PointsDeVie -= degats;
            }

            public int LanceLeDe()
            {
                return de.LanceLeDe();
            }
        }

        public class MonstreFacile
        {
            protected const int degatsBase = 10;
            protected De de;
            public bool EstVivant {get; private set;}

            public MonstreFacile()
            {
                EstVivant = true;
                de = new De();
            }

            public void EstVaincu()
            {
                EstVivant = false;
            }

            public virtual void Attaque(Joueur joueur)
            {
                int deMonstre = de.LanceLeDe();
                int deJoueur = de.LanceLeDe();
                if (deMonstre > deJoueur)
                {
                    int perçageBouclier = de.LanceLeDe();
                    if (perçageBouclier > 2)
                    {
                        joueur.SubitDesDegats(degatsBase);
                        
                    };
                    
                } else
                {
                    return;
                }
                
            }

            public int LanceLeDe()
            {
                return de.LanceLeDe();
            }


        }

        public class MonstreDifficile : MonstreFacile
        {
            private const int degatsMagique = 5;

            public override void Attaque(Joueur joueur)
            {
                base.Attaque(joueur);
                joueur.SubitDesDegats(AttaqueMagique());
            }

            public int AttaqueMagique()
            {
                int rand = de.LanceLeDe();
                if(rand == 6)
                {
                    rand = 0;
                }
                return degatsMagique * rand;
            }
        }

        public class De
        {
            private Random random;

            public De()
            {
                random = new Random();
            }

            public int LanceLeDe()
            {
                return random.Next(1, 7);
            }
        }

        private static Random random = new Random();

        private static MonstreFacile RandomizeMonster()
        {
            int rand = random.Next(2);

            if (rand == 0)
            {
                return new MonstreFacile();
            } else
            {
                return new MonstreDifficile();
            }
        }

        private static void Jeu()
        {
            Joueur nouveauJoueur = new Joueur(150);
            int mfVaincus = 0;
            int mdVaincus = 0;

            while (nouveauJoueur.EstVivant)
            {
                
                MonstreFacile monstre = RandomizeMonster();

                while (nouveauJoueur.EstVivant && monstre.EstVivant)
                {
                    nouveauJoueur.Attaque(monstre);
                    
                }

                if (nouveauJoueur.EstVivant)
                {
                    if(monstre is MonstreDifficile)
                    {
                        mdVaincus++;
                    }
                    else
                    {
                        mfVaincus++;
                    }
                } else
                {
                    Console.WriteLine("Vous avez été vaincu.");
                    break;
                }
            }
            Console.Write("Vous avez tué {0} monstres faciles et {1} monstres difficiles. Vous avez {2} points.", mfVaincus, mdVaincus, mfVaincus + mdVaincus * 2);
        }
    }
}
