//-----------------------------------------------------------------------
// Auteur : Michée Kamte Kouetche
// Fichier : Musicien.cs
// Projet : Travail pratique 2
// Date de création : 23 Novembre 2020
// Description : Classe qui permet de manipuler les artistes musiciens
//----------------------------------------------------------------------
using System;

namespace TravailPratique2
{
    class Musicien : Artiste, IMusical
    {
        // Attribut de la classe
        /// <summary>
        /// Le nombre de musiciens créés
        /// </summary>
        public static int NbreArtistesMusiciens;

        // Attribut
        NuméroArtiste chanson;

        // Propriétés
        /// <summary>
        /// La pièce musicale du musicien
        /// </summary>
        public NuméroArtiste PieceMusicale 
        {
            get { return chanson; }
        }
        public override NuméroArtiste NuméroArtiste
        {
            get { return PieceMusicale; }
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
        public Musicien(string nom, int numéro, char catégorie, string prestation, string titre, string durée)
            : base(nom, numéro, catégorie, prestation)
        {
            chanson = new NuméroArtiste(titre, durée);
            NbreArtistesMusiciens++;
        }

        /// <summary>
        /// Constructeur static
        /// </summary>
        static Musicien()
        {
            NbreArtistesMusiciens = 0;
        }

        public override string ExécuterNuméro()
        {
            return $"- Dossard {NuméroDossard} : Pour le musicien {NomArtiste}, « Quelle virtuose de son instrument lorsqu'il joue \"{PieceMusicale.Titre}\" !!!»";
        }
        public override string ÉcrireRapport()
        {
            return $"- Dossard {NuméroDossard} : {NomArtiste} a joué la pièce : \" {PieceMusicale.Titre} \" et sa prestation a duré {PieceMusicale.Durée}" + Environment.NewLine;
        }
    }
}
