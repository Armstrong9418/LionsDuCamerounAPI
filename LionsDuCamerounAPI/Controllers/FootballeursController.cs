using LionsDuCameroun.Core.BLL;
using LionsDuCameroun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LionsDuCamerounAPI.Controllers
{
    public class FootballeursController : ApiController
    {
        // Instanciation de la couche métier 
        private readonly FootballeurBLL _bl = new FootballeurBLL();

        // ════════════════════════════════════════════════════════════ 
        // GET — Lire tous les footballeurs 
        // URL : GET /api/Footballeurs/GetFootballeurList 
        // Réponse : 200 OK + liste JSON | 404 si la liste est vide 
        // ════════════════════════════════════════════════════════════ 
        [HttpGet]
        [ActionName("GetFootballeurList")]
        public IHttpActionResult GetFootballeurList()
        {
            var liste = _bl.GetFootballeurList();

            // Si la liste existe et n'est pas vide → 200 OK 
            // Sinon → 404 Not Found 
            return liste?.Count > 0
                ? Ok(liste)                      // 200 + données JSON 
                : (IHttpActionResult)NotFound();  // 404 
        }

        // ════════════════════════════════════════════════════════════ 
        // GET — Lire un footballeur par ID 
        // URL : GET /api/Footballeurs/GetFootballeurById?footballeurId=1 
        // Réponse : 200 OK + objet JSON | 404 si ID inexistant 
        // ════════════════════════════════════════════════════════════ 
        [HttpGet]
        [ActionName("GetFootballeurById")]
        public IHttpActionResult GetFootballeurById(int footballeurId)
        {
            var joueur = _bl.GetFootballeurById(footballeurId);

            return joueur != null
                ? Ok(joueur)
                : (IHttpActionResult)NotFound();
        }

        // ════════════════════════════════════════════════════════════ 
        // POST — Ajouter un footballeur 
        // URL : POST /api/Footballeurs/AddFootballeur 
        // Corps : objet JSON FootballeurProfile (sans FootballeurId) 
        // Réponse : 200 OK 'Joueur ajouté!' | 400 si données invalides 
        // ════════════════════════════════════════════════════════════ 
        [HttpPost]
        [ActionName("AddFootballeur")]
        public IHttpActionResult AddFootballeur([FromBody] FootballeurProfile profile)
        {
            // [FromBody] : lit l'objet JSON depuis le corps de la requête HTTP 
            // ModelState.IsValid : vérifie que les champs requis sont présents 
            if (!ModelState.IsValid)
                return BadRequest(ModelState);  // 400 avec détails de l'erreur
        
        bool succes = _bl.AddFootballeur(profile); 
 
            return succes
                ? Ok("Joueur ajouté avec succès ! Bienvenue chez les Lions !") 
                : (IHttpActionResult)InternalServerError();  // 500 
        } 
 
        // ════════════════════════════════════════════════════════════ 
        // PUT — Mettre à jour un footballeur 
        // URL : PUT /api/Footballeurs/UpdateFootballeur 
        // Corps : objet JSON complet (avec FootballeurId) 
        // Réponse : 200 OK | 400 si ID manquant | 404 si introuvable 
        // ════════════════════════════════════════════════════════════ 
        [HttpPut]
        [ActionName("UpdateFootballeur")]
        public IHttpActionResult UpdateFootballeur([FromBody] FootballeurProfile profile)
        {
            // Validation : on vérifie que l'ID est fourni et valide 
            if (profile == null || profile.FootballeurId <= 0)
                return BadRequest("L'identifiant (FootballeurId) est obligatoire pour une mise à jour."); 


            bool succes = _bl.UpdateFootballeur(profile);

            return succes
                ? Ok("Informations du joueur mises à jour avec succès !")
                : (IHttpActionResult)NotFound();
        }

        // ════════════════════════════════════════════════════════════ 
        // DELETE — Supprimer un footballeur 
        // URL : DELETE /api/Footballeurs/DeleteFootballeur?footballeurId=3 
        // Réponse : 200 OK | 404 si ID introuvable 
        // ════════════════════════════════════════════════════════════ 
        [HttpDelete]
        [ActionName("DeleteFootballeur")]
        public IHttpActionResult DeleteFootballeur(int footballeurId)
        {
            bool succes = _bl.DeleteFootballeur(footballeurId);

            return succes
                ? Ok("Joueur retiré de la sélection avec succès.")
                : (IHttpActionResult)NotFound();
        }
    }
}
