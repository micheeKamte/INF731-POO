//------------------------------------------------------------------------------------------------
// Auteur : Michée Kamte Kouetche
// Fichier : Artiste.cs
// Projet : Travail pratique 2
// Date de création : 23 Novembre 2020
// Description : Classe abstraite Artiste qui permet de donner une forme aux artistes du concours
//------------------------------------------------------------------------------------------------
using System;

namespace TravailPratique2
{
    // Classes exceptions pour lever les exceptions lors de la création des propriétés
    class NomArtisteInvalidException : ApplicationException { }
    class NuméroDossardInvalidException : ApplicationException { }
    class TypePrestationInvalidException : ApplicationException { }
    class TypeArtisteInvalidException : ApplicationException { }
    
    /// <summary>
    /// Classe Enum qui représente les différentes prestations du concours
    /// </summary>
    public enum TypePrestation
    {
        acrobate,
        humoriste,
        interprète,
        aci,
        musicien
    }

    abstract class Artiste
    {
        // Constantes
        const char PROFESSIONNEL = 'P';
        const char AMATEUR = 'A';

        // Attributs
        string nomArtiste;
        int numéroDossard;
        char catégorieArtiste;
        TypePrestation prestationArtiste;

        // Propriétés
        /// <summary>
        /// Nom de l'artiste qui est une chaîne de caractères
        /// </summary>
        public string NomArtiste
        {
            get { return nomArtiste; }
            private set
            {
                if(String.IsNullOrWhiteSpace(value))
                {
                    throw new NomArtisteInvalidException();
                }
                else if (value.Contains(","))
                {
                    string[] tableau = value.Split(',');
                    nomArtiste = $"{tableau[1].Trim()} {tableau[0].Trim()}";
                }
                else
                {
                    nomArtiste = value;
                }
            }
        }

        /// <summary>
        /// Numéro de dossard de l'artiste qui est un entier
        /// supérieur ou égal à 2000
        /// </summary>
        public int NuméroDossard
        {
            get { return numéroDossard; }
            private set
            {
                if(value < 2000)
                {
                    throw new NuméroDossardInvalidException();
                }
                numéroDossard = value;
            }
        }        
        
        /// <summary>
        /// Catégorie de l'artiste qui est un caractère. P pour professionnel
        /// et A pour amateur
        /// </summary>
        public char CatégorieArtiste
        {
            get { return catégorieArtiste; }
            private set
            {
                if (!EstProfessionnelOuAmateur(value))
                {
                    throw new TypeArtisteInvalidException();
                }
                catégorieArtiste = Char.ToUpper(value);
            }
        }
        
        /// <summary>
        /// Le type de prestation de l'artiste qui est un élément
        /// de la classe Enum Prestation
        /// </summary>
        public TypePrestation PrestationArtiste
        {
            get { return prestationArtiste; }
            private set
            {
                if(!Enum.IsDefined(typeof(TypePrestation), value))
                {
                    throw new TypePrestationInvalidException();
                }
                prestationArtiste = value;
            }
        }

        /// <summary>
        /// Propriété abstraite qui définit le numéro que l'artiste
        /// présente durant le concours
        /// </summary>
        public abstract NuméroArtiste NuméroArtiste { get; }
        
        /// <summary>
        /// Constructeur paramétrique
        /// </summary>
        /// <param name="nom">Chaîne qui réprésente le nom de l'artiste</param>
        /// <param name="numéro">Le numéro de dossard de l'artiste</param>
        /// <param name="catégorie">La catégorie de l'artiste</param>
        /// <param name="prestation">Le type de prestation de l'artiste</param>
        protected Artiste(string nom, int numéro, char catégorie, string prestation)
        {
            NomArtiste = nom;
            NuméroDossard = numéro;
            CatégorieArtiste = catégorie;
            PrestationArtiste = (TypePrestation)Enum.Parse(typeof(TypePrestation), prestation.ToLower());
        }

        /// <summary>
        /// Méthode abstraite qui représente la chaîne de caractères
        /// qui sera écrite à l'écran
        /// </summary>
        /// <returns>Chaîne de caractères donnant les informations concernant le numéro de l'artiste</returns>
        public abstract string ExécuterNuméro();
        
        /// <summary>
        /// Méthode abstraite qui représente la chaîne de caractères
        /// qui sera écrite dans le rapport
        /// </summary>
        /// <returns>Chaîne de caractères donnant les informations concernant le nom de l'artiste, le titre
        /// de sa prestation ainsi que sa durée</returns>
        public abstract string ÉcrireRapport();

        /// <summary>
        /// Vérification de la validité de la catégorie de l'artiste
        /// </summary>
        /// <param name="cat"></param>
        /// <returns>Boolean pour dire si la catégorie de l'artiste est valide ou pas</returns>
        public bool EstProfessionnelOuAmateur(char cat)
        {
            return (Char.ToUpper(cat) == PROFESSIONNEL || Char.ToUpper(cat) == AMATEUR);
        }

        /// <summary>
        /// Comparer deux artistes selon leurs numéros de dossard
        /// </summary>
        /// <param name="premier"></param>
        /// <param name="deuxieme"></param>
        /// <returns></returns>
        public static int ComparerNuméroArtiste(Artiste premier, Artiste deuxieme)
        {
            return premier.NuméroDossard.CompareTo(deuxieme.NuméroDossard);
        }

        public static int ComparerCatégorie(Artiste premier, Artiste deuxieme)
        {
            int resultat = premier.CatégorieArtiste.CompareTo(deuxieme.CatégorieArtiste);
            if (resultat == 0)
            {
                resultat = premier.NuméroDossard.CompareTo(deuxieme.NuméroDossard);
            }
            return resultat;
        }
    }
}
