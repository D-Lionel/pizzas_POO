using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Net;

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


        public string nom { get; protected set; }
        public float prix { get; protected set; }
        public bool vegetarienne { get; private set; }
        public List<string> ingredients { get; protected set; }

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
        static List<Pizza> GetPizzasFromCode()
        {
            var listePizzas = new List<Pizza>
            {
                new Pizza("4 frommages", 11.5f, true, new List<string>{ "emmental", "bleu"}),
                new Pizza("orientale", 9f, false, new List<string>{ "chorizo", "merguez"}),
                new Pizza("reine", 12f, false, new List<string>{ "jambon", "champignon", "emmental"}),
                new Pizza("végétarienne", 7.6f, true, new List<string>{ "emmental", "tomates", "aubergines"}),
                //new PizzaPersonnalisee(),
                //new PizzaPersonnalisee(),

            };
            return listePizzas;
        }

        static List<Pizza> GetPizzasFromFile(string filename)
        {
            string json = null;
            try
            {
                json = File.ReadAllText("listePizzas.json");
            }
            catch
            {
                Console.WriteLine("Erreur de lecture du ficher" + filename);
                return null;
            }

            List<Pizza> listePizzas = null;
            try
            {
                listePizzas = JsonConvert.DeserializeObject<List<Pizza>>(json);

            }
            catch
            {
                Console.WriteLine("Erreur : les données json ne sont pas valides");
                return null;
            }

            return listePizzas;
        }

        static void GenerateJsonFile(List<Pizza> listePizzas, string filename)
        {
            var json = JsonConvert.SerializeObject(listePizzas);
            File.WriteAllText(filename, json);
        }


        static List<Pizza> GetPizzasFromUrl(string url)
        {
            var webclient = new WebClient();
            string json = webclient.DownloadString(url);

            List<Pizza> listePizzas = null;
            try
            {
                listePizzas = JsonConvert.DeserializeObject<List<Pizza>>(json);

            }
            catch
            {
                Console.WriteLine("Erreur : les données json ne sont pas valides");
                return null;
            }

            return listePizzas;
        }

        static void Main(string[] args)
        {
            var filename = "listePizzas.json";

            //GetPizzasFromCode();
            // var listePizzas = GetPizzasFromFile("listePizzas.json");

            var listePizzas = GetPizzasFromUrl("http://codeavecjonathan.com/res/pizzas2.json");


            foreach (var pizza in listePizzas)
            {
                pizza.Afficher();
            }
        }
    }
}
