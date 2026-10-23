using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LionsDuCameroun.Models
{
    public class FootballeurProfile
    {
        /// <summary> 
        /// Identifiant unique du joueur (clé primaire SQL, auto-incrémenté) 
        /// Exemple : 1, 2, 3... 
        /// </summary> 
        public int FootballeurId { get; set; }

        /// <summary> 
        /// Nom complet du joueur 
        /// Exemple : 'Samuel Eto'o', 'Roger Milla' 
        /// </summary> 
        public string Nom { get; set; }

        /// <summary> 
        /// Club actuel du joueur 
        /// Exemple : 'Manchester United', 'Inter Milan' 
        /// </summary> 
        public string Club { get; set; }

        /// <summary> 
        /// Poste sur le terrain 
        /// Valeurs possibles : Gardien, Défenseur, Milieu, Attaquant 
        /// </summary> 
        public string Poste { get; set; }

        /// <summary> 
        /// Nombre de buts marqués en sélection nationale 
        /// </summary> 
        public int Buts { get; set; }

        /// <summary> 
        /// Nombre de matchs joués sous le maillot des Lions Indomptables 
        /// </summary> 
        public int Selections { get; set; }
    }
}
