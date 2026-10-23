using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using LionsDuCameroun.Models;

namespace LionsDuCameroun.Core.DAL
{
    public class FootballeurDAL
    {
        // ── Lit la chaîne de connexion depuis Web.config ────────────── 
        // Correspond à : <add name="LionsDuCameroun" ... /> dans Web.config 
        private readonly string _connStr =
            ConfigurationManager.ConnectionStrings["LionsDuCameroun"]
                                .ConnectionString;
        // ─── 1. Lire tous les footballeurs ─────────────────────────── 
        public List<FootballeurProfile> GetFootballeurList()
        {
            var list = new List<FootballeurProfile>();

            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand("usp_GetAllFootballeurs", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new FootballeurProfile
                        {
                            FootballeurId = (int)reader["FootballeurId"],
                            Nom = reader["Nom"].ToString(),
                            Club = reader["Club"].ToString(),
                            Poste = reader["Poste"].ToString(),
                            Buts = (int)reader["Buts"],
                            Selections = (int)reader["Selections"]
                        });
                    }
                } // le reader est fermé ici
            } // conn.Close() appelé automatiquement par le using

            return list;
        }

        public FootballeurProfile GetFootballeurById(int id)
        {
            FootballeurProfile joueur = null;

            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand("usp_GetFootballeurById", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        joueur = new FootballeurProfile
                        {
                            FootballeurId = (int)reader["FootballeurId"],
                            Nom = reader["Nom"].ToString(),
                            Club = reader["Club"].ToString(),
                            Poste = reader["Poste"].ToString(),
                            Buts = (int)reader["Buts"],
                            Selections = (int)reader["Selections"]
                        };
                    }
                }
            }
            return joueur;
        }

        // ─── 3. Ajouter un footballeur ─────────────────────────────── 
        public bool AddFootballeur(FootballeurProfile p)
        {
            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand("usp_AddFootballeur", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nom", p.Nom);
                cmd.Parameters.AddWithValue("@Club", p.Club);
                cmd.Parameters.AddWithValue("@Poste", p.Poste);
                cmd.Parameters.AddWithValue("@Buts", p.Buts);
                cmd.Parameters.AddWithValue("@Selections", p.Selections);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ─── 4. Mettre à jour un footballeur ──────────────────────── 
        public bool UpdateFootballeur(FootballeurProfile p)
        {
            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand("usp_UpdateFootballeur", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", p.FootballeurId);
                cmd.Parameters.AddWithValue("@Nom", p.Nom);
                cmd.Parameters.AddWithValue("@Club", p.Club);
                cmd.Parameters.AddWithValue("@Poste", p.Poste);
                cmd.Parameters.AddWithValue("@Buts", p.Buts);
                cmd.Parameters.AddWithValue("@Selections", p.Selections);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ─── 5. Supprimer un footballeur ──────────────────────────── 
        public bool DeleteFootballeur(int id)
        {
            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand("usp_DeleteFootballeur", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    } 
}
