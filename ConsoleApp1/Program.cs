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
            Console.WriteLine("Veuillez saisir le nom de votre personnage :");
            string name = Console.ReadLine();
            do
            {
                
                if (Jeu(name))
                {
                    NiveauFinal(name);
                }

                Console.WriteLine("Voulez-vous jouer à nouveau: O/N");

                playAgain = Console.ReadKey(true);

            } while (playAgain.Key == ConsoleKey.O);
            

            Console.WriteLine("Merci d'avoir joué. A bientôt. Appuyez sur une touche pour fermer.");
            Console.ReadKey(true);
            
        }

        public class Joueur
        {
            public string nom;
            public int PointsDeVie {get; private set;}
            

            public bool EstVivant {
                get
                {
                    return PointsDeVie > 0;
                }
            }

            public Joueur(int ptsDeVie, string name )
            {
                PointsDeVie = ptsDeVie;
                this.nom = name;
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

            public void Attaque (BossDeFin boss)
            {
                int ptsAttaque = De.LanceLeDe(26);
                boss.SubitDesDegats(ptsAttaque);
                Console.WriteLine($"{this.nom} attaque le Boss pour {ptsAttaque} points de vie.");
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

        public class BossDeFin
        {
            public string nom = "Boss";
            public int PointsDeVie { get; private set; }

            public BossDeFin(int pts)
            {
                PointsDeVie = pts;
                
            }

            public bool EstVivant { get { return PointsDeVie > 0; } }

            public void Attaque(Joueur joueur)
            {
                int ptsAttaque = De.LanceLeDe(26);
                int perçageBouclier = De.LanceLeDe();
                if (perçageBouclier > 2)
                {
                    joueur.SubitDesDegats(ptsAttaque);
                    Console.WriteLine($"{this.nom} attaque le héros pour {ptsAttaque} points de vie.");
                }
                else
                {
                    Console.WriteLine($"{joueur.nom} bloque l'attaque avec son bouclier.");
                    return;
                }
            }


            public void SubitDesDegats(int ptsAttaque)
            {
                PointsDeVie -= ptsAttaque;
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

        private static bool Jeu(string name)
        {
            
            Joueur nouveauJoueur = new Joueur(150,name);
            
            int mfVaincus = 0;
            int mdVaincus = 0;
            int points  = 0;

            while (nouveauJoueur.EstVivant && points<50)
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
                    points = mfVaincus + mdVaincus * 2;


                } else
                {
                    Console.WriteLine($"{nouveauJoueur.nom} avez été vaincu.");
                    break;
                }
            }
            Console.Write($"{nouveauJoueur.nom} a tué {mfVaincus} monstres faciles et {mdVaincus} monstres difficiles. Vous avez {points} points.");
            if (points >= 50)
            {
                Console.WriteLine("Bravo. Vous avez maintenant suffisamment d'expérience pour tenter de combattre l'impitoyable Boss de Fin. Voulez-vous tenter votre chance: O/N ");
                ConsoleKeyInfo continuer = Console.ReadKey(true);
                return continuer.Key == ConsoleKey.O;
            }
            else
            {
                return false;
            }
        }

        private static void NiveauFinal(string name)

        {

            Joueur player = new Joueur(200, name);
            
            BossDeFin boss = new BossDeFin(250);

            while (player.EstVivant && boss.EstVivant)

            {
                Console.WriteLine($"Il reste {player.PointsDeVie} points de vie à {player.nom} et il reste {boss.PointsDeVie} points de vie au boss de fin.");
                player.Attaque(boss);

                if (boss.EstVivant)

                    boss.Attaque(player);



            }

            if (player.EstVivant)

                Console.WriteLine("Bravo, vous avez sauvé la princesse (ou le prince !)");

            else

                Console.WriteLine("Game over...");

        }
    }
}
