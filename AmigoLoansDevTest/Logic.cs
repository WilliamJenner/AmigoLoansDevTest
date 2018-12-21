using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace AmigoLoansDevTest
{
    public class Logic
    {

        DataAccess database = new DataAccess();
        private string lastTwoWeeks;
        private string eligibleWorkers;

        /// <summary>
        /// Class constructor
        /// </summary>
        public Logic()
        {

        }

        /// <summary>
        /// Selects two random engineers from the database
        /// </summary>
        /// <returns>string[] containing both engineers</returns>
        public string[] SelectEngineers()
        {
            string[] engineers = new string[2]; // the system selects two engineers

            // reads in SQL files
            eligibleWorkers = File.ReadAllText(@"eligibleWorkers.sql");
            lastTwoWeeks = File.ReadAllText(@"lastTwoWeeks.sql");

            engineers = RandomlySelectTwoValues(CreateDataSet());

            return engineers;
        }

        /// <summary>
        /// Creates a set of engineers from two SQL scripts.
        /// </summary>
        /// <returns>string[] containing all data</returns>
        private string[] CreateDataSet()
        {
            string[] attributesTwoWeeks = {"name"};
            string[] attributesEligibleWorkers = {"name"};
            string[] results = database.SelectQuery(lastTwoWeeks, attributesTwoWeeks);
            string[] dataSet;

            // If there are no results from first script, take all results from second
            if (results.Length == 0)
            {
                results = database.SelectQuery(eligibleWorkers, attributesEligibleWorkers);
                dataSet = results;
            }

            // If there is only 1 result from first script, adds 1 other random result from second script
            else if (results.Length == 1)
            {
                string[] eligibleWorkersResults = database.SelectQuery(eligibleWorkers, attributesEligibleWorkers);

                string[] combinedResults = new string[2];
                Random rnd = new Random();
                int a = rnd.Next(0, eligibleWorkersResults.Length);

                combinedResults[0] = results[0];

                // If the randomly selected result is the same as the only first result, regenerate the index to get a new result
                while (eligibleWorkersResults[a] == results[0])
                {
                    a = rnd.Next(0, eligibleWorkersResults.Length);
                }

                combinedResults[1] = eligibleWorkersResults[a];
                dataSet = combinedResults;
            }

            else
            {
                dataSet = results;
            }

            return dataSet;
        }

        /// <summary>
        /// Will randomly select two values from a given string[].
        /// </summary>
        /// <param name="valueSet">string[] that will be selected from</param>
        /// <returns>string[] containing two random values<returns>
        private string[] RandomlySelectTwoValues(string[] valueSet)
        {
            
            int length = valueSet.Length;
            Random rnd = new Random();
            int a = rnd.Next(0, length);
            int b = rnd.Next(0, length);

            // If B == A, will regenerate B until it is not.
            while (b == a)
            {
                b = rnd.Next(0, length);
            }

            string[] randomValues = { valueSet[a], valueSet[b] };
            return randomValues;

        }

    }
}
