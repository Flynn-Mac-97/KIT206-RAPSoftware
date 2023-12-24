using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using KIT206_Assignment_01;
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
            // TO DO: just names, id, title , type, level for the list 


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
                    if (type == "Staff") {
                        // Creating a new Researcher object and initializing it with data from the row
                        r = new Researcher {
                            id = (int)row["id"],
                            givenName = row["given_name"].ToString(),
                            familyName = row["family_name"].ToString(),
                            title = CastStringAsTitle(row["title"].ToString()),
                            email = row["email"].ToString(),
                            unit = row["unit"].ToString(),
                            photo = row["photo"].ToString(),
                            campus = CastStringToCampus(row["campus"].ToString()),
                            utasStart = (DateTime)row["utas_start"],
                            currentStart = (DateTime)row["current_start"]
                        };
                    }

                    //if the researcher is a student
                    if (type == "Student") {
                        // Creating a new Researcher object and initializing it with data from the row
                        r = new Researcher {
                            id = (int)row["id"],
                            givenName = row["given_name"].ToString(),
                            familyName = row["family_name"].ToString(),
                            title = CastStringAsTitle(row["title"].ToString()),
                            email = row["email"].ToString(),
                            unit = row["unit"].ToString(),
                            photo = row["photo"].ToString(),
                            campus = CastStringToCampus(row["campus"].ToString()),
                            utasStart = (DateTime)row["utas_start"],
                            currentStart = (DateTime)row["current_start"]
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

        //Cast string to campus enum value.
        public Campus CastStringToCampus(string s) {
            switch (s) {
                // Matching string representation with the corresponding Campus enum value
                case "Hobart":
                    return Campus.HOBART;
                case "Launceston":
                    return Campus.LAUNCESTON;
                case "Cradle Coast":
                    return Campus.CRADLE_COAST;
                default:
                    return Campus.HOBART; // Default value
            }
        }

        /*// fetches researcher details for a researcher
        public Researcher FetchResearcherDetailsfromDB(int ID)
        {
            // Initialize an empty researcher class
            Researcher researcher = new Researcher;
            try
            {
                // DataSet to hold the data fetched from the database
                var researcherDetailsDataSet = new DataSet();
                // Data adapter to manage data retrieval from the database
                var researcherDetailsAdapter = new MySqlDataAdapter("select * from researcher", conn);
                // Filling the DataSet with data from the 'publication' table
                researcherDetailsAdapter.Fill(researcherDetailsDataSet, "researcher");

                // Iterating through each row in the 'publication' table
                foreach (DataRow row in researcherDetailsDataSet.Tables["researcher"].Rows)
                {
                    int id = (int)row["id"];

                    if (ID == id)
                    {
                        Researcher r = null; // Default value in case of failure
                        string type = row["type"].ToString();

                        // If the researcher is a staff member
                        if (type == "Staff")
                        {
                            // Creating a new Researcher object and initializing it with data from the row
                            r = new Researcher
                            {
                                id = (int)row["id"],
                                givenName = row["given_name"].ToString(),
                                familyName = row["family_name"].ToString(),
                                title = CastStringAsTitle(row["title"].ToString()),
                                email = row["email"].ToString(),
                                school = row["unit"].ToString(),
                                imageURL = row["photo"].ToString(),
                                campus = row["campus"].ToString(),
                                utasStart = (DateTime)row["utas_start"],
                                positionStart = (DateTime)row["current_start"],
                                publications = FetchPublicationListFromDB(id);
                            };
                        }

                        //if the researcher is a student
                        if (type == "Student")
                        {
                            // Creating a new Researcher object and initializing it with data from the row
                            r = new Researcher
                            {
                                id = (int)row["id"],
                                givenName = row["given_name"].ToString(),
                                familyName = row["family_name"].ToString(),
                                title = CastStringAsTitle(row["title"].ToString()),
                                email = row["email"].ToString(),
                                school = row["unit"].ToString(),
                                imageURL = row["photo"].ToString(),
                                campus = row["campus"].ToString(),
                                utasStart = (DateTime)row["utas_start"],
                                positionStart = (DateTime)row["current_start"],
                                publications = FetchPublicationListFromDB(id);
                            };
                        }
                        researcher = r;
                    }
                }
            }
            finally
            {
                // Ensuring the database connection is closed after data retrieval
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return researcher;

        }
*/
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

        // Converts a string representation of a ranking to the Ranking enum
        public Ranking CastStringAsRanking(string s) {
            Ranking r = Ranking.Q1; // Default title
            switch (r) {
                // Matching string representation with the corresponding Title enum value
                case "Q1":
                    r = Ranking.Q1;
                    break;
                case "Q2":
                    r = Ranking.Q2;
                    break;
                case "Q3":
                    r = Ranking.Q3;
                    break;
                case "Q4":
                    r = Ranking.Q4;
                    break;
            }
            return r;
        }

        // Converts a string representation of a ranking to the Ranking enum
        public PublicationType CastStringAsPublicationType(string s) {
            PublicationType type = PublicationType.JOURNAL// Default title
            switch (type) {
                // Matching string representation with the corresponding Title enum value
                case "Journal":
                    type = PublicationType.JOURNAL;
                    break;
                case "Conference":
                    type = PublicationType.CONFERENCE;
                    break;
                case "Other":
                    type = PublicationType.OTHER
                    break;
            }
            return type;
        }


        //fecthes publication list for a researcher
        public List<Publication> FetchPublicationListFromDB(int ID)
        {
            // Initialize an empty list of researchers
            List<Publication> publications = new List<Publication>();
            try
            {
                // DataSet to hold the data fetched from the database
                var publicationDataSet = new DataSet();
                // Data adapter to manage data retrieval from the database
                var publicationAdapter = new MySqlDataAdapter("select doi, title, year, authors from publication", conn);
                // Filling the DataSet with data from the 'publication' table
                publicationAdapter.Fill(publicationDataSet, "publication");

                // DataSet to hold the data fetched from the database
                var researcherPublicationDataSet = new DataSet();
                // Data adapter to manage data retrieval from the database
                var researcherPublicationAdapter = new MySqlDataAdapter("select * from researcher_publication", conn);
                // Filling the DataSet with data from the 'publication' table
                researcherPublicationAdapter.Fill(researcherPublicationDataSet, "researcher_publication");

                foreach (DataRow row in researcherPublicationDataSet.Tables["researcher_publication"].Rows)
                {
                    int id = (int)row["id"];
                    if (ID == id)
                    {
                        // Iterating through each row in the 'publication' table
                        foreach (DataRow row1 in publicationDataSet.Tables["publication"].Rows)
                        {
                            Publication p = null; // Default value in case of failure

                            p = new Publication
                            {
                                DOI = row["doi"].ToString(),
                                title = row["title"].ToString(),
                                author = row["authors"].ToString(),
                                yearPublished = (int)row["year"]
                            };
                            publications.Add(p);
                        }
                    }
                }   
            }
            finally
            {
                // Ensuring the database connection is closed after data retrieval
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return publications;
        }


        // fetches a publication details for a publication
        public Publication FetchPublicationDetailsFromDB(string doi)
        {
            // Initialize an empty publication class
            Publication publication = new Publication();
            try
            {
                // DataSet to hold the data fetched from the database
                var publicationDetailsDataSet = new DataSet();
                // Data adapter to manage data retrieval from the database
                var publicationDetailsAdapter = new MySqlDataAdapter("select * from publication", conn);
                // Filling the DataSet with data from the 'publication' table
                publicationDetailsAdapter.Fill(publicationDetailsDataSet, "publication");

                // Iterating through each row in the 'publication' table
                foreach (DataRow row in publicationDetailsDataSet.Tables["publication"].Rows)
                {
                    Publication p = null; // Default value in case of failure
                    string DOI = row["doi"].ToString();

                    if (doi == DOI) 
                    {
                        p = new Publication
                        {
                            DOI = row["doi"].ToString(),
                            title = row["title"].ToString(),
                            author = row["authors"].ToString(),
                            ranking = CastStringAsRanking(row["ranking"].ToString()),
                            yearPublished = (int)row["year"],
                            type = CastStringAsPublicationType(row["type"].ToString()),
                            citeLink = row["cite_as"].ToString(),
                            availability = (DateTime)row["availablie"]
                        };
                        publication = p;
                    }
                }
            }
            finally
            {
                // Ensuring the database connection is closed after data retrieval
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return publication;
        }


        // fetch position history of a researcher from database
        public List<Position> FetchPositionHistoryfromDB(int ID)
        {
             // Initialize an empty position list
            List<Position> positions = new List<Position>();
            try
            {
                // DataSet to hold the data fetched from the database
                var positionDataSet = new DataSet();
                // Data adapter to manage data retrieval from the database
                var positionAdapter = new MySqlDataAdapter("select * from position", conn);
                // Filling the DataSet with data from the 'position' table
                positionAdapter.Fill(positionDataSet, "position");

                // Iterating through each row in the 'position' table
                foreach (DataRow row in positionDataSet.Tables["position"].Rows)
                {
                    Position pos = null; // Default value in case of failure
                    int id = (int)row["id"].ToString();

                    if (id == ID) 
                    {
                        pos = new Position
                        {
                            level = CastEmploymentAsEnumType(row["level"].ToString()),
                            startDate = (DateTime)row["start"],
                            endDate = (DateTime)row["end"]
                        };
                        positions.Add(pos);
                    }
                }
            }
            finally
            {
                // Ensuring the database connection is closed after data retrieval
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return positions;
        }







        
    }


}



