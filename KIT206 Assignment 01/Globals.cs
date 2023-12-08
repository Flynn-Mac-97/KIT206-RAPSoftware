namespace KIT206_Assignment_01
{
    public enum ResearcherPerformance
    {
        POOR,
        BELOW_EXPECTATIONS,
        MEETING_MINIMUM,
        STAR_PERFORMER
    }
    public enum PublicationType
    {
        JOURNAL,
        CONFERENCE,
        OTHER
    }
    public enum EmploymentLevel
    {
        RESEARCH_ASSOCIATE,
        LECTURER,
        ASSISTANT_PROFESSOR,
        ASSOCIATE_PROFESSOR,
        PROFESSOR,
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
    public struct Date
    {
        int year;
        int month;
        int day;

        //Constructor which takes DD/MM/YYYY
        public Date(int day, int month, int year)
        {
            this.year = year;
            this.month = month;
            this.day = day;
        }
        public override string ToString()
        {
            return this.day + "/" + this.month + "/" + this.year;
        }
    }
}
