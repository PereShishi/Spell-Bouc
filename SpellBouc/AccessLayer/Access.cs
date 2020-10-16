using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using SpellBouc.UISpells;
using SpellBouc.Logs;

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
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var spell = new Spell
                    {

                        // Récupére données des lignes de la BDD
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Lvl = reader.GetInt32(2),
                        Type = reader.GetString(3),
                        Source = reader.GetString(4),
                        // 5: Niveau mal formaté 
                        // 6: Domaine
                        // 7: Initie
                        Composante = reader.GetString(8),
                        IncTime = reader.GetString(9),
                        Range = reader.GetString(10),
                        AreaEffect = reader.GetString(11),
                        Duration = reader.GetString(12),
                        SaveDice = reader.GetString(13),
                        MagicResist = GetMagicRes(reader),
                        Description = reader.GetString(15),
                        Comp = reader.GetString(16)
                    };

                    spellList.Add(spell);
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

                using var connection = new SqliteConnection("Data Source=" + Globals.DB_PLAYER_WIZARD_SPELL_PATH);
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
                command.Parameters.AddWithValue("$resistance_magie", SetMagicRes(inputSpell.MagicResist));
                command.Parameters.AddWithValue("$description", inputSpell.Description);
                command.Parameters.AddWithValue("$comp_sort", inputSpell.Comp);

                command.ExecuteNonQuery();

                return ErrorCode.SUCCESS;
            }
            catch
            {
                return ErrorCode.ERROR;
            }
        }

        /*
         * Requête qui supprime un sort de magicien dans la BDD su joueur.
         */
        internal static ErrorCode RemoveWizardSpellInPlayerDB(int id)
        {
            try
            {
                using var connection = new SqliteConnection("Data Source=" + Globals.DB_PLAYER_WIZARD_SPELL_PATH);
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
            @"
                    DELETE FROM 'sorts' WHERE id == $id;
                ";
                command.Parameters.AddWithValue("$id", id);
                command.ExecuteNonQuery();
                
                return ErrorCode.SUCCESS;
            }
            catch
            {
                return ErrorCode.ERROR;
            }
        }


        /*
         *  Retourne la résistance magique à partir du reader de la BDD, cette fonction est nécessaire car on récupère un String Oui/Non à partir de la BDD.
         */
        static private bool GetMagicRes(SqliteDataReader reader)
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
        static private String SetMagicRes(bool boolMagicResist)
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

        /*
         *  Récupère les utilisations de sorts du mage et les retroune dans un tableau à deux dimensions
         */
        internal static List<UIWizardPlayerSpell> GetUIWizardSpellsFromDB()
        {
            var wizardUIPlayerSpells = new List<UIWizardPlayerSpell>();

            using (var connection = new SqliteConnection("Data Source=" + Globals.DB_PLAYER_WIZARD_SPELL_PATH))
            {

                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT *
                    FROM 'player_spell_count';
                ";

                // Stocke tous les paramètres des sorts dans 
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var uiWizardPlayerSpell = new UIWizardPlayerSpell
                    {
                        // Récupére données des lignes de la BDD
                        Id = reader.GetInt32(0),
                        PlayerSpellCount = reader.GetInt32(1)
                    };

                    wizardUIPlayerSpells.Add(uiWizardPlayerSpell);

                }
            }


            return wizardUIPlayerSpells;
        }

        /*
         *  Change le player_count d'un sort de l'UI
         */
        internal static void ChangeWizardSpellPlayerCount(int id, int newSpellCount)
        {
            try
            {
                using var connection = new SqliteConnection("Data Source=" + Globals.DB_PLAYER_WIZARD_SPELL_PATH);
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                        REPLACE INTO 'player_spell_count'
                        (id, player_count)

                        VALUES($id,$newSpellCount);

                ";

                command.Parameters.AddWithValue("$id", id);
                command.Parameters.AddWithValue("$newSpellCount", newSpellCount);
                command.ExecuteNonQuery();
            }
            catch
            {
                // Gérrer l'erreur
                return;
            }
        }

        /*
         *  Ajoute un spell dans le spell_count de l'UI
         */  
        internal static ErrorCode AddSpellInUiDB(int inputID)
        {
            try 
            {
                using var connection = new SqliteConnection("Data Source=" + Globals.DB_PLAYER_WIZARD_SPELL_PATH);
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                        INSERT OR REPLACE INTO 'player_spell_count'
                        (id, player_count)

                        VALUES($id,0);

                    ";

                command.Parameters.AddWithValue("$id", inputID);
                command.ExecuteNonQuery();

                return ErrorCode.SUCCESS;
            }
            catch
            {
                return ErrorCode.ERROR;
            }

        }


        /*
         *  Supprime un spell dans le spell_count de l'UI
         */
        internal static ErrorCode RemoveSpellInUiDB(int inputID)
        {
            try
            {
                using var connection = new SqliteConnection("Data Source=" + Globals.DB_PLAYER_WIZARD_SPELL_PATH);
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                        DELETE FROM 'player_spell_count' WHERE id == $id;
                    ";

                command.Parameters.AddWithValue("$id", inputID);
                command.ExecuteNonQuery();

                return ErrorCode.SUCCESS;
            }
            catch
            {
                return ErrorCode.ERROR;
            }

        }
    }
}