using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace AmigoLoansDevTest
{
    public class DataClass
    {
        private SQLiteConnection sqlite;

        //constructor
        public DataClass()
        {
            sqlite = new SQLiteConnection("Data Source =DevTestDb.db;Version = 3;New=False");
            ConfigTables();
        }
        
        // Creates the tables if they do not exist
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


        public string[] SelectQuery()
        {
            sqlite.Open();
            string stm = @"
            Select Name, Engineers.Id 
            From Engineers
            Inner Join Rota on Rota.Engineer_Id = Engineers.Id
            ";
            string[] results = new string[2];

            using (SQLiteCommand cmd = new SQLiteCommand(stm, sqlite))
            {
                using (SQLiteDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        results[0] = rdr["Name"].ToString();
                        results[1] = rdr["Id"].ToString();

                    }
                }
                sqlite.Close();
                return results;
            }

        }
    }
}
