using System;
using System.Collections.Generic;
using System.Linq;

namespace pizza
{
    //La pizza perso permet de la composer soi meme, elle coute 5euros de base et chaque ingrédient ajoute 1.5 euros au prix
    class PizzaPersonnalisee : Pizza
    {
        static int nbPizzaPerso = 0;
        protected int numeroPizza;
        public PizzaPersonnalisee() : base("Personnalisée", 5, false, null)
        {
            nbPizzaPerso++;
            nom = "Personnalisée " + nbPizzaPerso;
            prix = 5f;

            ingredients = new List<string>();
            while (true)
            {
                Console.Write($"Rentrez un ingrédient pour la pizza perso {nbPizzaPerso} (ENTER pour terminer) : ");
                var ingredient = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(ingredient))
                {
                    break;
                }
                if (ingredients.Contains(ingredient))
                {
                    Console.WriteLine("Cet ingrédient est déjà dans votre pizza");
                }
                else
                {
                    ingredients.Add(ingredient);
                    prix += 1.5f;

                    Console.WriteLine(string.Join(", ", ingredients));
                    Console.WriteLine();
                }
                
            }
            
            this.numeroPizza = nbPizzaPerso;
        }
    }
    class Pizza
    {


        protected string nom;
        protected float prix { get; set; }
        public bool vegetarienne { get; private set; }
        public List<string> ingredients { get; set; }

        public Pizza(string nom, float prix, bool vegetarienne, List<string> ingredients)
        {
            this.nom = nom;
            this.prix = prix;
            this.vegetarienne = vegetarienne;
            this.ingredients = ingredients;

            
        }

        public void Afficher()
        {
            /*string badgeVegetarienne = "(V)";
            if (!vegetarienne)
            {
                badgeVegetarienne = "";
            }*/
            string badgeVegetarienne = vegetarienne ? "(V)" : "";

            string nomAfficher = FormatPremiereLettreMajuscule(nom);
            Console.WriteLine($"{nomAfficher} {badgeVegetarienne} - prix : {prix} euros");

            /*var ingredientsAfficher = new List<string>();
            foreach (var ingredient in ingredients)
            {
                ingredientsAfficher.Add(FormatPremiereLettreMajuscule(ingredient));
            }*/
            var ingredientsAfficher = ingredients.Select(i => FormatPremiereLettreMajuscule(i)).ToList();

            Console.WriteLine(string.Join(", ", ingredientsAfficher));
            Console.WriteLine();

        }

        private static string FormatPremiereLettreMajuscule(string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;
            string nomMinuscules = s.ToLower();
            string nomMajuscules = s.ToUpper();

            string resultat = nomMajuscules[0] + nomMinuscules[1..];

            return resultat;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            var listePizzas = new List<Pizza>
            {
                new Pizza("4 frommages", 11.5f, true, new List<string>{ "emmental", "bleu"}),
                new Pizza("orientale", 9f, false, new List<string>{ "chorizo", "merguez"}),
                new Pizza("reine", 12f, false, new List<string>{ "jambon", "champignon", "emmental"}),
                new Pizza("végétarienne", 7.6f, true, new List<string>{ "emmental", "tomates", "aubergines"}),
                new PizzaPersonnalisee(),
                new PizzaPersonnalisee(),

            };

            //listePizzas = listePizzas.OrderBy(p => p.prix).ToList();


            //Afficher la pizza la moins chère et la plus chère :

            /*float PrixMin = listePizzas[0].prix;
            float PrixMax = listePizzas[0].prix;
            string pizzPrixMin = null;
            string pizzPrixMax = null;

            foreach(var pizzaP in listePizzas)
            {
                if (PrixMin > pizzaP.prix)
                {
                    pizzPrixMin = pizzaP.nom;
                    PrixMin = pizzaP.prix;
                }
                if (PrixMax < pizzaP.prix)
                {
                    pizzPrixMax = pizzaP.nom;
                    PrixMax = pizzaP.prix;
                }
            }
            Console.WriteLine($"La pizza la moins chère est : {pizzPrixMin}");
            Console.WriteLine($"La pizza la plus chère est : {pizzPrixMax}");
            */

            //listePizzas = listePizzas.Where(pizza => pizza.vegetarienne).ToList();
            //listePizzas = listePizzas.Where(p => p.ingredients.Where(i => i.ToLower().Contains("tomate")).ToList().Count > 0).ToList();

            foreach (var pizza in listePizzas)
            {
                pizza.Afficher();
            }

            

        }
    }
}
