using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using SpellBouc.UISpells;
using SpellBouc.Logs;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.CompilerServices;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.WindowsRuntime;

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
                        Composante = FormatComp(reader.GetString(8)),
                        IncTime = reader.GetString(9),
                        Range = reader.GetString(10),
                        AreaEffect = reader.GetString(11),
                        Duration = reader.GetString(12),
                        SaveDice = reader.GetString(13),
                        MagicResist = GetMagicRes(reader),
                        Description = FormatDescr(reader.GetString(15)),
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

        /*
         * Récupère le nombre de sort max par jour à partir de la BDD du joueur
         */
        internal static int[] SetMaxNumberByLvl(int spellMax, string dbPath)
        {
            int[] MaxSpellNumberByLvl = new int[spellMax];
            try
            {
                using (var connection = new SqliteConnection("Data Source=" + Globals.DB_PLAYER_WIZARD_SPELL_PATH))
                {

                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText =
                    @"
                    SELECT *
                    FROM 'maxspells';
                    ";

                    int i = 0;
                    // Stocke tous les paramètres des sorts dans 
                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if(i < spellMax )
                        {
                            MaxSpellNumberByLvl[i] = reader.GetInt32(1);
                            i++;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            catch
            {
                 foreach(int lvl in MaxSpellNumberByLvl)
                {
                    MaxSpellNumberByLvl[lvl] = 0;
                }
            }

            return MaxSpellNumberByLvl;
            
        }

        /*
         * Update le nombre de sort max par jour à partir de la BDD du joueur
         */
        internal static ErrorCode UpdateMaxNumberByLvl(int[] maxSpellToUpdate, string dB_PLAYER_WIZARD_SPELL_PATH)
        {
            try
            {
                using var connection = new SqliteConnection("Data Source=" + Globals.DB_PLAYER_WIZARD_SPELL_PATH);
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                INSERT OR REPLACE INTO 'maxspells'(lvl, spellMax) VALUES
                (0, $lvl0),
                (1, $lvl1),
                (2, $lvl2),
                (3, $lvl3),
                (4, $lvl4);
            ";

                command.Parameters.AddWithValue("$lvl0", maxSpellToUpdate[0]);
                command.Parameters.AddWithValue("$lvl1", maxSpellToUpdate[1]);
                command.Parameters.AddWithValue("$lvl2", maxSpellToUpdate[2]);
                command.Parameters.AddWithValue("$lvl3", maxSpellToUpdate[3]);
                command.Parameters.AddWithValue("$lvl4", maxSpellToUpdate[4]);
                command.ExecuteNonQuery();

                return ErrorCode.SUCCESS;
            }
            catch 
            {
                return ErrorCode.ERROR;
            }

        }


        /********************************************* FORMATAGE *********************************************/

        /*
         *  Formate la description pour remplacer les balises inutiles
         */
        private static string FormatDescr(string descr)
        {
            string formatedDescr = "";
            if (descr != null && descr != "")
            {
                // Supprime toutes les balises:
                formatedDescr = descr.Replace("<br/>", "\n");
                formatedDescr = formatedDescr.Replace("\n\n", "\n");

                formatedDescr = formatedDescr.Replace("<p>", " ");
                formatedDescr = formatedDescr.Replace("</p>", " ");

                formatedDescr = formatedDescr.Replace("<h5>", "");
                formatedDescr = formatedDescr.Replace("</h5>", "");

                formatedDescr = formatedDescr.Replace("</a>", "");
                formatedDescr = formatedDescr.Replace("<a", "");

                formatedDescr = formatedDescr.Replace("<li>", "");
                formatedDescr = formatedDescr.Replace("</li>", "");

                formatedDescr = formatedDescr.Replace("<ul>", "");
                formatedDescr = formatedDescr.Replace("</ul>", "");

                formatedDescr = formatedDescr.Replace("<tr>", "");
                formatedDescr = formatedDescr.Replace("</tr>", "");

                formatedDescr = formatedDescr.Replace("<th>", "");
                formatedDescr = formatedDescr.Replace("</th>", "");

                formatedDescr = formatedDescr.Replace("<td>", "");
                formatedDescr = formatedDescr.Replace("</td>", "");

                formatedDescr = formatedDescr.Replace("</span>", "");

                formatedDescr = formatedDescr.Replace("<i>", "");
                formatedDescr = formatedDescr.Replace("</i>", "");

                formatedDescr = formatedDescr.Replace("<em>", "");
                formatedDescr = formatedDescr.Replace("</em>", "");

                formatedDescr = formatedDescr.Replace("<u>", "");
                formatedDescr = formatedDescr.Replace("</u>", "");

                // Supprime les href
                int occCount = Regex.Matches(formatedDescr, "href=").Count;

                for (int i = 0; i < occCount; i++)
                {
                    int pFrom = formatedDescr.IndexOf("href=");
                    if(pFrom == -1)
                    {
                        break;
                    }
                    int pTo = formatedDescr.IndexOf(">", pFrom);


                    string substrToDelete = formatedDescr.Substring(pFrom, pTo - pFrom + 1);
                    formatedDescr = formatedDescr.Replace(substrToDelete, "");
                }

                // Supprime les spans
                occCount = Regex.Matches(formatedDescr, "<span").Count;

                for (int i = 0; i < occCount; i++)
                {
                    int pFrom = formatedDescr.IndexOf("<span");
                    if (pFrom == -1)
                    {
                        break;
                    }
                    int pTo = formatedDescr.IndexOf(">", pFrom);


                    string substrToDelete = formatedDescr.Substring(pFrom, pTo - pFrom + 1);
                    formatedDescr = formatedDescr.Replace(substrToDelete, "");
                }

            }
            return formatedDescr;
        }

        /*
         *  Formate la composante du sorts pour retirer les caractères superflux 
         */
        private static string FormatComp(string comp)
        {
            string formatedDescr = "";
            if (comp != null && comp != "")
            {
                // Supprime toutes les balises:
                formatedDescr = comp.Replace("<abbr title=", "");
                formatedDescr = formatedDescr.Replace("/", "");
                formatedDescr = formatedDescr.Replace("(E", "");
                formatedDescr = formatedDescr.Replace("\n", "");
            }

            return formatedDescr;
        }
    }
}