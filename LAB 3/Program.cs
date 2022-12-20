using System;
using System.Linq;

namespace LAB_3
{
    public enum TimeFrame { Year, TwoYears, Long };

    public class Program
    {
        static void Main()
        {
            //1

            Console.WriteLine("\n1)\n*************************************************\n\n");
            ResearchTeamCollection<string> researchTeamString = new ResearchTeamCollection<string>("First Collection");

            ResearchTeam[] RT = new ResearchTeam[3];
            RT[0] = new ResearchTeam("North America Climate", "Nielsen", 1234, TimeFrame.TwoYears);
            RT[1] = new ResearchTeam("Atlantic Ocean Biosphere", "Ipsos", 9875, TimeFrame.Long);
            RT[2] = new ResearchTeam("Air pollution", "Kantar ", 500, TimeFrame.Year);
            researchTeamString.AddResearchTeams(RT);
            ResearchTeamCollection<string> researchTeamString1 = new ResearchTeamCollection<string>("Second Collection");

            ResearchTeam[] RT1 = new ResearchTeam[3];
            RT1[0] = new ResearchTeam("Remains of expeditions to the north of Canada", "Nielsen", 1234, TimeFrame.TwoYears);
            RT1[1] = new ResearchTeam("Indian Rainforest Fauna", "Ipsos", 9875, TimeFrame.Long);
            RT1[2] = new ResearchTeam("Underground lakes of Brazil", "Kantar", 500, TimeFrame.Year);
            researchTeamString1.AddResearchTeams(RT1);

            TeamsJournal teamJournal = new TeamsJournal();
            researchTeamString.ResearchTeamsChanged += teamJournal.TeamsJournalHandler;
            researchTeamString1.ResearchTeamsChanged += teamJournal.TeamsJournalHandler;
            researchTeamString.AddDefaults();
            researchTeamString1.AddDefaults();
            RT[0].researchTopic = "New Theme";
            RT[1].researchDuration = TimeFrame.Year;
            researchTeamString.Remove(RT[0]);
            RT[0].researchTopic = "Tigers' population";
            researchTeamString.Replace(RT[0], RT[2]);
            RT[0].researchDuration = TimeFrame.Year;

            

            Console.WriteLine("Publications for last 3 years:\n");
            RT[0].AddMembers(new Person("New", "Surname", new DateTime(1999, 12, 18)), new Person("New", "Surname", new DateTime(1999, 12, 10)));
            RT[0].AddPapers(new Paper("New", new Person(), new DateTime(2022, 10, 10)), new Paper("New", new Person(), new DateTime(2021, 10, 12)));
            RT[1].AddMembers(new Person("New", "Surname", new DateTime(1999, 12, 18)), new Person("New", "Surname", new DateTime(1999, 12, 10)));
            RT[1].AddPapers(new Paper("New", new Person(), new DateTime(2010, 10, 10)), new Paper("New", new Person(), new DateTime(2010, 10, 12)));
            RT[2].AddMembers(new Person("New", "Surname", new DateTime(1999, 12, 18)), new Person("New", "Surname", new DateTime(1999, 12, 10)));
            RT[2].AddPapers(new Paper("New", new Person(), new DateTime(2010, 10, 10)), new Paper("New", new Person(), new DateTime(2010, 10, 12)));
            //RT[0].PublicationList[0].PublicationDate = new DateTime(2021, 04, 10);
            foreach (Paper publication in RT[0].GetLastYearsPublications(3))
                Console.WriteLine(publication.ToString());


            Console.WriteLine(researchTeamString.ToString());

            Console.WriteLine("-----------------------\n");

            Console.WriteLine("****************************************");
            Console.ReadLine();
        }


    }
}
