//---------------------------------------------------------------------------------
// Auteur : Michée Kamte Kouetche
// Fichier : Program.cs
// Projet : Travail pratique 2
// Date de création : 23 Novembre 2020
// Description : Production de courts rapports du concours "Le Québec a du talent"
//---------------------------------------------------------------------------------
using System;
using System.IO;

namespace TravailPratique2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Participants concours = new Participants();
                bool continueLectureDesFichiers = true;
                while (continueLectureDesFichiers)
                {
                    Console.Write("Donnez le nom du fichier : ");
                    string valeurLue = Console.ReadLine();
                    if (!String.IsNullOrWhiteSpace(valeurLue) && File.Exists(Participants.CHEMIN_DES_FICHIERS + valeurLue))
                    {
                        Console.WriteLine();
                        Participants.NomFichier = valeurLue;
                        concours.LectureDuFichier();
                        concours.ProductionDuRapport();
                        
                        Console.WriteLine($"Le nombre total des participants est {Participants.NombrePartipants}");
                        Console.WriteLine($"Le nombre d'humoriste : {Humoriste.NbreArtistesHumoristes}");
                        Console.WriteLine($"Le nombre d'acrobate : {Acrobate.NbreArtistesAcrobates}");
                        Console.WriteLine($"Le nombre de musiciens : {Musicien.NbreArtistesMusiciens}");
                        Console.WriteLine($"Le nombre d'interprètes : {Interprète.NbreArtistesInterprètes}");
                        Console.WriteLine($"Le nombre d'auteur-compositeur interprète : {AuteurCompositeurInterprète.NbreArtistesACI}" + Environment.NewLine);
                    }
                    else if(!String.IsNullOrWhiteSpace(valeurLue) && !File.Exists(Participants.CHEMIN_DES_FICHIERS + valeurLue))
                    {
                        Console.WriteLine("Le fichier dont vous avez donné le nom est introuvable");
                    }
                    else
                    {
                        continueLectureDesFichiers = false;
                    }
                }
                Console.WriteLine();
                Console.WriteLine("--Fin du programme--");
            }
            catch { }
        }
    }
}
