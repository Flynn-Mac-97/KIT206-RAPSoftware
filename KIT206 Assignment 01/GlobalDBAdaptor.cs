using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace KIT206_Assignment_01
{
    class GlobalDBAdaptor
    {
        //Note that ordinarily these would (1) be stored in a settings file and (2) have some basic encryption applied
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";
        private const string server = "alacritas.cis.utas.edu.au";

        private MySqlConnection conn;

        //Constructor for the DB adaptor, creates a connection to the database
        public GlobalDBAdaptor()
        {
            //create a connection to the database
            string connectionString = String.Format("Database={0};Data Source={1};User Id={2};Password={3}", db, server, user, pass);
            conn = new MySqlConnection(connectionString);

            this.ReadData();
            this.ReadIntoDataSet();
        }
        /*
         * Using the ExecuteReader method to select from a single table
         */
        public void ReadData()
        {
            MySqlDataReader rdr = null;

            try
            {
                // Open the connection
                conn.Open();

                // 1. Instantiate a new command with a query and connection
                MySqlCommand cmd = new MySqlCommand("select given_name, family_name from researcher", conn);

                // 2. Call Execute reader to get query results
                rdr = cmd.ExecuteReader();

                // print the CategoryName of each record
                while (rdr.Read())
                {
                    //This illustrates how the raw data can be obtained using an indexer [] or a particular data type can be obtained using a GetTYPENAME() method.
                    //Console.WriteLine("{0} {1}", rdr[0], rdr.GetString(1));
                }
            }
            finally
            {
                // close the reader
                if (rdr != null)
                {
                    rdr.Close();
                }

                // Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        /*
        * Using the ExecuteReader method to select from a single table
        */
        public void ReadIntoDataSet()
        {
            try
            {
                var researcherDataSet = new DataSet();
                var researcherAdapter = new MySqlDataAdapter("select * from researcher", conn);
                researcherAdapter.Fill(researcherDataSet, "researcher");

                foreach (DataRow row in researcherDataSet.Tables["researcher"].Rows)
                {
                    //Again illustrating that indexer (based on column name) gives access to whatever data
                    //type was obtained from a given column, but can call ToString() on an entry if needed.
                    //Console.WriteLine("Name: {0} {1}", row["given_name"], row["family_name"].ToString());
                }
            }
            finally
            {
                // Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        /*
        * Using the ExecuteScalar method
        * returns number of records
        */
        public int GetNumberOfRecords()
        {
            int count = -1;
            try
            {
                // Open the connection
                conn.Open();

                // 1. Instantiate a new command
                MySqlCommand cmd = new MySqlCommand("select COUNT(*) from researcher", conn);

                // 2. Call ExecuteScalar to send command
                // This convoluted approach is safe since cannot predict actual return type
                count = int.Parse(cmd.ExecuteScalar().ToString());
            }
            finally
            {
                // Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return count;
        }
        //Demo function for testing functionality with no connection to a DB
        // a complete function which works with DB data should work in exactly the same way but instead not use random values.
        public List<Researcher> GenerateDummyResearchers(int amount)
        {
            string[] firstNames = new string[] {
                "Aiden", "Liam", "Emma", "Olivia",
                "Sofia", "Miguel", "Ananya", "Raj",
                "Yuki", "Hana", "Amir", "Fatima",
                "Luca", "Giulia", "Igor", "Anastasia",
                "Zara", "Kwame", "Chen", "Ling",
                "Carlos", "Isabella", "Noah", "Ava"
            };
            string[] lastNames = new string[] {
                "Smith", "Johnson", "Patel", "Chen",
                "Garcia", "Rodriguez", "Kim", "Kumar",
                "Müller", "Rossi", "Ivanov", "Hussein",
                "Kone", "Yamamoto", "Silva", "Santos",
                "Cohen", "Singh", "Li", "Zhang",
                "Nguyen", "Dubois", "O'Connor", "Johannsson"
            };

            //for random generation
            Random rnd = new Random();
            List<Researcher> dummyResearchers = new List<Researcher>();
            for (int i = 0; i < amount; i++)
            {
                //random names
                string randomFirstName = firstNames[rnd.Next(firstNames.Length)];
                string randomLastName = lastNames[rnd.Next(lastNames.Length)];

                //random title
                Title title = (Title)rnd.Next(3);

                string school = "ICT";

                Campus campus = (Campus)rnd.Next(3);

                string email = randomFirstName + "."+ randomLastName + "@utas.edu.au";

                EmploymentLevel currentJobTitle = (EmploymentLevel)rnd.Next(6);

                Date commencedCurrentPosition = new Date(rnd.Next(30)+1, rnd.Next(12)+1, 2023);
                Date commencedAtInstitute = new Date(rnd.Next(30)+1, rnd.Next(12)+1,2023);

                int tenure = rnd.Next(12);

                int publications = rnd.Next(64);

                float Q1Percentage = (float)rnd.NextDouble();

                //for now just make a general researcher
                Researcher newRandomResearcher = new Researcher(randomFirstName, randomLastName, title, email, school, currentJobTitle, commencedCurrentPosition, commencedAtInstitute, tenure, Q1Percentage);
                dummyResearchers.Add(newRandomResearcher);

                //if they are a student...
                if(currentJobTitle == EmploymentLevel.STUDENT)
                {

                }
                //if they are a staff...
                else
                {

                }
            }
            return dummyResearchers;
        }
    }
}

