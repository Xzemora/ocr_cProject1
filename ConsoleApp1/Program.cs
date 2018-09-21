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
                
            }

            public void Attaque( MonstreFacile monstre)
            {
                int deJoueur = De.LanceLeDe();
                int deMonstre = De.LanceLeDe();

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
                return De.LanceLeDe();
            }
            public int LanceLeDe( int valeur)
            {
                return De.LanceLeDe(valeur);
            }
        }

        public class MonstreFacile
        {
            protected const int degatsBase = 10;
            
            public bool EstVivant {get; private set;}

            public MonstreFacile()
            {
                EstVivant = true;
                
            }

            public void EstVaincu()
            {
                EstVivant = false;
            }

            public virtual void Attaque(Joueur joueur)
            {
                int deMonstre = De.LanceLeDe();
                int deJoueur = De.LanceLeDe();
                if (deMonstre > deJoueur)
                {
                    int perçageBouclier = De.LanceLeDe();
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
                return De.LanceLeDe();
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
                int rand = De.LanceLeDe();
                if(rand == 6)
                {
                    rand = 0;
                }
                return degatsMagique * rand;
            }
        }

        public static class De
        {
            private  static Random random = new Random();

            

            public static int LanceLeDe()
            {
                return random.Next(1, 7);
            }

            public static int LanceLeDe(int valeur)
            {
                return random.Next(1, valeur);

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
            Console.Write($"Vous avez tué {mfVaincus} monstres faciles et {mdVaincus} monstres difficiles. Vous avez {mfVaincus + mdVaincus * 2} points.");
        }
    }
}
