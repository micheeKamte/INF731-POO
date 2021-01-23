//------------------------------------------------------------------------------------------------
// Auteur : Michée Kamte Kouetche
// Fichier : NumeroArtiste.cs
// Projet : Travail pratique 2
// Date de création : 23 Novembre 2020
// Description : Classe NumeroArtiste qui permet de manipuler les numéros des artistes
//------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace TravailPratique2
{
    // Classes exceptions pour lever les exceptions lors de la création des propriétés
    class TitreInvalidException : ApplicationException { }
    class MinuteInvalidException : ApplicationException { }
    class SecondeInvalidException : ApplicationException { }
    class FormatInvalidException : ApplicationException { }
    class NuméroArtiste
    {
        // Constantes
        const char CARACTERE_DE_SEPARATION = ':';
        const int NOMBRE_SEPARATEUR_REQUIS = 1;
        const int SECONDE_MAX = 60;
        const int SECONDE_MIN = 0;
        const int MINUTE_MAX = 30;
        const int MINUTE_MIN = 0;
        const int CONVERSION_EN_SECONDES = 60;

        // Les attributs
        string titre;
        string durée;
        int minutes;
        int secondes;
        
        // Les propriétés
        /// <summary>
        /// Le titre du némero de l'artiste
        /// </summary>
        public string Titre
        {
            get { return titre; }
            private set 
            {
                if (String.IsNullOrWhiteSpace(value)) 
                {
                    throw new TitreInvalidException();
                }
                titre = value;
            }
        }

        /// <summary>
        /// La durée du numéro de l'artiste
        /// </summary>
        public string Durée
        {
            get { return durée; }
            private set 
            {
                if(!EstFormatValid(value))
                {
                    throw new FormatInvalidException();
                }
                durée = value;
            }
        }
        
        /// <summary>
        /// Le nombre de minutes du numéro de l'artiste
        /// </summary>
        public int Minutes
        {
            get { return minutes; }
            private set 
            {
                if (value > MINUTE_MAX || value < MINUTE_MIN)
                {
                    throw new MinuteInvalidException();
                }
                minutes = value;
            }
        }

        /// <summary>
        /// Le nombre de secondes du numéro de l'artiste
        /// </summary>
        public int Secondes
        {
            get { return secondes; }
            private set
            {
                if (value >= SECONDE_MAX || value < SECONDE_MIN)
                {
                    throw new SecondeInvalidException();
                }
                secondes = value;
            }
        }
        
        /// <summary>
        /// Le nombre total en secondes du numéro de l'artiste
        /// </summary>
        public int TempsEnSecondes
        {
            get { return Secondes + (Minutes * CONVERSION_EN_SECONDES); }
        }

        /// <summary>
        /// Constructeur paramétrique
        /// </summary>
        /// <param name="titre_">Chaîne qui représente le titre du numéro de l'artiste</param>
        /// <param name="durée_">Chaîne qui représente la durée du numéro de l'artiste</param>
        public NuméroArtiste(string titre_, string durée_)
        {
            Titre = titre_;
            Durée = durée_;
        }

        /// <summary>
        /// Vérification si le caractère est le séparateur
        /// </summary>
        /// <param name="caractère"></param>
        /// <returns></returns>
        public static bool EstCaractèreSéparateur(char caractère)
        {
            return caractère == CARACTERE_DE_SEPARATION;
        }

        /// <summary>
        /// Compte le nombre de caractères séparateurs présents dans la chaîne
        /// </summary>
        /// <param name="chaineCaractères"></param>
        /// <returns></returns>
        public int NombreCaractèresSéparateur(string chaineCaractères)
        {
            List<char> listeCaractères = new List<char>();
            listeCaractères.AddRange(chaineCaractères);
            var listeSéparateur = listeCaractères.FindAll(EstCaractèreSéparateur);
            return listeSéparateur.Count;
        }

        /// <summary>
        /// Vérification de la validité du format de la chaîne représentant
        /// la durée du numéro de l'artiste
        /// </summary>
        /// <param name="chaine"></param>
        /// <returns></returns>
        public bool EstFormatValid(string chaine)
        {
            if (NombreCaractèresSéparateur(chaine) == NOMBRE_SEPARATEUR_REQUIS)
            {
                string[] valeurs = chaine.Split(':');
                try 
                {
                    Minutes = int.Parse(valeurs[0]);
                    Secondes = int.Parse(valeurs[1]);
                    if(Minutes == MINUTE_MAX && Secondes > SECONDE_MIN)
                    {
                        return false;
                    }
                    if(Minutes == MINUTE_MIN && Secondes == SECONDE_MIN)
                    {
                        return false;
                    }
                    return true;
                }
                catch
                {
                    return false;
                }                
            }
            else
                return false;
        }
    }
}
