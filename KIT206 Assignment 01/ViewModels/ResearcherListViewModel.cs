﻿//For the researcher list view, we need to only display a smaller set of information so its best to init this from the DB
//Then when selecting a researcher, we can fetch the full details from the DB.
public class ResearcherViewModel {
    public int ID { get; set; }
    public string FamilyName { get; set; }
    public string GivenName { get; set; }
    public string Title { get; set; }
    public string Display {  get; set; }
    public string Level { get; set; }

    //to string override for researcher view model
    public override string ToString() {
        //Return all the generic details of this researcher
        return FamilyName + " " + GivenName + " " + Title + " " + Level;
    }
}