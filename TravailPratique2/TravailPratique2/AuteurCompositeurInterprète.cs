//--------------------------------------------------------------------------------------------
// Auteur : Michée Kamte Kouetche
// Fichier : AuteurCompositeurInterprète.cs
// Projet : Travail pratique 2
// Date de création : 23 Novembre 2020
// Description : Classe qui permet de manipuler les artistes auteurs-compositeurs interprètes
//--------------------------------------------------------------------------------------------
using System;

namespace TravailPratique2
{
    // Classe exception pour lever l'exceptions lors de la création de la propriété
    class InstrumentInvalidException : ApplicationException { }
    class AuteurCompositeurInterprète : Artiste, IMusical
    {
        // Attribut de la classe
        /// <summary>
        /// Le nombre d'auteurs-compositeurs interprètes créés
        /// </summary>
        public static int NbreArtistesACI;

        // Attributs
        NuméroArtiste interprétation;
        string instrument;

        // Propriétés
        /// <summary>
        /// Le nom de l'instrument que joue l'auteur-compositeur interprète
        /// </summary>
        public string Instrument
        {
            get { return instrument; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new InstrumentInvalidException();
                }
                instrument = value;
            }
        }

        /// <summary>
        /// La pièce musicale de l'auteur-compositeur interprète
        /// </summary>
        public NuméroArtiste PieceMusicale
        {
            get { return interprétation; }
        }

        public override NuméroArtiste NuméroArtiste
        {
            get { return PieceMusicale; }
        }

        // Constructeur
        /// <summary>
        /// Constructeur paramétrique
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="numéro"></param>
        /// <param name="catégorie"></param>
        /// <param name="prestation"></param>
        /// <param name="titre"></param>
        /// <param name="durée"></param>
        public AuteurCompositeurInterprète(string nom, int numéro, char catégorie, string prestation, string titre, string durée)
            : base(nom, numéro, catégorie, prestation)
        {
            interprétation = new NuméroArtiste(titre, durée);
            NbreArtistesACI++;
        }

        /// <summary>
        /// Constructeur static
        /// </summary>
        static AuteurCompositeurInterprète()
        {
            NbreArtistesACI = 0;
        }

        public override string ExécuterNuméro()
        {
            return $"- Dossard {NuméroDossard} : Pour l'auteur-compositeur interprète {NomArtiste}," +
                $" « Quelle maîtrise lorsqu'il s'accompagne en interprétant \"{PieceMusicale.Titre}\" ***»";
        }

        public override string ÉcrireRapport()
        {
            return $"- Dossard {NuméroDossard} : {NomArtiste} a interprété : \" {PieceMusicale.Titre} \" " +
                $"et sa piècre musicale a duré {PieceMusicale.Durée}" + Environment.NewLine;
        }
    }
}
