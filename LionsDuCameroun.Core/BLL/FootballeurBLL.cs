using LionsDuCameroun.Core.DAL;
using LionsDuCameroun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LionsDuCameroun.Core.BLL
{
    public class FootballeurBLL
    {
        // Instanciation de la DAL (injection simple) 
        private readonly FootballeurDAL _dal = new FootballeurDAL();

        // ── Chaque méthode délègue à la couche inférieure (DAL) ────── 

        public List<FootballeurProfile> GetFootballeurList()
            => _dal.GetFootballeurList();
        // '=>' est une syntaxe raccourcie pour { return  _dal.GetFootballeurList(); } 
 
        public FootballeurProfile GetFootballeurById(int id)
            => _dal.GetFootballeurById(id);

        public bool AddFootballeur(FootballeurProfile p)
            => _dal.AddFootballeur(p);

        public bool UpdateFootballeur(FootballeurProfile p)
            => _dal.UpdateFootballeur(p);

        public bool DeleteFootballeur(int id)
            => _dal.DeleteFootballeur(id);
    }
}
