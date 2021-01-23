//-----------------------------------------------------------------------
// Auteur : Michée Kamte Kouetche
// Fichier : Interprete.cs
// Projet : Travail pratique 2
// Date de création : 23 Novembre 2020
// Description : Classe qui permet de manipuler les artistes interprètes
//----------------------------------------------------------------------
using System;

namespace TravailPratique2
{
    class Interprète : Artiste, IMusical
    {
        // Attribut de la classe
        /// <summary>
        /// Le nombre d'interprètes créés
        /// </summary>
        public static int NbreArtistesInterprètes;

        // Attribut
        NuméroArtiste interprétation;

        // Propriétés
        /// <summary>
        /// La pièce musicale de l'interprète
        /// </summary>
        public NuméroArtiste PieceMusicale 
        {
            get { return interprétation; }
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
        public Interprète(string nom, int numéro, char catégorie, string prestation, string titre, string durée)
            : base(nom, numéro, catégorie, prestation)
        {
            interprétation = new NuméroArtiste(titre, durée);
            NbreArtistesInterprètes++;
        }

        /// <summary>
        /// Constructeur static
        /// </summary>
        static Interprète()
        {
            NbreArtistesInterprètes = 0;
        }

        public override string ExécuterNuméro()
        {
            return $"- Dossard {NuméroDossard} : Pour l'interprète {NomArtiste}, « Ouf, sa voix mélodieuse est sublime dans \"{PieceMusicale.Titre}\" !!!»";
        }

        public override string ÉcrireRapport()
        {
            return $"- Dossard {NuméroDossard} : {NomArtiste} a chanté : \" {PieceMusicale.Titre} \" et sa prestation a duré {PieceMusicale.Durée}" + Environment.NewLine;
        }
    }
}
