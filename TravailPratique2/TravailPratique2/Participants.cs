//---------------------------------------------------------------------------
// Auteur : Michée Kamte Kouetche
// Fichier : Participants.cs
// Projet : Travail pratique 2
// Date de création : 23 Novembre 2020
// Description : Classe qui permet de manipuler les participants au concours
//---------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TravailPratique2
{
    class Participants
    {
        // Constantes
        const int NOMBRE_MINIMUM = 2000;
        const int NOMBRE_MAXIMUM = 5000;
        const string NOM_FICHIER = "Journal - ";
        public const string CHEMIN_DES_FICHIERS = "../../";

        // Attributs de la classe
        static Random GénérateurNombre;
        /// <summary>
        /// Nom du fichier contenant les informations relatives aux participants
        /// </summary>
        public static string NomFichier;

        /// <summary>
        /// Nombre de participants au concours
        /// </summary>
        public static int NombrePartipants;

        /// <summary>
        /// Liste de tous les participants au concours
        /// </summary>
        List<Artiste> ListeParticipants { get; set; }

        /// <summary>
        /// Dictionnaire qui établit un lien entre la prestation de l'artiste
        /// et le constructeur associé
        /// </summary>
        Dictionary<string, Func<string, int, char, string, string, string, Artiste>> Dictionnaire { get; }
        
        /// <summary>
        /// Constructeur de la fabrique qui remplit le dictionnaire
        /// des artistes qui participent au concours ainsi que la méthode
        /// d'instanciation pour chaque artiste
        /// </summary>
        public Participants()
        {
            ListeParticipants = new List<Artiste>();
            Dictionnaire = new Dictionary<string, Func<string, int, char, string, string, string, Artiste>>()
            {
                { "acrobate", (nom, numéro, catégorie, prestation, titre, durée) => new Acrobate(nom, numéro, catégorie, prestation, titre, durée) },
                { "humoriste", (nom, numéro, catégorie, prestation, titre, durée) => new Humoriste(nom, numéro, catégorie, prestation, titre, durée) },
                { "interprète", (nom, numéro, catégorie, prestation, titre, durée) => new Interprète(nom, numéro, catégorie, prestation, titre, durée) },
                { "aci", (nom, numéro, catégorie, prestation, titre, durée) => new AuteurCompositeurInterprète(nom, numéro, catégorie, prestation, titre, durée) },
                { "musicien", (nom, numéro, catégorie, prestation, titre, durée) => new Musicien(nom, numéro, catégorie, prestation, titre, durée) }
            };
        }

        /// <summary>
        /// Constructeur static
        /// </summary>
        static Participants()
        {
            NombrePartipants = 0;
            GénérateurNombre = new Random();
            NomFichier = null;
        }

        /// <summary>
        /// Instanciation des artistes dépendamment de la prestation de chacun
        /// </summary>
        /// <param name="valeurs"></param>
        /// <returns></returns>
        public Artiste Instancier(string[] valeurs)
        {
            if (valeurs.Length == 5)
            {
                try
                {
                    string nom = valeurs[0].Trim();                    
                    string prestation = valeurs[1].Trim().ToLower();
                    char catégorie = char.Parse(valeurs[2].Trim());
                    string titre = valeurs[3].Trim();
                    string durée = valeurs[4].Trim();                    
                    int numéro = GénérerUnNombre();
                    Artiste participant = Dictionnaire[prestation](nom, numéro, catégorie, prestation, titre, durée);
                    return participant;
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        
        /// <summary>
        /// Générateur de numéro des participants
        /// </summary>
        /// <returns></returns>
        public int GénérerUnNombre()
        {            
            int numéroGénéré = GénérateurNombre.Next(NOMBRE_MINIMUM, NOMBRE_MAXIMUM + 1);
            while (EstNuméroPris(numéroGénéré))
            {
                numéroGénéré = GénérateurNombre.Next(NOMBRE_MINIMUM, NOMBRE_MAXIMUM + 1);
            }
            return numéroGénéré;
        }
        
        /// <summary>
        /// Vérification si le numéro généré est pris
        /// </summary>
        /// <param name="numéro"></param>
        /// <returns></returns>
        public bool EstNuméroPris(int numéro)
        {
            return ListeParticipants.Exists(x => x.NuméroDossard == numéro);
        }

        /// <summary>
        /// Lecture du fichier du concours contenant des informations sur les participants
        /// </summary>
        public void LectureDuFichier()
        {
            ViderLaListe();
            MettreLesCompteurÀZéro();
            string ligne;
            try
            {
                StreamReader lecture = new StreamReader(CHEMIN_DES_FICHIERS + NomFichier);
                while (!lecture.EndOfStream)
                {
                    ligne = lecture.ReadLine();
                    string[] tableau = ligne.Split(';');
                    Artiste nouveauParticipant = Instancier(tableau);
                    if (nouveauParticipant != null)
                    {
                        ListeParticipants.Add(nouveauParticipant);
                    }
                }
                
                // Fermeture du fichier
                lecture.Close();
            }
            catch { }
        }

        /// <summary>
        /// Production du rapport lié au fichier lu
        /// </summary>
        public void ProductionDuRapport()
        {
            if (ListeParticipants.Count == 0)
            {
                try
                {
                    StreamWriter ecriture = new StreamWriter(CHEMIN_DES_FICHIERS + NOM_FICHIER + $"{NomFichier}");
                    ecriture.WriteLine(("").PadRight(87, '-') + Environment.NewLine);
                    ecriture.WriteLine($"{"",4} Rapport produit pour le fichier  { NomFichier }" + Environment.NewLine);
                    ecriture.WriteLine($"{"",4} Par Michée Kamte Kouetche  " + Environment.NewLine);
                    ecriture.WriteLine(("").PadRight(87, '-') + Environment.NewLine);
                    ecriture.WriteLine($"{"",4} Il n'y a eu aucun participant réglémentaire dans la liste !!!");
                    ecriture.Close();
                }
                catch {}
            }
            else
            {
                CalculNombreDesPartipants();
                TrierNuméro();
                try
                {
                    StreamWriter ecriture = new StreamWriter(CHEMIN_DES_FICHIERS + NOM_FICHIER + $"{NomFichier}");
                    ecriture.WriteLine(("").PadRight(87, '-') + Environment.NewLine);
                    ecriture.WriteLine($"{"",4} Rapport produit pour le fichier  { NomFichier }" + Environment.NewLine);
                    ecriture.WriteLine($"{"",4} Par Michée Kamte Kouetche  " + Environment.NewLine);
                    ecriture.WriteLine(("").PadRight(87, '-') + Environment.NewLine);
                    ecriture.WriteLine("La liste des partipants pour ce concours est la suivante : " + Environment.NewLine);

                    var ListeArtistesMusicaux = ObtenirListeMusicaux();
                    ecriture.WriteLine("Comme artistes musicaux nous avons : " + Environment.NewLine);
                    Console.WriteLine("Artistes musicaux :" + Environment.NewLine);
                    foreach (var participant in ListeArtistesMusicaux)
                    {
                        Console.WriteLine(((Artiste)participant).ExécuterNuméro() + Environment.NewLine);
                        ecriture.WriteLine(((Artiste)participant).ÉcrireRapport());
                    }

                    var ListeArtistesNonMusicaux = ObtenirListeNonMusicaux();
                    ecriture.WriteLine("Comme artistes non musicaux nous avons : " + Environment.NewLine);
                    Console.WriteLine("Artistes non musicaux :" + Environment.NewLine);
                    foreach (var participant in ListeArtistesNonMusicaux)
                    {
                        Console.WriteLine(((Artiste)participant).ExécuterNuméro() + Environment.NewLine);
                        ecriture.WriteLine(((Artiste)participant).ÉcrireRapport());
                    }
                    ecriture.WriteLine(("").PadRight(87, '-') + Environment.NewLine);
                    ecriture.WriteLine($"Le nombre total des participants à ce concours est de : { NombrePartipants }" + Environment.NewLine);
                    ecriture.WriteLine($"Le temps total pour les artistes musicaux est de : {ObtenirDuréeTotaleNumérosMusicaux()}" + Environment.NewLine);
                    ecriture.WriteLine($"Le temps total pour les artistes non musicaux est de : {ObtenirDuréeTotaleNumérosNonMusicaux()}" + Environment.NewLine);
                    ecriture.WriteLine($"Le temps total pour toutes les prestations est de : {ObtenirDuréeTotalePrestations()}" + Environment.NewLine);

                    // Fermeture du fichier
                    ecriture.Close();
                }
                catch { }
            }
        }

        /// <summary>
        /// Vider la liste des participants au concours
        /// </summary>
        public void ViderLaListe()
        {
            ListeParticipants.Clear();
        }
        
        /// <summary>
        /// Remettre tous les compteurs des nombres de participants à zéro
        /// </summary>
        public void MettreLesCompteurÀZéro()
        {
            NombrePartipants = 0;
            Interprète.NbreArtistesInterprètes = 0;
            AuteurCompositeurInterprète.NbreArtistesACI = 0;
            Musicien.NbreArtistesMusiciens = 0;
            Humoriste.NbreArtistesHumoristes = 0;
            Acrobate.NbreArtistesAcrobates = 0;
        }

        /// <summary>
        /// Calculer le nombre de participants au concours
        /// </summary>
        public void CalculNombreDesPartipants()
        {
            NombrePartipants = 0;
            NombrePartipants += Interprète.NbreArtistesInterprètes;
            NombrePartipants += AuteurCompositeurInterprète.NbreArtistesACI;
            NombrePartipants += Musicien.NbreArtistesMusiciens;
            NombrePartipants += Humoriste.NbreArtistesHumoristes;
            NombrePartipants += Acrobate.NbreArtistesAcrobates;
        }

        /// <summary>
        /// Calculer la durée totale des artistes musicaux du concours
        /// </summary>
        /// <returns></returns>
        public string ObtenirDuréeTotaleNumérosMusicaux()
        {
            var ListeArtistesMusicaux = ObtenirListeMusicaux();
            int tempsTotal = 0;
            foreach(var participant in ListeArtistesMusicaux)
            {
                tempsTotal += participant.PieceMusicale.TempsEnSecondes;
            }
            TimeSpan span = TimeSpan.FromSeconds(tempsTotal);
            return span.ToString(@"hh\:mm\:ss");
        }

        /// <summary>
        /// Calculer la durée totale des artistes non musicaux du concours
        /// </summary>
        /// <returns></returns>
        public string ObtenirDuréeTotaleNumérosNonMusicaux()
        {
            var ListeArtistesNonMusicaux = ObtenirListeNonMusicaux();
            int tempsTotal = 0;
            foreach (var participant in ListeArtistesNonMusicaux)
            {
                tempsTotal += participant.NuméroJoué.TempsEnSecondes;
            }
            TimeSpan span = TimeSpan.FromSeconds(tempsTotal);
            return span.ToString(@"hh\:mm\:ss");
        }

        /// <summary>
        /// Calculer la durée totale de tous les participants au concours
        /// </summary>
        /// <returns></returns>
        public string ObtenirDuréeTotalePrestations()
        {
            int tempsTotal = 0;
            foreach (var participant in ListeParticipants)
            {
                tempsTotal += participant.NuméroArtiste.TempsEnSecondes;
            }
            TimeSpan span = TimeSpan.FromSeconds(tempsTotal);
            return span.ToString(@"hh\:mm\:ss");
        }

        /// <summary>
        /// Obtenir la liste des artistes musicaux
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IMusical> ObtenirListeMusicaux()
        {
            return ListeParticipants.OfType<IMusical>();
        }

        /// <summary>
        /// Obtenir la liste des artistes non musicaux
        /// </summary>
        /// <returns></returns>
        public IEnumerable<INonMusical> ObtenirListeNonMusicaux()
        {
            return ListeParticipants.OfType<INonMusical>();
        }

        /// <summary>
        /// Trier les artistes de la liste selon leur numéro de dossard
        /// </summary>
        public void TrierNuméro()
        {
            ListeParticipants.Sort(Artiste.ComparerNuméroArtiste);
        }

        /// <summary>
        /// Trier les artistes de la liste selon leur catégorie
        /// </summary>
        public void TrierCatégorie()
        {
            ListeParticipants.Sort(Artiste.ComparerCatégorie);
        }
    }
}
