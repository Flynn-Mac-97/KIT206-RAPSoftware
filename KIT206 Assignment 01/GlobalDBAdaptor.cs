using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace KIT206_Assignment_01 {
    class GlobalDBAdaptor {
        // Database connection configuration constants
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";
        private const string server = "alacritas.cis.utas.edu.au";

        // MySqlConnection object to manage the database connection
        private readonly MySqlConnection conn;

        // Constructor for GlobalDBAdaptor, initializes the database connection
        public GlobalDBAdaptor() {
            // Creating a connection string for the MySQL connection
            string connectionString = String.Format("Database={0};Data Source={1};User Id={2};Password={3}", db, server, user, pass);
            // Initializing the MySqlConnection object with the connection string
            conn = new MySqlConnection(connectionString);
        }

        // Fetches a list of researchers from the database
        public List<Researcher> FetchResearcherListFromDB() {
            // Initialize an empty list of researchers
            List<Researcher> researchers = new List<Researcher>();
            try {
                // DataSet to hold the data fetched from the database
                var researcherDataSet = new DataSet();
                // Data adapter to manage data retrieval from the database
                var researcherAdapter = new MySqlDataAdapter("select * from researcher", conn);
                // Filling the DataSet with data from the 'researcher' table
                researcherAdapter.Fill(researcherDataSet, "researcher");

                // Iterating through each row in the 'researcher' table
                foreach (DataRow row in researcherDataSet.Tables["researcher"].Rows) {
                    Researcher r = null; // Default value in case of failure
                    string type = row["type"].ToString();
                    // If the researcher is a staff member
                    if(type == "Staff") {
                        // Creating a new Researcher object and initializing it with data from the row
                        r = new Researcher {
                            id = (int)row["id"],
                            givenName = row["given_name"].ToString(),
                            familyName = row["family_name"].ToString(),
                            title = CastStringAsTitle(row["title"].ToString()),
                            email = row["email"].ToString(),
                            school = row["unit"].ToString(),
                            imageURL = row["photo"].ToString(),
                            campus = row["campus"].ToString(),
                            utasStart = (DateTime)row["utas_start"],
                            positionStart = (DateTime)row["current_start"]
                        };
                    }

                    //if the researcher is a student
                    if(type == "Student") {
                        // Creating a new Researcher object and initializing it with data from the row
                        r = new Researcher {
                            id = (int)row["id"],
                            givenName = row["given_name"].ToString(),
                            familyName = row["family_name"].ToString(),
                            title = CastStringAsTitle(row["title"].ToString()),
                            email = row["email"].ToString(),
                            school = row["unit"].ToString(),
                            imageURL = row["photo"].ToString(),
                            campus = row["campus"].ToString(),
                            utasStart = (DateTime)row["utas_start"],
                            positionStart = (DateTime)row["current_start"]
                        };
                    }

                    // Adding the new researcher to the list
                    researchers.Add(r);
                }
            }
            finally {
                // Ensuring the database connection is closed after data retrieval
                if (conn != null) {
                    conn.Close();
                }
            }
            return researchers;
        }

        // Converts a string representation of a title to the Title enum
        public Title CastStringAsTitle(string s) {
            Title t = Title.MR; // Default title
            switch (s) {
                // Matching string representation with the corresponding Title enum value
                case "Mr":
                    t = Title.MR;
                    break;
                case "Dr":
                    t = Title.DR;
                    break;
                case "Ms":
                    t = Title.MS;
                    break;
            }
            return t;
        }

        // Converts a string representation of employment level to the EmploymentLevel enum
        public EmploymentLevel CastEmploymentAsEnumType(string s) {
            switch (s) {
                // Matching string representation with the corresponding EmploymentLevel enum value
                case "A":
                    return EmploymentLevel.RESEARCH_ASSOCIATE;
                case "B":
                    return EmploymentLevel.LECTURER;
                case "C":
                    return EmploymentLevel.ASSISTANT_PROFESSOR;
                case "D":
                    return EmploymentLevel.ASSOCIATE_PROFESSOR;
                case "E":
                    return EmploymentLevel.PROFESSOR;
                default:
                    return EmploymentLevel.STUDENT; // Default value
            }
        }

        // Retrieves the total number of researcher records from the database
        public int GetNumberOfRecords() {
            int count = -1; // Default value in case of failure
            try {
                // Opening the database connection
                conn.Open();
                // Creating a command to execute the SQL query
                MySqlCommand cmd = new MySqlCommand("select COUNT(*) from researcher", conn);
                // Executing the query and parsing the result to an integer
                count = int.Parse(cmd.ExecuteScalar().ToString());
            }
            finally {
                // Ensuring the database connection is closed after executing the query
                if (conn != null) {
                    conn.Close();
                }
            }
            return count;
        }
    }
}


