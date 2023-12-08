using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206_Assignment_01
{
    class GlobalDBAdaptor
    {
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

                Date commencedCurrentPosition = new Date(10,10,1999);
                Date commencedAtInstitute = new Date(10,10,1999);

                int tenure = 42;

                int publications = 43;

                float Q1Percentage = 3;

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
