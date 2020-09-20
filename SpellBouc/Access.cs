using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using SpellBouc; 

namespace SpellBouc.AccessLayer
{
    class Access
    {

        /*
         * Requête qui récupère tous les sorts de magicien dans un BDD et les retourne dans une liste.
         */
        internal static List<Spell> GetWizardSpellsFromDB(String dbPath)
        {
            List<Spell> spellList = new List<Spell>();

            using (var connection = new SqliteConnection("Data Source=" + dbPath))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT *
                    FROM 'sorts';
                ";

                // Stocke tous les paramètres des sorts dans 
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var spell = new Spell();

                        // Récupére données des lignes de la BDD
                        spell.Id = reader.GetInt32(0);
                        spell.Name = reader.GetString(1);
                        spell.Lvl = reader.GetInt32(2);
                        spell.Type = reader.GetString(3);
                        spell.Source = reader.GetString(4);
                        // 5: Niveau mal formaté 
                        // 6: Domaine
                        // 7: Initie
                        spell.Composante = reader.GetString(8);
                        spell.IncTime = reader.GetString(9);
                        spell.Range = reader.GetString(10);
                        spell.AreaEffect = reader.GetString(11);
                        spell.Duration = reader.GetString(12);
                        spell.SaveDice = reader.GetString(13);
                        spell.MagicResist = getMagicRes(reader);
                        spell.Description = reader.GetString(15);
                        spell.Comp = reader.GetString(16);

                        spellList.Add(spell);
                    }
                }
            }

            return spellList;
        }

        /*
         * Requête qui ajoute un sort de magicien dans la BDD su joueur.
         */
        internal static ErrorCode AddWizardSpellInPlayerDB(Spell inputSpell)
        {
            try
            {

                using (var connection = new SqliteConnection("Data Source=" + Globals.DB_PLAYER_WIZARD_SPELL_PATH))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText =
                    @"
                        INSERT OR REPLACE INTO 'sorts'
                        (id, nom,niveau_mage, typesort, source, niveau, domaine, initie, composantes, temps_incantation, portee,
                        zone_effet, duree, jet_sauvegarde, resistance_magie, description, comp_sort, html
                        )

                        VALUES($id, $nom, $niveau_mage, $typesort, $source,'', '', '', $composantes, $temps_incantation, $portee,
                        $zone_effet, $duree, $jet_sauvegarde, $resistance_magie, $description, $comp_sort, ''
                        );
                    ";
                    command.Parameters.AddWithValue("$id", inputSpell.Id);
                    command.Parameters.AddWithValue("$nom", inputSpell.Name);
                    command.Parameters.AddWithValue("$niveau_mage", inputSpell.Lvl);
                    command.Parameters.AddWithValue("$typesort", inputSpell.Type);
                    command.Parameters.AddWithValue("$source", inputSpell.Source);
                    command.Parameters.AddWithValue("$composantes", inputSpell.Composante);
                    command.Parameters.AddWithValue("$temps_incantation", inputSpell.IncTime);
                    command.Parameters.AddWithValue("$portee", inputSpell.Range);
                    command.Parameters.AddWithValue("$zone_effet", inputSpell.AreaEffect);
                    command.Parameters.AddWithValue("$duree", inputSpell.Duration);
                    command.Parameters.AddWithValue("$jet_sauvegarde", inputSpell.SaveDice);
                    command.Parameters.AddWithValue("$resistance_magie", setMagicRes(inputSpell.MagicResist));
                    command.Parameters.AddWithValue("$description", inputSpell.Description);
                    command.Parameters.AddWithValue("$comp_sort", inputSpell.Comp);

                    return ErrorCode.SUCCESS;
                }
            }
            catch
            {
                return ErrorCode.ERROR;
            }
        }

        /*
         * Requête qui ajoute un sort de magicien dans la BDD su joueur.
         */
        internal static ErrorCode RemoveWizardSpellInPlayerDB(int id)
        {
            try
            {
                using (var connection = new SqliteConnection("Data Source=" + Globals.DB_PLAYER_WIZARD_SPELL_PATH))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText =
                @"
                    DELETE FROM 'sorts' WHERE id == $id;
                ";
                    command.Parameters.AddWithValue("$id", id);
                    return ErrorCode.SUCCESS;
                }
            }
            catch
            {
                return ErrorCode.ERROR;
            }
        }
        

        /*
         *  Retourne la résistance magique à partir du reader de la BDD, cette fonction est nécessaire car on récupère un String Oui/Non à partir de la BDD.
         */
        static private bool getMagicRes(SqliteDataReader reader)
        {
            var magicResistFromDB = reader.GetString(14);

            if (magicResistFromDB.Contains("oui"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /*
         *  Set la résistance magique pour être au même format que celui de la BDD, cette fonction est nécessaire car on récupère un String oui/non à partir de la BDD.
         */
        static private String setMagicRes(bool boolMagicResist)
        {

            if (boolMagicResist == true)
            {
                return "oui";
            }
            else
            {
                return "non";
            }

        }


    }
}
