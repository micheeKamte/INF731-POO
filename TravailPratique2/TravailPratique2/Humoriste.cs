//-----------------------------------------------------------------------
// Auteur : Michée Kamte Kouetche
// Fichier : Humoriste.cs
// Projet : Travail pratique 2
// Date de création : 23 Novembre 2020
// Description : Classe qui permet de manipuler les artistes humoristes
//----------------------------------------------------------------------
using System;

namespace TravailPratique2
{
    class Humoriste : Artiste, INonMusical
    {
        // Attribut de la classe
        /// <summary>
        /// Le nombre d'humoristes créés
        /// </summary>
        public static int NbreArtistesHumoristes;

        // Attribut
        NuméroArtiste numéroHumour;

        // Propriétés
        /// <summary>
        /// Le numéro joué par l'humoriste
        /// </summary>
        public NuméroArtiste NuméroJoué
        {
            get { return numéroHumour; }
        }
        public override NuméroArtiste NuméroArtiste
        {
            get { return NuméroJoué; }
        }

        // Constructeurs
        /// <summary>
        /// Constructeur paramétrique
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="numéro"></param>
        /// <param name="catégorie"></param>
        /// <param name="prestation"></param>
        /// <param name="titre"></param>
        /// <param name="durée"></param>
        public Humoriste(string nom, int numéro, char catégorie, string prestation, string titre, string durée)
            : base(nom, numéro, catégorie, prestation)
        {
            numéroHumour = new NuméroArtiste(titre, durée);
            NbreArtistesHumoristes++;
        }

        /// <summary>
        /// Constructeur static
        /// </summary>
        static Humoriste()
        {
            NbreArtistesHumoristes = 0;
        }

        public override string ExécuterNuméro()
        {
            return $"- Dossard {NuméroDossard} : Pour l'humoriste {NomArtiste}, « ... Son Numéro \"{NuméroJoué.Titre}\" nous a fait tellement rire ... »";
        }

        public override string ÉcrireRapport()
        {
            return $"- Dossard {NuméroDossard} : {NomArtiste} a fait le numéro d'humour : \" {NuméroJoué.Titre} \" et sa prestation a duré {NuméroJoué.Durée}" + Environment.NewLine;
        }
    }
}
