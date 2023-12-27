namespace KIT206_Assignment_01
{
    public static class Globals {
        public const string XmlFilePath = @"../../Fundings_Rankings.xml";
        // other global constants here if needed
    }
    public enum ResearcherType
    {
        STAFF,
        STUDENT
    }
    public enum ResearcherPerformance
    {
        POOR, //at or below 70%
        BELOW_EXPECTATIONS, // above 70% and below 110%
        MEETING_MINIMUM, // at or above 110%
        STAR_PERFORMER // at or above 220%
    }
    public enum PublicationType
    {
        JOURNAL,
        CONFERENCE,
        OTHER
    }
    public enum EmploymentLevel 
    {
        RESEARCH_ASSOCIATE,  //level A
        LECTURER,            //level B
        ASSISTANT_PROFESSOR, //level C
        ASSOCIATE_PROFESSOR, //level D
        PROFESSOR,           //level E
        STUDENT
    }
    public enum Title
    {
        MR,
        DR,
        MS,
    }
    public enum Campus
    {
        HOBART,
        LAUNCESTON,
        CRADLE_COAST
    }

    public enum Ranking 
    { 
        Q1,
        Q2,
        Q3,
        Q4
    }
}
