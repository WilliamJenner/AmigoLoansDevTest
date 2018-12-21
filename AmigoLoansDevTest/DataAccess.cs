using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace AmigoLoansDevTest
{
    public class DataAccess
    {
        private SQLiteConnection sqlite;

        /// <summary>
        /// Class constructor
        /// </summary>
        public DataAccess()
        {
            sqlite = new SQLiteConnection("Data Source=DevTestDb.db;Version=3;New=False");
            ConfigTables();
        }

        /// <summary>
        /// Creates tables if they do not exist
        /// </summary>
        public void ConfigTables()
        {
            sqlite.Open();
            string createScripts = @"
            CREATE TABLE IF NOT EXISTS [Engineers] (
            [Id] INTEGER  NOT NULL
            , [Name] text NOT NULL
            , CONSTRAINT [sqlite_master_PK_Engineers] PRIMARY KEY ([Id])
            );

            CREATE TABLE IF NOT EXISTS [Rota] (
            [Id] INTEGER  NOT NULL
            , [Engineer_Id] bigint  NOT NULL
            , [Shift] text NOT NULL
            , CONSTRAINT [sqlite_master_PK_Rota] PRIMARY KEY ([Id])
            , FOREIGN KEY ([Shift]) REFERENCES [engineer] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
            );

            ";

            using (SQLiteCommand cmd = new SQLiteCommand(createScripts, sqlite)) { }

            sqlite.Close();
        }

        /// <summary>
        /// Queries a database
        /// </summary>
        /// <param name="query">String containing an SQL query</param>
        /// <param name="attributes">The expected attribute(s)/column(s) from the output data</param>
        /// <returns>A string[] containing all data in left -> right order.</returns>
        public string[] SelectQuery(string query, string[] attributes)
        {
            List<string> results = new List<string>();
            sqlite.Open();

            using (SQLiteCommand cmd = new SQLiteCommand(query, sqlite))
            {
                using (SQLiteDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        for (int i = 0; i < attributes.Length; i++)
                        {
                            results.Add(rdr[attributes[i]].ToString());
                        }
                    }
                }
                sqlite.Close();
            }

            string[] resultsArray = results.ToArray();
            return resultsArray;

        }

    }
}
