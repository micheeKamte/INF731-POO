//-----------------------------------------------------------------------
// Auteur : Michée Kamte Kouetche
// Fichier : Acrobate.cs
// Projet : Travail pratique 2
// Date de création : 23 Novembre 2020
// Description : Classe qui permet de manipuler les artistes acrobates
//----------------------------------------------------------------------
using System;

namespace TravailPratique2
{
    class Acrobate : Artiste, INonMusical
    {
        // Attribut de la classe
        /// <summary>
        /// Le nombre d'acrobates créés
        /// </summary>
        public static int NbreArtistesAcrobates;

        // Attribut
        NuméroArtiste numéroAcrobatie;

        /// <summary>
        /// Le numéro joué par l'acrobate
        /// </summary>
        public NuméroArtiste NuméroJoué
        {
            get { return numéroAcrobatie; }
        }

        public override NuméroArtiste NuméroArtiste
        {
            get { return NuméroJoué; }
        }

        // Constructeurs
        /// <summary>
        /// Constructeur paramtrique
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="numéro"></param>
        /// <param name="catégorie"></param>
        /// <param name="prestation"></param>
        /// <param name="titre"></param>
        /// <param name="durée"></param>
        public Acrobate(string nom, int numéro, char catégorie, string prestation, string titre, string durée)
            :base(nom, numéro, catégorie, prestation)
        {
            numéroAcrobatie = new NuméroArtiste(titre, durée);
            NbreArtistesAcrobates++;
        }

        /// <summary>
        /// Constructeur static
        /// </summary>
        static Acrobate()
        {
            NbreArtistesAcrobates = 0;
        }
        public override string ExécuterNuméro()
        {
            return $"- Dossard {NuméroDossard} : Pour l'acrobate {NomArtiste}, « --- Leur Numéro \"{NuméroJoué.Titre}\" : digne du cirque du soleil --- »";
        }
        public override string ÉcrireRapport()
        {
            return $"- Dossard {NuméroDossard} : {NomArtiste} a exécuté : \" {NuméroJoué.Titre} \" qui a duré {NuméroJoué.Durée}" + Environment.NewLine;
        }
    }
}
