using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SpellBouc
{
    class Access
    {
      
        static public void GetWizardCompleteSpells()
        {

            using (var connection = new SqliteConnection("Data Source=" + Globals.DB_PATH))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT *
                    FROM 'sorts'
                ";
                //command.Parameters.AddWithValue("$id", test);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);

                        Console.WriteLine($"Hello, {name}!");
                    }
                }
            }
        }


    }
}
