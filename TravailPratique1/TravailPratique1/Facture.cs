//-----------------------------------------------------------------------------------
// Auteur : Michée Kamte Kouetche
// Fichier : Facture.cs
// Projet : Travail pratique 1
// Date de création : 28 Septembre 2020
// Description : Classe qui nous permet de produire la facture à partir d'un fichier
//-----------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;

namespace TravailPratique1
{
    public static class Facture
    {
        //Constantes
        public const double TAUX_TPS = 0.05;
        public const double TAUX_TVQ = 0.09975;
        public const string CHEMIN_DES_FICHIERS = "../../";
        const string NOM_FICHIER = "Facture - ";

        // Attributs
        /// <summary>
        /// La liste contenant tous les articles de la facture
        /// </summary>
        static List<Article> listeArticles;

        /// <summary>
        /// Total de la TPS de la facture
        /// </summary>
        static double totalTPS;

        /// <summary>
        /// Total de la TVQ de la facture
        /// </summary>
        static double totalTVQ;

        /// <summary>
        /// Totat de la facture sans taxes incluses
        /// </summary>
        static double sousTotal;

        /// <summary>
        /// Total de la facture avec taxes incluses
        /// </summary>
        static double totalFacture;

        /// <summary>
        /// Nom du fichier contenant les articles à lire
        /// </summary>
        public static string nomFichier;

        // Constructeur static
        /// <summary>
        /// Constructeur static permettant de définir les valeurs par défaut
        /// au debut du programme
        /// </summary>
        static Facture()
        {
            listeArticles = new List<Article>();
            totalTPS = 0;
            totalTVQ = 0;
            sousTotal = 0;
            totalFacture = 0;
            nomFichier = null;
        }

        /// <summary>
        /// Lecture des articles qui se trouvent dans un fichier
        /// et les ajoute dans la liste de la facture
        /// </summary>
        public static void lectureDesArticles()
        {
            viderLaListe();
            string ligne;
            try
            {
                StreamReader lecture = new StreamReader(CHEMIN_DES_FICHIERS + nomFichier);

                while (!lecture.EndOfStream)
                {
                    ligne = lecture.ReadLine();
                    string[] tableau = ligne.Split(';');
                    Article nouveauArticle = Article.creerUnArticle(tableau);
                    if(nouveauArticle != null)
                    {
                        listeArticles.Add(nouveauArticle);
                    }
                }

                // Fermeture du fichier
                lecture.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        /// <summary>
        /// Ecriture de la facture dans un fichier
        /// </summary>
        public static void productionDeLaFacture()
        {
            if (nomFichier == null)
            {
                Console.WriteLine("Aucun nom de fichier n'a été lu !!!");
            }
            else if (listeArticles.Count == 0)
            {
                try
                {
                    StreamWriter ecriture = new StreamWriter(CHEMIN_DES_FICHIERS + NOM_FICHIER + $"{nomFichier}");
                    ecriture.WriteLine(("").PadRight(87, '-') + Environment.NewLine);
                    ecriture.WriteLine($"{"",4} Facture produite pour le fichier  { nomFichier }" + Environment.NewLine);
                    ecriture.WriteLine($"{"",4} Par Michée Kamte Kouetche  " + Environment.NewLine);
                    ecriture.WriteLine(("").PadRight(87, '-') + Environment.NewLine);

                    ecriture.WriteLine($"{"",4} Il n'y a aucun article dans la liste !!!");
                    ecriture.Close();

                    Console.WriteLine($"... La facture a été produite dans le fichier " + NOM_FICHIER + $"{nomFichier}");
                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception : " + e.Message);
                }
            }
            else
            {
                calculTotaux();
                try
                {
                    StreamWriter ecriture = new StreamWriter(CHEMIN_DES_FICHIERS + NOM_FICHIER + $"{nomFichier}");
                    ecriture.WriteLine(("").PadRight(87, '-') + Environment.NewLine);
                    ecriture.WriteLine($"{"",4} Facture produite pour le fichier  { nomFichier }" + Environment.NewLine);
                    ecriture.WriteLine($"{"",4} Par Michée Kamte Kouetche  " + Environment.NewLine);
                    ecriture.WriteLine(("").PadRight(87, '-') + Environment.NewLine);

                    foreach (Article article in listeArticles)
                    {
                        ecriture.WriteLine(article.ToString());
                    }

                    ecriture.WriteLine();
                    ecriture.WriteLine(("").PadRight(87, '-') + Environment.NewLine);
                    ecriture.WriteLine($"{"Sous-total  :".PadLeft(74)} {sousTotal.ToString("c").PadLeft(11)}");
                    ecriture.WriteLine($"{"TPS  :".PadLeft(74)} {totalTPS.ToString("c").PadLeft(11)}");
                    ecriture.WriteLine($"{"TVQ  :".PadLeft(74)} {totalTVQ.ToString("c").PadLeft(11)}");
                    ecriture.WriteLine($"{"Total  :".PadLeft(74)} {totalFacture.ToString("c").PadLeft(11)}");

                    ecriture.Close();

                    Console.WriteLine("... La facture a été produite dans le fichier " + NOM_FICHIER + $"{nomFichier}");
                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception : " + e.Message);
                }
            }
        }

        /// <summary>
        /// Calcul tous les totaux de la facture y compris les taxes (TPS et TVQ)
        /// </summary>
        static void calculTotaux()
        {
            annulerTotaux();
            foreach (Article article in listeArticles)
            {
                sousTotal = sousTotal + article.PrixUnitaire * article.QuantiteArticle;

                if (String.Compare(article.Categorie, Article.ARTICLE_TAXABLE) == 0)
                {
                    totalTPS = totalTPS + ( article.PrixUnitaire * article.QuantiteArticle * TAUX_TPS );
                    totalTVQ = totalTVQ + (article.PrixUnitaire * article.QuantiteArticle * TAUX_TVQ);
                }

                totalFacture = sousTotal + totalTPS + totalTVQ;
            }
        }

        /// <summary>
        /// Vider tous les éléments d'une liste
        /// </summary>
        static void viderLaListe()
        {
            listeArticles.Clear();
        }

        /// <summary>
        /// Remettre les totaux à 0
        /// </summary>
        static void annulerTotaux()
        {
            totalTPS = 0;
            totalTVQ = 0;
            sousTotal = 0;
            totalFacture = 0;
        }
    }
}
