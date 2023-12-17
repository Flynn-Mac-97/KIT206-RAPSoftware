using System;

namespace KIT206_Assignment_01
{
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
    //struct for employment level and a start and end date for position
    public struct Position {
        public EmploymentLevel level { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
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
        //date constructure that takes string and converts it to date
        public Date(string date)
        {
            //convert string to dateTime
            DateTime dateTime = Convert.ToDateTime(date);
            this.year = dateTime.Year;
            this.month = dateTime.Month;
            this.day = dateTime.Day;
        }
        
        public override string ToString()
        {
            return this.day + "/" + this.month + "/" + this.year;
        }
    }
}
