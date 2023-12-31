//For the researcher list view, we need to only display a smaller set of information so its best to init this from the DB
//Then when selecting a researcher, we can fetch the full details from the DB.
public class ResearcherViewModel {
    public int ID { get; set; }
    public string FamilyName { get; set; }
    public string GivenName { get; set; }
    public string Title { get; set; }
    public string Level { get; set; }
}