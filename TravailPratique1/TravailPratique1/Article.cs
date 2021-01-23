//--------------------------------------------------------------------
// Auteur : Michée Kamte Kouetche
// Fichier : Article.cs
// Projet : Travail pratique 1
// Date de création : 28 Septembre 2020
// Description : Classe Article qui permet de manipuler les articles
//--------------------------------------------------------------------
using System;
using System.Linq;

namespace TravailPratique1
{
    // Exceptions pour le controle de la création des propriétés
    class NumeroInvalidException : Exception { }
    class CategorieInvalideException : Exception { }
    class ValeurInvalideException : Exception { }
    class DescriptionInvalideException : Exception { }

    class Article
    {
        // Constantes
        public const string ARTICLE_NON_TAXABLE = "NT";
        public const string ARTICLE_TAXABLE = "FP";

        // Attributs
        string numero;
        string categorie;
        int quantiteArticle;
        string description;
        double prixUnitaire;

        // Propriétés
        /// <summary>
        /// Numéro de l'article qui est une chaîne de caractères sans espace
        /// </summary>
        public string Numero
        {
            get { return numero; }
            private set 
            {
                if (String.IsNullOrWhiteSpace(value) || value.Contains(' '))
                {
                    Console.WriteLine("Numero n'a été pas lu");
                    throw new NumeroInvalidException();
                }
                numero = value;
            }
        }

        /// <summary>
        /// Catégorie qui est une chaine de caractère et indique si l'article est taxable (FP) ou
        /// non taxable (NT)
        /// </summary>
        public string Categorie
        {
            get { return categorie; }
            private set
            {
                if(String.Compare(value, ARTICLE_NON_TAXABLE, true) == 0 || String.Compare(value, ARTICLE_TAXABLE, true) == 0)
                {
                    categorie = value.ToUpper();
                }
                else 
                {
                    Console.WriteLine("Categorie n'a été pas lue");
                    throw new CategorieInvalideException();
                }
                
            }
        }

        /// <summary>
        /// La quantité d'article qui est un entier et indique le nombre d'articles achetés.
        /// Elle peut être positive ou égale à 0
        /// </summary>
        public int QuantiteArticle
        {
            get { return quantiteArticle; }
            private set 
            {
                if (value < 0)
                {
                    Console.WriteLine("Quantité n'a été pas lue");
                    throw new ValeurInvalideException();
                }
                quantiteArticle = value;
            }
        }

        /// <summary>
        /// La description de l'article qui est une chaîne de caractères pouvant
        /// comporter des espaces
        /// </summary>
        public string Description
        {
            get { return description; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine("Description n'a été pas lue");
                    throw new DescriptionInvalideException();
                }
                description = value.Trim();
            }
        }

        /// <summary>
        /// Le prix de l'article qui est un double
        /// </summary>
        public double PrixUnitaire
        {
            get { return prixUnitaire; }
            private set
            {
                if (value < 0)
                {
                    Console.WriteLine("Prix n'a été pas lu");
                    throw new ValeurInvalideException();
                }
                prixUnitaire = value;
            }
        }

        // Constructeurs
        /// <summary>
        /// Constructeur par defaut
        /// </summary>
        public Article() { }

        /// <summary>
        /// Constructeur paramétrique
        /// </summary>
        /// <param name="numero">Chaîne qui represente le numero de l'article</param>
        /// <param name="categorie">Chaîne qui indique si l'article est taxable ou pas</param>
        /// <param name="quantiteArticle">Quantité d'articles à facturer</param>
        /// <param name="description">Description de l'article</param>
        /// <param name="prixUnitaire">Le prix unitaire de l'article</param>
        public Article(string numero, string categorie, int quantiteArticle, string description, double prixUnitaire)
        {
            this.numero = numero;
            this.categorie = categorie;
            this.quantiteArticle = quantiteArticle;
            this.description = description;
            this.prixUnitaire = prixUnitaire;
        }

        /// <summary>
        /// Constructeur de copie
        /// </summary>
        /// <param name="articleExistant"></param>
        public Article(Article articleExistant)
        {
            numero = articleExistant.Numero;
            categorie = articleExistant.Categorie;
            quantiteArticle = articleExistant.QuantiteArticle;
            description = articleExistant.Description;
            prixUnitaire = articleExistant.PrixUnitaire;
        }

        /// <summary>
        /// Surchage de la méthode ToString() pour afficher un article
        /// </summary>
        /// <returns>Une chaine de caractère</returns>
        public override string ToString()
        {
            if (String.Compare(this.categorie, ARTICLE_NON_TAXABLE) == 0)
            {
                return ($"  {this.numero.PadRight(10)}{this.quantiteArticle.ToString().PadLeft(8)}" +
                    $"  {this.description.PadRight(40, '.')}{this.prixUnitaire.ToString().PadLeft(8)}  {" ".PadRight(3)}" +
                    $"{(this.quantiteArticle * this.prixUnitaire).ToString("c").PadLeft(11)}");
            }
            else
            {
                return ($"  {this.numero.PadRight(10)}{this.quantiteArticle.ToString().PadLeft(8)}" +
                    $"  {this.description.PadRight(40, '.')}{this.prixUnitaire.ToString().PadLeft(8)}  {this.categorie.PadRight(3)}" +
                    $"{(this.quantiteArticle * this.prixUnitaire).ToString("c").PadLeft(11)}");
            }
        }

        /// <summary>
        /// Création d'un article à partir d'un tableau de valeurs
        /// </summary>
        /// <param name="valeurs"></param>
        /// <returns>Un article</returns>
        public static Article creerUnArticle(string[] valeurs)
        {
            if (valeurs.Length == 5)
            {
                try 
                {
                    Article article = new Article();
                    article.Numero = valeurs[0];
                    article.Categorie = valeurs[1];
                    article.QuantiteArticle = int.Parse(valeurs[2]);
                    article.Description = valeurs[3];
                    article.PrixUnitaire = double.Parse(valeurs[4]);
                    return new Article(article);
                }
                catch
                {
                    Console.WriteLine($"L'article n'a pas pu être ajouté ");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("Le tableau doit contenir exactement 5 éléments");
                return null;
            }
        }

    }
}
