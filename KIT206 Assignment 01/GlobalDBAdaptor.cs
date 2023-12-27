using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using KIT206_Assignment_01;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;

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

        //Fetch specific researcher from database
        //uses Dataset
        public Researcher FetchResearcherFromDB(int ID) {
            if(ID == 0)return null; 
            Researcher r = null; // Default value in case of failure
            try {
                // DataSet to hold the data fetched from the database
                var researcherDataSet = new DataSet();
                // Data adapter to manage data retrieval from the database
                var researcherAdapter = new MySqlDataAdapter("select * from researcher where id = " + ID, conn);
                // Filling the DataSet with data from the 'researcher' table
                researcherAdapter.Fill(researcherDataSet, "researcher");
                // If the query returned a row
                if (researcherDataSet.Tables["researcher"].Rows.Count != 0) {
                    DataRow row = researcherDataSet.Tables["researcher"].Rows[0];
                    //Get the type of this researcher
                    string type = row["type"].ToString();
                    // If the researcher is a staff member
                    if (type == "Staff") {
                        // Creating a new Researcher object and initializing it with data from the row
                        r = NewStaffFromDataRow(row);
                    }

                    //if the researcher is a student
                    if (type == "Student") {
                        // Creating a new Researcher object and initializing it with data from the row
                        r = NewStudentFromDataRow(row);
                    }
                }
            }
            finally {
                // Ensuring the database connection is closed after data retrieval
                if (conn != null) {
                    conn.Close();
                }
            }
            return r;
        }

        // Fetches a list of researchers from the database
        //creates a comprehensive list of researchers
        public List<Researcher> FetchCompleteListFromDB() {
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
                    string type = row["type"].ToString();
                    // If the researcher is a staff member
                    if (type == "Staff") {
                        // Creating a new Researcher object and initializing it with data from the row
                        Staff r = NewStaffFromDataRow(row);
                        //added this to calc performance
                        r.InitialiseStaffFundingPerformance();
                        researchers.Add(r);
                    }

                    //if the researcher is a student
                    if (type == "Student") {
                        // Creating a new Researcher object and initializing it with data from the row
                        Student r = NewStudentFromDataRow(row);

                        // Adding the new researcher to the list
                        researchers.Add(r);
                    }
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

        //Creates a student researcher from a data row
        private Student NewStudentFromDataRow(DataRow row) {
            return new Student {
                id = (int)row["id"],
                type = ResearcherType.STUDENT,
                level = CastEmploymentAsEnumType(row["level"].ToString()),
                givenName = row["given_name"].ToString(),
                familyName = row["family_name"].ToString(),
                title = CastStringAsTitle(row["title"].ToString()),
                email = row["email"].ToString(),
                unit = row["unit"].ToString(),
                photo = row["photo"].ToString(),
                campus = CastStringToCampus(row["campus"].ToString()),
                utasStart = (DateTime)row["utas_start"],
                currentStart = (DateTime)row["current_start"],
                publications = FetchPublicationListFromDB((int)row["id"]),

                //student specific
                degree = row["degree"].ToString(),
                supervisor = (Staff)FetchResearcherFromDB(row["supervisor_id"] != DBNull.Value ? Convert.ToInt32(row["supervisor_id"]) : 0),
            };
        }
        //Creates a staff researcher from a data row
        private Staff NewStaffFromDataRow(DataRow row) {
            return new Staff {
                id = (int)row["id"],
                type = ResearcherType.STAFF,
                level = CastEmploymentAsEnumType(row["level"].ToString()),
                givenName = row["given_name"].ToString(),
                familyName = row["family_name"].ToString(),
                title = CastStringAsTitle(row["title"].ToString()),
                email = row["email"].ToString(),
                unit = row["unit"].ToString(),
                photo = row["photo"].ToString(),
                campus = CastStringToCampus(row["campus"].ToString()),
                utasStart = (DateTime)row["utas_start"],
                currentStart = (DateTime)row["current_start"],
                publications = FetchPublicationListFromDB((int)row["id"]),
                positionHistory = FetchPositionHistoryfromDB((int)row["id"]),
                //staff specific
                supervisions = new List<Student>(),

            };
        }

        //fetch list of students that a supervisor is supervising
        public List<Student> FetchSupervisionsFromDB(int ID) {
            // Initialize an empty list of students
            List<Student> students = new List<Student>();
            try {
                // DataSet to hold the data fetched from the database
                var studentDataSet = new DataSet();
                // Data adapter to manage data retrieval from the database
                var studentAdapter = new MySqlDataAdapter("select * from researcher where supervisor_id = " + ID, conn);
                // Filling the DataSet with data from the 'researcher' table
                studentAdapter.Fill(studentDataSet, "researcher");

                // Iterating through each row in the 'researcher' table
                foreach (DataRow row in studentDataSet.Tables["researcher"].Rows) {
                    // Creating a new Researcher object and initializing it with data from the row
                    Student s = (Student)NewStudentFromDataRow(row);
                    // Adding the new researcher to the list
                    students.Add(s);
                }
            }
            finally {
                // Ensuring the database connection is closed after data retrieval
                if (conn != null) {
                    conn.Close();
                }
            }
            return students;
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
            switch (s) {
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
            PublicationType type = PublicationType.JOURNAL;// Default title
            switch (s) {
                // Matching string representation with the corresponding Title enum value
                case "Journal":
                    type = PublicationType.JOURNAL;
                    break;
                case "Conference":
                    type = PublicationType.CONFERENCE;
                    break;
                case "Other":
                    type = PublicationType.OTHER;
                    break;
            }
            return type;
        }


        public List<Publication> FetchPublicationListFromDB(int ID) {
            // Initialize an empty list of publications
            List<Publication> publications = new List<Publication>();

            try {
                // Query to find publications for a specific researcher
                string query = $"SELECT publication.doi, publication.title, publication.ranking, publication.authors, publication.year, publication.type, publication.cite_as, publication.available FROM publication JOIN researcher_publication ON publication.doi = researcher_publication.doi WHERE researcher_publication.researcher_id = {ID}";

                // DataSet to hold the data fetched from the database
                var dataSet = new DataSet();

                // Data adapter to manage data retrieval from the database
                var adapter = new MySqlDataAdapter(query, conn);

                // Filling the DataSet with data
                adapter.Fill(dataSet, "matchedPublications");

                // Iterating through each row in the matched publications
                foreach (DataRow row in dataSet.Tables["matchedPublications"].Rows) {
                    // Creating a new Publication object from the DataRow
                    Publication p = new Publication {
                        DOI = row["doi"].ToString(),
                        title = row["title"].ToString(),
                        author = row["authors"].ToString(),
                        ranking = CastStringAsRanking(row["ranking"].ToString()),
                        yearPublished = row["year"] != DBNull.Value ? Convert.ToInt32(row["year"]) : 0,
                        type = CastStringAsPublicationType(row["type"].ToString()),
                        citeLink = row["cite_as"].ToString(),
                        availability = (DateTime)row["available"]
                    };
                    publications.Add(p);
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Error fetching publication list: " + ex.Message);
            }
            finally {
                // Ensuring the database connection is closed after data retrieval
                if (conn != null) {
                    conn.Close();
                }
            }

            return publications;
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
                var positionAdapter = new MySqlDataAdapter("select * from position WHERE id = " + ID, conn);
                // Filling the DataSet with data from the 'position' table
                positionAdapter.Fill(positionDataSet, "position");

                // Iterating through each row in the 'position' table
                foreach (DataRow row in positionDataSet.Tables["position"].Rows)
                {
                    Position pos = new Position
                    {
                        level = CastEmploymentAsEnumType(row["level"].ToString()),
                        startDate = (DateTime)row["start"]
                    };

                    //if end date is not null, add it to the position
                    if(row["end"] != DBNull.Value){ pos.endDate = (DateTime)row["end"]; };

                    positions.Add(pos);
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



