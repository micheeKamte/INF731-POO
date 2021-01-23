//----------------------------------------------------------------------------
// Auteur : Michée Kamte Kouetche
// Fichier : Program.cs
// Projet : Travail pratique 1
// Date de création : 28 Septembre 2020
// Description : Production de la facture d'un client d'une chaine de magasin
//----------------------------------------------------------------------------
using System;
using System.IO;

namespace TravailPratique1
{
    class Program
    {
        static void Main(string[] args)
        {
            bool continueLectureDesFichiers = true;
            while (continueLectureDesFichiers)
            {
                Console.Write("Donnez le nom du fichier contenant les articles à facturer : ");
                string valeurLue = Console.ReadLine();

                if (!String.IsNullOrWhiteSpace(valeurLue) && File.Exists(Facture.CHEMIN_DES_FICHIERS + valeurLue))
                {
                    Facture.nomFichier = valeurLue;
                    Facture.lectureDesArticles();
                    Facture.productionDeLaFacture();
                }
                else
                {
                    continueLectureDesFichiers = false;
                    Console.WriteLine("Le fichier est introuvable !!!");
                }
            }

            Console.WriteLine();
            Console.WriteLine("--Fin du programme--");
        }
    }
}
